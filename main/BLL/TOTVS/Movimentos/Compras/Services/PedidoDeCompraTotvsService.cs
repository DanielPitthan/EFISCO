using BLL.TOTVS.Movimentos.Compras.Interfaces;
using DAL.TOTVS.Movimentos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.TOTVS.Movimentos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.TOTVS.Movimentos.Compras.Services
{
    public class PedidoDeCompraTotvsService : IPedidoDeCompraTotvsService
    {
        private readonly IPedidoDeCompraTotvsDAO pedidoTotvsDAO;

        public PedidoDeCompraTotvsService(IPedidoDeCompraTotvsDAO _pedidoTotvsDao)
        {
            pedidoTotvsDAO = _pedidoTotvsDao;
        }

        public async Task<IList<PedidoDeCompraTotvs>> GetByPedido(string pedido)
        {
            IList<PedidoDeCompraTotvs> pedidos = await pedidoTotvsDAO.All()
                                             .Where(x => x.C7_NUM.Contains(pedido))
                                             .ToListAsync();
            return pedidos;
        }
    }
}
