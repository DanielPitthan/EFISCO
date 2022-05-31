using Models.TOTVS.Movimentos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.TOTVS.Movimentos.Compras.Interfaces
{
    public interface IPedidoDeCompraTotvsService
    {
        public Task<IList<PedidoDeCompraTotvs>> GetByPedido(string pedido);
        public Task<IList<PedidoDeCompraTotvs>> BuscaEmAberto(string filial, string fornecedor, string a5_PRODUTO);
    }
}
