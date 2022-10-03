using Microsoft.EntityFrameworkCore;
using Models.Infra;
using Models.Interfaces;
using System.Threading.Tasks;

namespace DAL.Infra.Interfaces
{
    public interface IParametrosDAO
    {
        DbSet<Parametro> GetAll();
        Task<bool> AddSysnc<TSource>(TSource item);
        Task<bool> UpdateAsync<TSource>(TSource item) where TSource : class, IIdentifier;
        Task<bool> DeleteAsync<TSource>(TSource item);
    }
}
