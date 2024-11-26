using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModels.DocumentDTOs
{
    public class UpdateDocumentFromRequestDTO
    {
        public Guid Id { get; set; }
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public string? UrlPath { get; set; }
        public string? FileType { get; set; }
        public int? PageNumber { get; set; }
        public int? NumberOfCopies { get; set; }
        public bool? NotarizationRequest { get; set; }
        public int? NumberOfNotarizedCopies { get; set; }
        [JsonIgnore]
        public string? TranslationStatus { get; set; }
        [JsonIgnore]
        public string? NotarizationStatus { get; set; }
        public Guid? NotarizationId { get; set; }
        public Guid? DocumentTypeId { get; set; }
    }
}
