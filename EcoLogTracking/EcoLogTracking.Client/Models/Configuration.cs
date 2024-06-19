namespace EcoLogTracking.Client.Models
{
    public class Configuration
    {
        int Id { get; set; }

        int Period { get; set; }

        DateTime DeletedDate { get; set; }

        public Configuration() { }

        public Configuration(int id, int period, DateTime deletedDate)
        {
            Id = id;
            Period = period;
            DeletedDate = deletedDate;
        }
    }
}
