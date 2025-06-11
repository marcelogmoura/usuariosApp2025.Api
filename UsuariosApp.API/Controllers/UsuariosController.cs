using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models.Dtos;


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
        public IActionResult Criar
            ([FromBody] CriarUsuarioRequestDto request)
        {
            try
            {
                var response = _usuarioService.CriarUsuario(request);

                return StatusCode(201, new
                {
                    Message = "Usuário criado com sucesso.",
                    Data = response
                });
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { Message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro inesperado.", Details = e.Message });
            }
        }

        [HttpPost("autenticar")]
        public IActionResult Autenticar()
        {
            return Ok();
        }
    }


}
