using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FeedBack : BaseEntity
    {
        public string? Message { get; set; }
        public Guid AccountId { get; set; }
        public Guid OrderId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Order? Order { get; set; }
    }
}
