using System.Threading.Tasks;

namespace DAL.DAOBaseNfeXml
{
    public interface IDataAccessBaseNfeXml
    {
        Task<bool> AddSysnc<TSource>(TSource item);
        Task<bool> UpdateAsync<TSource>(TSource item);
        Task<bool> DeleteAsync<TSource>(TSource item);

    }
}
