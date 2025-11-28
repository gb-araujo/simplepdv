using Microsoft.EntityFrameworkCore;
using SimplePDV.Application.Services;
using SimplePDV.Domain.Interfaces;
using SimplePDV.Infrastructure.Data;
using SimplePDV.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMovimentoEstoqueRepository, MovimentoEstoqueRepository>();

// Services
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<MovimentoEstoqueService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Sempre habilitar Swagger para facilitar testes
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
