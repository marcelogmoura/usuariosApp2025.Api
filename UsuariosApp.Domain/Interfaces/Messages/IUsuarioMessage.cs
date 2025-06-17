using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Dtos;

namespace UsuariosApp.Domain.Interfaces.Messages
{
    public interface IUsuarioMessage
    {
        void EnviarMensagem(UsuarioMessageDto usuario);
    }
}
