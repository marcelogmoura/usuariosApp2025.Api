using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UsuariosApp.Infra.Messages.Settings
{
    public class SmtpSettings
    {
        public static string Host => "localhost"; //servidor de emails
        public static int Port => 1025; //porta do servidor
        public static string EmailFrom
        => "nao-responda@google.com.br"; //email do remetente
    }
}
