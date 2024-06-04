using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using ToastType = BlazorBootstrap.ToastType;


namespace EcoLogTracking.Client.Components
{
    public partial class LogViewer

    {
     
        [Parameter]
        public string Id { get; set; }

       
        private Log Log { get; set; }


        /// <summary>
        /// Servicio para mostrar indicadores de precarga.
        /// </summary>
        [Inject] protected PreloadService PreloadService { get; set; }
        
       private Grid<Log> DataGrid = default!;

        private ObservableCollection<Log> LogList { get; set; } = new ObservableCollection<Log>();
       
        /// <summary>
        /// Indicador de si la página está cargando.
        /// </summary>
        private bool isLoading = true;

     
        protected override async Task OnInitializedAsync()
        {
          
        }

        private RenderFragment RenderLoadingIndicator() => builder =>
        {
            if (isLoading)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "loading-indicator");
                builder.AddContent(2, "Cargando...");
                builder.CloseElement();
            }
        };


        #region StartUp   

        private async Task<GridDataProviderResult<Log>> LogsDataProvider(GridDataProviderRequest<Log> request)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            while (LogList.IsNullOrEmpty())
            {
                await Task.Delay(5);
            }

            stopwatch.Stop();
            Debug.WriteLine($"Tiempo total de espera: {stopwatch.ElapsedMilliseconds} ms");

            return await Task.FromResult(request.ApplyTo(LogList.OrderBy(Log => Log.Id)));
        }


        private async Task GetLogData()
        {
            //LogList = new ObservableCollection<Log>(await Http.GetFromJsonAsync<List<Log>>("api/Log"));
        }
        #endregion



        private async Task TaskFormResult()
        {             
            await GetLogData();
            await DataGrid.RefreshDataAsync();
        }     

    }
}