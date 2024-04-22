using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username must be filled")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password must be filled")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
