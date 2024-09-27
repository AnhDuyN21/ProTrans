using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public Guid ShipperId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? AttachmentId { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Attachment? Attachment { get; set; }
    }
}
