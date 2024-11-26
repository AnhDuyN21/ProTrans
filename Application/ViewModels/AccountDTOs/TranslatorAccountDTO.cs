using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AccountDTOs
{
	public class TranslatorAccountDTO
	{
		public Guid Id { get; set; }
		public string? UserName { get; set; }
		public string? FullName { get; set; }
		public string? Code { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Password { get; set; }
		public string? Dob { get; set; }
		public string? Gender { get; set; }
		public Guid? RoleId { get; set; }
		public Guid? AgencyId { get; set; }
		public string? AgencyName { get; set; }
		public bool? IsDeleted { get; set; }
		public decimal? Distance { get; set; }
		public int? AssignmentsInProgress { get; set; }
	}
}
