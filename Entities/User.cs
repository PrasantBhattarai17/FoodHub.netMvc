using System.ComponentModel.DataAnnotations;

namespace YetiMunch.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordH { get; set; }





    }
}
