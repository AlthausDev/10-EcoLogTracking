using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using EcoLogTracking.Client.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Net.Http.Json;


namespace EcoLogTracking.Client.Components
{
    public partial class LogViewer
    {
        #region Atributes
        [Inject] protected PreloadService PreloadService { get; set; } = default!;

        public static Grid<Log> DataGrid = default!;

        public static ObservableCollection<Log> LogList { get; set; } = new ObservableCollection<Log>();

        private bool IsLoading = true;

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
      

        #region Initialize
        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;

            }
            finally
            {
                IsLoading = false;
                await Task.Delay(500);
                PreloadService.Hide();
            }
        }
        #endregion

        public async Task<GridDataProviderResult<Log>> LogsDataProvider(GridDataProviderRequest<Log> request)
        {
            return await Task.FromResult(request.ApplyTo(LogList.OrderBy(Log => Log.Id)));
        }


        private async Task<IEnumerable<FilterOperatorInfo>> GridFiltersTranslationProvider()
        {
            var filtersTranslation = new List<FilterOperatorInfo>();

            // número/fecha/booleano
            filtersTranslation.Add(new("=", "Igual", FilterOperator.Equals));
            filtersTranslation.Add(new("!=", "No es igual", FilterOperator.NotEquals));
            // número/fecha
            filtersTranslation.Add(new("<", "Menor que", FilterOperator.LessThan));
            filtersTranslation.Add(new("<=", "Menor o igual que", FilterOperator.LessThanOrEquals));
            filtersTranslation.Add(new(">", "Mayor que", FilterOperator.GreaterThan));
            filtersTranslation.Add(new(">=", "Mayor o igual que", FilterOperator.GreaterThanOrEquals));
            // cadena
            filtersTranslation.Add(new("*a*", "Contiene", FilterOperator.Contains));
            filtersTranslation.Add(new("a**", "Comienza con", FilterOperator.StartsWith));
            filtersTranslation.Add(new("**a", "Termina en", FilterOperator.EndsWith));
            filtersTranslation.Add(new("=", "Igual a", FilterOperator.Equals));

            filtersTranslation.Add(new("x", "Limpiar", FilterOperator.Clear));

            return await Task.FromResult(filtersTranslation);
        }

        #endregion

        #region Events
        private async Task OnClickShowDetails()
        {
            Dictionary<string, object> parameters = new()
            {
                { "Id", SelectedLogItem.Id },
                { "MachineName", SelectedLogItem.MachineName },
                { "Logged", SelectedLogItem.Logged },
                { "Level", SelectedLogItem.Level },
                { "Message", SelectedLogItem.Message },
                { "Logger", SelectedLogItem.Logger },
                { "Request_method", SelectedLogItem.Request_method },
                { "Stacktrace", SelectedLogItem.Stacktrace },
                { "File_name", SelectedLogItem.File_name },
                { "All_event_properties", SelectedLogItem.All_event_properties },
                { "Status_code", SelectedLogItem.Status_code },
                { "Origin", SelectedLogItem.Origin }
            };
            await MainPanel.ModalInstance.ShowAsync<DetailsModal>(title: "Detalles del Registro", parameters: parameters);
        }


        #endregion

        #region ApiCalls
        private async Task GetAllLogData()
        {
            LogList = new ObservableCollection<Log>(await Http.GetFromJsonAsync<List<Log>>("api/Log"));

            foreach (var log in LogList)
            {
                int index = log.Message.IndexOf("]");
                log.Status_code = log.Message.Substring(0, index + 1);
                log.Message = log.Message.Substring(index + 1);
            }
        }
        #endregion

        #region Loader
        private RenderFragment RenderLoadingIndicator() => builder =>
        {
            PreloadService.Show(spinnerColor: SpinnerColor.Success, "Cargando...");
            if (IsLoading)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "loading-indicator");
                builder.AddContent(2, "Cargando...");
                builder.CloseElement();

            }
            PreloadService.Hide();
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