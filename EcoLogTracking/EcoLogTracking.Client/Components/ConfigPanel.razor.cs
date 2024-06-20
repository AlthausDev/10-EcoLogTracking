using BlazorBootstrap;
using EcoLogTracking.Client.Models;
using EcoLogTracking.Client.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EcoLogTracking.Client.Components
{
    /// <summary>
    /// Componente que gestiona la configuración del panel de administración.
    /// </summary>
    public partial class ConfigPanel
    {
        #region Parámetros y Propiedades

        [Parameter]
        public MainPanel MainPanelInstance { get; set; }

        public int DeleteFrecuencyDays { get; set; }

        /// <summary>
        /// Nombre de usuario actual.
        /// </summary>
        public string? UserName { get; set; } = MainPanel.User.UserName;

        /// <summary>
        /// Correo electrónico actual.
        /// </summary>
        public string? Email { get; set; } = MainPanel.User.Mail;

        public string newUserName { get; set; } = string.Empty;
        public string newEmail { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;

        public static Tabs tabs = default!;

        #endregion Parámetros y Propiedades

        #region Campos

        private List<ToastMessage> toastMessages = new();
        private ConfirmDialog dialog = default!;
        private Configuration Configuration { get; set; }
        public static bool IsDangerTabActive = false;
        private record TabMessage(string Event, string ActiveTabTitle, string PreviousActiveTabTitle);
        private List<TabMessage> messages = new();

        #endregion Campos

        #region Ciclo de Vida

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            // Cargar la configuración inicial desde el servidor
            Configuration = await Http.GetFromJsonAsync<Configuration>("/api/Configuration");
            DeleteFrecuencyDays = Configuration.Period;

            // Asegurar que la UI se actualice después de obtener los datos
            StateHasChanged();
        }

        #endregion Ciclo de Vida

        #region Manejadores de Eventos de Pestañas

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

        #endregion Manejadores de Eventos de Pestañas

        #region Métodos

        /// <summary>
        /// Cambia el estado de la pestaña "Danger Zone".
        /// </summary>
        /// <param name="isActive">Estado activo/desactivo.</param>
        public async Task ToggleDangerTab(bool isActive)
        {
            IsDangerTabActive = isActive;
            await JS.InvokeVoidAsync("applyFilter", isActive);
        }

        /// <summary>
        /// Muestra la primera pestaña activa.
        /// </summary>
        public static async Task ShowFirstTabAsync() => await tabs.ShowFirstTabAsync();

        /// <summary>
        /// Muestra un cuadro de diálogo de confirmación.
        /// </summary>
        /// <param name="Message">Mensaje de confirmación.</param>
        /// <returns>True si se confirma; False si se cancela.</returns>
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

        /// <summary>
        /// Maneja el evento de clic para eliminar un usuario.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento de clic para eliminar todos los registros de la base de datos.
        /// </summary>
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
                        ShowMessage(ToastType.Danger, "Todos los registros de la base de datos han sido eliminados exitosamente");
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

        /// <summary>
        /// Maneja el evento de clic para actualizar la información del usuario.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento de clic para registrar un nuevo usuario.
        /// </summary>
        private async Task OnClickRegister()
        {
            try
            {
                User newUser = new()
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

        /// <summary>
        /// Limpia los campos de entrada para un nuevo usuario.
        /// </summary>
        private void OnClickClear()
        {
            newUserName = string.Empty;
            newEmail = string.Empty;
            newPassword = string.Empty;
        }

        #endregion Métodos

        #region Toast

        /// <summary>
        /// Muestra un mensaje de toast con el tipo y mensaje especificados.
        /// </summary>
        private void ShowMessage(ToastType toastType, string message) => toastMessages.Add(CreateToastMessage(toastType, message));

        /// <summary>
        /// Crea un objeto de mensaje de toast con el tipo y mensaje especificados.
        /// </summary>
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
