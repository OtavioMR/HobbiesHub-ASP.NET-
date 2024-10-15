using System.Numerics;
namespace HobbiesHub_API_REST.Models
{
    public class GrupoModel
    {
        public int Id { get; set; } 
        public string NameGrupo { get; set; }
        public string CategoryGrupo { get; set; }
        public string DescriptionGrupo { get;set; }
        public int LimiteUsuariosGrupo { get; set; }
    }
}
