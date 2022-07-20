using System.ComponentModel.DataAnnotations;

namespace HoursBank.Domain.Dtos
{
    public class LoginDto 
    {
        [Required(ErrorMessage = "E-mail � obrigat�rio")]
        [EmailAddress(ErrorMessage = "E-mail em formato inv�lido")]
        [StringLength(50, ErrorMessage = "E-mail deve ter no m�ximo {1} caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha � obrigat�ria")]
        [StringLength(16, ErrorMessage = "Senha deve ter no m�ximo {1} caracteres")]
        [MinLength(8, ErrorMessage = "Senha deve ter no m�nimo {1} caracteres")]
        public string Password { get; set; }
    }
}
