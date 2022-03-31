using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TranferBankInformation.Repositories;
using TranferBankInformation.Repositories.Infrastructure;
using TranferBankInformation.Services.Implementation;
using TranferBankInformation.Services.Services;

namespace TransferBankInformation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<AppSettings>(Configuration);
            var serviceProvider = services.BuildServiceProvider();
            var settings = serviceProvider.GetService<IOptions<AppSettings>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            services.AddDbContext<TransferBankInformationContext>(options =>
            {
                options.UseSqlServer(settings.Value.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });

             services.AddTransient<ISupplierBankServices, SupplierBankServices>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                      builder =>
                                      {
                                          builder.WithOrigins("http://localhost:56091", "https://localhost:44324")
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod();
                                      });
            });

            services.AddRazorPages()
               .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
               .AddDataAnnotationsLocalization(); ;

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            //Injecta el servicio HttpContextAccessor, para que pueda ser utilizada clases fuera del alcance del code behind de los razor pages. https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-2.2
            services.AddHttpContextAccessor();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettings> settings)
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

            var cultureProviders = new[]
           {
                new QueryStringRequestCultureProvider()  //Deja solo este proveedor para poder pasar la cultura en el query string para efectos de prueba, pero que no se usen ni CookieRequestCultureProvider ni AcceptLanguageHeaderRequestCultureProvider (definida por el navegador).
            };

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("es-ES"),
                new CultureInfo("es-MX"),
                new CultureInfo("es"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(settings.Value.DefaultCulture),
                RequestCultureProviders = cultureProviders,
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            //End globalization and localization

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
