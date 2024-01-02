using DemoTrabajadoresBackend;
using DemoTrabajadoresBackend.EndPoints;
using DemoTrabajadoresBackend.Entitdades;
using DemoTrabajadoresBackend.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });

    opciones.AddPolicy("libre", configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositoryDepartamento, RepositoryDepartamento>();
builder.Services.AddScoped<IRepositoryProvincia, RepositoryProvincia>();
builder.Services.AddScoped<IRepositoryDistrito, RepositoryDistrito>();
builder.Services.AddScoped<IRepositoryTrabajador, RepositoryTrabajador>();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseOutputCache();

app.MapGroup("/departamentos").MapDepartamentos();
app.MapGroup("/provincias").MapProvincias();
app.MapGroup("/distritos").MapDistritos();
app.MapGroup("/trabajadores").MapTrabajadores();



app.Run();
