namespace Application.ViewModels.AgencyDTOs
{
    public class AgencyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
