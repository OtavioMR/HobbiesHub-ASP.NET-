namespace HobbiesHub_API_REST.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string UsuarioName { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioNameSystem { get; set; }  
        public string UsuarioSenhaHash { get; set; }
        public int UsuarioAge { get; set; }
        public int DateCadastrro { get; set; }
    }
}
