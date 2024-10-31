using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentPrice : BaseEntity
    {
        //Field
        public decimal? TranslationPrice { get; set; }
        public decimal? NotarizationPrice { get; set; }
        public decimal? Price { get; set; }
        //Foreignkey
        public Guid DocumentId { get; set; }
        //Relationship
        public virtual Document? Document { get; set; }
    }
}
