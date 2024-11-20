using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DistanceDTOs
{
    public class DistanceDTO
    {
        public Guid Id { get; set; }
        public Guid? RootAgencyId { get; set; }
        public Guid? TargetAgencyId { get; set; }
        public decimal? Value { get; set; }

    }
}
