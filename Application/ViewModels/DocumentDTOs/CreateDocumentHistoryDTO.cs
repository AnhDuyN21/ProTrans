using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DocumentDTOs
{
    public class CreateDocumentHistoryDTO
    {
        public Guid DocumentId { get; set; }
        public string Name { get; set; }
        public string OldValue { get; set; }
    }
}
