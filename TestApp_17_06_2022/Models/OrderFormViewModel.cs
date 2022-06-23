namespace TestApp_17_06_2022.Models
{
    public class OrderFormViewModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public double Count { get; set; }
        
        public  decimal Price { get; set; }
        
        public long ProductId { get; set; }

        public decimal TotalPriceWithVAT { get; set; }
    }
}