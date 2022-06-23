using TestApp_17_06_2022.Enums;

namespace TestApp_17_06_2022.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        
        public string Login { get; set; }

        public string Password { get; set; }
        
        public Roles Role { get; set; }
    }
}