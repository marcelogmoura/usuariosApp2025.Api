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
        public void EnviarMensagem(UsuarioMessageDto usuario)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = RabbitMQSettings.Host,
                Port = RabbitMQSettings.Port,
                UserName = RabbitMQSettings.User,
                Password = RabbitMQSettings.Pass,
                VirtualHost = RabbitMQSettings.VHost
            };

            using var connection = connectionFactory.CreateConnection();

            using (var model = connection.CreateModel())
            {

                model.QueueDeclare(
                queue: RabbitMQSettings.Queue,
                durable: true, // fila n sera apagada se o servidor reiniciar
                autoDelete: false, // n permitir remocao de mensagens automaticamente
                arguments: null
                );

                var jsonContent = JsonConvert.SerializeObject(usuario);

                model.BasicPublish(
                exchange: string.Empty,
                routingKey: RabbitMQSettings.Queue,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(jsonContent)
                );
            }
        }
    }
}
