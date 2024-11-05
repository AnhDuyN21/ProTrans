using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RequestDTOs
{
    public class CustomerUpdateRequestDTO
    {
        [EnumDataType(typeof(RequestStatus), ErrorMessage = "Trạng thái của request phải là Accept hoặc Refuse")]
        public string? Status { get; set; }
    }
}
