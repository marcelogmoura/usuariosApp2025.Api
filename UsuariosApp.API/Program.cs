using Scalar.AspNetCore;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;
using UsuariosApp.Infra.Messages.Consumers;
using UsuariosApp.Infra.Messages.Producers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddRouting(map => map.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(config =>
{
    config.AddPolicy("DefaultPolicy",
        builder => builder
            .WithOrigins("http://localhost:4200")            
            .AllowAnyMethod()
            .AllowAnyHeader());
});

#region injeções de dependência

builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuarioMessage, UsuarioMessageProducer>();
#endregion


#region Configurando os Workers (serviços de segundo plano)
// builder.Services.AddHostedService<UsuarioMessageConsumer>();  // comentado para manter a mensagem no RabbitMQ para testes
#endregion

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("DefaultPolicy");

app.UseSwagger();
app.UseSwaggerUI();



app.MapScalarApiReference(options => {
    options
    .WithTitle("UsuariosApp 2025 - API para controle de usuários.")
    .WithTheme(ScalarTheme.BluePlanet);
});

app.UseAuthorization();
app.MapControllers();

app.Run();

// program public para os testes nao falharem
public partial class Program { }