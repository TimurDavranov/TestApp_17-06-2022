using System.ComponentModel.DataAnnotations;

namespace TestApp_17_06_2022.Models
{
    public class LoginViewModel
    {
        public long Id { get; set; }
        
        [MinLength(3)]
        [Required]
        public string Login { get; set; }
        
        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        public bool IsReg { get; set; } = false;

    }
}