﻿using Application.ViewModels.TranslatorSkillDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AccountDTOs
{
    public class CreateTranslatorDTO
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public Guid? AgencyId { get; set; }
        public List<CreateTranslatorSkillDTO>? Skills { get; set; }
    }
}
