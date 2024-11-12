using System.ComponentModel.DataAnnotations;

namespace Firebase_API.Models
{
    public class UsuarioModel
    {
        public string? Id { get; set; }
        public string NameUsuario { get; set; }
        public string NameSystemUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public DateTime DateOfBirth { get; set; }

        public UsuarioModel() { }

        // Construtor com parâmetros
        public UsuarioModel(string nameUsuario, string nameSystemUsuario, string emailUsuario, string senhaUsuario, DateTime dateOfBirth)
        {
            NameUsuario = nameUsuario;
            NameSystemUsuario = nameSystemUsuario;
            EmailUsuario = emailUsuario;
            SenhaUsuario = senhaUsuario; // Senha já com hash
            DateOfBirth = dateOfBirth;
        }
    }
}
