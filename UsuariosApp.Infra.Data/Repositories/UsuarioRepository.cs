using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Models.Entities;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Adicionar(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(usuario);
                dataContext.SaveChanges();
            }
        }
        public bool VerificarEmailJaExiste(string email)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Usuario>()
                    .Where(u => u.Email.Equals(email))
                    .Any();
            }
        }

        public Usuario? Obter(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Usuario>()
                    .Where(u => u.Email.Equals(email) && u.Senha.Equals(senha))
                    .FirstOrDefault();
            }
        }
    }
}
