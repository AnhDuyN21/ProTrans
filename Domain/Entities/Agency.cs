﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Agency : BaseEntity
    {
        public string Name { get; set; }
        public string Address {  get; set; }
        public virtual ICollection<Account>? Accounts { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}