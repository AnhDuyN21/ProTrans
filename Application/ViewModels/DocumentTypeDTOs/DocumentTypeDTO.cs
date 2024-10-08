namespace Application.ViewModels.DocumentTypeDTOs
{
    public class DocumentTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal PriceFactor { get; set; }
        public bool IsDeleted { get; set; }
    }
}
