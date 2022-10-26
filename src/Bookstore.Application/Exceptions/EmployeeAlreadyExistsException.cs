namespace Bookstore.DataAccess.Exceptions
{
    public class EmployeeAlreadyExistsException : Exception
    {
        public EmployeeAlreadyExistsException() : base() { }
        public EmployeeAlreadyExistsException(string message) : base(message) { }
        public EmployeeAlreadyExistsException(string message, Exception exc) : base(message, exc) { }
    }
}
