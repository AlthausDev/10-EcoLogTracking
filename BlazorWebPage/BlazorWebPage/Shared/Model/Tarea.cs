namespace BlazorWebPage.Shared
{
    /// <summary>
    /// Representa una tarea individual en el sistema.
    /// </summary>
    public class Tarea
    {
        /// <summary>
        /// Identificador único de la tarea.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la tarea.
        /// </summary>
        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set => _nombre = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("El nombre no puede estar vacío.");
        }

        /// <summary>
        /// Descripción detallada de la tarea.
        /// </summary>
        private string _descripcion;
        public string Descripcion
        {
            get => _descripcion;
            set => _descripcion = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("La descripción no puede estar vacía.");
        }

        /// <summary>
        /// Indica si la tarea ha sido finalizada.
        /// </summary>
        public bool Finalizado { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Tarea()
        {
        }

        /// <summary>
        /// Constructor con inicialización de propiedades.
        /// </summary>
        public Tarea(string nombre, string descripcion, bool finalizado = false)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Finalizado = finalizado;
        }
    }
}
