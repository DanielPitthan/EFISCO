using Models.Interfaces;
using System.Threading.Tasks;

namespace DAL.DAOBaseNfeXml
{
    public interface IDataAccessBaseNfeXml
    {
        public Task<bool> AddSysnc<TSource>(TSource item);
        public Task<bool> UpdateAsync<TSource>(TSource item);
        public abstract bool Update<TSource>(TSource item);
        public Task<bool> DeleteAsync<TSource>(TSource item);

        public TEntity GetById<TEntity>( int id) where TEntity : class, IIdentifier;
    }
}
