using BLL.TOTVS.Movimentos.Compras.Interfaces;
using DAL.TOTVS.Movimentos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.TOTVS.Movimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TOTVS.Movimentos.Compras.Services
{
   public class PedidoDeCompraTotvsService:IPedidoDeCompraTotvsService
    {
        private IPedidoDeCompraTotvsDAO pedidoTotvsDAO;

        public PedidoDeCompraTotvsService(IPedidoDeCompraTotvsDAO _pedidoTotvsDao)
        {
            this.pedidoTotvsDAO = _pedidoTotvsDao;
        }

        public async Task<IList<PedidoDeCompraTotvs>> GetByPedido(string pedido)
        {
            IList<PedidoDeCompraTotvs> pedidos = await this.pedidoTotvsDAO.All()
                                             .Where(x => x.C7_NUM.Contains(pedido))
                                             .ToListAsync();
            return pedidos;
        }
    }
}
