using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; // 1. Adicionar o using do Logger
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UsuariosApp.Domain.Models.Dtos;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Consumers
{
    public class UsuarioMessageConsumer : BackgroundService
    {
        private readonly ILogger<UsuarioMessageConsumer> _logger;

        // 2. Injetar o ILogger no construtor
        public UsuarioMessageConsumer(ILogger<UsuarioMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory { /* ... suas configurações ... */ };
            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            model.QueueDeclare(
                queue: RabbitMQSettings.Queue,
                durable: true,
                autoDelete: false,
                exclusive: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(model);

            // 3. Adicionar logs dentro do evento de recebimento da mensagem
            consumer.Received += (sender, args) =>
            {
                var payload = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(payload);
                var usuario = JsonConvert.DeserializeObject<UsuarioMessageDto>(message);

                _logger.LogInformation($"\n--- MENSAGEM RECEBIDA DA FILA '--{RabbitMQSettings.Queue}--' ---\n");

                try
                {
                    _logger.LogInformation("Processando e-mail para o usuário: {UsuarioEmail}", usuario.Email);
                    EnviarEmailDeBoasVindas(usuario);
                    _logger.LogInformation("E-mail para {UsuarioEmail} enviado com sucesso.", usuario.Email);

                    model.BasicAck(args.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "### ERRO AO PROCESSAR MENSAGEM DO CONSUMER ###");
                }
            };

            model.BasicConsume(queue: RabbitMQSettings.Queue, autoAck: false, consumer: consumer);
        }

        private void EnviarEmailDeBoasVindas(UsuarioMessageDto usuario)
        {
            // ... (lógica de envio de e-mail permanece) ...
        }
    }
}