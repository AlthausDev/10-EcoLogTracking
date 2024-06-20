namespace EcoLogTracking.Server.Models
{
    public class DateFilter
    {
        /// <summary>
        /// Fecha de inicio para el filtrado que se desea llevar a cabo
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Fecha de fin para el filtrado que se desea llevar a cabo
        /// </summary>
        public DateTime? DateEnd { get; set; }

        public DateFilter(DateTime? dateStart, DateTime? dateEnd)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public DateFilter() { }

    }
}
