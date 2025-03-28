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
using System.Xml.Serialization;

namespace XmlNFe.Nfes.Informacoes.Cana
{
    public class forDia
    {
        [XmlIgnore]
        public int Id { get; set; }
        private decimal _qtde;
        private decimal _qTotMes;
        private decimal _qTotAnt;
        private decimal _qTotGer;

        /// <summary>
        ///     ZC05 - Dia
        /// </summary>
        [XmlAttribute]
        public int dia { get; set; }

        /// <summary>
        ///     ZC06 - Quantidade
        /// </summary>
        public decimal qtde
        {
            get => _qtde;
            set => _qtde = value.Arredondar(10);
        }

        /// <summary>
        ///     ZC07 - Quantidade Total do Mês
        /// </summary>
        public decimal qTotMes
        {
            get => _qTotMes;
            set => _qTotMes = value.Arredondar(10);
        }

        /// <summary>
        ///     ZC08 - Quantidade Total Anterior
        /// </summary>
        public decimal qTotAnt
        {
            get => _qTotAnt;
            set => _qTotAnt = value.Arredondar(10);
        }

        /// <summary>
        ///     ZC09 - Quantidade Total Geral
        /// </summary>
        public decimal qTotGer
        {
            get => _qTotGer;
            set => _qTotGer = value.Arredondar(10);
        }
    }
}