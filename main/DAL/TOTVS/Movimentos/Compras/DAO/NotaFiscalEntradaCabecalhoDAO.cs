using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.TOTVS.Movimentos.Compras.Interfaces;
using Models.TOTVS.Movimentos.Compras;
using System;
using System.Linq;
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
            return Contexto.SF1010;
        }
    }
}
