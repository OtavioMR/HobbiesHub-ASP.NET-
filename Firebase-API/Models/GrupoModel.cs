namespace Firebase_API.Models
{
    public class GrupoModel
    {
        public string Id {  get; set; }
        public string NomeGrupo { get; set; }
        public string DescricaoGrupo { get; set; }
        public string CategoriaGrupo { get; set; }
        public List<UsuarioModel> Membros { get; set; }

        public GrupoModel()
        {
            Membros = new List<UsuarioModel>();
        }

        // Construtor com parâmetros 
        public GrupoModel(string nomeGrupo, string descricaoGrupo, string categoriaGrupo)
        {
            NomeGrupo = nomeGrupo;
            DescricaoGrupo = descricaoGrupo;
            CategoriaGrupo = categoriaGrupo;
        }

        // Método para adicionar um usuário ao grupo
        public void AdicionarMembrio(UsuarioModel usuario)
        {
            if (!Membros.Contains(usuario))
            {
                Membros.Add(usuario);
            }
        }
    }
}
