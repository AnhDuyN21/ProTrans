using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
	public enum DocumentNotarizationStatus
	{
		None,
		Waiting,
		Processing,
		PickingUp,
		PickedUp,
		Notarizating,
		Notarizated
	}
}
