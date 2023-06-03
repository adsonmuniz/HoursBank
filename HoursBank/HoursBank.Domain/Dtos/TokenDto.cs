using System.ComponentModel.DataAnnotations;

namespace HoursBank.Domain.Dtos
{
    public class TokenDto 
    {
        [Required(ErrorMessage = "Token bearer � obrigat�rio")]
        public string Token { get; set; }
    }
}
