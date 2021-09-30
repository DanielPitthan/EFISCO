using ContextBinds.EntityCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.DAOBaseNfeXml
{
    public abstract class DataAccessBaseNfeXml : IDataAccessBaseNfeXml
    {
        public ContextEFNFeXml Contexto { get; set; }

        public DataAccessBaseNfeXml(ContextEFNFeXml _context)
        {
            Contexto = _context;
        }

        public virtual async Task<bool> AddSysnc<TSource>(TSource item)
        {

            Contexto.Entry(item).State = EntityState.Added;
            try
            {

                int rows = await Contexto.SaveChangesAsync().ConfigureAwait(false);
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
            Contexto.Entry(item).State = EntityState.Modified;

            try
            {

                int rows = await Contexto.SaveChangesAsync().ConfigureAwait(false);
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
            Contexto.Entry(item).State = EntityState.Deleted;
            try
            {

                int rows = await Contexto.SaveChangesAsync().ConfigureAwait(true);
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
