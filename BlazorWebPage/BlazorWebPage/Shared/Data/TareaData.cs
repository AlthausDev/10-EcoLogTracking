namespace BlazorWebPage.Shared.Data
{
    public static class TareaData
    {
        public static Tarea[] tareas { get; set; }

        static TareaData() => cargarDatos();

        private static void cargarDatos()
        {
            tareas = new Tarea[] {
                new("Tarea 1", "Descripción de la tarea 1" , false),
                new("Tarea 2", "Descripción de la tarea 2", true),
                new("Tarea 3", "Descripción de la tarea 3", false),
                new("Tarea 4", "Descripción de la tarea 4", false),
                new("Tarea 5", "Descripción de la tarea 5", true),
                new("Tarea 6", "Descripción de la tarea 6", false),
                new("Tarea 7", "Descripción de la tarea 7", true),
                new("Tarea 8", "Descripción de la tarea 8", true),
                new("Tarea 9", "Descripción de la tarea 9", false),
                new("Tarea 10", "Descripción de la tarea 10", false),
                new("Tarea 11", "Descripción de la tarea 11" , false),
                new("Tarea 12", "Descripción de la tarea 12", true),
                new("Tarea 13", "Descripción de la tarea 13", false),
                new("Tarea 14", "Descripción de la tarea 14", false),
                new("Tarea 15", "Descripción de la tarea 15", true),
                new("Tarea 16", "Descripción de la tarea 16", false),
                new("Tarea 17", "Descripción de la tarea 17", true),
                new("Tarea 18", "Descripción de la tarea 18", true),
                new("Tarea 19", "Descripción de la tarea 19", false),
                new("Tarea 20", "Descripción de la tarea 20", false)
        };

        }
    }
}
