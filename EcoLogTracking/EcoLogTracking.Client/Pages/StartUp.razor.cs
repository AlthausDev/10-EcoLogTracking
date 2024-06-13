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
                // Obtener el token desde el almacenamiento local
                string? token = await storageService.GetItemAsStringAsync("token");

                // Determinar si el token está presente
                bool isTokenPresent = !string.IsNullOrEmpty(token);

                // Decidir a qué página navegar basándose en la presencia del token
                string nextPage = isTokenPresent ? "/logger" : "/login";

                //TEMP: comentado temporalmente para facilitar las pruebas de desarrollo
                //NavManager.NavigateTo(nextPage);
                NavManager.NavigateTo("/logger");

            }
            catch (Exception)
            {
                _ = Http.DefaultRequestHeaders.Remove("Authorization");
                NavManager.NavigateTo("/login");
            }
        }
    }
}
