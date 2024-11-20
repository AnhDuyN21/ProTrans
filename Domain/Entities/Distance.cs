using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Distance : BaseEntity
    {
        //Field
        public decimal? Value {  get; set; }
        //Foreignkey
        public Guid? RootAgencyId { get; set; }
        public Guid? TargetAgencyId { get; set; }
        //Relationship
        public Agency? RootAgency { get; set; }
        public Agency? TargetAgency { get; set; }
    }
}
