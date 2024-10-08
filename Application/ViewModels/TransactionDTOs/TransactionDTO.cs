namespace Application.ViewModels.TransactionDTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid OrderId { get; set; }
    }
}