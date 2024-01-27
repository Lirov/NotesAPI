using System.ComponentModel.DataAnnotations;

namespace NotesAPI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
