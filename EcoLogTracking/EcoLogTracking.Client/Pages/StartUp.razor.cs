using EcoLogTracking.Client.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace EcoLogTracking.Client.Pages
{
    public partial class StartUp
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await CheckToken();
        }

        private async Task CheckToken()
        {
            try
            {   
                string? token = await storageService.GetItemAsStringAsync("token");
                bool isTokenPresent = !string.IsNullOrEmpty(token);

                //string nextPage = isTokenPresent ? "/logger" : "/login";
                string nextPage = "/logger";

                if (isTokenPresent)
                {
                   
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);
               
                    List<Claim> claims = jwtSecurityToken.Claims.ToList();

                    int Id = int.Parse(claims.ElementAtOrDefault(2)?.Value ?? "0");
                    MainPanel.User = await Http.GetFromJsonAsync<User>($"/user/{Id}");  
                }

                NavManager.NavigateTo(nextPage);
            }
            catch (SecurityTokenException ex)
            {               
                Console.WriteLine($"SecurityTokenException: {ex.Message}");
                _ = Http.DefaultRequestHeaders.Remove("Authorization");
                NavManager.NavigateTo("/login");
            }
            catch (Exception ex)
            {               
                Console.WriteLine($"Exception: {ex.Message}");
                _ = Http.DefaultRequestHeaders.Remove("Authorization");
                NavManager.NavigateTo("/login");
            }
        }

    }
}
