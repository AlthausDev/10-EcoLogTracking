using BlazorBootstrap;
using BlazorWebPage.Shared;
using System.Net.Http.Json;
using ToastType = BlazorBootstrap.ToastType;

namespace BlazorWebPage.Client.Pages
{
    public partial class Todo
    {
        public List<Tarea> Tareas { get; set; } = new List<Tarea>();

        private Tarea nuevaTarea { get; set; } = new Tarea();
        private Tarea NuevaTarea
        {
            get
            {
                return nuevaTarea;
            }
            set
            {
                if (nuevaTarea != value)
                {
                    nuevaTarea = value;
                }
            }
        }


        private Tarea? selectedTarea { get; set; } = null;
        public Tarea? SelectedTarea
        {
            get
            {
                return selectedTarea;
            }
            set
            {
                if (selectedTarea != value)
                {
                    selectedTarea = value;
                    SelectedChangeHandler();
                }
            }
        }

        protected bool IsDisabled { get; set; } = true;
        protected bool IsDisabledEdit { get; set; } = true;

        private Enum? accion = Accion.Espera;
        private Modal modal = default!;

        List<ToastMessage> messages = new();

        private PieChart pieChart = default!;
        private PieChartOptions pieChartOptions = default!;
        private ChartData chartData = default!;
        private string[]? backgroundColors = ColorBuilder.CategoricalTwelveColors;


        protected override async Task OnInitializedAsync()
        {
            await getData();
            await InitializeGraph();
        }

        #region ApiOperations    
        private async Task getData()
        {
            Tarea[]? tareasArray = await Http.GetFromJsonAsync<Tarea[]>("tarea");

            if (tareasArray is not null)
            {
                Tareas = [.. tareasArray];
            }
        }


        private async Task Post()
        {
            Tareas.Add(nuevaTarea);
            await Http.PostAsJsonAsync("Tarea", NuevaTarea);
            await getData();
        }
      
        private async Task Put()
        {
            await Http.PutAsJsonAsync("Tarea", NuevaTarea);

            Tareas.Insert(Tareas.IndexOf(selectedTarea), nuevaTarea);
            Tareas.Remove(selectedTarea);
        }

        private async Task Delete()
        {
            if (selectedTarea != null)
            {
                Tareas.Remove(selectedTarea);
                HttpResponseMessage httpResponseMessage = await Http.DeleteAsync($"/DelTask/{selectedTarea.Id}");            
                
                await getData();
                SelectedTarea = null;

                if (accion.Equals(Accion.Espera))
                {
                    ShowMessage(ToastType.Danger, "Registro eliminado con éxito");
                }
            }
        }
        #endregion ApiOperations

        #region Modal
        private async Task execTarea()
        {
            if (accion.Equals(Accion.Crear))
            {
                await Post();
                ShowMessage(ToastType.Success, "Registro agregado con éxito");
            }
            else
            {
                await Put();
                ShowMessage(ToastType.Warning, "Registro editado con éxito");
            }
            await HideModal();
        }


        private async Task OnClickShowModal(Enum accion)
        {
            this.accion = accion;

            if (accion.Equals(Accion.Editar))
            {
                NuevaTarea = selectedTarea;
            }

            if (accion.Equals(Accion.Crear))
            {
                NuevaTarea = new Tarea();
            }

            await modal.ShowAsync();
        }

        private async Task HideModal()
        {
            accion = Accion.Espera;
            SelectedTarea = null;
            NuevaTarea = new Tarea();
            await modal.HideAsync();
        }
        #endregion Modal       

        #region SelectRow
        private void selectTarea(Tarea tarea)
        {
            SelectedTarea = tarea;
            Console.WriteLine(tarea.Nombre);
        }

        private string GetRowClass(Tarea tarea)
        {
            return tarea == SelectedTarea ? "selected-row" : "";
        }
        #endregion SelectRow

        #region AutoComplete
        private async Task<AutoCompleteDataProviderResult<Tarea>> TareasDataProvider(AutoCompleteDataProviderRequest<Tarea> request)
        {
            return await Task.FromResult(request.ApplyTo(Tareas.OrderBy(tarea => tarea.Nombre)));
        }

        #endregion AutoComplete

        #region Toast
        private void ShowMessage(ToastType toastType, string message) => messages.Add(CreateToastMessage(toastType, message));

        private ToastMessage CreateToastMessage(ToastType toastType, string message)
        {
            var toastMessage = new ToastMessage();
            toastMessage.Type = toastType;
            toastMessage.Message = message;

            return toastMessage;
        }
        #endregion Toast

        #region Graph     
        //TODO no se cuentan correctamente el numero de lineas
        protected async Task InitializeGraph()
        {
            chartData = new ChartData { Labels = GetDataLabels(), Datasets = GetDataSet() };

            pieChartOptions = new()
            {
                Responsive = true
            };
            pieChartOptions.Plugins.Title!.Text = "Tareas Finalizadas";
            pieChartOptions.Plugins.Title.Display = true;
            pieChartOptions.Plugins.Title.Font.Size = 18;
            pieChartOptions.Plugins.Legend.Display = false;

            if (true)
            {
                await pieChart.InitializeAsync(chartData, pieChartOptions);
            }
            await base.OnAfterRenderAsync(true);
        }

        private List<IChartDataset> GetDataSet()
        {
            var datasets = new List<IChartDataset>
            {
                GetPieChartDataset()
            };

            return datasets;
        }

        private PieChartDataset GetPieChartDataset()
        {
            return new() { Label = " ", Data = GetChartData(), BackgroundColor = GetBackgroundColors() };
        }

        private List<double> GetChartData()
        {
            List<double> data = new();

            double count = (from Tarea tarea in Tareas
                            where tarea.Finalizado
                            select tarea).Count();

            data.Add(Tareas.Count - count);
            data.Add(count);

            return data;
        }

        //10 finalizado, 3 no finalizado
        private List<string> GetBackgroundColors()
        {
            List<string> colors = new()
            {
                backgroundColors[3],
                backgroundColors[10]
            };

            return colors;
        }

        private static List<string> GetDataLabels()
        {

            return new List<string>()
                {
                "No Finalizado",
                "Finalizado"
            };
        }
        #endregion Graph

        #region Handlers
        private void SelectedChangeHandler()
        {
            IsDisabledEdit = selectedTarea == null;
        }

        private void ValueChangeHandler()
        {
            IsDisabled = (String.IsNullOrWhiteSpace(NuevaTarea.Nombre) || String.IsNullOrWhiteSpace(NuevaTarea.Descripcion));
        }

        private async Task OnAutoCompleteChanged(Tarea tarea)
        {
            SelectedTarea = tarea;
            //await JS.InvokeVoidAsync($"searchFuction('{SelectedTarea.Nombre}')");
            Console.WriteLine($"'{tarea?.Nombre}' selected.");
        }
        #endregion Handlers

        #region Enums
        public enum Accion
        {
            Espera,
            Crear,
            Editar
        }

        public enum IsFinalizado
        {
            No,
            Si
        }
        #endregion Enums
    }

}
