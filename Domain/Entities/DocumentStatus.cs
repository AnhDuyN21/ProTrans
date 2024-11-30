using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentStatus : BaseEntity
    {
        //Field
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
        //Foreignkey
        public Guid DocumentId { get; set; }
        //Relationship
        public virtual Document Document { get; set; }
    }
}
