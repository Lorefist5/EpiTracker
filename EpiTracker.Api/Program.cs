using EpiTracker.Application;
using EpiTracker.Infrastructure;
using EpiTracker.Infrastructure.Common.Seeders;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication().AddInfrastructure();

var app = builder.Build();

//Seed data

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var seeder = services.GetRequiredService<IndividualsSeeder>();
await seeder.SeedAsync();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
 
app.MapControllers();

app.Run();
