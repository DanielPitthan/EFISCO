using DAL.DAOBase;
using Microsoft.EntityFrameworkCore;
using Models.NFe;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.XmlDAL.Interfaces
{
    public interface INFeFilesMensagensDAO : IDataAccessBase
    {
        public IQueryable<NFeFilesMensagens> GetAll();
    }
}
