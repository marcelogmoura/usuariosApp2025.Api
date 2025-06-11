using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// using statements...
namespace UsuariosApp.Domain.Models.Dtos
{
    public class CriarUsuarioRequestDto
    {
        [MaxLength(20, ErrorMessage = "Máximo de {1} caracteres")]
        [MinLength(3, ErrorMessage = "Mínimo de {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Required(ErrorMessage = "Informe o e-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string? Senha { get; set; }
    }
}
