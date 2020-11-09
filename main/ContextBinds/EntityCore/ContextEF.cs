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
using Models.Empresas;

namespace ContextBinds.EntityCore
{
    public class ContextEF : DbContext
    {
        #region DBSETs

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Parametro> Parametro { get; set; }
        public DbSet<NFeFiles> NFeFiles { get; set; }
        public DbSet<NFeFilesMensagens> NFeFilesMensagens { get; set; }

        #endregion

        public ContextEF(DbContextOptions<ContextEF> options) : base(options) { }

    }
}
