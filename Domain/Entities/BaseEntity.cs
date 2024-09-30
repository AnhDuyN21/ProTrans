namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Guid? DeletedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }
}
