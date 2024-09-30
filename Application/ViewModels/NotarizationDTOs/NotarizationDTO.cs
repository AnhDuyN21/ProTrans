namespace Application.ViewModels.NotarizationDTOs
{
    public class NotarizationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
