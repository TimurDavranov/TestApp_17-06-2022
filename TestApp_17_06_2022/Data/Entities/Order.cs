namespace TestApp_17_06_2022.Data.Entities
{
    public class Order
    {
        public long Id { get; set; }
        
        public long ProductId { get; set; }
        
        public Product Product { get; set; }
        
        public double Count { get; set; }
        
        public decimal Price { get; set; }
    }
}