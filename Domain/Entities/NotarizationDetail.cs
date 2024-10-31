using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NotarizationDetail : BaseEntity
    {
        //Foreignkey
        public Guid AssignmentNotarizationId { get; set; }
        public Guid DocumentId { get; set; }
        //Relationship
        public virtual AssignmentNotarization? AssignmentNotarization { get; set; }
        public virtual Document? Document { get; set; }
    }
}
