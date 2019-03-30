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

namespace GestaoFinancaPessoal
{
    //    public class Startup
    //    {
    //        public IConfiguration Configuration { get; }
    //        private IHostingEnvironment Environment { get; }

    //        public Startup(IConfiguration configuration, IHostingEnvironment env)
    //        {
    //            Configuration = configuration;
    //            Environment = env;
    //        }


    //        // This method gets called by the runtime. Use this method to add services to the container.
    //        public void ConfigureServices(IServiceCollection services)
    //        {
    //            services.Configure<CookiePolicyOptions>(options =>
    //            {
    //                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    //                options.CheckConsentNeeded = context => true;
    //                options.MinimumSameSitePolicy = SameSiteMode.None;
    //            });


    //            string connectionString = Configuration.GetConnectionString("Default");
    //            services.AddDbContext<DbContext,FinancaContexto>(options => options.UseSqlServer(connectionString));
    //            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


    //            services.AddIdentity<ApplicationUser, IdentityRole>()
    //                .AddEntityFrameworkStores<ApplicationDbContext>()
    //                .AddDefaultTokenProviders();

    //            var skipHTTPS = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
    //            // requires using Microsoft.AspNetCore.Mvc;
    //            services.Configure<MvcOptions>(options =>
    //            {
    //                // Set LocalTest:skipHTTPS to true to skip HTTPS requirement in 
    //                // debug mode. This is useful when not using Visual Studio.
    //                if (Environment.IsDevelopment() && !skipHTTPS)
    //                {
    //                    options.Filters.Add(new RequireHttpsAttribute());
    //                }
    //            });

    //            services.AddSingleton<IEmailSender, EmailSender>();

    //            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

    //            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    //            //services.AddMvc(config =>
    //            //{
    //            //    var policy = new AuthorizationPolicyBuilder()
    //            //                     .RequireAuthenticatedUser()
    //            //                     .Build();
    //            //    config.Filters.Add(new AuthorizeFilter(policy));
    //            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    //            services.AddDistributedMemoryCache();   //adicionar session
    //            services.AddSession();                  //adicionar session

    //            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores();
    //            //services.AddDefaultIdentity<IdentityUser>();
    //            //services.AddDefaultIdentity<IdentityUser>()
    //            //    .AddDefaultUI(UIFramework.Bootstrap4)
    //            //    .AddEntityFrameworkStores<FinancaContexto>();





    //            ////services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
    //            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //            //            .AddCookie(o => o.LoginPath = new PathString("/Autenticacao/login"))
    //            //            .AddFacebook(o =>
    //            //            {
    //            //                o.AppId = Configuration["facebook:appid"];
    //            //                o.AppSecret = Configuration["facebook:appsecret"];
    //            //            });





    //            // Authorization handlers.
    //            services.AddScoped<IAuthorizationHandler,
    //                                  ContactIsOwnerAuthorizationHandler>();

    //            services.AddSingleton<IAuthorizationHandler,
    //                                  ContactAdministratorsAuthorizationHandler>();

    //            services.AddSingleton<IAuthorizationHandler,
    //                                  AutenticacaoAuthorizationHandler>();
    //        }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        else
    //        {
    //            app.UseExceptionHandler("/Home/Error");
    //            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //            app.UseHsts();
    //        }

    //        app.UseHttpsRedirection();
    //        app.UseStaticFiles();
    //        app.UseCookiePolicy();

    //        app.UseSession();   //poem para rodar a session

    //        //app.UseMiddleware(typeof(ErrorHandlingMiddleware));
    //        app.UseMvc(routes =>
    //        {
    //            routes.MapRoute(
    //                name: "default",
    //                template: "{controller=Home}/{action=Index}/{id?}");


    //        });

    //        app.UseAuthentication();


    //    }
    //}
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

            var skipHTTPS = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
            // requires using Microsoft.AspNetCore.Mvc;
            services.Configure<MvcOptions>(options =>
            {
                // Set LocalTest:skipHTTPS to true to skip HTTPS requirement in 
                // debug mode. This is useful when not using Visual Studio.
                if (Environment.IsDevelopment() && !skipHTTPS)
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            });

            // services.AddMvc();
            //.AddRazorPagesOptions(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Account/Manage");
            //    options.Conventions.AuthorizePage("/Account/Logout");
            //});

            services.AddSingleton<IEmailSender, EmailSender>();

            // requires: using Microsoft.AspNetCore.Authorization;
            //           using Microsoft.AspNetCore.Mvc.Authorization;
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(new RequireHttpsAttribute());
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
