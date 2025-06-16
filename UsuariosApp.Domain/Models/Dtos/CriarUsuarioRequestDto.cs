using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UsuariosApp.Domain.Models.Dtos
{
    public class CriarUsuarioRequestDto
    {
        [MaxLength(20, ErrorMessage = "No máximo {1} caracteres")]
        [MinLength(3, ErrorMessage = "No mínimo {1} caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Nome { get; set; }
                
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string? Senha { get; set; }
    }
}


