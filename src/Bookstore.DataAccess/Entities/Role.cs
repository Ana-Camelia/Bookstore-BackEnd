namespace Bookstore.DataAccess.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
