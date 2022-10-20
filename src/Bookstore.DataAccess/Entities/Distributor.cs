namespace Bookstore.DataAccess.Entities
{
    public class Distributor : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
