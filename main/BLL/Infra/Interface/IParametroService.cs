using Models.Infra;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Infra.Interface
{
    public interface IParametroService
    {
        Task<Parametro> ObterParametro(string codigo);
        Task<bool> MergeAsync(Parametro parametro);
        Task<bool> ExcluirAsync(Parametro parametro);
        Task<IList<Parametro>> List();
    }
}
