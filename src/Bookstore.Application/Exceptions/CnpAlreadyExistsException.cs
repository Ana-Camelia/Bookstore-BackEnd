namespace Bookstore.Application.Exceptions
{
    public class CnpAlreadyExistsException : Exception
    {
        public CnpAlreadyExistsException() : base() { }
        public CnpAlreadyExistsException(string message) : base(message) { }
        public CnpAlreadyExistsException(string message, Exception exc) : base(message, exc) { }
    }
}
