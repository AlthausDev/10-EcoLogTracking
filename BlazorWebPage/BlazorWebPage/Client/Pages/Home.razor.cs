using BlazorBootstrap;
using BlazorWebPage.Client.Shared.Modals;
using BlazorWebPage.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using ToastType = BlazorBootstrap.ToastType;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using BlazorWebPage.Shared.Model;
using BlazorWebPage.Client.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorWebPage.Client.Pages
{
    public partial class Home
    {
        private Modal ModalInstance = default!;
        private string? Message = string.Empty;
        private dynamic Token = string.Empty;

        public List<User> Usuarios { get; set; } = new List<User>();
        public static User NewUser { get; set; } = new();

        private double[] DataArray = new double[12];

        private List<ToastMessage> Messages = new();
        private LineChart LineChartInstance = default!;
        private LineChartOptions LineChartOptions = default!;
        private ChartData ChartDataInstance = default!;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await GetToken();
                await GetData();
                await InitializeGraph();
            }
            catch { }
        }

        #region Modal

        #region Login
        private async Task Login()
        {
            var parameters = new Dictionary<string, object>
            {
                { "Login", EventCallback.Factory.Create<MouseEventArgs>(this, IniciarSesion) },
                { "Cerrar", EventCallback.Factory.Create<MouseEventArgs>(this, HideModal) }
            };
            await ModalInstance.ShowAsync<ModalLogin>(title: "Logearse", parameters: parameters);
        }

        private async Task IniciarSesion()
        {
            foreach (User user in Usuarios)
            {
                if (user.UserName == NewUser.UserName)
                {
                    User? usuario = await GetUserById(user.Id);

                    if (NewUser.Password.Equals(usuario.Password))
                    {
                        await GenerateTokenAsync(usuario);

                        NavManager.NavigateTo($"/Sesion/{usuario.Id}", true);
                        break;
                    }
                }
            }
            await HideModal();
            ShowMessage(ToastType.Danger, "Inicio de sesión fallido");
        }

        #endregion Login

        #region Registro

        private async Task Registro()
        {
            var parameters = new Dictionary<string, object>
            {
                { "Registrar", EventCallback.Factory.Create<MouseEventArgs>(this, NuevoUsuario) },
                { "Cerrar", EventCallback.Factory.Create<MouseEventArgs>(this, HideModal) }
            };
            await ModalInstance.ShowAsync<ModalRegistro>(title: "Registrarse", parameters: parameters);
        }

        private async Task NuevoUsuario()
        {
            bool existe = Usuarios.Any(user => user.UserName == NewUser.UserName || (user.Email == NewUser.Email && !string.IsNullOrEmpty(NewUser.Email)));

            if (existe)
            {
                ShowMessage(ToastType.Danger, "El nombre de usuario o email ya está registrado");
                await HideModal();
            }
            else
            {
                await PostNewUser();
                ShowMessage(ToastType.Success, "Registro realizado con éxito");
                await IniciarSesion();
            }
        }
        #endregion Registro              

        private async Task HideModal()
        {
            NewUser = new User();
            await ModalInstance.HideAsync();
        }

        #endregion Modal

        #region ApiOperations
        private async Task GetData()
        {
            User[]? usuariosArray = await Http.GetFromJsonAsync<User[]>("user");
            if (usuariosArray != null)
            {
                Usuarios = [.. usuariosArray];
                foreach (User user in Usuarios)
                {
                    GetUsersMonth(user);
                }
            }
        }

        private async Task<User?> GetUserById(int id)
        {
            return await Http.GetFromJsonAsync<User>($"user/{id}");
        }

        private async Task PostNewUser()
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync("user", NewUser);
            if (response.IsSuccessStatusCode)
            {
                await GetData();
            }
            else
            {
                ShowMessage(ToastType.Danger, "Error al registrar el usuario");
            }
        }

        #endregion ApiOperations

        #region AuxMethods
        private async Task GenerateTokenAsync(User usuario)
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync("user/login", usuario);
            if (response.IsSuccessStatusCode)
            {
                TokenResponse tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                await JS.InvokeVoidAsync("localStorage.setItem", new object[] { "token", tokenResponse.Token });
                Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.Token}");
            }
            else
            {
                Console.WriteLine("Error al generar el token");
            }
        }

        private double GetUsersMonth(User user)
        {
            int index = user.FechaRegistro.IndexOf("/");
            string mes = user.FechaRegistro[..index];
            double mesDouble = double.Parse(mes);

            FillDataArray(mesDouble);
            return mesDouble;
        }

        private void FillDataArray(double mes)
        {
            int position = (int)(mes - 1);
            DataArray[position]++;
        }

        private void ImprimirUsuarios()
        {
            foreach (var usuario in Usuarios)
            {
                Console.WriteLine(usuario.ToString());
            }
        }

        private bool CheckFormat(string word)
        {
            if (string.IsNullOrWhiteSpace(word) || word.Length < 3)
            {
                ShowMessage(ToastType.Warning, "El Nombre de usuario y la contraseña deben tener al menos 3 caracteres");
                return false;
            }
            return true;
        }

        private async Task GetToken()
        {
            try
            {
                string getToken = await storageService.GetItemAsStringAsync("token");

                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(getToken);
                List<Claim> claims = jwtSecurityToken.Claims.ToList();

                Console.WriteLine(claims);
                int Id = int.Parse(claims.ElementAt(0).Value);

                User? usuario = await GetUserById(Id);
                NavManager.NavigateTo($"/Sesion/{usuario.Id}", true);

            }
            catch (Exception ex)
            {
                Http.DefaultRequestHeaders.Remove("Authorization");
            }
        }
        #endregion AuxMethods

        #region Toast
        private void ShowMessage(ToastType toastType, string message) => Messages.Add(CreateToastMessage(toastType, message));

        private ToastMessage CreateToastMessage(ToastType toastType, string message)
        {
            return new ToastMessage
            {
                Type = toastType,
                Message = message
            };
        }
        #endregion Toast

        #region LineChart

        protected async Task InitializeGraph()
        {
            var colors = ColorBuilder.CategoricalTwelveColors;

            var labels = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            var datasets = new List<IChartDataset>
            {
                new LineChartDataset
                {
                    Label = "Registros",
                    Data = DataArray.ToList(),
                    BackgroundColor = new List<string> { colors[7] },
                    BorderColor = new List<string> { colors[7] },
                    BorderWidth = new List<double> { 2 },
                    HoverBorderWidth = new List<double> { 4 },
                    PointBackgroundColor = new List<string> { colors[7] },
                    PointRadius = new List<int> { 3 },
                    PointHoverRadius = new List<int> { 4 },
                    Datalabels = new() { Align = "end", Anchor = "end" }
                }
            };

            ChartDataInstance = new ChartData
            {
                Labels = labels,
                Datasets = datasets
            };

            LineChartOptions = new LineChartOptions
            {
                Responsive = true,
                Interaction = new Interaction { Mode = InteractionMode.Index }
            };

            LineChartOptions.Scales.X!.Title!.Text = "Meses";
            LineChartOptions.Scales.X.Title.Display = true;

            LineChartOptions.Scales.Y!.Title!.Text = "Usuarios Registrados";
            LineChartOptions.Scales.Y.Title.Display = true;

            LineChartOptions.Plugins.Datalabels.Color = "white";

            await LineChartInstance.InitializeAsync(chartData: ChartDataInstance, chartOptions: LineChartOptions, plugins: new string[] { "ChartDataLabels" });
            await base.OnAfterRenderAsync(true);
        }
        #endregion LineChart
    }
}
