using System.Numerics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalsServer.Model
{
    public class User
    {
        public ulong Id { get; set; }
        
        [Required]
        public string Login { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public sbyte Position { get; set; } // 0 - админ, модуль числа - номер подразделения, знак числа - (отрицательный - руководитель), (положительный - работник)
    }
}
