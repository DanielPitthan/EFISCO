using DAL.DAOBase;
using Models.Empresas;
using System.Linq;

namespace DAL.Empresas.Interfaces
{
    public interface IEmpresaDAO : IDataAccessBase
    {
        IQueryable<Empresa> All();
    }
}
