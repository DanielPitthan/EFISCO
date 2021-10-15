using DAL.DAOBase;
using DAL.DAOBaseNfeXml;
using Models.Empresas;
using System.Linq;

namespace DAL.Empresas.Interfaces
{
    public interface IEmpresaDAO : IDataAccessBaseNfeXml
    {
        IQueryable<Empresa> All();
    }
}
