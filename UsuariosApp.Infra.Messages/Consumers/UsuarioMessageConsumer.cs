using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace UsuariosApp.Infra.Messages.Consumers
{
    public class UsuarioMessageConsumer : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Debug.WriteLine("--- O CONSUMER ATUALIZADO E CORRETO ESTÁ SENDO EXECUTADO ---");
            return Task.CompletedTask;
        }
    }
}
