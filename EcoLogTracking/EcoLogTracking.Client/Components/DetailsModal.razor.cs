using Microsoft.AspNetCore.Components;

namespace EcoLogTracking.Client.Components
{
    partial class DetailsModal
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string MachineName { get; set; }

        [Parameter]
        public DateTime Logged { get; set; }

        [Parameter]
        public string Level { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public string Logger { get; set; }

        [Parameter]
        public string Request_method { get; set; }
    }
}
