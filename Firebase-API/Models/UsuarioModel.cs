namespace Firebase_API.Models
{
    public class UsuarioModel
    {
        public string Id { get; set; } // ID do usuário
        public string NameUsuario { get; set; } // Nome do usuário
        public string NameSystemUsuario { get; set; } // Nome de usuário
        public string EmailUsuario { get; set; } // Email do usuário
        public string SenhaUsuario { get; set; } // Senha do usuário (lembre-se de usar hash)
        public DateTime DateOfBirth { get; set; } // Data de nascimento do usuário

        public UsuarioModel() { }

        // Construtor com parâmetros
        public UsuarioModel(string nameUsuario, string nameSystemUsuario, string emailUsuario, string senhaUsuario, DateTime dateOfBirth)
        {
            NameUsuario = nameUsuario;
            NameSystemUsuario = nameSystemUsuario;
            EmailUsuario = emailUsuario;
            SenhaUsuario = senhaUsuario; // Lembre-se de aplicar hash aqui
            DateOfBirth = dateOfBirth;
        }
    }
}
