using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.DAOBaseNfeXml;
using DAL.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Infra;

namespace DAL.Infra.DAO
{
    public class ParametroDAO : DataAccessBaseNfeXml, IParametrosDAO
    {


        public ParametroDAO(ContextEFNFeXml _context) : base(_context) { }

        public DbSet<Parametro> GetAll()
        {
            return Contexto.Parametro;
        }

        public override bool Update<TSource>(TSource item)
        {
            return base.Update(item);
        }
    }
}
