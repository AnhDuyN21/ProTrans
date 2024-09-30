namespace Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public Guid? AccountId { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public DateTime NotificationTime { get; set; }
        public virtual Account? Account { get; set; }
    }
}
