using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Dtos.Users
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(Constants.Constants.MaxNameSize)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(Constants.Constants.MaxEmailSize)]
        public string Email { get; set; }
        [Required]
        [MaxLength(Constants.Constants.MaxPasswordSize)]
        public string Password { get; set; }
    }
}
