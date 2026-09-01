var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Rama Base: solo la configuracion inicial del host.
// El registro de Core / Persistence / Externals y los modulos (controllers)
// se incorporan en las ramas Core y CQRS.

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
