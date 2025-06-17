using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models.Dtos;
using UsuariosApp.Domain.Services;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("criar")]
        public IActionResult Criar(
            [FromBody] CriarUsuarioRequestDto request)
        {
            try
            {
                var response = _usuarioService.CriarUsuario(request);

                return StatusCode(201, new
                {
                    Message = "Usuário cadastrado com sucesso",
                    Data = response
                });
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody] AutenticarUsuarioRequestDto request)
            
        {
            try
            {
                var response = _usuarioService.AutenticarUsuario(request);

                return StatusCode(200, new
                {
                    Message = "Usuário autenticado com sucesso",
                    Data = response
    });
            }
            catch (ApplicationException e)
            {
                return StatusCode(401, new { e.Message }); // Unauthorized
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }

        }

    }
}
