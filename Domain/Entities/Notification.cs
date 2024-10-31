namespace Domain.Entities
{
    public class Notification
    {
        //Field
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public DateTime NotificationTime { get; set; }
        //Foreignkey
        public Guid? AccountId { get; set; }
        //Relationship
        public virtual Account? Account { get; set; }
    }
}
