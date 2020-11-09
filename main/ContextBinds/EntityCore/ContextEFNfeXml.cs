using Microsoft.EntityFrameworkCore;
using DFe.Classes.Assinatura;
using Models.Infra;
using Models.NFe;
using XmlNFe.Nfes;
using XmlNFe.Nfes.Informacoes;
using XmlNFe.Nfes.Informacoes.Cobranca;
using XmlNFe.Nfes.Informacoes.Destinatario;
using XmlNFe.Nfes.Informacoes.Detalhe;
using XmlNFe.Nfes.Informacoes.Detalhe.ProdEspecifico;
using XmlNFe.Nfes.Informacoes.Detalhe.Tributacao;
using XmlNFe.Nfes.Informacoes.Emitente;
using XmlNFe.Nfes.Informacoes.Identificacao;
using XmlNFe.Nfes.Informacoes.Observacoes;
using XmlNFe.Nfes.Informacoes.Pagamento;
using XmlNFe.Nfes.Informacoes.Total;
using XmlNFe.Nfes.Informacoes.Transporte;
using XmlNFe.Nfes.Informacoes.Detalhe.Tributacao.Estadual;
using XmlNFe.Nfes.Informacoes.Detalhe.Tributacao.Federal;
using Models.FileStoranges;
using Models.Cadastros.Fornecedores;
using Models.Cadastros.Produtos;

namespace ContextBinds.EntityCore
{
    public class ContextEFNFeXml : DbContext
    {
        #region DBSETs
        
        public DbSet<prod> prod { get; set; }
        public DbSet<ProdutoIntegrado> ProdutoIntegrado { get; set; }
        public DbSet<EmitenteIntegrado> EmitenteIntegrado { get; set; }
        public DbSet<FileStorange> FileStorange { get; set; }
        public DbSet<NFe> NFe { get; set; }
        public DbSet<infNFe> infNFe { get; set; }
        public DbSet<infNFeSupl> infNFeSupl { get; set; }
        public DbSet<Signature> Signature { get; set; }
        public DbSet<SignedInfo> SignedInfo { get; set; }
        public DbSet<KeyInfo> KeyInfo { get; set; }
        public DbSet<X509Data> X509Data { get; set; }
        public DbSet<CanonicalizationMethod> CanonicalizationMethod { get; set; }
        public DbSet<SignatureMethod> SignatureMethod { get; set; }
        public DbSet<Reference> Reference { get; set; }
        public DbSet<Transform> Transform { get; set; }
        public DbSet<DigestMethod> DigestMethod { get; set; }
        public DbSet<ide> ide { get; set; }
        public DbSet<NFref> NFref { get; set; }
        public DbSet<emit> emit { get; set; }
        public DbSet<enderEmit> enderEmit { get; set; }
        public DbSet<dest> dest { get; set; }
        public DbSet<enderDest> enderDest { get; set; }
        public DbSet<retirada> retirada { get; set; }
        public DbSet<entrega> entrega { get; set; }
        public DbSet<autXML> autXML { get; set; }
        public DbSet<det> det { get; set; }
        public DbSet<ProdutoEspecifico> ProdutoEspecifico { get; set; }
        public DbSet<comb> comb { get; set; }
        public DbSet<CIDE> CIDE { get; set; }
        public DbSet<imposto> imposto { get; set; }
        public DbSet<impostoDevol> impostoDevol { get; set; }
        public DbSet<total> total { get; set; }
        public DbSet<retTrib> retTrib { get; set; }
        public DbSet<transp> transp { get; set; }
        public DbSet<transporta> transporta { get; set; }
        public DbSet<retTransp> retTransp { get; set; }
        public DbSet<vol> vol { get; set; }
        public DbSet<veicTransp> veicTransp { get; set; }
        public DbSet<reboque> reboque { get; set; }
        public DbSet<cobr> cobr { get; set; }
        public DbSet<infAdic> infAdic { get; set; }
        public DbSet<compra> compra { get; set; }
        public DbSet<pag> pag { get; set; }
        public DbSet<fat> fat { get; set; }
        public DbSet<dup> dup { get; set; }
        public DbSet<obsCont> obsCont { get; set; }
        public DbSet<obsFisco> obsFisco { get; set; }
        public DbSet<procRef> procRef { get; set; }
        public DbSet<ICMS> ICMS { get; set; }
        public DbSet<ICMS00> ICMS00 { get; set; }
        public DbSet<ICMS10> ICMS10 { get; set; }
        public DbSet<ICMS20> ICMS20 { get; set; }
        public DbSet<ICMS30> ICMS30 { get; set; }
        public DbSet<ICMS40> ICMS40 { get; set; }
        public DbSet<ICMS51> ICMS51 { get; set; }
        public DbSet<ICMS60> ICMS60 { get; set; }
        public DbSet<ICMS70> ICMS70 { get; set; }
        public DbSet<ICMS90> ICMS90 { get; set; }
        public DbSet<ICMSPart> ICMSPart { get; set; }
        public DbSet<ICMSST> ICMSST { get; set; }
        public DbSet<ICMSSN101> ICMSSN101 { get; set; }
        public DbSet<ICMSSN102> ICMSSN102 { get; set; }
        public DbSet<ICMSSN201> ICMSSN201 { get; set; }
        public DbSet<ICMSSN202> ICMSSN202 { get; set; }
        public DbSet<ICMSSN500> ICMSSN500 { get; set; }
        public DbSet<ICMSSN900> ICMSSN900 { get; set; }
        public DbSet<IPITrib> IPITrib { get; set; }
        public DbSet<IPINT> IPINT { get; set; }
        public DbSet<COFINSAliq> COFINSAliq { get; set; }
        public DbSet<COFINSQtde> COFINSQtde { get; set; }
        public DbSet<COFINSNT> COFINSNT { get; set; }
        public DbSet<COFINSOutr> COFINSOutr { get; set; }


        #endregion

        public ContextEFNFeXml(DbContextOptions<ContextEFNFeXml> options) : base(options) { }

    }
}
