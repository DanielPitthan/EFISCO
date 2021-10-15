using DAL.DAOBaseNfeXml;
using Microsoft.EntityFrameworkCore;
using Models.FileStoranges;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.FileStoranges.Interfaces
{
    public interface IFileStorangeDAO : IDataAccessBaseNfeXml
    {
        IQueryable<FileStorange> GetAll();
        Task<FileStorange> GetByFileNameAndType(string fileName, string type);


    }
}
