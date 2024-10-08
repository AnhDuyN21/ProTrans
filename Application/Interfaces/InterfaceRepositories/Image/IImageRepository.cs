namespace Application.Interfaces.InterfaceRepositories.Image
{
    public interface IImageRepository : IGenericRepository<Domain.Entities.Image>
    {
        List<string> GetImagesByAttachmentId(Guid attachmentId);
    }
}
