using ClosedXML.Excel;
using EcoLogTracking.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLogTracking.Client.Components
{
    public partial class ExportExcel
    {
        [Parameter]
        public DateTime FirstDate { get; set; } = LogViewer.FirstLogDate;

        public DateTime LastDate { get; set; } = DateTime.Now;

        [Parameter]
        public EventCallback<MouseEventArgs> Cerrar { get; set; }
    

        private async Task OnClickExport(MouseEventArgs e)
        {
            LastDate = LastDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            await ExportExcelAsync();
        }

        private async Task OnClickHide(MouseEventArgs e)
        {
            await Cerrar.InvokeAsync(e);
        }

        /// <summary>
        /// Exporta los logs a un archivo Excel.
        /// </summary>
        private async Task ExportExcelAsync()
        {
            Debug.WriteLine(LastDate.ToString());

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Logs");
            var logList = LogViewer.LogList
                .Where(log => log.Logged >= FirstDate && log.Logged <= LastDate)
                .ToList();
           
            // Define las cabeceras del archivo Excel
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Fecha y hora";
            worksheet.Cell(1, 3).Value = "Nivel de log";
            worksheet.Cell(1, 4).Value = "Nombre";
            worksheet.Cell(1, 5).Value = "Mensaje";
            worksheet.Cell(1, 6).Value = "Nombre de la máquina";
            worksheet.Cell(1, 7).Value = "Método de la solicitud";
            worksheet.Cell(1, 8).Value = "Traza de la excepción";
            worksheet.Cell(1, 9).Value = "Nombre del archivo";
            worksheet.Cell(1, 10).Value = "Propiedades";
            worksheet.Cell(1, 11).Value = "Código de estado HTTP";

            // Llena el archivo Excel con los datos de los logs
            for (int row = 2; row <= logList.Count + 1; row++)
            {
                var log = logList[row - 2];
                worksheet.Cell(row, 1).Value = log.Id;
                worksheet.Cell(row, 2).Value = log.Logged;
                worksheet.Cell(row, 3).Value = log.Level;
                worksheet.Cell(row, 4).Value = log.Logger;
                worksheet.Cell(row, 5).Value = log.Message;
                worksheet.Cell(row, 6).Value = log.MachineName;
                worksheet.Cell(row, 7).Value = log.Request_method;
                worksheet.Cell(row, 8).Value = log.Stacktrace;
                worksheet.Cell(row, 9).Value = log.File_name;
                worksheet.Cell(row, 10).Value = log.All_event_properties;
                worksheet.Cell(row, 11).Value = log.Status_code;
            }

            // Guarda el archivo Excel y lo descarga
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            var excelFileName = $"Logs_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            await JS.InvokeAsync<object>("DescargarExcel", excelFileName, Convert.ToBase64String(memoryStream.ToArray()));
            await Cerrar.InvokeAsync();
        }
    }
}
