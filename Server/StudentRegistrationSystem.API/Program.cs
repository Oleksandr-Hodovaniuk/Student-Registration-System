using Infrastructure.Extensions;
using Infrastructure.Persistence.Seeders.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adds base services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adds custom services to the container and connect to a database.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

//Data seeding.
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var seeders = services.GetServices<ISeeder>();
foreach (var seeder in seeders)
    await seeder.SeedAsync();

// Configures the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
