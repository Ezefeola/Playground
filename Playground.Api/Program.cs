using Plaground.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region Services Area
string? connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new KeyNotFoundException("No se encontro una conexion.");

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(connectionString);

#endregion Services Area

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
