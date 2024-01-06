using App_Biblioteca1;
using App_Biblioteca1.DTOs;
using App_Biblioteca1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//injection of dependency of DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
      opciones.UseSqlServer(connectionString));

//AutoMappper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<BooksDTO, Books>();
    cfg.CreateMap<Books, BooksDTO>();
}, typeof(Program));

var app = builder.Build();

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
