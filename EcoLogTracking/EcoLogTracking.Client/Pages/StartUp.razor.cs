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

                bool IsTokenEmpty = string.IsNullOrEmpty(getToken);
               
                if (IsTokenEmpty)
                {                    
                    NavManager.NavigateTo("/login");
                }
                else
                {
                    NavManager.NavigateTo("/logger");
                }
            }
            catch (Exception)
            {
                _ = Http.DefaultRequestHeaders.Remove("Authorization");
                NavManager.NavigateTo("/login");
            }           
        }
    }
}
