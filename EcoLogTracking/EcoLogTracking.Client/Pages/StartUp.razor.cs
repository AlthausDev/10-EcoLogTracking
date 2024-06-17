using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
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
                   
                    MainPanel.User.Id = int.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                    MainPanel.User.UserName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
              

                    Console.WriteLine($"User ID: {MainPanel.User.Id}, UserName: {MainPanel.User.UserName}");
                    Debug.WriteLine($"User ID: {MainPanel.User.Id}, UserName: {MainPanel.User.UserName}");
                    Debug.WriteLine($"User ID: {MainPanel.User.Id}, UserName: {MainPanel.User.UserName}");
                    Debug.WriteLine($"User ID: {MainPanel.User.Id}, UserName: {MainPanel.User.UserName}");
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
