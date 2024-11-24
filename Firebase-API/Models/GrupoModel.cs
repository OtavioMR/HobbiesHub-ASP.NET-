using System.Collections.Generic;

namespace Firebase_API.Models
{
    public class GrupoModel
    {
        public string Id { get; set; } = string.Empty; // Inicializar propriedades
        public string NomeGrupo { get; set; } = string.Empty; // Inicializar propriedades
        public string AdministradorId { get; set; } = string.Empty; // Inicializar propriedades
        public List<string> Membros { get; set; } = new List<string>(); // Inicializar propriedades

        public void AdicionarMembro(string usuarioId)
        {
            if (!Membros.Contains(usuarioId))
            {
                Membros.Add(usuarioId);
            }
        }
    }
}
