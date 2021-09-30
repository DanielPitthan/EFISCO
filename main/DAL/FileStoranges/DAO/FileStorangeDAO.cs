using ContextBinds.EntityCore;
using DAL.DAOBaseNfeXml;
using DAL.FileStoranges.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.FileStoranges;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.FileStoranges.DAO
{
    public class FileStorangeDAO : DataAccessBaseNfeXml, IFileStorangeDAO
    {
        public FileStorangeDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public DbSet<FileStorange> GetAll()
        {
            return Contexto.FileStorange;
        }

        public async Task<FileStorange> GetByFileNameAndType(string fileName, string type)
        {
            FileStorange arquivos = await GetAll()
                   .AsNoTracking()
                   .Where(x => x.OriginalFileName == fileName && x.FileType == type)
                   .FirstOrDefaultAsync();
            return arquivos;
        }
    }
}
