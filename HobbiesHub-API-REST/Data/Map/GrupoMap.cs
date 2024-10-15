using HobbiesHub_API_REST.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HobbiesHub_API_REST.Data.Map
{
    public class GrupoMap : IEntityTypeConfiguration<GrupoModel>
    {
        public void Configure(EntityTypeBuilder<GrupoModel> builder)
        {
            // Definindo a chave primária
            builder.HasKey(x => x.Id);

            // Configurando as propriedades
            builder.Property(x => x.NameGrupo)
                .IsRequired()
                .HasMaxLength(100); // Correção para HasMaxLength

            builder.Property(x => x.CategoryGrupo)
                .IsRequired()
                .HasMaxLength(100); // Correção para HasMaxLength

            builder.Property(x => x.DescriptionGrupo)
                .IsRequired()
                .HasMaxLength(255); // Defina um comprimento adequado para a senha hash

            builder.Property(x => x.LimiteUsuariosGrupo)
                .IsRequired(); // Ajuste conforme necessário

            // Outras configurações se necessário
        }
    }
}
