using Microsoft.AspNetCore.Builder;

namespace SoftwareManagement.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftwareManagement.Api");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }
    }
}