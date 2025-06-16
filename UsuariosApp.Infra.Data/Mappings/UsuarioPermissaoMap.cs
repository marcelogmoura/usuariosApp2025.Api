using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Entities;


namespace UsuariosApp.Infra.Data.Mappings
{
    public class UsuarioPermissaoMap : IEntityTypeConfiguration<UsuarioPermissao>
    {
        public void Configure(EntityTypeBuilder<UsuarioPermissao> builder)
        {
            builder.ToTable("USUARIO_PERMISSAO");

            builder.HasKey(up => new
            {
                up.UsuarioId,
                up.PermissaoId
            });

            builder.Property(up => up.UsuarioId)
                .HasColumnName("USUARIO_ID")
                .IsRequired();

            builder.Property(up => up.PermissaoId)
                .HasColumnName("PERMISSAO_ID");

            builder.HasOne(up => up.Usuario)
                .WithMany(u => u.Permissoes)
                .HasForeignKey(up => up.UsuarioId);

            builder.HasOne(up => up.Permissao)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(up => up.PermissaoId);
        }
    }
}
