using Models.Empresas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Empresas.Interfaces
{
    public interface IEmpresaService
    {
        Task<IList<Empresa>> ListAllAsync();
        Task<Empresa> GetByIdAsync(int id);
        Task<Empresa> GetByCnpjsync(string cnpj);
    }
}
