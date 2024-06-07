using System;

namespace EcoLogTracking.Server.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string? Logger { get; set; }
        public string Message { get; set; }
        public string MachineName { get; set; }
        public string? Request_method { get; set; }
        public string? Stacktrace { get; set; }
        public string? File_name { get; set; }
        public string? All_event_properties { get; set; }

        public Log() { }

        public Log(int id, DateTime logged, string level, string message, string machineName, string? logger, string? request_method, string? stacktrace, string? file_name, string? all_event_properties)
        {
            Id = id;
            Logged = logged;
            Level = level;
            Message = message;
            MachineName = machineName;
            Logger = logger;
            Request_method = request_method;
            Stacktrace = stacktrace;
            File_name = file_name;
            All_event_properties = all_event_properties;
        }

        public Log(DateTime logged, string level, string message, string machineName, string? logger, string? request_method, string? stacktrace, string? file_name, string? all_event_properties)
        {
            Logged = logged;
            Level = level;
            Message = message;
            MachineName = machineName;
            Logger = logger;
            Request_method = request_method;
            Stacktrace = stacktrace;
            File_name = file_name;
            All_event_properties = all_event_properties;
        }
    }
}
