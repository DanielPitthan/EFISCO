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

        public IQueryable<FileStorange> GetAll()
        {
            return Contexto.FileStorange.AsNoTracking();
        }

        public async Task<FileStorange> GetByFileNameAndType(string fileName, string type)
        {
            FileStorange arquivos = await GetAll()
                   .AsNoTracking()
                   .Where(x => x.OriginalFileName == fileName && x.FileType == type)
                   .FirstOrDefaultAsync();
            return arquivos;
        }

        public override bool Update<TSource>(TSource _item)
        {
            FileStorange item = _item as FileStorange;

            var local = base.Contexto.Set<FileStorange>()
                                  .Local
                                  .FirstOrDefault(entry => entry.Id.Equals(item.Id));
            if (local != null)
            {
                Contexto.Entry(local).State = EntityState.Detached;
            }


            Contexto.Entry(item).State = EntityState.Modified;




            int rows = Contexto.SaveChanges();
            return rows > 0;

        }


    }
}
