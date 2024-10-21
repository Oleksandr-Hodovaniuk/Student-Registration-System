using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adds services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adds custom services to the container and connects to a database.
builder.Services.AddInfractructure(builder.Configuration);

var app = builder.Build();
//HELLO
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
