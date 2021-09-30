using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.Empresas.Interfaces;
using Models.Empresas;
using System.Linq;

namespace DAL.Empresas.DAO
{
    public class EmpresaDAO : DataAccessBase, IEmpresaDAO
    {
        public EmpresaDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public IQueryable<Empresa> All()
        {
            return Contexto.Empresa.AsQueryable();
        }
    }
}
