using DAL.DAOBase;
using Microsoft.EntityFrameworkCore;
using Models.NFe;
using System.Linq;

namespace DAL.XmlDAL.Interfaces
{
    public interface INFeFilesDAO:IDataAccessBase
    {
        public IQueryable<NFeFiles> GetAll();
    }
}
