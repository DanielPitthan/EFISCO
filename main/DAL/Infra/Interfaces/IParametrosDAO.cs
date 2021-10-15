using Microsoft.EntityFrameworkCore;
using Models.Infra;
using System.Threading.Tasks;

namespace DAL.Infra.Interfaces
{
    public interface IParametrosDAO
    {
        DbSet<Parametro> GetAll();
        Task<bool> AddSysnc<TSource>(TSource item);
        Task<bool> UpdateAsync<TSource>(TSource item);
        Task<bool> DeleteAsync<TSource>(TSource item);
    }
}
