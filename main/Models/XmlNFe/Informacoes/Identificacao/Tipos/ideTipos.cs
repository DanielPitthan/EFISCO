/********************************************************************************/
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

using System.ComponentModel;
using System.Xml.Serialization;

namespace XmlNFe.Nfes.Informacoes.Identificacao.Tipos
{
    /// <summary>
    ///     Indicador da forma de pagamento
    ///     <para>0 – Pagamento à vista;</para>
    ///     <para>1 – Pagamento à prazo;</para>
    ///     <para>2 - Outros.</para>
    /// </summary>
    public enum IndicadorPagamento
    {
        /// <summary>
        /// 0 – Pagamento à vista
        /// </summary>
        [Description("Pagamento à vista")]
        [XmlEnum("0")]
        AVista = 0,

        /// <summary>
        /// 1 – Pagamento à prazo
        /// </summary>
        [Description("Pagamento à prazo")]
        [XmlEnum("1")]
        APrazo = 1,

        /// <summary>
        /// 2 - Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("2")]
        Outras = 2
    }

    /// <summary>
    ///     Indicador da Forma de Pagamento
    ///     <para>0 – Pagamento à vista;</para>
    ///     <para>1 – Pagamento à prazo;</para>
    /// </summary>
    public enum IndicadorPagamentoDetalhePagamento
    {
        /// <summary>
        /// 0 – Pagamento à vista
        /// </summary>
        [Description("Pagamento à vista")]
        [XmlEnum("0")]
        DetPgVista = 0,

        /// <summary>
        /// 1 – Pagamento à prazo
        /// </summary>
        [Description("Pagamento à prazo")]
        [XmlEnum("1")]
        DetPgPrazo = 1,
    }

    /// <summary>
    ///     Tipo do Documento Fiscal
    ///     <para>0 - Entrada;</para>
    ///     <para>1 - Saída</para>
    /// </summary>
    public enum TipoNFe
    {
        /// <summary>
        /// 0 - Entrada
        /// </summary>
        [Description("Entrada")]
        [XmlEnum("0")]
        Entrada = 0,

        /// <summary>
        /// 1 - Saída
        /// </summary>
        [Description("Saída")]
        [XmlEnum("1")]
        Saida = 1
    }

    /// <summary>
    ///     Identificador de Local de destino da operação
    ///     <para>1 - Operação interna;</para>
    ///     <para>2 - Operação interestadual;</para>
    ///     <para>3 - Operação com exterior.</para>
    /// </summary>
    public enum DestinoOperacao
    {
        /// <summary>
        /// 1 - Operação interna
        /// </summary>
        [Description("Operação interna")]
        [XmlEnum("1")]
        Interna = 1,

        /// <summary>
        /// 2 - Operação interestadual
        /// </summary>
        [Description("Operação interestadual")]
        [XmlEnum("2")]
        Interestadual = 2,

        /// <summary>
        /// 3 - Operação com exterior
        /// </summary>
        [Description("Operação com exterior")]
        [XmlEnum("3")]
        Exterior = 3
    }

    /// <summary>
    ///     Formato de impressão do DANFE
    ///     <para>0 - Sem DANFE;</para>
    ///     <para>1 - DANFe Retrato;</para>
    ///     <para>2 - DANFe Paisagem;</para>
    ///     <para>3 - DANFe Simplificado;</para>
    ///     <para>4 - DANFe NFC-e;</para>
    ///     <para>5 - DANFe NFC-e em mensagem eletrônica</para>
    /// </summary>
    public enum TipoImpressao
    {
        /// <summary>
        /// 0 - Sem DANFE
        /// </summary>
        [Description("Sem DANFE")]
        [XmlEnum("0")]
        SemGeracao = 0,

        /// <summary>
        /// >1 - DANFe Retrato
        /// </summary>
        [Description("DANFe Retrato")]
        [XmlEnum("1")]
        Retrato = 1,

        /// <summary>
        /// 2 - DANFe Paisagem
        /// </summary>
        [Description("DANFe Paisagem")]
        [XmlEnum("2")]
        Paisagem = 2,

        /// <summary>
        /// 3 - DANFe Simplificado
        /// </summary>
        [Description("DANFe Simplificado")]
        [XmlEnum("3")]
        Simplificado = 3,

        /// <summary>
        /// 4 - DANFe NFC-e
        /// </summary>
        [Description("DANFe NFC-e")]
        [XmlEnum("4")]
        NFCe = 4,

        /// <summary>
        /// 5 - DANFe NFC-e em mensagem eletrônica
        /// </summary>
        [Description("DANFe NFC-e em mensagem eletrônica")]
        [XmlEnum("5")]
        MsgEletronica = 5
    }

    /// <summary>
    ///     Forma de emissão da NF-e
    ///     <para>1 - Emissão normal (não em contingência)</para>
    ///     <para>2 - Contingência FS-IA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>3 - Contingência SCAN (Sistema de Contingência do Ambiente Nacional)</para>
    ///     <para>4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência)</para>
    ///     <para>5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN)</para>
    ///     <para>7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)</para>
    ///     <para>9 - Contingência off-line da NFC-e</para>
    ///     <para>Nota: Para a NFC-e somente estão disponíveis e são válidas as opções de contingência 5 e 9</para>
    /// </summary>
    public enum TipoEmissao
    {
        /// <summary>
        /// 1 - Emissão normal (não em contingência)
        /// </summary>
        [Description("Normal")]
        [XmlEnum("1")]
        Normal = 1,

        /// <summary>
        /// 2 - Contingência FS-IA, com impressão do DANFE em formulário de segurança
        /// </summary>
        [Description("Contingência FS-IA")]
        [XmlEnum("2")]
        FSIA = 2,

        /// <summary>
        /// 3 - Contingência SCAN (Sistema de Contingência do Ambiente Nacional)
        /// </summary>
        [Description("Contingência SCAN")]
        [XmlEnum("3")]
        SCAN = 3,

        /// <summary>
        /// 4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência)
        /// </summary>
        [Description("Contingência DPEC")]
        [XmlEnum("4")]
        EPEC = 4,

        /// <summary>
        /// 5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança
        /// </summary>
        [Description("Contingência FS-DA")]
        [XmlEnum("5")]
        FSDA = 5,

        /// <summary>
        /// 6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN)
        /// </summary>
        [Description("Contingência SVC-AN")]
        [XmlEnum("6")]
        SVCAN = 6,

        /// <summary>
        /// 7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)
        /// </summary>
        [Description("Contingência SVC-RS")]
        [XmlEnum("7")]
        SVCRS = 7,

        /// <summary>
        /// 9 - Contingência off-line da NFC-e
        /// </summary>
        [Description("Contingência off-line")]
        [XmlEnum("9")]
        OffLine = 9
    }

    /// <summary>
    ///     Finalidade da emissão da NF-e
    ///     <para>1 - NFe normal</para>
    ///     <para>2 - NFe complementar</para>
    ///     <para>3 - NFe de ajuste</para>
    ///     <para>4 - Devolução de mercadoria</para>
    /// </summary>
    public enum FinalidadeNFe
    {
        /// <summary>
        /// 1 - NFe normal
        /// </summary>
        [Description("NFe normal")]
        [XmlEnum("1")]
        Normal = 1,

        /// <summary>
        /// 2 - NFe complementar
        /// </summary>
        [Description("NFe complementar")]
        [XmlEnum("2")]
        Complementar = 2,

        /// <summary>
        /// 3 - NFe de ajuste
        /// </summary>
        [Description("NFe de ajuste")]
        [XmlEnum("3")]
        Ajuste = 3,

        /// <summary>
        /// 4 - Devolução de mercadoria
        /// </summary>
        [Description("Devolução de mercadoria")]
        [XmlEnum("4")]
        Devolucao = 4
    }

    /// <summary>
    ///     Processo de emissão utilizado com a seguinte codificação:
    ///     <para>0 - Emissão de NF-e com aplicativo do contribuinte;</para>
    ///     <para>1 - Emissão de NF-e avulsa pelo Fisco;</para>
    ///     <para>2 - Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;</para>
    ///     <para>3 - Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.</para>
    /// </summary>
    public enum ProcessoEmissao
    {
        /// <summary>
        /// 0 - Emissão de NF-e com aplicativo do contribuinte
        /// </summary>
        [Description("Emissão de NF-e com aplicativo do contribuinte")]
        [XmlEnum("0")]
        AplicativoContribuinte = 0,

        /// <summary>
        /// 1 - Emissão de NF-e avulsa pelo Fisco
        /// </summary>
        [Description("Emissão de NF-e avulsa pelo Fisco")]
        [XmlEnum("1")]
        AvulsaFisco = 1,

        /// <summary>
        /// 2 - Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco
        /// </summary>
        [Description("Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco")]
        [XmlEnum("2")]
        AvulsaContribuinte = 2,

        /// <summary>
        /// 3 - Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco
        /// </summary>
        [Description("Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco")]
        [XmlEnum("3")]
        ContribuinteAplicativoFisco = 3
    }

    /// <summary>
    ///     Indica operação com Consumidor final
    ///     <para>0 - Normal;</para>
    ///     <para>1 - Consumidor final;</para>
    /// </summary>
    public enum ConsumidorFinal
    {
        /// <summary>
        /// 0 - Normal
        /// </summary>
        [Description("Normal")]
        [XmlEnum("0")]
        Nao = 0,

        /// <summary>
        /// 1 - Consumidor final
        /// </summary>
        [Description("Consumidor final")]
        [XmlEnum("1")]
        ConsumidorFinal = 1
    }

    /// <summary>
    ///     Indicador de presença do comprador no estabelecimento comercial no momento da operação
    ///     <para>0 - Não se aplica</para>
    ///     <para>1 - Operação presencial;</para>
    ///     <para>2 - Operação não presencial, pela Internet;</para>
    ///     <para>3 - Operação não presencial, Teleatendimento;</para>
    ///     <para>4 - NFC-e em operação com entrega a domicílio;</para>
    ///     <para>5 - Operação presencial, fora do estabelecimento;</para>
    ///     <para>9 - Operação não presencial, outros.</para>
    /// </summary>
    public enum PresencaComprador
    {
        /// <summary>
        /// 0 - Não se aplica
        /// </summary>
        [Description("Não se aplica")]
        [XmlEnum("0")]
        Nao = 0,

        /// <summary>
        /// 1 - Operação presencial
        /// </summary>
        [Description("Operação presencial")]
        [XmlEnum("1")]
        Presencial = 1,

        /// <summary>
        /// 2 - Operação não presencial, pela Internet
        /// </summary>
        [Description("Operação não presencial, pela Internet")]
        [XmlEnum("2")]
        Internet = 2,

        /// <summary>
        /// 3 - Operação não presencial, Teleatendimento
        /// </summary>
        [Description("Operação não presencial, Teleatendimento")]
        [XmlEnum("3")]
        Teleatendimento = 3,

        /// <summary>
        /// 4 - NFC-e em operação com entrega a domicílio
        /// </summary>
        [Description("NFC-e em operação com entrega a domicílio")]
        [XmlEnum("4")]
        EntregaDomicilio = 4,

        /// <summary>
        /// 5 - Operação presencial, fora do estabelecimento
        /// </summary>
        [Description("Operação presencial, fora do estabelecimento")]
        [XmlEnum("5")]
        PresencialForaEstabelecimento = 5,

        /// <summary>
        /// 9 - Operação não presencial, outros
        /// </summary>
        [Description("Operação não presencial, outros")]
        [XmlEnum("9")]
        Outros = 9
    }

    /// <summary>
    ///     Indicador do tipo de Operação do CSC
    ///     <para>1 - Consulta CSC Ativos;</para>
    ///     <para>2 - Solicita novo CSC;</para>
    ///     <para>3 - Revoga CSC Ativo</para>
    /// </summary>
    public enum IdentificadorOperacaoCsc
    {
        /// <summary>
        /// 1 - Consulta CSC Ativos
        /// </summary>
        [Description("Consulta CSC Ativos")]
        [XmlEnum("1")]
        ConsultaCscAtivos = 1,

        /// <summary>
        /// 2 - Solicita novo CSC
        /// </summary>
        [Description("Solicita novo CSC")]
        [XmlEnum("2")]
        SolicitaNovoCsc = 2,

        /// <summary>
        /// 3 - Revoga CSC Ativo
        /// </summary>
        [Description("Revoga CSC Ativo")]
        [XmlEnum("3")]
        RevogaCscAtivo = 3
    }

    /// <summary>
    ///     Modelo do Documento Fiscal
    ///     <para>01 - Modelo 01</para>
    ///     <para>02 - Modelo 02</para>
    /// </summary>
    public enum refMod
    {
        /// <summary>
        /// 01 - Modelo 01
        /// </summary>
        [Description("Modelo 01")]
        [XmlEnum("01")]
        modelo = 1,

        /// <summary>
        /// 02 - Modelo 02
        /// </summary>
        [Description("Modelo 02")]
        [XmlEnum("02")]
        modelo2 = 2
    }
}