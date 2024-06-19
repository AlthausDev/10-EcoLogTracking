namespace EcoLogTracking.Server.Models
{
    public class Configuration
    {
        public int Id { get; set; }

        public int Period { get; set; }

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
