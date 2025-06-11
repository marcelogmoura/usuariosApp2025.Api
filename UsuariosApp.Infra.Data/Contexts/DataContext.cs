using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Data.Mappings;


namespace UsuariosApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=\"localhost, 1434\";Initial " +
                "Catalog=master;User ID=sa;Password=Senha#Forte123;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PermissaoMap());
            modelBuilder.ApplyConfiguration(new UsuarioPermissaoMap());
        }
    }
}
