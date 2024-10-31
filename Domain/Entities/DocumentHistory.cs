using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentHistory : BaseEntity
    {
        //Field
        public string? Name { get; set; }
        public string? oldValue { get; set; }
        //Foreignkey
        public Guid DocumentId { get; set; }
        //Relationship
        public virtual Document? Document { get; set; }
    }
}
