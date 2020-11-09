using DAL.TOTVS.Movimentos.Compras.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.TOTVS.Movimentos.Compras;
using ContextBinds.EntityCore;
using DAL.DAOBase;
using System.Threading.Tasks;

namespace DAL.TOTVS.Movimentos.Compras.DAO
{
    public class NotaFiscalEntradaCabecalhoDAO : DataAccessBaseTOTVS, INotaFiscalEntradaCabecalhoTotvsDAO
    {
        public NotaFiscalEntradaCabecalhoDAO(ContextTOTVS _context) : base(_context)
        {
        }

        public override Task<bool> AddRawSql<TSource>(TSource item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<NotaFiscalEntradaCabecalhoTotvs> All()
        {
            return this.Contexto.SF1010;
        }
    }
}
