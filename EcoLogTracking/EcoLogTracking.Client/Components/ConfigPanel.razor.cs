using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using EcoLogTracking.Client.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EcoLogTracking.Client.Components
{
    public partial class ConfigPanel
    {
        [Parameter]
        public MainPanel MainPanelInstance { get; set; }
        public int DeleteFrecuencyDays { get; set; }
        public string? UserName { get; set; } = MainPanel.User.UserName;
        public string? Email { get; set; } = MainPanel.User.Mail;


        public string newUserName { get; set; } = string.Empty;
        public string newEmail { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;

        public static Tabs tabs = default!;

        private List<ToastMessage> toastMessages = new();
        private ConfirmDialog dialog = default!;

        private Configuration Configuration { get; set; }

        public static bool IsDangerTabActive = false;

        record TabMessage(string Event, string ActiveTabTitle, string PreviousActiveTabTitle);
        List<TabMessage> messages = new List<TabMessage>();


        protected override async void OnInitialized()
        {         
            base.OnInitialized();

            Configuration = await Http.GetFromJsonAsync<Configuration>("/api/Configuration");
            DeleteFrecuencyDays = Configuration.Period;

            StateHasChanged();
        }

        private void OnTabShowingAsync(TabsEventArgs args)
            => messages.Add(new("OnShowing", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private void OnTabHidingAsync(TabsEventArgs args)
            => messages.Add(new("OnHiding", args.ActiveTabTitle, args.PreviousActiveTabTitle));

        private async Task OnTabShownAsync(TabsEventArgs args)
        {
            messages.Add(new("OnShown", args.ActiveTabTitle, args.PreviousActiveTabTitle));
            IsDangerTabActive = args.ActiveTabTitle.Contains("Danger Zone");
            await ToggleDangerTab(IsDangerTabActive);
        }

        private async Task OnTabHiddenAsync(TabsEventArgs args)
        {
            messages.Add(new("OnHidden", args.ActiveTabTitle, args.PreviousActiveTabTitle));
            IsDangerTabActive = args.ActiveTabTitle.Contains("Danger Zone");
            await ToggleDangerTab(IsDangerTabActive);
        }

        public async Task ToggleDangerTab(bool isActive)
        {
            IsDangerTabActive = isActive;
            await JS.InvokeVoidAsync("applyFilter", isActive);
        }

        public static async Task ShowFirstTabAsync() => await tabs.ShowFirstTabAsync();



        private async Task OnClickDeteleUserAsync()
        {
            string Message = "¿Está seguro de que desea eliminar este usuario?";


            if (await ConfirmDialogAsync(Message))
            {
                var response = await Http.DeleteAsync($"User/{MainPanel.User.Id}");
                Debug.WriteLine(response);
                await MainPanelInstance.OnClickLogOut();
            }
            else
            {
                ShowMessage(ToastType.Secondary, "Acción de eliminación cancelada.");
            }
        }

        private async Task OnClickDeleteAllLogs()
        {
            string Message = "¿Está seguro de que desea eliminar todos los registros de la base de datos?";


            if (await ConfirmDialogAsync(Message))
            {
                try
                {
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
            else
            {    
                ShowMessage(ToastType.Secondary, "Acción de eliminación cancelada.");
            }



        }
        private async Task OnClickUpdate()
        {
            try
            {               
                User userToUpdate = MainPanel.User;
                userToUpdate.UserName = UserName;
                userToUpdate.Mail = Email;

                Configuration.Period = DeleteFrecuencyDays;

                var response = await Http.PutAsJsonAsync("api/User", userToUpdate);
                _ = await Http.PutAsJsonAsync("api/Configuration", Configuration);


                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    ShowMessage(ToastType.Success, "Usuario actualizado correctamente.");
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
                
                User newUser = new User
                {
                    UserName = newUserName,
                    Mail = newEmail,
                    Password = newPassword                   
                };

                var response = await Http.PostAsJsonAsync("api/User", newUser);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    ShowMessage(ToastType.Success, "Usuario creado correctamente.");
                    OnClickClear();                  
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

        private void OnClickClear()
        {
            newUserName = string.Empty;
            newEmail = string.Empty;
            newPassword = string.Empty;
        }

        private async Task<bool> ConfirmDialogAsync(string Message)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "Message", Message }
            };

            var options = new ConfirmDialogOptions
            {
                YesButtonColor = ButtonColor.Danger,
                YesButtonText = "Eliminar",
                NoButtonText = "Cancelar",
                IsVerticallyCentered = true,
                Dismissable = true
            };

            var DialogResponse = await dialog.ShowAsync<ConfirmDialogComponent>(
                title: "Confirmar Eliminación",
                parameters,
                confirmDialogOptions: options);

            return DialogResponse;
        }

        #region Toast
        private void ShowMessage(ToastType toastType, string message) => toastMessages.Add(CreateToastMessage(toastType, message));

        private ToastMessage CreateToastMessage(ToastType toastType, string message)
        {
            var toastMessage = new ToastMessage();
            toastMessage.Type = toastType;
            toastMessage.Message = message;

            return toastMessage;
        }
        #endregion Toast
    }
}
