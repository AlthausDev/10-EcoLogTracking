using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using ConsolaBackService.Models;
using Microsoft.Extensions.Configuration;
using System.Runtime.Remoting.Messaging;

namespace ConsolaBackService
{
    internal class Service : BackgroundService
    {
        private readonly ILogger<Service> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private Timer _timerEmail;
        private string _apiBaseUrl;
        private string _logFilePath;
        private EmailSettings _emailSettings;

        private int _hour;
        private int _minute;

        public Service(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            _logFilePath = _configuration["DestinationFolder:Route"];
            _emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            _hour = Convert.ToInt32(_configuration["DateTimeExec:hour"]);
            _minute = Convert.ToInt32(_configuration["DateTimeExec:minute"]);

        }

        /// <summary>
        /// MÉTODO QUE OBTIENE EL PERIODO Y LA FECHA DEL ULTIMO BORRADO
        /// </summary>
        /// <returns>Objeto configuracion con id, período y fecha de último borrado</returns>
        public async Task<Configuration> ObtenerConfiguration()
        {
            try
            {
                Configuration response = await _httpClient.GetFromJsonAsync<Configuration>(_apiBaseUrl +"/api/Configuration");
              
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }



        /// <summary>
        /// MÉTODO QUE GUARDA ARCHIVO.TXT EN RUTA ESPECIFICADA CON REGISTROS DE ERROR Y LUEGO LO ENVIA A DIRECCION ESPECIFICADA
        /// </summary>
        public async void SendEmail()
        {
            try
            {
                StreamWriter sw = new StreamWriter(_logFilePath);
                DateTime date = DateTime.Now;
                DateTime secondDate = date.AddDays(-1);
                DateFilter dateFilter = new DateFilter {
                    DateStart = secondDate,
                    DateEnd = date
                };
                    
                List<Log> listaLogs = new List<Log>();
                var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl + "/GetBetween", dateFilter);
                string responseAUX = await response.Content.ReadAsStringAsync();
                
                listaLogs = JsonSerializer.Deserialize<List<Log>>(responseAUX);
                foreach (Log l in listaLogs)
                {
                    if (l.Level.Equals("ERROR")) {
                        sw.WriteLine(l.ToString());
                    }
                }
                sw.Close();
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
                message.To.Add(new MailboxAddress(_emailSettings.ToName, _emailSettings.ToAddress));
                message.Subject = "Logs " + "[Date: " + DateTime.Now.AddDays(-1).ToString("dddd, dd/MM/yyyy") + "]";

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = "Recap error logs [Date: " + DateTime.Now.AddDays(-1).ToString("dddd, dd/MM/yyyy") + "]"
                };

                bodyBuilder.Attachments.Add(_logFilePath);
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    client.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
                Console.WriteLine("Email enviado correctamente.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        /// <summary>
        /// MÉTODO QUE ELIMINA REGISTROS EN LA BASE DE DATOS SI EL PERIODO Y LA FECHA LO INDICA
        /// </summary>
        /// <param name="configuration">Objeto configuración que se obtiene de la base de datos e informa de período y fecha último borrado</param>
        public void Delete(Configuration configuration)
        {
            DateTime nextDelete = configuration.DeletedDate.AddDays(configuration.Period);
            if (DateTime.Now > nextDelete)
            {
                _httpClient.DeleteAsync(_logFilePath + configuration.Period);
                Configuration newConfig = new Configuration {
                    Id = configuration.Id,
                    Period = configuration.Period,
                    DeletedDate = DateTime.Now
                };
                _httpClient.PutAsJsonAsync(_apiBaseUrl + "/api/Configuration",newConfig);
            }
            

        }

        /// <summary>
        /// MÉTODO QUE RECIBE LA HORA Y MINUTOS A LOS QUE SE QUIERE QUE SE EJECUTE LA TAREA CADA DIA
        /// </summary>
        /// <param name="hour">Hora a la que se quiere ejecutar el método</param>
        /// <param name="minute">Minutos a los que se quiere ejecutar el método</param>
        private async void TaskEmail(int hour, int minute)
        {
            Configuration configuration = await ObtenerConfiguration();

            DateTime now = DateTime.Now;
            DateTime scheduledTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
            if (now > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }
            double tickTime = (scheduledTime - now).TotalMilliseconds;
            _timerEmail = new Timer(x =>
            {
                SendEmail();
                Delete(configuration);
                TaskEmail(hour, minute);
            }, null, (int)tickTime, Timeout.Infinite);
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("path: "+_apiBaseUrl);
            Console.WriteLine("path: " +  _logFilePath);
            Console.WriteLine("path: " + _emailSettings.FromName);
            Console.WriteLine("path: " + _hour);
            Console.WriteLine("path: " + _minute);

            TaskEmail(_hour,_minute); 
            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}