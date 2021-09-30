using BLL.Cadastros.Produtos.Interfaces;
using BLL.TOTVS.Cadastros.Interfaces;
using DAL.TOTVS.Cadastros.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Cadastros.Produtos;
using Models.TOTVS.Cadastros;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.TOTVS.Cadastros.Servicos
{
    public class ProdutoVersusFornecedorTotvsService : IProdutoVersusFornecedorTotvsService
    {
        private readonly IProdutoVersusFornecedorTotvsDAO produtoFornecedorDAO;
        private readonly IProdutoIntegradoService produtoIntegradoService;
        private readonly IFornecedorTotvsService fornecedorTotvsService;

        public ProdutoVersusFornecedorTotvsService(IProdutoVersusFornecedorTotvsDAO _produtoFornecedorDAO,
                                                   IProdutoIntegradoService _produtoIntegradoService,
                                                   IFornecedorTotvsService _fornecedorTotvsService)
        {
            produtoFornecedorDAO = _produtoFornecedorDAO;
            produtoIntegradoService = _produtoIntegradoService;
            fornecedorTotvsService = _fornecedorTotvsService;
        }

        public async Task<bool> AmarrarFornecedor(ProdutoIntegrado produtoIntegrado)
        {

            Models.TOTVS.Cadastros.ClienteFornecedor.FornecedorTotvs fornecedor = await fornecedorTotvsService.LocateByCnpjAsync(produtoIntegrado.CodigoTotvsEmpresaFilial, produtoIntegrado.CnpjFornecedor);

            List<ProdutoVersusFornecedorTotvs> produtoVsFornecedor = await produtoFornecedorDAO.All()
                        .Where(p => p.A5_FILIAL == produtoIntegrado.CodigoTotvsEmpresaFilial
                               && p.A5_FORNECE == fornecedor.A2_COD
                               && p.A5_LOJA == fornecedor.A2_LOJA
                               && p.A5_PRODUTO == produtoIntegrado.CodigoProdutoTOTVS
                               )
                        .AsNoTracking()
                        .ToListAsync();
            if (produtoVsFornecedor.Count == 0)
            {
                await produtoFornecedorDAO.AddRawSql(produtoIntegrado);
            }

            produtoIntegrado.IntegradaoTOTVS = true;
            return await produtoIntegradoService.AlterarAsync(produtoIntegrado);


        }

        public async Task<ProdutoVersusFornecedorTotvs> Get(string filial, string codigoFornecedor, string codigoReferencia)
        {
            ProdutoVersusFornecedorTotvs produtoVersusFornecedor = await produtoFornecedorDAO.All()
                 .Where(a5 => a5.A5_FILIAL == filial && a5.A5_FORNECE == codigoFornecedor && a5.A5_CODPRF == codigoReferencia)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();
            return produtoVersusFornecedor;
        }
    }
}
