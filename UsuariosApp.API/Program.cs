using Scalar.AspNetCore;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddRouting(map => map.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Injeção de dependências

builder.Services.AddTransient<IUsuarioService, UsuarioService>() ;
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

#endregion


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapScalarApiReference(options => {
    options
    .WithTitle("UsuariosApp - API para controle de usuários.")
    .WithTheme(ScalarTheme.BluePlanet);
});

app.UseAuthorization();
app.MapControllers();
app.Run();