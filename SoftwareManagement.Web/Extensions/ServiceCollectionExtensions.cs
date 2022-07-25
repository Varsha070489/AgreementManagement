using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;


using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using SoftwareManagement.Web.Abstractions;
using SoftwareManagement.Web.Helpers;

namespace SoftwareManagement.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddAuthenticationScheme(configuration);
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddHttpContextAccessor();
        }

        public static void AddHTTPClientFactory(this IServiceCollection services, IConfiguration configuration)
        {
            // HttpClientFactory
            services.AddHttpClient("GirmesofttechERPAPIURL", client =>
            {
                client.BaseAddress = new Uri(configuration["APIURL"]);
            });

            // HttpClientFactory will take care of connection caching, ProjectsAPI is the name 
            // of the factory, just above.
            services.AddSingleton<IHTTPClientHelper, HTTPClientHelper>(s =>
                         new HTTPClientHelper(s.GetService<IHttpClientFactory>(), "GirmesofttechERPAPIURL", s.GetService<IHttpContextAccessor>())
                         );
        }
        private static void AddAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
