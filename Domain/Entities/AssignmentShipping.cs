using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssignmentShipping : BaseEntity
    {
        //ForeignKey
        public Guid ShipperId { get; set; }
        public Guid OrderId { get; set; }
        //Feild
        public DateTime Deadline { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        //Relationship
        public virtual Account? Shipper { get; set; }
        public virtual Order? Order { get; set; }
        public virtual ICollection<ImageShipping>? ImageShippings { get; set; }
    }
}
