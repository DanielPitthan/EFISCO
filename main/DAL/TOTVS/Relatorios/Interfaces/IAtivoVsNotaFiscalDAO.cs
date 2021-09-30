using Models.Relatorios.Ativo;
using System.Linq;

namespace DAL.TOTVS.Relatorios.Interfaces
{
    public interface IAtivoVsNotaFiscalDAO
    {
        public IQueryable<AtivoVsNotaFiscal> All();
    }
}
