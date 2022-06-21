using System.Collections.Generic;

namespace TestApp_17_06_2022.Data.Entities
{
    public class Product
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}