using System;

namespace Models.Relatorios.Ativo
{
    public class AtivoVsNotaFiscal
    {
        public string Filial { get; set; }
        public string Grupo { get; set; }
        public string Ativo { get; set; }
        public string Item { get; set; }
        public DateTime? Aquisicao { get; set; }
        public string Descricao { get; set; }
        public string Chapa { get; set; }
        public string Fornecedor { get; set; }
        public string Loja { get; set; }
        public string NF { get; set; }
        public string Serie { get; set; }
        public string ItemNF { get; set; }
        public double Quantidade { get; set; }
        public double ValorDeAquisicao { get; set; }
        public double? IcmsdoItem { get; set; }
        public string Produto { get; set; }
        public string CentroCusto { get; set; }
        public double ValorOriginal { get; set; }
        public string ContaContabil { get; set; }
        public DateTime? Emissao { get; set; }
        public string CodigoAtivo { get; set; }
        public string Descrica { get; set; }
        public double? VlrCiapApropriado { get; set; }
        public string CFOP { get; set; }
    }
}
