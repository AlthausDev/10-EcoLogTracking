using Microsoft.AspNetCore.Components;

namespace EcoLogTracking.Client.Components
{
    public partial class ConfirmDialogComponent
    {

        public string Name { get; set; } = string.Empty;
        [Parameter] public string Message { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
