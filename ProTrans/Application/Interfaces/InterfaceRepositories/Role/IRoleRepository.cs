using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories.Role
{
    public interface IRoleRepository
    {
        string GetRoleName(Guid roleId);
        Guid GetIdCustomerRole();
    }
}
