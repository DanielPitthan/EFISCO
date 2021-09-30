using DAL.DAOBase;
using Models.TOTVS.Movimentos;
using System.Linq;

namespace DAL.TOTVS.Movimentos.Interfaces
{
    public interface IPedidoDeCompraTotvsDAO : IDataAccessBaseTOTVS
    {
        public IQueryable<PedidoDeCompraTotvs> All();
    }
}
