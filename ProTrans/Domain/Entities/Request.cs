using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Request : BaseEntity
    {
        public Guid? CustomerId { get; set; }
        public DateTime? Deadline { get; set; }
        public string? Status { get; set; }
        public bool ShipRequest { get; set; } = false;
        public Guid? ShipperId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
        public virtual Order? Order { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
    }
}
