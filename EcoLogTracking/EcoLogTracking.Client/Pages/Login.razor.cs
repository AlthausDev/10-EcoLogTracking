using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using Irony.Parsing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace EcoLogTracking.Client.Pages
{
    public partial class Login
    {
        public static User user = new();
        private List<ToastMessage> messages = new();
        private string UserName { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;

        #region Login     
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("addEnterEventListener", "loginButton");
            }
        }

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

                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(loginResponse);
                    List<Claim> claims = jwtSecurityToken.Claims.ToList();

                    string userIdClaim = jwtSecurityToken.Claims.ElementAtOrDefault(2)?.Value ?? "0"; // Index 0 is NameIdentifier
                    string userNameClaim = jwtSecurityToken.Claims.ElementAtOrDefault(1)?.Value ?? string.Empty; // Index 1 is Name



                    MainPanel.User.Id = int.Parse(userIdClaim); // Ensure the conversion is correct
                    MainPanel.User.UserName = userNameClaim.ToString();
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
