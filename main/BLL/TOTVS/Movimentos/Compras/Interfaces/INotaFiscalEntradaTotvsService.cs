﻿using Models.TOTVS.Movimentos.Compras;
using System.Threading.Tasks;

namespace BLL.TOTVS.Movimentos.Compras.Interfaces
{
    public interface INotaFiscalEntradaTotvsService
    {
        public Task<NotaFiscalEntradaCabecalhoTotvs> GetByChave(string chave);
    }
}
