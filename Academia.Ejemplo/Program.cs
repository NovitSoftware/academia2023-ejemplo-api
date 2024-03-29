using Academia.Ejemplo.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuracion CORS mediante politica
builder.Services.AddCors(options => 
    options.AddPolicy("Novit", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

//configuracion de la base de datos
string connectionString = builder.Configuration.GetConnectionString("AcademiaEjemplo");

builder.Services.AddDbContext<AplicacionDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<DbContext, AplicacionDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//implementacion CORS
app.UseCors("Novit");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
