namespace Bookstore.DataAccess.Exceptions
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException() : base() {}
        public RoleNotFoundException(string message) : base(message) {}
        public RoleNotFoundException(string message, Exception exc) : base(message, exc) {}

    }
}
