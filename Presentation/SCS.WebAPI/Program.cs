using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi;
using Scs.Application;
using Scs.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Service Registration ---

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// A. Add Infrastructure (Database, Repos)
// We pass configuration so Infrastructure can read the ConnectionString
builder.Services.AddInfrastructureServices(builder.Configuration);

// B. Add Application (MediatR, Validators, etc.)
builder.Services.AddApplicationServices();

// C. Swagger Config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SCS Clearance System API",
        Version = "v1"
    });
});

var app = builder.Build();

// --- 2. Request Pipeline ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCS API V1");
    });
}
else
{
    // Optional: Keep Swagger JSON available in Prod, but hide UI
    app.UseSwagger();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();