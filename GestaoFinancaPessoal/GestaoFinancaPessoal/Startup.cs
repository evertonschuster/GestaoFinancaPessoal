using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using GestaoFinancaPessoal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI;
using GestaoFinancaPessoal.Architecture;
using GestaoFinancaPessoal.Controllers;

namespace GestaoFinacaPessoal
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<FinancaContexto>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDistributedMemoryCache();   //adicionar session
            services.AddSession();                  //adicionar session

            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores();
            //services.AddDefaultIdentity<IdentityUser>();
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<FinancaContexto>();





            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(o => o.LoginPath = new PathString("/Autenticacao/login"))
                        .AddFacebook(o =>
                        {
                            o.AppId = Configuration["facebook:appid"];
                            o.AppSecret = Configuration["facebook:appsecret"];
                        });


            services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();

        app.UseSession();   //poem para rodar a session

        app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");


        });

        app.UseAuthentication();


    }
}
}
