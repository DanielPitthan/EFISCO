using ContextBinds.EntityCore;
using DAL.DAOBase;
using DAL.TOTVS.Movimentos.Interfaces;
using Models.TOTVS.Movimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TOTVS.Movimentos.DAO
{
    public class PedidoDeComprasTotvsDAO : DataAccessBaseTOTVS, IPedidoDeCompraTotvsDAO
    {
        public PedidoDeComprasTotvsDAO(ContextTOTVS _context) : base(_context)
        {
        }

        public override Task<bool> AddRawSql<TSource>(TSource item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PedidoDeCompraTotvs> All()
        {
            return this.Contexto.SC7010;
        }
    }
}
