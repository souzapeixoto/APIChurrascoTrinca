using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
