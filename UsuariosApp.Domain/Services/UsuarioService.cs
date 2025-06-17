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
            if ( _usuarioRepository.VerificarEmailJaExiste(request.Email))
                throw new Exception("Email já cadastrado");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                Senha = CryptoHelper.EncryptSHA256(request.Senha)
            };
            _usuarioRepository.Adicionar(usuario);

            var usuarioMessage = new UsuarioMessageDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCadastro = DateTime.Now
            };
            _usuarioMessage.EnviarMensagem(usuarioMessage);


            var response = new CriarUsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCadastro = DateTime.Now
            };

            return response;
        }
    }
}
