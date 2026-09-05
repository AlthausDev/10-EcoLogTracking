# EcoLogTracking

Proyecto .NET orientado a experimentar con captura, persistencia y consulta de logs dentro de una solución separada por responsabilidades.

## Estructura

La solución principal se encuentra en `EcoLogTracking/` e incluye varios proyectos:

- `EcoLogTracking.Client` — cliente de la aplicación.
- `EcoLogTracking.Server` — servidor y endpoints.
- `EcoLogTracking.Database` — acceso y persistencia de datos.
- `EcoConsoleBackService` — procesamiento/servicio auxiliar en segundo plano.
- `RequestLoggingMiddleware` — middleware dedicado al registro de peticiones.

## Objetivo

El repositorio sirve como laboratorio para separar la infraestructura de logging de la aplicación principal y probar un flujo completo desde la captura de eventos hasta su almacenamiento y consulta.

`LogRecap.txt` conserva un resumen ligero del estado de los logs durante el desarrollo.

## Licencia

El código original puede reutilizarse bajo la [licencia de atribución](LICENSE). Si lo usas o adaptas, incluye una referencia razonable a **Sam Althaus / AlthausDev** y, cuando sea práctico, al repositorio original.

Los componentes de terceros conservan sus propias licencias.
