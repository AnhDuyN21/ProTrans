using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssignmentNotarization : BaseEntity
    {
        public Guid ShipperId { get; set; }
        public Guid DocumentId { get; set; }
        public int NumberOfNotarization {  get; set; }
        public string Status { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Document? Document { get; set; }
    }
}
