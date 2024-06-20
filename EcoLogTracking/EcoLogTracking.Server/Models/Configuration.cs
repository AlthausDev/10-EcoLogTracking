namespace EcoLogTracking.Server.Models
{
    public class Configuration
    {
        /// <summary>
        /// Identificador de la Configuración
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Período de borrado completo de la base de datos (modificable desde la interfaz)
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Último borrado completo realizado sobre la base de datos
        /// </summary>
        public DateTime DeletedDate { get; set; }

        public Configuration() { }

        public Configuration(int id, int period, DateTime deletedDate)
        {
            Id = id;
            Period = period;
            DeletedDate = deletedDate;
        }
    }
}
