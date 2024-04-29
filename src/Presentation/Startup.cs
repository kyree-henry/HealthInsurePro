using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using HealthInsurePro.Presentation.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;

namespace HealthInsurePro.Presentation
{
    public class Startup
    {
        public IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcServices();
            services.AddSwaggerServices();
            services.AddDatabaseServices();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                IApiVersionDescriptionProvider? provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (ApiVersionDescription? description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description!.GroupName}/swagger.json", description!.GroupName.ToUpperInvariant());
                }
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseCors("OpenCorsPolicy");

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}