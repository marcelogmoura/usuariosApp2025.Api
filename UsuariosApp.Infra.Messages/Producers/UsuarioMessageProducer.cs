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
            //conectando / criando a fila
            using (var model = connection.CreateModel())
            {
                //construindo a fila
                model.QueueDeclare(
                queue: RabbitMQSettings.Queue, //nome da fila
                durable: true, //fila não será apagada se o servidor for reiniciado
                autoDelete: false, //não permitir a remoção de mensagens automaticamente                    exclusive: false, //permitir que                    a fila seja compartilhada com outros sistemas
                arguments: null //nenhum argumento adicional
                );
                //serializando os dados do usuário para JSON
                var jsonContent = JsonConvert.SerializeObject(usuario);
                //gravar os dados na fila
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
