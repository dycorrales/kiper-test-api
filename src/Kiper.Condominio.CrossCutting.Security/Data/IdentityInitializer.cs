using Kiper.Condominio.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kiper.Condominio.CrossCutting.Identity.Data
{
    public class IdentityInitializer
    {
        private readonly SecurityContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IdentityInitializer(UserManager<ApplicationUser> userManager, SecurityContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _context.Database.Migrate();
            _context.Database.EnsureCreated();

            CreateRole(Roles.ADMIN);

            var adminUser = new ApplicationUser()
            {
                UserName = "Admin",
                Email = "condominio@kiper.com.br"
            };

            CreateUser(adminUser, "Admin@2019", Roles.ADMIN);
            AddRoleAsync(adminUser, Roles.ADMIN);
        }

        private void CreateRole(string role)
        {
            if (!_roleManager.RoleExistsAsync(role).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(role)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Error in creation of {role}.");
                }
            }
        }

        private void AddRoleAsync(ApplicationUser user, string role)
        {
            if (user != null && role != null)
            {
                user = _userManager.FindByNameAsync(user.UserName).Result;

                var roles = _userManager.GetRolesAsync(user).Result;

                if (roles == null || roles.Count == 0)
                    _userManager.AddToRoleAsync(user, role).Wait();

            }
        }

        private void CreateUser(ApplicationUser user, string password, string role)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var result = _userManager.CreateAsync(user, password).Result;

                if (result.Succeeded && role != null)
                {
                    _userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }
    }
}
