using Models.TOTVS.Movimentos.Compras;
using System.Linq;

namespace DAL.TOTVS.Movimentos.Compras.Interfaces
{
    public interface INotaFiscalEntradaCabecalhoTotvsDAO
    {
        public IQueryable<NotaFiscalEntradaCabecalhoTotvs> All();
    }
}
