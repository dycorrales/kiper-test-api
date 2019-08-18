using Kiper.Condominio.Core.Helpers;

namespace Kiper.Condominio.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Response Commit();
    }
}
