using System.ComponentModel.DataAnnotations;

namespace NotesAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }
    }
}
