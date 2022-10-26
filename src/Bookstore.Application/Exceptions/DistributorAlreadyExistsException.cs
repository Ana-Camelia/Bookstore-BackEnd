namespace Bookstore.Application.Exceptions
{
    public class DistributorAlreadyExistsException : Exception
    {
        public DistributorAlreadyExistsException() : base() { }
        public DistributorAlreadyExistsException(string message) : base(message) { }
        public DistributorAlreadyExistsException(string message, Exception exc) : base(message, exc) { }
    }
}
