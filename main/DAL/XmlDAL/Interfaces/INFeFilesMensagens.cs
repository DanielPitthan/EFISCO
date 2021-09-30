using DAL.DAOBase;
using Models.NFe;
using System.Linq;

namespace DAL.XmlDAL.Interfaces
{
    public interface INFeFilesMensagensDAO : IDataAccessBase
    {
        public IQueryable<NFeFilesMensagens> GetAll();
    }
}
