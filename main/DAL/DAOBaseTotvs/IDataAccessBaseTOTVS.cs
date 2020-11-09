using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOBase
{
    public interface IDataAccessBaseTOTVS
    {
        Task<bool> AddSysnc<TSource>(TSource item);
        Task<bool> UpdateAsync<TSource>(TSource item);
        Task<bool> DeleteAsync<TSource>(TSource item);
        Task<bool> AddRawSql<TSource>(TSource item);


    }
}
