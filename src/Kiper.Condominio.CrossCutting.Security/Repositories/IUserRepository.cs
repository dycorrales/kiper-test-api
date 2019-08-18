using Kiper.Condominio.CrossCutting.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kiper.Condominio.CrossCutting.Identity.Repositories
{
    public interface IUserRepository
    {
        void InsertUser(ApplicationUser user, string password);
        void UpdateUser(ApplicationUser user);
        Task<ApplicationUser> GetUser(Guid id);
        void DeleteUser(ApplicationUser user);
    }
}
