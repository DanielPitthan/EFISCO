using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.Empresas.Interfaces;
using Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Empresas.DAO
{
    public class EmpresaDAO : DataAccessBase, IEmpresaDAO
    {
        public EmpresaDAO(ContextEF _context) : base(_context)
        {
        }

        public IQueryable<Empresa> All()
        {
            return this.Contexto.Empresa.AsQueryable();
        }
    }
}
