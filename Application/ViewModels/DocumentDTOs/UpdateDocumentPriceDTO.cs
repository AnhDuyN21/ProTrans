﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DocumentDTOs
{
    public class UpdateDocumentPriceDTO
    {
        public decimal? TranslationPrice { get; set; }
        public decimal? NotarizationPrice { get; set; }
        public decimal? Price => (TranslationPrice ?? 0) + (NotarizationPrice ?? 0);
    }
}