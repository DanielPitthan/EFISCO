using BlazorDownloadFile;
using Blazorise;
using Blazorise.Bootstrap;
using ContextBinds.EntityCore;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            //services.AddDbContext<ContextEF>(options =>
            //{
            //    options.UseSqlServer(ConecxaoAtiva.StringConnectionBaseWebFrame())
            //     .EnableSensitiveDataLogging(); 
            //}, ServiceLifetime.Transient);


            services.AddDbContext<ContextEFNFeXml>(options =>
            {
                options.UseSqlServer(@"Server=192.168.1.252;Database=NFeXML;User Id=EFISCO;Password=Imperador; MultipleActiveResultSets=True; Encrypt=False;")
                 .EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            services.AddDbContext<ContextTOTVS>(options =>
            {
                options.UseSqlServer(@"Server=192.168.1.252;Database=PROTHEUS_PRODUCAO;User ID=totvs;Password=totvs#741; Connect Timeout=3000; MultipleActiveResultSets=True; Encrypt=False;")
                 .EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            services
                 .AddBlazorise(options =>
                 {
                 })
                 .AddBootstrapProviders();


            services.AddBlazorDownloadFile(ServiceLifetime.Scoped);

            services.AddMatBlazor();

            #region Inje��o de Dependencias    
            Dependencias dependencias = new Dependencias(services);
            services = dependencias.Get();
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

            //app.ApplicationServices
            //      .UseBootstrapProviders();

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
