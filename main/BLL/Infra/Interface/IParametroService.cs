using Models.Infra;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infra.Interface
{
    public interface IParametroService
    {
        Task<Parametro> ObterParametro(string codigo);
    }
}
