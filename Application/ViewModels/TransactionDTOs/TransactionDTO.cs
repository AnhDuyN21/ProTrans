﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TransactionDTOs
{
	public class TransactionDTO
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public Guid OrderId { get; set; }
	}
}