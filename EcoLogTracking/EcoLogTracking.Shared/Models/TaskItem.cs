namespace EcoLogTracking.Shared.Models
{
    public class TaskItem : BaseModel
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public TaskItem()
        {
        }

        public TaskItem(int logId, int userId, int stateId, string name, DateTime expirationDate)
        {
            LogId = logId;
            UserId = userId;
            StateId = stateId;
            Name = name;
            ExpirationDate = expirationDate;
        }
    }
}
