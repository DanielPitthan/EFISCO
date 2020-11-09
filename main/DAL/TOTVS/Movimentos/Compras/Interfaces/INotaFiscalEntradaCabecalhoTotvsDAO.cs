using Models.TOTVS.Movimentos.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TOTVS.Movimentos.Compras.Interfaces
{
    public interface INotaFiscalEntradaCabecalhoTotvsDAO
    {
        public IQueryable<NotaFiscalEntradaCabecalhoTotvs> All();
    }
}
