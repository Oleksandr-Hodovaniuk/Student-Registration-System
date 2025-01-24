using Infrastructure.Extensions;
using Infrastructure.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Registration of controller services.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registration of custom services.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Data seeding.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DefaultDataSeeder>();
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
