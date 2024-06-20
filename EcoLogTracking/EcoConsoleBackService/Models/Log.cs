using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcoConsoleBackService
{
    public class Log
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("logged")]
        public DateTime Logged { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; }

        [JsonPropertyName("logger")]
        public string Logger { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("machineName")]
        public string MachineName { get; set; }

        [JsonPropertyName("request_method")]
        public string Request_method { get; set; }

        [JsonPropertyName("stacktrace")]
        public string Stacktrace { get; set; }

        [JsonPropertyName("file_name")]
        public string File_name { get; set; }

        [JsonPropertyName("all_event_properties")]
        public string All_event_properties { get; set; }

        [JsonPropertyName("status_code")]
        public string Status_code { get; set; }

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

        public Log(int id, DateTime logged, string level, string logger, string message, string machineName, string request_method, string stacktrace, string file_name, string all_event_properties, string status_code, string origin)
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

        public override string ToString()
        {
            const int titleWidth = 17; 

            return $"Origin:".PadRight(titleWidth) + $"{Origin}\n" +
                   $"MachineName:".PadRight(titleWidth) + $"{MachineName}\n" +
                   $"Id:".PadRight(titleWidth) + $"{Id}\n" +
                   $"Logged:".PadRight(titleWidth) + $"{Logged}\n" +
                   $"Level:".PadRight(titleWidth) + $"{Level}\n" +
                   $"Message:".PadRight(titleWidth) + $"{Message}\n" +
                   $"Logger:".PadRight(titleWidth) + $"{Logger}\n" +
                   "--------------------------------------\n";
        }
    }
}
