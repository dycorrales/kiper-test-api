using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kiper.Condominio.Core.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        int? GetRole();
        bool IsAdminUser { get; }
    }
}
