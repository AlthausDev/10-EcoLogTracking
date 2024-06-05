using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using EcoLogTracking.Client.Pages;
using EcoLogTracking.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
        [Inject] protected MockLogService MockLogService { get; set; }

        private Log Log { get; set; } = new();

        private Grid<Log> DataGrid = default!;  

        private ObservableCollection<Log> LogList { get; set; } = new ObservableCollection<Log>();

        private bool IsLoading = false;

        private Log? selectedLogItem { get; set; } = null;

        public Log? SelectedLogItem
        {
            get
            {
                return selectedLogItem;
            }
            set
            {
                if (selectedLogItem != value)
                {
                    selectedLogItem = value;
                }
            }
        }
        #endregion


        #region Initialize
        protected override async Task OnInitializedAsync()
        {
            LogList = new ObservableCollection<Log>(MockLogService.GetMockLogs());

            //await GetAllLogData();
        }

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
        #endregion

        #region Events
        private async Task OnClickShowDetails()
        {
            var parameters = new Dictionary<string, object>
            {
                { "Id", SelectedLogItem.Id },
                { "MachineName", SelectedLogItem.MachineName },
                { "Logged", SelectedLogItem.Logged },
                { "Level", SelectedLogItem.Level },
                { "Message", SelectedLogItem.Message },
                { "Logger", SelectedLogItem.Logger },
                { "Request_method", SelectedLogItem.Request_method }
            };
            await MainPanel.ModalInstance.ShowAsync<DetailsModal>(title: "Detalles del Registro", parameters: parameters);
        }
        #endregion

        #region ApiCalls
        private async Task GetAllLogData()
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

        #region SelectRow    
        private async Task SelectLogItem(GridRowEventArgs<Log> args)
        {
            SelectedLogItem = args.Item;
        }

        #endregion SelectRow   
    }
}