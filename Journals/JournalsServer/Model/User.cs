using System.ComponentModel.DataAnnotations;

namespace JournalsServer.Model
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
