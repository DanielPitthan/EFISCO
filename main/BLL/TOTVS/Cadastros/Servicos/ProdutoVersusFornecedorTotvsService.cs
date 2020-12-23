using BLL.Cadastros.Produtos.Interfaces;
using BLL.TOTVS.Cadastros.Interfaces;
using DAL.TOTVS.Cadastros.DAO;
using DAL.TOTVS.Cadastros.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Produtos;
using Models.TOTVS.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TOTVS.Cadastros.Servicos
{
    public class ProdutoVersusFornecedorTotvsService : IProdutoVersusFornecedorTotvsService
    {
        private IProdutoVersusFornecedorTotvsDAO produtoFornecedorDAO;
        private IProdutoIntegradoService produtoIntegradoService;
        private IFornecedorTotvsService fornecedorTotvsService;

        public ProdutoVersusFornecedorTotvsService(IProdutoVersusFornecedorTotvsDAO _produtoFornecedorDAO,
                                                   IProdutoIntegradoService _produtoIntegradoService,
                                                   IFornecedorTotvsService _fornecedorTotvsService)
        {
            this.produtoFornecedorDAO = _produtoFornecedorDAO;
            this.produtoIntegradoService = _produtoIntegradoService;
            this.fornecedorTotvsService = _fornecedorTotvsService;
        }

        public async Task<bool> AmarrarFornecedor(ProdutoIntegrado produtoIntegrado)
        {
          
            var fornecedor = await this.fornecedorTotvsService.LocateByCnpjAsync(produtoIntegrado.CodigoTotvsEmpresaFilial,produtoIntegrado.CnpjFornecedor);

           var produtoVsFornecedor =  await this.produtoFornecedorDAO.All()
                        .Where(p => p.A5_FILIAL == produtoIntegrado.CodigoTotvsEmpresaFilial
                               && p.A5_FORNECE == fornecedor.A2_COD
                               && p.A5_LOJA ==fornecedor.A2_LOJA
                               && p.A5_PRODUTO==produtoIntegrado.CodigoProdutoTOTVS
                               )
                        .ToListAsync();
            if (produtoVsFornecedor.Count==0)
            {
                await this.produtoFornecedorDAO.AddRawSql(produtoIntegrado);
            } 

            produtoIntegrado.IntegradaoTOTVS = true;
            return await this.produtoIntegradoService.AlterarAsync(produtoIntegrado);


        }

        public async Task<ProdutoVersusFornecedorTotvs> Get(string filial, string codigoFornecedor, string codigoReferencia)
        {
            var produtoVersusFornecedor = await produtoFornecedorDAO.All()
                 .Where(a5 => a5.A5_FILIAL == filial && a5.A5_FORNECE == codigoFornecedor && a5.A5_CODPRF == codigoReferencia)
                 .SingleOrDefaultAsync();
            return produtoVersusFornecedor;
        }
    }
}
