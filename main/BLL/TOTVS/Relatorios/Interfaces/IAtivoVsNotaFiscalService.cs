using Models.Relatorios.Ativo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.TOTVS.Relatorios.Interfaces
{
    public interface IAtivoVsNotaFiscalService
    {
        public Task<IList<AtivoVsNotaFiscal>> GerarRelatorioAsync(AtivoVsNotaFiscalFilter parametros);


    }
}
