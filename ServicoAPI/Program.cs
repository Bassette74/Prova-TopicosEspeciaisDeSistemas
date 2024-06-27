using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServicoAPI.Data;
using ServicoAPI.Helpers;
using ServicoAPI.Models;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using ServicoAPI.Interfaces;
using ServicoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar serviços
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26))));

var key = Encoding.ASCII.GetBytes("abcabcabcabcabcabcabcabcabcabcabc");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton(new JwtHelper("abcabcabcabcabcabcabcabcabcabcabc"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddSingleton<IDbContextFactory, DbContextFactory>(); 
builder.Services.AddScoped<IContratoService, ContratoService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Registrar rotas
app.MapPost("/register", async (IUserService userService, User user) =>
{
    await userService.RegisterUserAsync(user);
    return Results.Ok();
});

app.MapPost("/login", async (IUserService userService, string email, string password) =>
{
    var token = await userService.AuthenticateAsync(email, password);
    if (token == null)
    {
        return Results.Unauthorized();
    }
    return Results.Ok(new { token });
});

app.MapPost("/servicos", [Authorize] async (IServicoService servicoService, Servico servico) =>
{
    var createdServico = await servicoService.CreateServicoAsync(servico);
    return Results.Ok(createdServico);
});

app.MapPut("/servicos/{id}", [Authorize] async (IServicoService servicoService, int id, Servico servico) =>
{
    var updatedServico = await servicoService.UpdateServicoAsync(id, servico);
    if (updatedServico == null) return Results.NotFound();
    return Results.Ok(updatedServico);
});

app.MapGet("/servicos/{id}", [Authorize] async (IServicoService servicoService, int id) =>
{
    var servico = await servicoService.GetServicoByIdAsync(id);
    if (servico == null) return Results.NotFound();
    return Results.Ok(servico);
});

app.MapPost("/contratos", [Authorize] async (IContratoService contratoService, Contrato contrato) =>
{
    var createdContrato = await contratoService.CreateContratoAsync(contrato);
    return Results.Ok(createdContrato);
});

app.MapGet("/clientes/{clienteId}/servicos", [Authorize] async (IContratoService contratoService, int clienteId) =>
{
    var servicos = await contratoService.GetServicosByClienteIdAsync(clienteId);
    return Results.Ok(servicos);
});

app.Run();