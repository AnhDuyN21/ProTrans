﻿namespace Application.ViewModels.AccountDTOs
{
    public class CreateAccountDTO
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? Dob { get; set; }
        public string? Gender { get; set; }
        public Guid? Role { get; set; }
        public Guid? AgencyId { get; set; }
    }
}
