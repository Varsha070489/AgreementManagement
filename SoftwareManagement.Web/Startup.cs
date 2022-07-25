using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using SoftwareManagement.Application.Extensions;
using SoftwareManagement.Web.Abstractions;
using SoftwareManagement.Web.Extensions;
using SoftwareManagement.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SoftwareManagement.Web
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


            services.AddNotyf(o =>
            {
                o.DurationInSeconds = 10;
                o.IsDismissable = true;
                o.HasRippleEffect = true;
            });
            services.AddApplicationLayer();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddInfrastructure(Configuration);
            services.AddHTTPClientFactory(Configuration);
            services.AddTransient<ITokenService, TokenService>();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            //services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()));
            services.AddMvc(options =>
            options.Filters.Add(new AuthorizeFilter())
            ).AddFluentValidation(fvc => {
                fvc.RegisterValidatorsFromAssemblyContaining<Startup>();
                fvc.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options =>
                   {
                       options.Cookie.HttpOnly = true;
                       options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                       options.Cookie.SameSite = SameSiteMode.Lax;
                       options.LoginPath = new PathString("/Account/Login");
                       options.LogoutPath = new PathString("/Account/Logout");
                   });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CookieSecurePolicy.None;
            });
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseNotyf();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin());
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}