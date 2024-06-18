using BlazorBootstrap;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Presentation;
using EcoLogTracking.Client.Components;
using EcoLogTracking.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static DateTime FirstDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public static DateTime LastDate { get; set; } = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);


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

        private async void OnClickSearch()
        {
           await GetLogDataBetweenDates();
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
        /// Actualiza el estado del botón de configuración.
        /// </summary>
        private void UpdateButtonState()
        {
            IsActiveConfigButton = ShowConfig == "block";
        }

        #region ApiCalls
        public async Task GetLogDataBetweenDates()
        {
            LogViewer.LogList = new ObservableCollection<Log>(await Http.GetFromJsonAsync<List<Log>>($"api/GetBetween/{FirstDate}/{LastDate}"));

            foreach (var log in LogViewer.LogList)
            {
                int index = log.Message.IndexOf("]");
                log.Status_code = log.Message.Substring(0, index + 1);
                log.Message = log.Message.Substring(index + 1);
            }
        }
        #endregion

        /// <summary>
        /// Oculta el modal.
        /// </summary>
        private async Task HideModal()
        {
            await ModalInstance.HideAsync();
        }   
    }
}
