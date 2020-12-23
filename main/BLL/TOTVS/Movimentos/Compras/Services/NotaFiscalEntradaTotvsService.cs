using BLL.TOTVS.Movimentos.Compras.Interfaces;
using DAL.TOTVS.Movimentos.Compras.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.TOTVS.Movimentos.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TOTVS.Movimentos.Compras.Services
{
    public class NotaFiscalEntradaTotvsService : INotaFiscalEntradaTotvsService
    {
        private INotaFiscalEntradaCabecalhoTotvsDAO notaFiscalEntradaDAO;

        public NotaFiscalEntradaTotvsService(INotaFiscalEntradaCabecalhoTotvsDAO _notaFiscalEntradaDAO)
        {
            this.notaFiscalEntradaDAO = _notaFiscalEntradaDAO;
        }

        public async Task<NotaFiscalEntradaCabecalhoTotvs> GetByChave(string chave)
        {
            NotaFiscalEntradaCabecalhoTotvs nota = await this.notaFiscalEntradaDAO.All()
                  .Where(x => x.F1_CHVNFE.Equals(chave) && x.D_E_L_E_T_=="")
                  .FirstOrDefaultAsync();
            return nota;
        }
    }
}
