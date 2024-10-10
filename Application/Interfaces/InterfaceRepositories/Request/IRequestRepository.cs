using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories.Request
{
    public interface IRequestRepository : IGenericRepository<Domain.Entities.Request>
    {
        Guid GetCurrentCustomerId();
    }
}
