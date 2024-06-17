using BlazorBootstrap;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using EcoLogTracking.Client.Components;
using EcoLogTracking.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Net.Http.Json;

namespace EcoLogTracking.Client.Pages
{
    public partial class MainPanel
    {
        private bool IsLoading { get; set; } = true;
        public static Modal ModalInstance { get; set; } = default!;
        public static User User { get; set; } = new();

        //private string ShowLogs { get; set; } = "none";
        //private string ShowConfig { get; set; } = "block";

        private string ShowLogs { get; set; } = "block";
        private string ShowConfig { get; set; } = "none";

        private bool IsActiveConfigButton { get; set; } = false;

        
        /// <summary>
        /// Maneja la exportación a Excel.
        /// </summary>
        private async Task OnClickExportExcel()
        {
            var parameters = new Dictionary<string, object>
            {
                { "Cerrar", EventCallback.Factory.Create<MouseEventArgs>(this, HideModal) }
            };
            await ModalInstance.ShowAsync<ExportExcel>(title: "Exportar", parameters: parameters);
        }

        /// <summary>
        /// Alterna entre mostrar los logs y la configuración.
        /// </summary>
        private void OnClickToggle()
        {
            ShowLogs = ShowLogs == "block" ? "none" : "block";
            ShowConfig = ShowConfig == "block" ? "none" : "block";
            UpdateButtonState();
        }

        /// <summary>
        /// Actualiza el estado del botón de configuración.
        /// </summary>
        private void UpdateButtonState()
        {
            IsActiveConfigButton = ShowConfig == "block";
        }

        /// <summary>
        /// Maneja el cierre de sesión del usuario.
        /// </summary>
        public async Task OnClickLogOut()
        {
            try
            {
                await storageService.RemoveItemAsync("token");
                await storageService.ClearAsync();
                Http.DefaultRequestHeaders.Remove("Authorization");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al cerrar sesión: {ex.Message}");
            }
            finally
            {
                NavManager.NavigateTo("/login", forceLoad: true);
            }
        }

        /// <summary>
        /// Oculta el modal.
        /// </summary>
        private async Task HideModal()
        {
            await ModalInstance.HideAsync();
        }
    }
}
