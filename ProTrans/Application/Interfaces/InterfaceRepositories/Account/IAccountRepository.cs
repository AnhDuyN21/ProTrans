using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories.Account
{
    public interface IAccountRepository : IGenericRepository<Domain.Entities.Account>
    {
        Task<bool> CheckEmailNameExited(string email);
        Task<bool> CheckPhoneNumberExited(string phonenumber);
        Task<bool> CheckCodeExited(string code);
    }
}
