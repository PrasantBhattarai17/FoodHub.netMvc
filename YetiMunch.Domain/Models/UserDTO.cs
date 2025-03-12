using System.ComponentModel.DataAnnotations;

namespace YetiMunch.Models
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

    }
}
