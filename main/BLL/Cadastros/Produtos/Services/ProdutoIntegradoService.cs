using BLL.Cadastros.Produtos.Interfaces;
using DAL.Cadastros.Produtos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Produtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Cadastros.Produtos.Services
{
    public class ProdutoIntegradoService : IProdutoIntegradoService
    {
        private readonly IProdutoIntegradoDAO produtoIntegradoDAO;

        public ProdutoIntegradoService(IProdutoIntegradoDAO _produtoIntegradoDAO)
        {
            produtoIntegradoDAO = _produtoIntegradoDAO;
        }

        public async Task<bool> AdicionarAsync(ProdutoIntegrado produtoIntegrado)
        {
            return await produtoIntegradoDAO.AddSysnc(produtoIntegrado);
        }

        public async Task<bool> AlterarAsync(ProdutoIntegrado produtoIntegrado)
        {
            return await produtoIntegradoDAO.UpdateAsync(produtoIntegrado);
        }

        public async Task<bool> ExcluirAsync(ProdutoIntegrado produtoIntegrado)
        {
            return await produtoIntegradoDAO.DeleteAsync(produtoIntegrado);
        }

        public async Task<ProdutoIntegrado> GetAsync(ProdutoIntegrado produtoIntegrado)
        {
            ProdutoIntegrado produto = await produtoIntegradoDAO.All()
                                     .Where(p => p.Id == produtoIntegrado.Id)
                                     .SingleOrDefaultAsync();
            return produto;

        }

        public async Task<ProdutoIntegrado> ExistsProd(string codiReferencia, string cnpjFornecedor)
        {
            ProdutoIntegrado produto = await produtoIntegradoDAO.All()
                                    .Include(p => p.Produto)
                                    .Where(p => p.Produto.cProd == codiReferencia && p.CnpjFornecedor == cnpjFornecedor)
                                    .FirstOrDefaultAsync();
            return produto;
        }

        public async Task<IList<ProdutoIntegrado>> GetNaoIntegrados()
        {
            IList<ProdutoIntegrado> produtos = await produtoIntegradoDAO.All()
                                                     .Where(p => p.IntegradaoTOTVS == false)
                                                     .Include(prod => prod.Produto)
                                                     .AsNoTracking()
                                                     .ToListAsync();
            return produtos;
        }
    }
}
