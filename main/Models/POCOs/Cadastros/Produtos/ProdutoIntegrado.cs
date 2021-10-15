using Models.Interfaces;
using System;
using XmlNFe.Nfes.Informacoes.Detalhe;

namespace Models.Cadastros.Produtos
{
    public class ProdutoIntegrado: IIdentifier
    {
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public bool IntegradaoTOTVS { get; set; }
        public prod Produto { get; set; }
        public string CnpjFornecedor { get; set; }
        public string CodigoTotvsEmpresaFilial { get; set; }
        public string CodigoProdutoTOTVS { get; set; }
    }
}
