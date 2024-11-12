using Application.Seeders;
using Core.Entities;
using Infrastructure.Extensions;
using StudentRegistrationSystem.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

////Adds swagger authorization configuration.
builder.AddPresentation();

//Adds custom services to the container and connect to a database.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

//Data seeding.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
await seeder.SeedAsync();

//Configures the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
