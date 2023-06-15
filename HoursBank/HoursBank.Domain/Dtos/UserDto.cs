using System.ComponentModel.DataAnnotations;

namespace HoursBank.Domain.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage="E-mail é obrigatório")]
        [StringLength(50, ErrorMessage = "E-mail deve ter no máximo {1} caracteres")]
        public string Email { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Password { get; set; }
        public long Hours { get; set; }
        public bool Admin { get; set; }
        public bool? Active { get; set; }
        public int? TeamId { get; set; }
    }
}
