using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.XmlDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.NFe;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.XmlDAL.DAO
{
    public class NFeFilesDAO : DataAccessBase, INFeFilesDAO
    {
        public NFeFilesDAO(ContextEFNFeXml _context) : base(_context)
        {
        }

        public IQueryable<NFeFiles> GetAll()
        {
            return Contexto.NFeFiles;
        }

        public override async Task<bool> UpdateAsync<TSource>(TSource item)
        {
            NFeFiles nfeFile = item as NFeFiles;

            NFeFiles localContext = Contexto.Set<NFeFiles>().Local.FirstOrDefault(entry => entry.Id.Equals(nfeFile.Id));
            if (localContext != null)
            {
                Contexto.Entry(localContext).State = EntityState.Detached;
            }
            Contexto.Entry(nfeFile).State = EntityState.Modified;
            int rowsAffecteds = await Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rowsAffecteds > 0;
        }


        public override bool Update<TSource>(TSource item)
        {
            NFeFiles nfeFile = item as NFeFiles;

            NFeFiles localContext = Contexto.Set<NFeFiles>().Local.FirstOrDefault(entry => entry.Id.Equals(nfeFile.Id));
            if (localContext != null)
            {
                Contexto.Entry(localContext).State = EntityState.Detached;
            }
            Contexto.Entry(nfeFile).State = EntityState.Modified;
            int rowsAffecteds = Contexto.SaveChanges();
            return rowsAffecteds > 0;
        }
    }
}
