using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Image;

namespace Infrastructures.Repositories.Image
{
    public class ImageRepository : GenericRepository<Domain.Entities.Image>, IImageRepository
    {
        private readonly AppDbContext _appDbContext;


        public ImageRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _appDbContext = context;
        }

        public List<string> GetImagesByAttachmentId(Guid attachmentId)
        {
            return _appDbContext.Image.Where(x => x.AttachmentId == attachmentId).Select(x => x.ImageUrl).ToList();
        }
    }
}
