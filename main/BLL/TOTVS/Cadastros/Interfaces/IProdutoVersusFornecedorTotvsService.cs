using Models.Cadastros.Produtos;
using Models.TOTVS.Cadastros;
using System.Threading.Tasks;

namespace BLL.TOTVS.Cadastros.Interfaces
{
    public interface IProdutoVersusFornecedorTotvsService
    {
        /// <summary>
        /// Obtem a amarracao Produto x Fornecedor
        /// </summary>
        /// <param name="filial"></param>
        /// <param name="codigoFornecedor"></param>
        /// <param name="codigoReferencia"></param>
        /// <returns></returns>
        Task<ProdutoVersusFornecedorTotvs> Get(string filial, string codigoFornecedor, string codigoReferencia);

        Task<bool> AmarrarFornecedor(ProdutoIntegrado produtoIntegrado);
    }
}
