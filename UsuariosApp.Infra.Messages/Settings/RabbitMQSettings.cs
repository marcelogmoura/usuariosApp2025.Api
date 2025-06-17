using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UsuariosApp.Infra.Messages.Settings
{
    public class RabbitMQSettings
    {
        public static string Host => "localhost"; //servidor do RabbitMQ
        public static int Port => 5672; //porta para conexão com o servidor
        public static string User => "guest"; //usuário de acesso
        public static string Pass => "guest"; //senha de acesso
        public static string VHost => "/"; //endereço virtual do servidor
        public static string Queue => "Usuarios"; //nome da fila
    }
}
