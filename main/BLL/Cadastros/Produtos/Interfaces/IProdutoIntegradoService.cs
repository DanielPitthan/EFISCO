using Models.Cadastros.Produtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Cadastros.Produtos.Interfaces
{
    public interface IProdutoIntegradoService
    {
        public Task<bool> AdicionarAsync(ProdutoIntegrado produtoIntegrado);
        public Task<bool> AlterarAsync(ProdutoIntegrado produtoIntegrado);
        public Task<bool> ExcluirAsync(ProdutoIntegrado produtoIntegrado);
        public Task<ProdutoIntegrado> GetAsync(ProdutoIntegrado produtoIntegrado);
        public Task<ProdutoIntegrado> ExistsProd(string codiReferencia, string cnpjFornecedor);
        public Task<IList<ProdutoIntegrado>> GetNaoIntegrados();
    }
}
