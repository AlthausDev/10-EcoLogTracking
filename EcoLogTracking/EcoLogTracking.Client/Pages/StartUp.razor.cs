using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


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
                string getToken = await storageService.GetItemAsStringAsync("token");

                bool IsAnyToken = string.IsNullOrEmpty(getToken);

                if (IsAnyToken)
                {
                    NavManager.NavigateTo("/logger");
                    //NavManager.NavigateTo("/login");
                }
                else
                {
                    NavManager.NavigateTo("/login");
                }
                 

                //var handler = new JwtSecurityTokenHandler();
                //var jwtSecurityToken = handler.ReadJwtToken(getToken);
                //List<Claim> claims = jwtSecurityToken.Claims.ToList();

                //userId = int.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                //UserType = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;                
            }
            catch (Exception)
            {
                _ = Http.DefaultRequestHeaders.Remove("Authorization");
                NavManager.NavigateTo("/logger");
            }           
        }
    }
}
