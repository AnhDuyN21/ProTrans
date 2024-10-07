namespace Domain.Entities
{
	public class Transaction
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public Guid OrderId { get; set; }
		public virtual Account? Account { get; set; }
		public virtual Order? Order { get; set; }
	}
}