using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories.Image
{
    public interface IImageRepository : IGenericRepository<Domain.Entities.Image>
    {
        List<string> GetImagesByAttachmentId(Guid attachmentId);
    }
}
