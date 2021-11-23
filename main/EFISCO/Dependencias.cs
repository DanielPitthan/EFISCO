using BLL.Cadastros.Fornecedores.Interfaces;
using BLL.Cadastros.Fornecedores.Services;
using BLL.Cadastros.Produtos.Interfaces;
using BLL.Cadastros.Produtos.Services;
using BLL.Certificados.Interfaces;
using BLL.Certificados.Services;
using BLL.Empresas.Interfaces;
using BLL.Empresas.Services;
using BLL.Infra.Interface;
using BLL.Infra.Services;
using BLL.NFE.Interfaces;
using BLL.NFE.Services;
using BLL.TOTVS.Cadastros.Interfaces;
using BLL.TOTVS.Cadastros.Servicos;
using BLL.TOTVS.Movimentos.Compras.Interfaces;
using BLL.TOTVS.Movimentos.Compras.Services;
using BLL.TOTVS.Relatorios.Interfaces;
using BLL.TOTVS.Relatorios.Services;
using CrossCuting.Factorys;
using CrossCuting.Tools;
using DAL.Cadastros.Fornecedores.DAO;
using DAL.Cadastros.Fornecedores.Interfaces;
using DAL.Cadastros.Produtos.DAO;
using DAL.Cadastros.Produtos.Interfaces;
using DAL.Certificados.DAO;
using DAL.Certificados.Interfaces;
using DAL.Empresas.DAO;
using DAL.Empresas.Interfaces;
using DAL.FileStoranges.DAO;
using DAL.FileStoranges.Interfaces;
using DAL.Infra.DAO;
using DAL.Infra.Interfaces;
using DAL.TOTVS.Cadastros.DAO;
using DAL.TOTVS.Cadastros.Interfaces;
using DAL.TOTVS.Movimentos.Compras.DAO;
using DAL.TOTVS.Movimentos.Compras.Interfaces;
using DAL.TOTVS.Movimentos.DAO;
using DAL.TOTVS.Movimentos.Interfaces;
using DAL.TOTVS.Relatorios.DAO;
using DAL.TOTVS.Relatorios.Interfaces;
using DAL.XmlDAL.DAO;
using DAL.XmlDAL.Interfaces;
using EFISCO.Areas.UploadFiles;
using EFISCO.Pages.Components.AlertComponent;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using System.Net.Http;
namespace EFISCO
{
    public class Dependencias
    {
        public IServiceCollection services;
        public Dependencias(IServiceCollection serviceCollection)
        {
            services = serviceCollection;



            
            services.AddTransient<IEmailDAO, EmailDAO>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IFileStorangeService, FileStorangeService>();
            services.AddTransient<EmailControl>();
            services.AddTransient<UploadFactory>();
            services.AddScoped<AlertService>();
            services.AddTransient<ICertificadoService, CertificadoService>();
            services.AddTransient<ICertificadoDAO, CertificadoDAO>();
            services.AddTransient<IAtivoVsNotaFiscalService, AtivoVsNotaFiscalService>();
            services.AddTransient<IAtivoVsNotaFiscalDAO, AtivoVsNotaFiscalDAO>();
            services.AddScoped<DialogService>();
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
            services.AddTransient<IParametrosDAO, ParametroDAO>();
            services.AddTransient<IParametroService, ParametrosServices>();
            services.AddTransient<INFeFilesDAO, NFeFilesDAO>();
            services.AddTransient<Upload>();
        }

        public IServiceCollection Get()
        {
            return services;
        }
    }
}
