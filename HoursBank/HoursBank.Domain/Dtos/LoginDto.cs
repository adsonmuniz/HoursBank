using System.ComponentModel.DataAnnotations;

namespace HoursBank.Domain.Dtos
{
    public class LoginDto 
    {
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        [StringLength(50, ErrorMessage = "E-mail deve ter no máximo {1} caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(16, ErrorMessage = "Senha deve ter no máximo {1} caracteres")]
        [MinLength(8, ErrorMessage = "Senha deve ter no mínimo {1} caracteres")]
        public string Password { get; set; }
    }
}
