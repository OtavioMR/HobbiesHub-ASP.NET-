using HobbiesHub_API_REST.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbiesHub_API_REST.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            // Definindo a chave primária
            builder.HasKey(x => x.Id);

            // Configurando as propriedades
            builder.Property(x => x.UsuarioName)
                .IsRequired()
                .HasMaxLength(100); // Correção para HasMaxLength

            builder.Property(x => x.UsuarioEmail)
                .IsRequired()
                .HasColumnType("TEXT"); // Coluna de texto para emails

            builder.Property(x => x.UsuarioNameSystem)
                .IsRequired()
                .HasMaxLength(100); // Correção para HasMaxLength

            builder.Property(x => x.UsuarioSenhaHash)
                .IsRequired()
                .HasMaxLength(255); // Defina um comprimento adequado para a senha hash

            builder.Property(x => x.UsuarioAge)
                .IsRequired(); // Ajuste conforme necessário

            builder.Property(x => x.UsuarioDateCadastro)
                .IsRequired(); // Ajuste conforme necessário

            // Outras configurações se necessário
        }
    }
}
