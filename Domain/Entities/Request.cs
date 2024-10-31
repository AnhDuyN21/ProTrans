namespace Domain.Entities
{
    public class Request : BaseEntity
    {

        public DateTime? Deadline { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public string? Status { get; set; }
        public bool? IsConfirmed { get; set; } 
        public bool? PickUpRequest { get; set; } 
        public bool ShipRequest { get; set; } 
        //Foreignkey
        public Guid? CustomerId { get; set; }

        //Relationship
        public virtual Account? CustomerAccount { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
        public virtual Order? Order { get; set; }

    }
}
