using System.ComponentModel.DataAnnotations;

namespace HoursBank.Domain.Dtos
{
    public class TokenDto 
    {
        [Required(ErrorMessage = "Token bearer é obrigatório")]
        public string Token { get; set; }
    }
}
