using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DocumentDTOs
{
    public class DocumentPriceDTO
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public decimal? TranslationPrice { get; set; }
        public decimal? NotarizationPrice { get; set; }
        public decimal? Price { get; set; }
    }
}
