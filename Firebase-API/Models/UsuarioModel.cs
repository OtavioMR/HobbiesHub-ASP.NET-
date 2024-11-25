using System.ComponentModel.DataAnnotations;

namespace Firebase_API.Models
{
    public class UsuarioModel
    {
        // Id pode ser null até ser atribuído um valor
        public string? Id { get; set; }

        // Propriedade NameUsuario não pode ser nula ou vazia
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome de usuário não pode ter mais que 100 caracteres.")]
        public string NameUsuario { get; set; }

        // Propriedade NameSystemUsuario não pode ser nula ou vazia
        [Required(ErrorMessage = "O nome do sistema do usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do sistema do usuário não pode ter mais que 100 caracteres.")]
        public string NameSystemUsuario { get; set; }

        // Validação para o formato de email
        [Required(ErrorMessage = "O email do usuário é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string EmailUsuario { get; set; }

        // Senha não pode ser nula, mas pode ser mantida como string, sem expô-la diretamente
        [Required(ErrorMessage = "A senha do usuário é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string SenhaUsuario { get; set; }

        // Data de nascimento obrigatória, sem permitir valores nulos
        [Required(ErrorMessage = "A data de nascimento do usuário é obrigatória.")]
        public DateTime DateOfBirth { get; set; }

        // Construtor padrão
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
