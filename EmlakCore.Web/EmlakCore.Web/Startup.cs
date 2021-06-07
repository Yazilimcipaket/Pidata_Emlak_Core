using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmlakCore.Web.Tools;
using Microsoft.AspNetCore.Http;
using EmlakCore.Web.Models;
using EmlakCore.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EmlakCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IService, EmlakCoreService>();
            services.AddScoped<IdecodeTokenService, DecodeToken>();
            services.AddScoped<IidenttyService, Identity>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IValidator<GirisYapModel>, GirisYapModelValidator>();
            services.AddTransient<IValidator<KayitOlModel>, KayitOlModelVadilator>();
            services.AddTransient<IValidator<EmlakIlanVerModel>, EmlakIlanVerVadilator>();
            services.AddTransient<IValidator<YetkiliKayitOlModel>, YetkiliKayitOlModelVadilator>();
            services.AddControllersWithViews().AddFluentValidation();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.LoginPath = "/Kullanici/GirisYap";
                     options.AccessDeniedPath = "/Kullanici/Red";
                     options.Cookie = new CookieBuilder
                     {
                         Name = "EmlakCoreMwc",
                         HttpOnly = false,
                         SameSite = SameSiteMode.Lax,
                         SecurePolicy = CookieSecurePolicy.Always
                     };
                     options.SlidingExpiration = true;
                     options.ExpireTimeSpan = TimeSpan.FromDays(1);
                 });
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AnaSayfa}/{action=Index}/{id?}");
            });
            app.UseCors(x => x.AllowAnyMethod());
        }
    }
}
