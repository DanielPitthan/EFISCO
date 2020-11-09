using System;
using System.Xml.Serialization;
using DFe.Utils;

namespace XmlNFe.Nfes.Informacoes.Detalhe
{
    public class rastro
    {
        [XmlIgnore]
        public int Id { get; set; }
        private decimal _qLote;

        /// <summary>
        /// I81 - Número do Lote do produto
        /// Versão 4.0
        /// </summary>
        public string nLote { get; set; }

        /// <summary>
        /// I82 - Quantidade de produto no Lote
        /// Versão 4.0
        /// </summary>
        public decimal qLote
        {
            get { return _qLote.Arredondar(3); }
            set { _qLote = value.Arredondar(3); }
        }

        /// <summary>
        /// I83 - Data de fabricação/ Produção
        /// Versão 4.0
        /// </summary>
        [XmlIgnore]
        public DateTime? dFab { get; set; }

        [XmlElement("dFab")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ProxydFab
        {
            get { return dFab.ParaDataString(); }
            set { dFab = DateTime.Parse(value); }
        }

        /// <summary>
        /// I84 - Data de validade
        /// Versão 4.0
        /// </summary>
        [XmlIgnore]
        public DateTime? dVal { get; set; }

        [XmlElement("dVal")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ProxydVal
        {
            get { return dVal.ParaDataString(); }
            set { dVal = DateTime.Parse(value); }
        }

        /// <summary>
        /// I85 - Código de Agregação
        /// Versão 4.0
        /// </summary>
        public string cAgreg { get; set; }
    }
}