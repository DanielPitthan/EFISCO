using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.XmlDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.NFe;
using System.Linq;

namespace DAL.XmlDAL.DAO
{
    public class NFeFilesDAO : DataAccessBase,INFeFilesDAO
    {
        public NFeFilesDAO(ContextEF _context) : base(_context)
        {
        }

        public IQueryable<NFeFiles> GetAll()
        {
            return this.Contexto.NFeFiles;
        }
    }
}
