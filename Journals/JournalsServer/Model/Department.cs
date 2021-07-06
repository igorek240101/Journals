using System.ComponentModel.DataAnnotations;

namespace JournalsServer.Model
{
    public class Department
    {
        [Required]
        public sbyte Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
