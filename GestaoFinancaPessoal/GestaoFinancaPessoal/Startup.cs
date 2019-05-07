using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GestaoFinancaPessoal.Data;
using GestaoFinancaPessoal.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using GestaoFinancaPessoal.Authorization;
using GestaoFinancaPessoal.Architecture;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace GestaoFinancaPessoal
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        #region ConfigureServices
        #region snippet_defaultPolicy
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                string connectionString = Configuration.GetConnectionString("DefaultLocalPost");
                services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

                //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            }
            else
            {
                string connectionString = Configuration.GetConnectionString("DefaultHeroku");
                services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            }


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
               // Default Password settings.
               options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });
            //var skipHTTPS = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
            //// requires using Microsoft.AspNetCore.Mvc;
            //services.Configure<MvcOptions>(options =>
            //{
            //    // Set LocalTest:skipHTTPS to true to skip HTTPS requirement in 
            //    // debug mode. This is useful when not using Visual Studio.
            //    if (Environment.IsDevelopment() && !skipHTTPS)
            //    {
            //        options.Filters.Add(new RequireHttpsAttribute());
            //    }
            //});

            // services.AddMvc();
            //.AddRazorPagesOptions(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Account/Manage");
            //    options.Conventions.AuthorizePage("/Account/Logout");
            //});

            services.AddSingleton<IEmailSender, EmailSender>();

            // requires: using Microsoft.AspNetCore.Authorization;
            //           using Microsoft.AspNetCore.Mvc.Authorization;
            services.AddMvc(
            config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                //config.Filters.Add(new RequireHttpsAttribute());
            });
            #endregion

            services.AddDistributedMemoryCache();   //adicionar session
            services.AddSession();                  //adicionar session

            // Authorization handlers.
            services.AddScoped<IAuthorizationHandler,
                                  ContactIsOwnerAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler,
                                  ContactAdministratorsAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler,
                                  AutenticacaoAuthorizationHandler>();

            services.AddSingleton<IEmailSender, EmailSender>();

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
        }
        #endregion

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });



            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();   //poem para rodar a session
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
