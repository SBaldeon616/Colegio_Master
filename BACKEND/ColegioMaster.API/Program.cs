using ColegioMaster.DtoModels.JuegoRecreativo;
using ColegioMaster.DtoModels.Mascota;
using ColegioMaster.Negocio.EstadoCliente;
using ColegioMaster.Negocio.Juegos;
using ColegioMaster.Negocio.Mascota;
using ColegioMaster.Repositorio.Implement;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Registro de dependencias para EstadoCliente
builder.Services.AddScoped<IEstadoClienteService, EstadoClienteService>();
builder.Services.AddScoped<IEstadoClienteRepositorio, EstadoClienteRepositorio>();

// Registro de dependencias para JuegoRecreativo
builder.Services.AddScoped<IJuegoRecreativoService, JuegoRecreativoService>();
builder.Services.AddScoped<IJuegoRecreativoRepositorio, JuegoRecreativoRepositorio>();

// Registro de dependencias para Mascota
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<IMascotaRepositorio, MascotaRepositorio>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();