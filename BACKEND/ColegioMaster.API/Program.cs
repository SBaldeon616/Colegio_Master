using ColegioMaster.API.Extensions;
using ColegioMaster.DtoModels.Mascota;
using ColegioMaster.Negocio.EstadoCliente;
using ColegioMaster.Negocio.Juegos;
using ColegioMaster.Negocio.Mascota;
using ColegioMaster.Repositorio.Implement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// --- CONFIGURACIÓN DE CORS ---
// Define el nombre de la política
const string MisOrigenesPermitidos = "_misOrigenesPermitidos";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MisOrigenesPermitidos,
                      policy =>
                      {
                          // Opción para desarrollo: Permite cualquier origen, método y encabezado
                          policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();

                          // Opción para producción (Recomendado): Reemplaza la línea de arriba y descomenta esto:
                          // policy.WithOrigins("https://tu-frontend.com", "http://localhost:4200") 
                          //       .AllowAnyMethod()
                          //       .AllowAnyHeader();
                      });
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Colegio Master API",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese: Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Registro de dependencias para EstadoCliente
builder.Services.AddScoped<IEstadoClienteService, EstadoClienteService>();
builder.Services.AddScoped<IEstadoClienteRepositorio, EstadoClienteRepositorio>();

// Registro de dependencias para JuegoRecreativo
builder.Services.AddScoped<IJuegoRecreativoService, JuegoRecreativoService>();
builder.Services.AddScoped<IJuegoRecreativoRepositorio, JuegoRecreativoRepositorio>();

// Registro de dependencias para Mascota
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<IMascotaRepositorio, MascotaRepositorio>();

var app = builder.Build();

app.UseMiddleware<CustomMidleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Colegio Master API V1");
    });
}


// --- ACTIVACIÓN DEL MIDDLEWARE DE CORS ---
// Importante: Debe ir antes de UseAuthorization y MapControllers
app.UseCors(MisOrigenesPermitidos);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();