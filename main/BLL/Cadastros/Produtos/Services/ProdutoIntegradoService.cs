using BLL.Cadastros.Produtos.Interfaces;
using DAL.Cadastros.Produtos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cadastros.Produtos.Services
{
    public class ProdutoIntegradoService:IProdutoIntegradoService
    {
        private IProdutoIntegradoDAO produtoIntegradoDAO;

        public ProdutoIntegradoService(IProdutoIntegradoDAO _produtoIntegradoDAO)
        {
            this.produtoIntegradoDAO = _produtoIntegradoDAO;
        }

        public async Task<bool> AdicionarAsync(ProdutoIntegrado produtoIntegrado)
        {
            return await this.produtoIntegradoDAO.AddSysnc(produtoIntegrado);
        }

        public async Task<bool> AlterarAsync(ProdutoIntegrado produtoIntegrado)
        {
            return await this.produtoIntegradoDAO.UpdateAsync(produtoIntegrado); 
        }

        public async Task<bool> ExcluirAsync(ProdutoIntegrado produtoIntegrado)
        {
            return await this.produtoIntegradoDAO.DeleteAsync(produtoIntegrado);
        }

        public async Task<ProdutoIntegrado> GetAsync(ProdutoIntegrado produtoIntegrado)
        {
            var produto = await this.produtoIntegradoDAO.All()
                                     .Where(p => p.Id == produtoIntegrado.Id)
                                     .SingleOrDefaultAsync();
            return produto;

        }

        public async Task<ProdutoIntegrado> ExistsProd(string codiReferencia, string cnpjFornecedor)
        {
            var produto = await this.produtoIntegradoDAO.All()
                                    .Include(p=> p.Produto)                                    
                                    .Where(p =>p.Produto.cProd== codiReferencia && p.CnpjFornecedor == cnpjFornecedor)
                                    .FirstOrDefaultAsync();
            return produto;
        }

        public async Task<IList<ProdutoIntegrado>> GetNaoIntegrados()
        {
            IList<ProdutoIntegrado> produtos = await this.produtoIntegradoDAO.All()
                                                     .Where(p => p.IntegradaoTOTVS == false)
                                                     .Include(prod=> prod.Produto)
                                                     .AsNoTracking()
                                                     .ToListAsync();
            return produtos;
        }
    }
}
