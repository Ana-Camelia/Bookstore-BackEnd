namespace Bookstore.Application.Models.Employee
{
    public class EmployeeRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

    }
}
