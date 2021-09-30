using System.Threading.Tasks;

namespace DAL.DAOBase
{
    public interface IDataAccessBase
    {
        Task<bool> AddSysnc<TSource>(TSource item);
        Task<bool> UpdateAsync<TSource>(TSource item);
        Task<bool> DeleteAsync<TSource>(TSource item);

        bool Add<TSource>(TSource item);
        bool Update<TSource>(TSource item);
        bool Delete<TSource>(TSource item);


    }
}
