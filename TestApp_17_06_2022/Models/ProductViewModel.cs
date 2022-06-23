using System.Collections.Generic;

namespace TestApp_17_06_2022.Models
{
    public class ProductViewModel
    {
        public List<ProductFormViewModel> ProductList { get; set; } = new List<ProductFormViewModel>();
        
        public List<OrderFormViewModel> OrderList { get; set; } = new List<OrderFormViewModel>();
    }
}