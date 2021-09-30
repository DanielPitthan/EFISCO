using System.Xml.Serialization;
using XmlNFe.Nfes.Informacoes.Identificacao.Tipos;

namespace XmlNFe.Nfes.Informacoes.Pagamento
{
    public class detPag
    {
        [XmlIgnore]
        public int Id { get; set; }
        private decimal _vPag;


        public IndicadorPagamentoDetalhePagamento? indPag { get; set; }

        public bool indPagSpecified => indPag.HasValue;

        /// <summary>
        /// YA02 - Forma de pagamento
        /// </summary>
        public FormaPagamento tPag { get; set; }

        public decimal vPag
        {
            get => _vPag.Arredondar(2);
            set => _vPag = value.Arredondar(2);
        }

        public card card { get; set; }


    }
}