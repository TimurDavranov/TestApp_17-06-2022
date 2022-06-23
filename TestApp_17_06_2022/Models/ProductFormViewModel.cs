using System.ComponentModel.DataAnnotations;

namespace TestApp_17_06_2022.Models
{
    public class ProductFormViewModel
    {
        public long Id { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}