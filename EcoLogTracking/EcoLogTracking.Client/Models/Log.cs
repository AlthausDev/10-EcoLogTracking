using EcoLogTracking.Client.Models;

namespace EcoLogTracking.Client.Models
{
    public class Log : BaseModel
    {       
        public string MachineName { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Request_method { get; set; }

        public Log()
        {
        }

        public Log(int Id, string machineName, DateTime logged, string level, string message, string logger, string request_method)
        {
            this.Id = Id;
            MachineName = machineName;
            Logged = logged;
            Level = level;
            Message = message;
            Logger = logger;
            Request_method = request_method;
        }

        public Log(string machineName, DateTime logged, string level, string message, string logger, string request_method)
        {
            MachineName = machineName;
            Logged = logged;
            Level = level;
            Message = message;
            Logger = logger;
            Request_method = request_method;
        }       
    }
}
