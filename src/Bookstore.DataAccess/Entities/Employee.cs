using System.Data;

namespace Bookstore.DataAccess.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
