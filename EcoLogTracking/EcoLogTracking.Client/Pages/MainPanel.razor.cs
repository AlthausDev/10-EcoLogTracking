﻿using BlazorBootstrap;
using ClosedXML.Excel;
using EcoLogTracking.Client.Components;
using EcoLogTracking.Client.Models;
using Microsoft.JSInterop;


namespace EcoLogTracking.Client.Pages
{
    public partial class MainPanel
    {
        private bool isLoading = true;
        public static Modal ModalInstance = default!;


        /*
  * Método que da formato al excel de los usuarios que vamos a exportar
  //*/
              private async Task OnClickExportExcel()
              {
                  using (var libro = new XLWorkbook())
                  {
                      IXLWorksheet hoja = libro.Worksheets.Add("Logs");
                List<Log> LogList = LogViewer.LogList.ToList();
                      hoja.Cell(1, 1).Value = "Id";
                    hoja.Cell(1, 2).Value = "Fecha y hora";
                    hoja.Cell(1, 3).Value = "Nivel de log";
                    hoja.Cell(1, 4).Value = "Nombre";
                    hoja.Cell(1, 5).Value = "Mensaje";
                    hoja.Cell(1, 6).Value = "Nombre de la máquina";
                    hoja.Cell(1, 7).Value = "Método de la solicitud";
                    hoja.Cell(1, 8).Value = "Traza de la excepción";
                    hoja.Cell(1, 9).Value = "Nombre del archivo";
                    hoja.Cell(1, 10).Value = "Propiedades";
                    hoja.Cell(1, 11).Value = "Código de estado HTTP";
                
                int fila = 2; 
                foreach (var log in LogList)
                {
                    hoja.Cell(fila, 1).Value = log.Id;
                    hoja.Cell(fila, 2).Value = log.Logged;
                    hoja.Cell(fila, 3).Value = log.Level;
                    hoja.Cell(fila, 4).Value = log.Logger;
                    hoja.Cell(fila, 5).Value = log.Message;
                    hoja.Cell(fila, 6).Value = log.MachineName;
                    hoja.Cell(fila, 7).Value = log.Request_method;
                    hoja.Cell(fila, 8).Value = log.Stacktrace;
                    hoja.Cell(fila, 9).Value = log.File_name;
                    hoja.Cell(fila, 10).Value = log.All_event_properties;
                    hoja.Cell(fila, 11).Value = log.Status_code;
                    fila++; 
                }

                using (var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);
                    var nombreExcel = string.Concat("Logs_", DateTime.Now.ToString(), ".xlsx");

                    await JS.InvokeAsync<object>(
                    "DescargarExcel", nombreExcel, Convert.ToBase64String(memoria.ToArray())
                );

                }
            }
        }

    }

   
}
