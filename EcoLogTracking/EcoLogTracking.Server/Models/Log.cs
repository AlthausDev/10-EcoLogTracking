namespace EcoLogTracking.Server.Models
{
    public class Log : BaseModel
    {
        private int ID { get; set; }
        private string MachineName { get; set; }

        private DateTime Logged { get; set; }

        private string Level { get; set; }

        private string Message { get; set; }

        private string Logger { get; set; }

        private string request_method { get; set; }



        public Log(){}

        public Log(int id, string machinename, DateTime logged, string level, string message, string logger, string r_m)
        {
            ID = id;
            MachineName = machinename;
            Logged = logged;
            Level = level;
            Message = message;
            Logger = logger;
            request_method = r_m;
        }
    }
    }
