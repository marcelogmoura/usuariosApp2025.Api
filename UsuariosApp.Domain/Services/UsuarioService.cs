using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models.Dtos;
using UsuariosApp.Domain.Models.Entities;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioMessage _usuarioMessage;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUsuarioMessage usuarioMessage)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioMessage = usuarioMessage;
        }

        public CriarUsuarioResponseDto CriarUsuario(CriarUsuarioRequestDto request)
        {
            if (_usuarioRepository.VerificarSeEmailJaExiste(request.Email))
                throw new ArgumentException("E-mail já cadastrado.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                Senha = CryptoHelper.EncryptSHA256(request.Senha)                
            };

            _usuarioRepository.Adicionar(usuario);

            #region Enviar mensagem para a mensageria   

            var usuarioMessage = new UsuarioMessageDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCadastro = DateTime.UtcNow
            };

            _usuarioMessage.EnviarMensagem(usuarioMessage);

            #endregion


            var response = new CriarUsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCadastro = DateTime.UtcNow
            };

            return response;
        }
    }
}
