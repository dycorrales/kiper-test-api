using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kiper.Condominio.CrossCutting.Identity.Data;
using Kiper.Condominio.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kiper.Condominio.CrossCutting.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void DeleteUser(ApplicationUser user)
        {
            _userManager.DeleteAsync(user).Wait();
        }

        public async Task<ApplicationUser> GetUser(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public void InsertUser(ApplicationUser user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, Roles.ADMIN).Wait();
            }

        }

        public void UpdateUser(ApplicationUser user)
        {
            _userManager.UpdateAsync(user).Wait();
        }
    }
}
