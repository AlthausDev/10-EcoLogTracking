namespace EcoLogTracking.Server.Models
{
    public class Log 
    {
        public int ID { get; set; }
        public string MachineName { get; set; }

        public DateTime Logged { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public string Logger { get; set; }

        public string Method { get; set; }
        public string Stacktrace { get; set; }
        public string File_name { get; set; } 
        public string All_event_properties { get; set; } 

        public Log(){}

        public Log(int id, string machinename, DateTime logged, string level, string message, string logger, string r_m, string st, string fl, string aep)
        {
            ID = id;
            MachineName = machinename;
            Logged = logged;
            Level = level;
            Message = message;
            Logger = logger;
            Method = r_m;
            Stacktrace = st;
            File_name = fl;
            All_event_properties = aep;

        }
    }
    }
