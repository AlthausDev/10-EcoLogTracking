namespace EcoLogTracking.Client.Models
{
    public class DateFilter
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public DateFilter(DateTime? dateStart, DateTime? dateEnd)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public DateFilter() { }

    }
}
