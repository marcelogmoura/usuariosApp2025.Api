using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Models.Dtos;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Producers
{
    
    public class UsuarioMessageProducer : IUsuarioMessage
    {
        private readonly ILogger<UsuarioMessageProducer> _logger;

        // O construtor precisa do ILogger. As configurações vêm da classe estática.
        public UsuarioMessageProducer(ILogger<UsuarioMessageProducer> logger)
        {
            _logger = logger;
        }

        public void EnviarMensagem(UsuarioMessageDto usuario)
        {
            _logger.LogInformation("Serviço de mensageria: Iniciando envio para o usuário {UsuarioEmail}...", usuario.Email);

            
            var connectionFactory = new ConnectionFactory
            {
                HostName = RabbitMQSettings.Host,
                Port = RabbitMQSettings.Port,
                UserName = RabbitMQSettings.User,
                Password = RabbitMQSettings.Pass,
                VirtualHost = RabbitMQSettings.VHost
            };

            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    _logger.LogInformation("Conexão com o RabbitMQ estabelecida com sucesso.");

                    using (var model = connection.CreateModel())
                    {
                        model.QueueDeclare(
                            queue: RabbitMQSettings.Queue,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        var jsonContent = JsonConvert.SerializeObject(usuario);

                        _logger.LogInformation("Publicando mensagem na fila '{QueueName}'.", RabbitMQSettings.Queue);

                        model.BasicPublish(
                            exchange: string.Empty,
                            routingKey: RabbitMQSettings.Queue,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(jsonContent));

                        _logger.LogInformation("Mensagem publicada com sucesso!");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "### ERRO AO ENVIAR MENSAGEM PARA O RABBITMQ ###");
            
            }
        }
    }
}