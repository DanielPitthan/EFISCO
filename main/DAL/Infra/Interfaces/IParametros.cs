using DAL.DAOBase;
using Microsoft.EntityFrameworkCore;
using Models.Infra;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infra.Interfaces
{
    public interface IParametros
    {
        DbSet<Parametro> GetAll();
        Task<bool> AddSysnc<TSource>(TSource item);
        Task<bool> UpdateAsync<TSource>(TSource item);
        Task<bool> DeleteAsync<TSource>(TSource item);
    }
}
