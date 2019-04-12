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
            string connectionString = Configuration.GetConnectionString("Default");
            //services.AddDbContext<DbContext, FinancaContexto>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

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
