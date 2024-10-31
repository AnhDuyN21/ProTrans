using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ImageShipping : BaseEntity
    {
        //Field
        public string? UrlPath { get; set; }
        //Foreignkey
        public Guid AssignmentShippingId { get; set; }
        public Guid? DocumentId { get; set; }
        //Relationship
        public virtual AssignmentShipping? AssignmentShipping { get; set; }
        public virtual Document? Document { get; set; }
    }
}
