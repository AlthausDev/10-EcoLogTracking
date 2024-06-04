using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;


namespace EcoLogTracking.Client.Components
{
    public partial class LogViewer
    {
        #region Atributes
        [Inject] protected PreloadService PreloadService { get; set; }

        private Log Log { get; set; } = new();
        private Grid<Log> DataGrid = default!;

        private ObservableCollection<Log> LogList { get; set; } = new ObservableCollection<Log>();

        private bool IsLoading = false;
        #endregion


        #region Initialize
        protected override async Task OnInitializedAsync()
        {

        }

        private async Task<GridDataProviderResult<Log>> LogsDataProvider(GridDataProviderRequest<Log> request)
        {
            //Stopwatch stopwatch = new();
            //stopwatch.Start();

            //while (LogList.IsNullOrEmpty())
            //{
            //    await Task.Delay(5);
            //}

            //stopwatch.Stop();
            //Debug.WriteLine($"Tiempo total de espera: {stopwatch.ElapsedMilliseconds} ms");

            return await Task.FromResult(request.ApplyTo(LogList.OrderBy(Log => Log.Id)));
        }
        #endregion

        #region ApiCalls
        private async Task GetLogData()
        {
            LogList = new ObservableCollection<Log>(await Http.GetFromJsonAsync<List<Log>>("api/Log"));
        }
        #endregion

        #region Loader
        private RenderFragment RenderLoadingIndicator() => builder =>
        {
            if (IsLoading)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "loading-indicator");
                builder.AddContent(2, "Cargando...");
                builder.CloseElement();
            }
        };
        #endregion
    }
}