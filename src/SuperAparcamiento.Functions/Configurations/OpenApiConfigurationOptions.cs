using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace SuperAparcamiento.Functions.Configurations;

public class OpenApiConfigurationOptions
{
    public class MyOpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = GetOpenApiDocVersion(),
            Title = "Super Aparcamientos API",
            Description = "API para la gestión de un sistema de aparcamientos.",
            Contact = new OpenApiContact()
            {
                Name = "Ángel Herrador",
                Email = "angel6v3@gmail.com",
                Url = new Uri("https://ahc.software"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            },
        };

        public override OpenApiVersionType OpenApiVersion { get; set; } = GetOpenApiVersion();
    }
}
