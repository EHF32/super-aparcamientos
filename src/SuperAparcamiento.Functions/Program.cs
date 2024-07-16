using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SuperAparcamiento.DataAccess;
using SuperAparcamiento.DataAccess.Persistence;
using SuperAparcamiento.Functions.Middleware;
using SuperAparcamiento.Logic;
using SuperAparcamiento.Logic.Contract.Validators;

var host = new HostBuilder();

host.ConfigureFunctionsWebApplication(builder =>
{
    // Gestión global de errores
    builder.UseMiddleware<SuperAparcamientoErrorMiddleware>();

    builder.UseNewtonsoftJson();
});

host.ConfigureServices(services =>
{
    // Configura la capa de acceso a datos (Base de datos y repositorios)
    services.AddDataAccessLayer();

    // Configura la capa de lógica de la aplicación (Servicios)
    services.AddLogic();

    // Logging
    services.AddLogging();

    // Configura la validación de los contratos
    services.AddFluentValidationAutoValidation();
    services.AddValidatorsFromAssembly(typeof(IValidationMarker).Assembly);
});


var app = host.Build();

// Inicializa la base de datos y la rellena con los datos iniciales si no está creada
await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AparcamientoDbContext>();
    await db.Database.EnsureCreatedAsync();
}

await app.RunAsync();
