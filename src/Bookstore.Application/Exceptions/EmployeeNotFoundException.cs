namespace Bookstore.DataAccess.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException() : base() { }
        public EmployeeNotFoundException(string message) : base(message) { }
        public EmployeeNotFoundException(string message, Exception exc) : base(message, exc) { }
    }
}
