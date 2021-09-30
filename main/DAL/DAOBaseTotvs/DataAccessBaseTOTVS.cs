using ContextBinds.EntityCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.DAOBase
{
    public abstract class DataAccessBaseTOTVS : IDataAccessBaseTOTVS
    {
        public ContextTOTVS Contexto { get; set; }

        public DataAccessBaseTOTVS(ContextTOTVS _context)
        {
            Contexto = _context;
        }

        public virtual async Task<bool> AddSysnc<TSource>(TSource item)
        {
            Contexto.Entry(item).State = EntityState.Added;
            int rows = await Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rows > 0;
        }

        public virtual async Task<bool> UpdateAsync<TSource>(TSource item)
        {
            Contexto.Entry(item).State = EntityState.Modified;
            int rows = Contexto.SaveChanges();
            return rows > 0;
        }

        public virtual async Task<bool> DeleteAsync<TSource>(TSource item)
        {
            Contexto.Entry(item).State = EntityState.Deleted;
            int rows = await Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rows > 0;
        }

        public abstract Task<bool> AddRawSql<TSource>(TSource item);
    }
}
