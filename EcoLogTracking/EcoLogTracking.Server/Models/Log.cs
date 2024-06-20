using System.Text.Json.Serialization;

namespace EcoLogTracking.Server.Models
{
    public class Log
    {

        /// <summary>
        /// Identificador unico del log
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Fecha y hora en la que se genero el log
        /// </summary>
        [JsonPropertyName("logged")]
        public DateTime Logged { get; set; }

        /// <summary>
        /// Nivel del log (por ejemplo, 'Info', 'Warning', 'Error')
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; }

        /// <summary>
        /// Nombre del componente o clase que genero el log
        /// </summary>
        [JsonPropertyName("logger")]
        public string Logger { get; set; }

        /// <summary>
        /// Mensaje descriptivo de la ejecución o evento registrado
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Nombre de la maquina (hostname) donde se genero el log
        /// </summary>
        [JsonPropertyName("machineName")]
        public string MachineName { get; set; }

        /// <summary>
        /// Metodo HTTP de la solicitud que genero el log (por ejemplo, 'GET', 'POST')
        /// </summary>
        [JsonPropertyName("request_method")]
        public string Request_method { get; set; }

        /// <summary>
        /// Detalle de la traza de la pila en caso de una excepcion o error
        /// </summary>
        [JsonPropertyName("stacktrace")]
        public string Stacktrace { get; set; }

        /// <summary>
        /// Nombre del archivo relacionado con el log, si aplica
        /// </summary>
        [JsonPropertyName("file_name")]
        public string File_name { get; set; }

        /// <summary>
        /// Propiedades adicionales del evento registrado en el log
        /// </summary>
        [JsonPropertyName("all_event_properties")]
        public string All_event_properties { get; set; }

        /// <summary>
        /// Código de estado HTTP asociado al log
        /// </summary>
        [JsonPropertyName("status_code")]
        public string Status_code { get; set; }

        /// <summary>
        /// Corporacion o entidad origen donde se genero el log
        /// </summary>
        [JsonPropertyName("origin")]
        public string Origin { get; set; }


        public Log() { }

        public Log(DateTime logged, string level, string logger, string message, string machineName, string request_method, string stacktrace, string file_name, string all_event_properties, string status_code, string origin)
        {
            Logged = logged;
            Level = level;
            Logger = logger;
            Message = message;
            MachineName = machineName;
            Request_method = request_method;
            Stacktrace = stacktrace;
            File_name = file_name;
            All_event_properties = all_event_properties;
            Status_code = status_code;
            Origin = origin;

        }

        public Log(int id, DateTime logged, string level, string? logger, string message, string machineName, string? request_method, string? stacktrace, string? file_name, string? all_event_properties, string? status_code, string? origin)
        {
            Id = id;
            Logged = logged;
            Level = level;
            Logger = logger;
            Message = message;
            MachineName = machineName;
            Request_method = request_method;
            Stacktrace = stacktrace;
            File_name = file_name;
            All_event_properties = all_event_properties;
            Status_code = status_code;
            Origin = origin;
        }
    }
}