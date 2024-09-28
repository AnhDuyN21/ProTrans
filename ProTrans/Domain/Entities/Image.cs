using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public Guid? AttachmentId { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Attachment? Attachment { get; set; }
    }
}
