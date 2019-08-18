using Kiper.Condominio.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kiper.Condominio.CrossCutting.Identity.Data
{
    public class SecurityContext : IdentityDbContext<ApplicationUser>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        {
        }
    }
}
