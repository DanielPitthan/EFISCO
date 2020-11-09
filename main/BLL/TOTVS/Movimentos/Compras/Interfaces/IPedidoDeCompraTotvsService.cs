using Models.TOTVS.Movimentos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TOTVS.Movimentos.Compras.Interfaces
{
    public interface IPedidoDeCompraTotvsService
    {
        public Task<IList<PedidoDeCompraTotvs>> GetByPedido(string pedido);
    }
}
