using EcoLogTracking.Shared.Models;
using System.Net.Http.Json;

namespace EcoLogTracking.Shared.Data
{
    public class LogData
    {
        public static Log[] Categories { get; set; }

        public static async Task LoadTestCategories(HttpClient http)
        {
            Categories = [

                new("Informe"),
                new("Presentación"),
                new("Reunión"),
                new("Correo electrónico"),
                new("Llamadas de seguimiento"),
                new("Sitio web"),
                new("Pruebas de software"),
                new("Lista de verificación")
            ];

            foreach (Log log in Categories)
            {
                _ = await http.PostAsJsonAsync("api/Log", log);
            }
        }
    }
}
