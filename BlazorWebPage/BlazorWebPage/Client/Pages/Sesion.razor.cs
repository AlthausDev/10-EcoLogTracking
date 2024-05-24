using Azure;
using BlazorBootstrap;
using Blazored.LocalStorage.StorageOptions;
using BlazorWebPage.Client.Shared;
using BlazorWebPage.Client.Shared.Modals;
using BlazorWebPage.Shared;
using BlazorWebPage.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Channels;
using static System.Net.WebRequestMethods;
using ToastType = BlazorBootstrap.ToastType;

namespace BlazorWebPage.Client.Pages
{
    partial class Sesion
    {
        [Parameter]
        public string Id { get; set; }
        private string fecha;

        public string Token { get; set; }

        public static User user = new();

        private IEnumerable<User> Usuarios = default!;
        private Modal modal = default!;

        private string ShowTable = "none";

        protected override async Task OnInitializedAsync()
        {
            NavMenu.IsHidden = false;
            NavMenu.Id = Id;
            await getData();
                    
            Http.DefaultRequestHeaders.Remove("Authorization");
            Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {await storageService.GetItemAsStringAsync("token")}");
            
            fecha = DateFormat(user.FechaRegistro);
            if (user.UserName.Equals("admin") || user.UserName.Equals("111"))
            {
                ShowTable = "block";
            }
        }


        #region ApiOperations
        private async Task getAllData()
        {
            User[]? usuariosArray = await Http.GetFromJsonAsync<User[]>("user/admin");

            foreach (User user in usuariosArray)
            {
                user.FechaRegistro = DateFormat(user.FechaRegistro);
            }

            if (usuariosArray is not null)
            {
                Usuarios = [.. usuariosArray];
            }
        }

        private async Task getData()
        {
            try
            {
                user = await Http.GetFromJsonAsync<User>($"user/{Id}");
                Console.WriteLine(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink);
                Console.WriteLine("\nSource ---\n{0}", ex.Source);
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
                Console.WriteLine("\nTargetSite ---\n{0}", ex.TargetSite);
            }
        }

        private async Task Put(User user)
        {
            await Http.PutAsJsonAsync("User", user);
        }


        private async Task Delete()
        {
            HttpResponseMessage httpResponseMessage = await Http.DeleteAsync($"/Delete/{user.Id}");
            Console.WriteLine(httpResponseMessage);

            await ReturnHome();
        }
        #endregion

        #region EditModal

        private async Task EditModal()
        {
            try
            {
                Console.WriteLine(user.ToString());
                var parameters = new Dictionary<string, object>
            {
                { "Id", user.Id },
                { "UserName", user.UserName },
                { "Password", user.Password },
                { "Nombre", user.Nombre },
                { "Email", user.Email },
                { "Editar", EventCallback.Factory.Create<MouseEventArgs>(this, EditarDatos) },
                { "Cerrar", EventCallback.Factory.Create<MouseEventArgs>(this, HideModal) },
                { "Volver", EventCallback.Factory.Create<MouseEventArgs>(this, ReturnHome) }
            };
                await modal.ShowAsync<ModalEditUser>(title: "Editar", parameters: parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink);
                Console.WriteLine("\nSource ---\n{0}", ex.Source);
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
                Console.WriteLine("\nTargetSite ---\n{0}", ex.TargetSite);
                await modal.ShowAsync<ModalEditUser>(title: "Editar");
            }
        }


        private async Task EditarDatos()
        {
            await Put(user);
            await HideModal();
        }

        private async Task HideModal()
        {
            await modal.HideAsync();
        }

        private async Task ReturnHome()
        {
            await storageService.RemoveItemAsync("token");

            NavManager.NavigateTo("/", true);
        }

        #endregion

        #region Grid
        private async Task<GridDataProviderResult<User>> UsersDataProvider(GridDataProviderRequest<User> request)
        {
            await getAllData();
            return await Task.FromResult(request.ApplyTo(Usuarios.OrderBy(user => user.Id)));
        }

        #endregion

        #region Aux     
        private string DateFormat(string fecha)
        { 
            DateTime enteredDate = DateTime.Parse(fecha);
            string date = enteredDate.ToString("MM/dd/yyyy");

            return date;
        }
        #endregion

    }
}
