using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Dtos;

namespace UsuariosApp.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        CriarUsuarioResponseDto CriarUsuario(CriarUsuarioRequestDto request);

        AutenticarUsuarioResponseDto AutenticarUsuario(AutenticarUsuarioRequestDto request);
    }
}
