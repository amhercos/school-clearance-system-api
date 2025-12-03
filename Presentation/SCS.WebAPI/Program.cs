using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// .NET 10 built-in OpenAPI services
builder.Services.AddOpenApi();

// MediatR services
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Scs.Application.DependencyInjection).Assembly)
);

var app = builder.Build();

// Development-only API documentation and UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();        // Maps /openapi/{documentName}.json
    //app.UseOpenApi();        // Serves OpenAPI spec
    //app.UseSwaggerUi();      // Serves Swagger UI
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
