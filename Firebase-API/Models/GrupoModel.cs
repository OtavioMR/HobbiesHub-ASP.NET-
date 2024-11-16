namespace Firebase_API.Models
{
    public class GrupoModel
    {
        public string Id { get; set; }
        public string NomeGrupo { get; set; }
        public string DescricaoGrupo { get; set; }
        public string HobbyId { get; set; }
        public string AdministradorId { get; set; }
        public List<string> Membros { get; set; }

        public GrupoModel()
        {
            Membros = new List<string>();
        }

        public void AdicionarMembro(string usuarioId)
        {
            if (!Membros.Contains(usuarioId))
            {
                Membros.Add(usuarioId);
            }
        }

        public void RemoverMembro(string usuarioId)
        {
            Membros.Remove(usuarioId);
        }
    }
}
