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

namespace XmlNFe.Nfes.Informacoes.Transporte
{
    public class vol
    {
        [XmlIgnore]
        public int Id { get; set; }
        private decimal? _pesoL;
        private decimal? _pesoB;

        /// <summary>
        ///     X27 - Quantidade de volumes transportados
        /// </summary>
        public int? qVol { get; set; }

        /// <summary>
        ///     X28 - Espécie dos volumes transportados
        /// </summary>
        public string esp { get; set; }

        /// <summary>
        ///     X29 - Marca dos volumes transportados
        /// </summary>
        public string marca { get; set; }

        /// <summary>
        ///     X30 - Numeração dos volumes transportados
        /// </summary>
        public string nVol { get; set; }

        /// <summary>
        ///     X31 - Peso Líquido (em kg)
        /// </summary>
        public decimal? pesoL
        {
            get => _pesoL.Arredondar(3);
            set => _pesoL = value.Arredondar(3);
        }

        /// <summary>
        ///     X32 - Peso Bruto (em kg)
        /// </summary>
        public decimal? pesoB
        {
            get => _pesoB.Arredondar(3);
            set => _pesoB = value.Arredondar(3);
        }

        /// <summary>
        ///     X33 - Grupo Lacres
        ///     <para>Ocorrência: 0-5000</para>
        /// </summary>
        [XmlElement("lacres")]
        public List<lacres> lacres { get; set; }

        public bool ShouldSerializeqVol()
        {
            return qVol.HasValue;
        }

        public bool ShouldSerializepesoL()
        {
            return pesoL.HasValue;
        }

        public bool ShouldSerializepesoB()
        {
            return pesoB.HasValue;
        }
    }
}