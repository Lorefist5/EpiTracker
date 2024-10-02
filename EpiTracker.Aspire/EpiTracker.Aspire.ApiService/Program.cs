using EpiTracker.Application;
using EpiTracker.Aspire.ApiService.Features.Individuals.Apis;
using EpiTracker.Infrastructure;
using EpiTracker.Infrastructure.Common.Seeders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults & components
builder.AddServiceDefaults();
builder.Services.AddApplication().AddInfrastructure();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Swagger generation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EpiTracker API", Version = "v1" });
});

// Add other services to the container
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EpiTracker API V1");
    });


    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<IndividualsSeeder>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline
app.UseExceptionHandler();

app.MapIndividualApis();
app.MapIndividualStatisticsApis();

app.MapDefaultEndpoints();

app.Run();