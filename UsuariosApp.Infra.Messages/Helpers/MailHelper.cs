using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Helpers
{
    public class MailHelper
    {
        public static void SendMessage(string to, string subject, string body)
        {
            //configurações do protocolo SMTP para fazer o envio dos emails
            var smtpClient = new SmtpClient
            (SmtpSettings.Host, SmtpSettings.Port)
            {
                EnableSsl = false
            };

            //Criar a mensagem de email e fazer o envio
            var mailMessage = new MailMessage(SmtpSettings.EmailFrom, to,
            subject, body);
            mailMessage.IsBodyHtml = true;
            //formatando o conteudo do email em HTML
            //enviand o email
            smtpClient.Send(mailMessage);
        }
    }
}