function DescargarExcel(nombreArchivo, base64Archivo) {
    const link = document.createElement("a");
    link.download = nombreArchivo;
    link.href = "data:application/vnd.ms-excel;base64," + base64Archivo;

    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

}