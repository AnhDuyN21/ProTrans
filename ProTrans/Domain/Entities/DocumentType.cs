using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentType : BaseEntity
    {
        public string Name { get; set; }
        public decimal PriceFactor { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
    }
}
