using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContextBinds.EntityCore;
using Microsoft.EntityFrameworkCore;
using DAL.XmlDAL.Interfaces;
using DAL.XmlDAL.DAO;
using BLL.NFE.Interfaces;
using BLL.NFE.Services;
using DAL.Infra.Interfaces;
using DAL.Infra.DAO;
using BLL.Infra.Interface;
using BLL.Infra.Services;
using System.Net.Http;
using DAL.FileStoranges.Interfaces;
using DAL.FileStoranges.DAO;
using EFISCO.Areas.UploadFiles;
using BLL.Empresas.Interfaces;
using BLL.Empresas.Services;
using DAL.Empresas.DAO;
using DAL.Empresas.Interfaces;
using DAL.TOTVS.Cadastros.Interfaces;
using DAL.TOTVS.Cadastros.DAO;
using BLL.TOTVS.Cadastros.Interfaces;
using BLL.TOTVS.Cadastros.Servicos;
using DAL.TOTVS.Movimentos.Interfaces;
using DAL.TOTVS.Movimentos.DAO;
using BLL.TOTVS.Movimentos.Compras.Interfaces;
using BLL.TOTVS.Movimentos.Compras.Services;
using DAL.TOTVS.Movimentos.Compras.Interfaces;
using DAL.TOTVS.Movimentos.Compras.DAO;
using DAL.Cadastros.Fornecedores.Interfaces;
using DAL.Cadastros.Fornecedores.DAO;
using BLL.Cadastros.Fornecedores.Interfaces;
using BLL.Cadastros.Fornecedores.Services;
using Blazorise;
using Blazorise.Bootstrap;
using DAL.Cadastros.Produtos.DAO;
using BLL.Cadastros.Produtos.Interfaces;
using DAL.Cadastros.Produtos.Interfaces;
using BLL.Cadastros.Produtos.Services;
using BlazorDownloadFile;

namespace EFISCO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDbContext<ContextEF>(options =>
            {
                options.UseSqlServer(ConecxaoAtiva.StringConnectionBaseWebFrame())
                 .EnableSensitiveDataLogging(); 
            }, ServiceLifetime.Transient);


            services.AddDbContext<ContextEFNFeXml>(options =>
            {
                options.UseSqlServer(ConecxaoAtiva.StringConnectionBaseNfe())
                 .EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            services.AddDbContext<ContextTOTVS>(options =>
            {
                options.UseSqlServer(ConecxaoAtiva.StringConnectionBaseTotvs())
                 .EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            services
                 .AddBlazorise(options =>
                 {
                     options.ChangeTextOnKeyPress = true; // optional
                 })
                 .AddBootstrapProviders();


            services.AddBlazorDownloadFile(ServiceLifetime.Scoped);

            #region Injeção de Dependencias    
            services.AddTransient<Radzen.DialogService>();
            services.AddTransient<INfeDownloadListService, NfeDownloadService>();
            services.AddTransient<IProdutoIntegradoDAO, ProdutoIntegradoDAO>();
            services.AddTransient<IProdutoIntegradoService, ProdutoIntegradoService>();
            services.AddTransient<IEmitenteIntegradoService, EmitenteIntegradoService>();
            services.AddTransient<IEmitenteIntegradoDAO, EmitenteIntegradoDAO>();
            services.AddTransient<INotaFiscalEntradaTotvsService, NotaFiscalEntradaTotvsService>();
            services.AddTransient<INotaFiscalEntradaCabecalhoTotvsDAO, NotaFiscalEntradaCabecalhoDAO>();
            services.AddTransient<IPedidoDeCompraTotvsDAO, PedidoDeComprasTotvsDAO>();
            services.AddTransient<IPedidoDeCompraTotvsService, PedidoDeCompraTotvsService>();
            services.AddTransient<INFeFilesMensagensService, NFeFilesMensagensService>();
            services.AddTransient<IProdutoVersusFornecedorTotvsDAO, ProdutoVersusFornecedorTotvsDAO>();
            services.AddTransient<IProdutoVersusFornecedorTotvsService, ProdutoVersusFornecedorTotvsService>();
            services.AddTransient<INFeFilesMensagensDAO, NFeFilesMensagensDAO>();
            services.AddTransient<IFornecedorTotvsDAO, FornecedorTotvsDAO>();
            services.AddTransient<IFornecedorTotvsService, FornecedorTotvsService>();
            services.AddTransient<IProdutoTotvsDAO, ProdutoTotvsDAO>();
            services.AddTransient<IProdutoTotvsService, ProdutoTotvsService>();
            services.AddTransient<IEmpresaDAO, EmpresaDAO>();
            services.AddTransient<IEmpresaService, EmpresaService>();
            services.AddTransient<IFileStorangeDAO, FileStorangeDAO>();
            services.AddTransient<HttpClient>();
            services.AddTransient<INFeDAO, NFeDAO>();
            services.AddTransient<INFeXmlService, NFeXmlService>();
            services.AddTransient<IParametros, ParametroDAO>();
            services.AddTransient<IParametroService, ParametrosServices>();
            services.AddTransient<INFeFilesDAO, NFeFilesDAO>();
            services.AddTransient<Upload>();


            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ApplicationServices
                  .UseBootstrapProviders();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }
    }
}
