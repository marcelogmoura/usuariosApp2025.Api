using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// using statements...
namespace UsuariosApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {     
            optionsBuilder.UseSqlServer("Data Source=localhost,1434;Initial Catalog=master;User ID=sa;Password=SenhaForte@2025;Encrypt=False;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new Mappings.UsuarioMap());
            modelBuilder.ApplyConfiguration(new Mappings.PermissaoMap());
            modelBuilder.ApplyConfiguration(new Mappings.UsuarioPermissaoMap());
        }
    }
}
