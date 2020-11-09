using ContextBinds.EntityCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOBaseNfeXml
{
    public abstract class DataAccessBaseNfeXml : IDataAccessBaseNfeXml
    {
        public ContextEFNFeXml Contexto { get; set; }

        public DataAccessBaseNfeXml(ContextEFNFeXml _context)
        {
            this.Contexto = _context;
        }

        public virtual async Task<bool> AddSysnc<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Added;
            try
            {

                var rows = await this.Contexto.SaveChangesAsync().ConfigureAwait(false);
                return rows > 0;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Modified;

            try
            {
                
                var rows = await this.Contexto.SaveChangesAsync().ConfigureAwait(false);
                return rows > 0;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public virtual async Task<bool> DeleteAsync<TSource>(TSource item)
        {
            this.Contexto.Entry(item).State = EntityState.Deleted;
            try
            {

                var rows = await this.Contexto.SaveChangesAsync().ConfigureAwait(false);
                return rows > 0;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
