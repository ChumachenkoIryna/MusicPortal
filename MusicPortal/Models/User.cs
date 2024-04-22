using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        [Range(0, 2)]
        public int Level { get; set; }
        public string? Salt { get; set; }
    }
}
