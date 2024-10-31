namespace Domain.Entities
{
    public class Request : BaseEntity
    {
        public Guid? CustomerId { get; set; }
        public Guid? ShipperId { get; set; }
        public DateTime? Deadline { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public string? Status { get; set; }
        public bool? IsConfirmed { get; set; } = false;
        public bool? PickUpRequest { get; set; } = false;
        public bool ShipRequest { get; set; } = false;
        public virtual Account? CustomerAccount { get; set; }
        public virtual Account? ShipperAccount { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
        public virtual Order? Order { get; set; }

    }
}
