using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.XmlDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.NFe;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.XmlDAL.DAO
{
    public class NFeFilesMensagensDAO : DataAccessBase, INFeFilesMensagensDAO
    {
        public NFeFilesMensagensDAO(ContextEF _context) : base(_context)
        {
        }

      
        public IQueryable<NFeFilesMensagens> GetAll()
        {
            return this.Contexto.NFeFilesMensagens;
        }


    }
}
