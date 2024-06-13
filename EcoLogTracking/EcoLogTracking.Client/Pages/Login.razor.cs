using BlazorBootstrap;
using EcoLogTracking.Client.Models;
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
            string? result = await LoginUser(UserName, Password);
            LoginResult(result);
        }

        private void LoginResult(string? result)
        {
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    ShowMessage(ToastType.Danger, "Credenciales incorrectas. Por favor, inténtelo de nuevo.");
                    return;
                }

                NavManager.NavigateTo("/logger");
            }
            catch (Exception ex)
            {
                ShowMessage(ToastType.Danger, $"Ocurrió un error al procesar el inicio de sesión: {ex.Message}");
            }
        }
        #endregion

        #region Api            
        private async Task<string?> LoginUser(string Username, string Password)
        {
            try
            {
                var user = new User(Username, Password);
                var response = await Http.PostAsJsonAsync("api/user/login", user);

                if (response.IsSuccessStatusCode)
                {
                    string loginResponse = await response.Content.ReadAsStringAsync();
                    await GenerateTokenAsync(loginResponse);
                    return loginResponse;
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar iniciar sesión: {ex.Message}");
                return null;
            }
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
