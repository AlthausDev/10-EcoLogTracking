using Microsoft.AspNetCore.Components;

namespace EcoLogTracking.Client.Components
{
    public partial class DetailsModal
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public DateTime Logged { get; set; }

        [Parameter]
        public string Level { get; set; }

        [Parameter]
        public string? Logger { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public string MachineName { get; set; }

        [Parameter]
        public string? Request_method { get; set; }

        [Parameter]
        public string? Stacktrace { get; set; }

        [Parameter]
        public string? File_name { get; set; }

        [Parameter]
        public string? All_event_properties { get; set; }

        [Parameter]
        public string? Status_code { get; set; }

        [Parameter]
        public string? Origin { get; set; }
    }
}
