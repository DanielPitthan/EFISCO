using Microsoft.EntityFrameworkCore;
using Models.Relatorios.Ativo;
using Models.TOTVS.Cadastros;
using Models.TOTVS.Cadastros.ClienteFornecedor;
using Models.TOTVS.Cadastros.Produtos;
using Models.TOTVS.Movimentos;
using Models.TOTVS.Movimentos.Compras;

namespace ContextBinds.EntityCore
{
    public class ContextTOTVS : DbContext
    {
        public ContextTOTVS(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AtivoVsNotaFiscal> AtivoVsNotaFiscals { get; set; }
        public DbSet<NotaFiscalEntradaCabecalhoTotvs> SF1010 { get; set; }
        public DbSet<FornecedorTotvs> SA2010 { get; set; }
        public DbSet<ClienteTotvs> SA1010 { get; set; }
        public DbSet<ProdutoTotvs> SB1010 { get; set; }
        public DbSet<ProdutoVersusFornecedorTotvs> SA5010 { get; set; }
        public DbSet<PedidoDeCompraTotvs> SC7010 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FornecedorTotvs>(eb =>
            {
                eb.HasNoKey();
            });
            modelBuilder.Entity<ClienteTotvs>().HasNoKey();
            modelBuilder.Entity<ProdutoTotvs>().HasNoKey();
            modelBuilder.Entity<ProdutoVersusFornecedorTotvs>().HasNoKey();
            modelBuilder.Entity<PedidoDeCompraTotvs>().HasNoKey();
            modelBuilder.Entity<NotaFiscalEntradaCabecalhoTotvs>().HasNoKey();
            modelBuilder.Entity<AtivoVsNotaFiscal>().HasNoKey();
        }
    }
}
