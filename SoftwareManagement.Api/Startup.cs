
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SoftwareManagement.Api.Extensions;
using SoftwareManagement.Api.Middlewares;
using SoftwareManagement.Api.Services;
using SoftwareManagement.Application.Extensions;
using SoftwareManagement.Domain.Interfaces.Tenants;
using SoftwareManagement.Infrastructure.DbContexts;
using SoftwareManagement.Infrastructure.Extensions;
using SoftwareManagement.Infrastructure.Repositories.TenantProvider;


namespace SoftwareManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITenantProvider, TenantProvider>();
            services.AddApplicationLayer();
            services.AddContextInfrastructure(_configuration);
            services.AddPersistenceContexts(_configuration);
            services.AddRepositories();
            services.AddApplicationRepositories();
            services.AddSharedInfrastructure(_configuration);
            services.AddEssentials();
            services.AddControllers();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ApplicationDbContext applicationDbContext,
            TenantDbContext tenantDbContext
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //tenantDbContext.Database.MigrateAsync();
            //applicationDbContext.Database.MigrateAsync();
            tenantDbContext.Database.Migrate();
            //applicationDbContext.Database.EnsureCreatedAsync();
            app.ConfigureSwagger();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin());
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}