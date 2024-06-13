using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EcoLogTracking.Client.Components
{
    partial class ExportExcel
    {
        [Parameter]
        public DateTime FirstDate { get; set; } = DateTime.Now.Date;
        public DateTime LastDate { get; set; } = DateTime.Now.Date;

        [Parameter] public EventCallback<MouseEventArgs> Exportar { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> Cerrar { get; set; }

    }
}
