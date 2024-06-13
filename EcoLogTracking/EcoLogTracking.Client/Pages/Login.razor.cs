using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace EcoLogTracking.Client.Pages
{
    public partial class Login
    {

        public static User user = new();

       
        private List<ToastMessage> messages = new();
        private string UserName { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;
    

        #region Login     
        private async Task OnClickLogin()
        {
            var loginResult = await LoginUser(UserName, Password);
            LoginResult(loginResult);
        }

        private void LoginResult(ActionResult<User> loginResult)
        {
            try
            {
                if (loginResult == null || loginResult.Value == null)
                {
                    ShowMessage(ToastType.Danger, "Credenciales incorrectas. Por favor, inténtelo de nuevo.");
                    return;
                }
                
            }
            catch (Exception ex)
            {
                ShowMessage(ToastType.Danger, $"Ocurrió un error al procesar el inicio de sesión: {ex.Message}");
            }
        }
        #endregion
                

        #region Api            
        private async Task<ActionResult<User>> LoginUser(string Username, string Password)
        {
            //    try
            //    {
            //        var credentials = new LoginCredentials(Username, Password);
            //        var response = await Http.PostAsJsonAsync("api/User/login", credentials);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            LoginResponse loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            //            await GenerateTokenAsync(loginResponse.Token);
            //            return new ActionResult<User>(loginResponse.User);
            //        }
            //        else
            //        {
            //            return new ActionResult<User>(new NotFoundResult());
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Error al intentar iniciar sesión: {ex.Message}");
            //        return new ActionResult<User>(new StatusCodeResult(500));
            //    }
            return null;
        }


        #endregion Api     

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

        #region Token
        private async Task GenerateTokenAsync(string token)
        {
            await JS.InvokeVoidAsync("localStorage.setItem", new object[] { "token", token });
            Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        #endregion Token   
    }
}
