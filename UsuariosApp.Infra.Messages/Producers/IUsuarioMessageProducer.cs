using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Models.Dtos;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Producers
{
    public class IUsuarioMessageProducer : IUsuarioMessage
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

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    model.QueueDeclare(
                        queue: RabbitMQSettings.Queue,
                        durable: true, // fila persistente, n apaga
                        exclusive: false, // compartilhada entre aplicações
                        autoDelete: false, // n apaga quando n tiver mais consumidores
                        arguments: null); 

                    var jsonContent = JsonConvert.SerializeObject(usuario);

                    model.BasicPublish(
                        exchange: string.Empty, 
                        routingKey: RabbitMQSettings.Queue, 
                        basicProperties: null,  
                        body: Encoding.UTF8.GetBytes(jsonContent));
                }
            }
        }
    }
}
