using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Infra;

namespace DAL.Infra.DAO
{
    public class ParametroDAO : DataAccessBase, IParametros
    {


        public ParametroDAO(ContextEFNFeXml _context) : base(_context) { }

        public DbSet<Parametro> GetAll()
        {
            return Contexto.Parametro;
        }
    }
}
