using DAL.DAOBase;
using Models.TOTVS.Movimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.TOTVS.Movimentos.Interfaces
{
    public interface IPedidoDeCompraTotvsDAO:IDataAccessBaseTOTVS
    {
        public IQueryable<PedidoDeCompraTotvs> All();
    }
}
