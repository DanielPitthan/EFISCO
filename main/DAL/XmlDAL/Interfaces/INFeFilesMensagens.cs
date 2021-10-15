using DAL.DAOBase;
using DAL.DAOBaseNfeXml;
using Models.NFe;
using System.Linq;

namespace DAL.XmlDAL.Interfaces
{
    public interface INFeFilesMensagensDAO : IDataAccessBaseNfeXml
    {
        public IQueryable<NFeFilesMensagens> GetAll();
    }
}
