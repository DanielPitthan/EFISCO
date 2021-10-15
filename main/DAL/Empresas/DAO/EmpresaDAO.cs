using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.DAOBaseNfeXml;
using DAL.Empresas.Interfaces;
using Models.Empresas;
using System.Linq;

namespace DAL.Empresas.DAO
{
    public class EmpresaDAO : DataAccessBaseNfeXml, IEmpresaDAO
    {
        public EmpresaDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public IQueryable<Empresa> All()
        {
            return Contexto.Empresa.AsQueryable();
        }

        public override bool Update<TSource>(TSource item)
        {
           return base.Update(item);
        }
    }
}
