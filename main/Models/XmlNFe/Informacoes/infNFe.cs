﻿/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
using System.Collections.Generic;
using System.Xml.Serialization;
using XmlNFe.Nfes.Informacoes.Cana;
using XmlNFe.Nfes.Informacoes.Cobranca;
using XmlNFe.Nfes.Informacoes.Destinatario;
using XmlNFe.Nfes.Informacoes.Detalhe;
using XmlNFe.Nfes.Informacoes.Emitente;
using XmlNFe.Nfes.Informacoes.Identificacao;
using XmlNFe.Nfes.Informacoes.Observacoes;
using XmlNFe.Nfes.Informacoes.Pagamento;
using XmlNFe.Nfes.Informacoes.Total;
using XmlNFe.Nfes.Informacoes.Transporte;
using Shared.XmlNFe.Nfes.Informacoes.InfRespTec;
using System.ComponentModel.DataAnnotations;

namespace XmlNFe.Nfes.Informacoes
{
    public class infNFe
    {
        public infNFe()
        {
            det = new List<det>();
        }
        [Key]
        public int infNFeId { get; set; }

        /// <summary>
        ///     A02 - Versão do leiaute da NFe (2.0, 3.1, etc)
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     A03 - Identificador da TAG a ser assinada
        ///     <para>informar a chave de acesso da NF-e precedida do literal "NFe", acrescentada a validação do formato (v2.0).</para>
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        ///     B01 - Identificação da NF-e
        /// </summary>
        public virtual ide ide { get; set; }

        /// <summary>
        ///     C01 - Grupo de identificação do emitente da NF-e
        /// </summary>
        public virtual emit emit { get; set; }

        /// <summary>
        ///     D01 - Informações do fisco emitente (uso exclusivo do fisco)
        /// </summary>
        //public virtual avulsa avulsa { get; set; }

        /// <summary>
        ///     E01 - Grupo de identificação do Destinatário da NF-e
        /// </summary>
        public virtual dest dest { get; set; }

        /// <summary>
        ///     F01 - Identificação do Local de retirada
        /// </summary>
        public virtual retirada retirada { get; set; }

        /// <summary>
        ///     G01 - Identificação do Local de entrega
        /// </summary>
        public virtual entrega entrega { get; set; }

        /// <summary>
        ///     GA01 - Pessoas autorizadas a acessar o XML da NF-e
        ///     <para>Ocorrência: 0-10</para>
        /// </summary>
        [XmlElement("autXML")]
        public virtual List<autXML> autXML { get; set; }

        /// <summary>
        ///     H01 - Dados dos detalhes da NF-e
        ///     <para>Ocorrência: 1-990</para>
        /// </summary>
        [XmlElement("det")]
        public virtual List<det> det { get; set; }

        /// <summary>
        ///     W01 - Grupo Totais da NF-e
        /// </summary>
        public virtual total total { get; set; }

        /// <summary>
        ///     X01 - Grupo Informações do Transporte
        /// </summary>
        public virtual transp transp { get; set; }

        /// <summary>
        ///     Y01 - Grupo Cobrança
        /// </summary>
        public virtual cobr cobr { get; set; }

        /// <summary>
        ///     YA01 - Grupo de Formas de Pagamento
        ///     <para>Ocorrência: 0-100</para>
        /// </summary>
        [XmlElement("pag")]
        public virtual List<pag> pag { get; set; }

        /// <summary>
        ///     Z01 - Grupo de Informações Adicionais
        /// </summary>
        public virtual infAdic infAdic { get; set; }

        /// <summary>
        ///     ZA01 - Grupo Exportação
        /// </summary>
       // public virtual exporta exporta { get; set; }

        /// <summary>
        ///     ZB01 - Grupo Compra
        /// </summary>
        public virtual compra compra { get; set; }

        /// <summary>
        ///     ZC01 - Grupo Cana
        /// </summary>
       // public virtual cana cana { get; set; }

        //public virtual infRespTec infRespTec { get; set; }
    }
}