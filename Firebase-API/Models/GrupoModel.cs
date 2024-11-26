using System;
using System.Collections.Generic;

namespace Firebase_API.Models
{
    public class GrupoModel
    {
        public string Id { get; set; } = string.Empty;
        public string NomeGrupo { get; set; } = string.Empty;
        public string DescricaoGrupo { get; set; } = string.Empty; // Descrição do grupo
        public string HobbyId { get; set; } = string.Empty;
        public string AdministradorId { get; set; } = string.Empty; // ID do administrador do grupo
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow; // Data de criação do grupo
        public List<string> Membros { get; set; } = new List<string>();
        public List<ChatMessageModel> Mensagens { get; set; } = new List<ChatMessageModel>(); // Mensagens do grupo

        public void AdicionarMembro(string usuarioId)
        {
            if (!Membros.Contains(usuarioId))
            {
                Membros.Add(usuarioId);
            }
        }

        public void AdicionarMensagem(ChatMessageModel mensagem)
        {
            Mensagens.Add(mensagem);
        }
    }
}
