using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Entities;

// using statements...
namespace UsuariosApp.Domain.Helpers
{
    public class JwtHelper
    {
        public static string SecretKey
        => "39E48946-C701-4021-9841-38085F265214";

        public static string CreateToken(Usuario usuario)
        {
            
            var key = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(SecretKey));
            

            var credentials = new SigningCredentials
            (key, SecurityAlgorithms.HmacSha256);
            

            var claims = new[]
            {

            new Claim(ClaimTypes.Name, usuario.Id.ToString())
            };
            
            var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
