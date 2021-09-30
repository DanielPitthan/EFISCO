using System.Xml.Serialization;

namespace Shared.XmlNFe.Nfes.Informacoes.InfRespTec
{
    public class infRespTec
    {
        [XmlIgnore]
        public int Id { get; set; }
        public string CNPJ { get; set; }

        public string xContato { get; set; }

        public string email { get; set; }

        public string fone { get; set; }

        [XmlIgnore]
        public int? idCSRT { get; set; }

        [XmlElement(ElementName = "idCSRT")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ProxyidCSRT
        {
            get
            {
                if (idCSRT == null)
                {
                    return null;
                }

                return idCSRT.Value.ToString("D2");
            }
            set
            {
                if (value == null)
                {
                    idCSRT = null;
                    return;
                }
                idCSRT = int.Parse(value);
            }
        }

        public string hashCSRT { get; set; }
    }
}
