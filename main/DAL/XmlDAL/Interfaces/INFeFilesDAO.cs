using DAL.DAOBase;
using DAL.DAOBaseNfeXml;
using Models.NFe;
using System.Linq;

namespace DAL.XmlDAL.Interfaces
{
    public interface INFeFilesDAO : IDataAccessBaseNfeXml
    {
        public IQueryable<NFeFiles> GetAll();
    }
}
