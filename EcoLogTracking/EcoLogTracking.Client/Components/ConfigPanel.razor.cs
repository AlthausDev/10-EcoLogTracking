using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using EcoLogTracking.Client.Pages;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EcoLogTracking.Client.Components
{
    public partial class ConfigPanel
    {
        public int DeleteFrecuencyDays { get; set; }
        public string UserName { get; set; } = MainPanel.User.UserName;
        public string Email { get; set; } = "Default@ecoalgo.com";


        public string newUserName { get; set; }
        public string newEmail { get; set; }
        public string newPassword { get; set; }

        record TabMessage(string Event, string ActiveTabTitle, string PreviousActiveTabTitle);
        List<TabMessage> messages = new List<TabMessage>();

        private void OnTabShowingAsync(TabsEventArgs args)
            => messages.Add(new("OnShowing", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private void OnTabShownAsync(TabsEventArgs args)
            => messages.Add(new("OnShown", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private void OnTabHidingAsync(TabsEventArgs args)
            => messages.Add(new("OnHiding", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private void OnTabHiddenAsync(TabsEventArgs args)
            => messages.Add(new("OnHidden", args.ActiveTabTitle, args.PreviousActiveTabTitle));
     
        private async Task OnClickDeteleUserAsync()
        {
            throw new NotImplementedException();
        }
        private async Task OnClickDeleteAllLogs()
        {
            try
            {              
                int numDias = 0;                 
                var response = await Http.DeleteAsync("/" + 0);
              
                if (response.IsSuccessStatusCode)
                {                   
                    var message = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(message);
                }
                else
                {           
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la petición HTTP: {ex.Message}");
            }
        }
        private async Task OnClickUpdate()
        {
            try
            {               
                User userToUpdate = MainPanel.User;
                userToUpdate.UserName = UserName;

                var response = await Http.PutAsJsonAsync("api/User", userToUpdate);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(message); 
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la petición HTTP: {ex.Message}");
            }
        }

        private async Task OnClickRegister()
        {
            try
            {
                // Construye el objeto User con los datos del nuevo usuario a registrar
                User newUser = new User
                {
                    UserName = newUserName,
                    //Email = newEmail,
                    Password = newPassword,
                    // Incluir otros campos necesarios para el registro
                };

                var response = await Http.PostAsJsonAsync("api/User", newUser);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(message); // Opcional: puedes imprimir el mensaje de éxito
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la petición HTTP: {ex.Message}");
            }
        }

        private void OnClickClear(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            newUserName = string.Empty;
            newEmail = string.Empty;
            newPassword = string.Empty;
        }
    }
}
