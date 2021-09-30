using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.XmlDAL.Interfaces;
using Models.NFe;
using System.Linq;

namespace DAL.XmlDAL.DAO
{
    public class NFeFilesMensagensDAO : DataAccessBase, INFeFilesMensagensDAO
    {
        public NFeFilesMensagensDAO(ContextEFNFeXml _context) : base(_context)
        {
        }


        public IQueryable<NFeFilesMensagens> GetAll()
        {
            return Contexto.NFeFilesMensagens;
        }


    }
}
