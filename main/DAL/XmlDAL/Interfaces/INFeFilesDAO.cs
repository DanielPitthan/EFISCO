using DAL.DAOBase;
using Models.NFe;
using System.Linq;

namespace DAL.XmlDAL.Interfaces
{
    public interface INFeFilesDAO : IDataAccessBase
    {
        public IQueryable<NFeFiles> GetAll();
    }
}
