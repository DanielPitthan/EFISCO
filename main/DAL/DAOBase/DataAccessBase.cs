using ContextBinds.EntityCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOBase
{
    public abstract class DataAccessBase:IDataAccessBase
    {
        public ContextEF Contexto { get; set; }

        public DataAccessBase(ContextEF _context)
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
            var rows = await this.Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rows > 0;
        }

        public virtual async Task<bool> DeleteAsync<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Deleted;
            var rows = await this.Contexto.SaveChangesAsync().ConfigureAwait(false);
            return rows > 0;
        }

        public virtual bool Add<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Added;
            var rows = this.Contexto.SaveChanges();
            return rows > 0;
        }

        public virtual bool Update<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Modified;
            var rows = this.Contexto.SaveChanges();
            return rows > 0;
        }

        public virtual  bool Delete<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Deleted;
            var rows = this.Contexto.SaveChanges();
            return rows > 0;
        }
    }
}
