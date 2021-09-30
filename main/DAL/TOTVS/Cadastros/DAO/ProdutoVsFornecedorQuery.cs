using Models.Cadastros.Produtos;

namespace DAL.TOTVS.Cadastros.DAO
{
    public static class ProdutoVsFornecedorQuery
    {
        public static string Insert(ProdutoIntegrado produtoIntegrado)
        {
            return $@" exec ProdutoVsFornecedor '{produtoIntegrado.CodigoTotvsEmpresaFilial}',
                                                '{produtoIntegrado.CnpjFornecedor}',
                                                '{produtoIntegrado.CodigoProdutoTOTVS}',
                                                '{produtoIntegrado.Produto.cProd}'";
        }
    }
}
