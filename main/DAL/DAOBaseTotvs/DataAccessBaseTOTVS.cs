using ContextBinds.EntityCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOBase
{
    public abstract class DataAccessBaseTOTVS: IDataAccessBaseTOTVS
    {
        public ContextTOTVS Contexto { get; set; }

        public DataAccessBaseTOTVS(ContextTOTVS _context)
        {
            this.Contexto = _context;
        }

        public virtual async Task<bool> AddSysnc<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Added;
            var rows = await this.Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rows > 0;
        }
      
        public virtual async Task<bool> UpdateAsync<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Modified;
            var rows = this.Contexto.SaveChanges();            
            return rows > 0;
        }

        public virtual async Task<bool> DeleteAsync<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Deleted;
            var rows = await this.Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rows > 0;
        }

        public abstract Task<bool> AddRawSql<TSource>(TSource item);
    }
}
