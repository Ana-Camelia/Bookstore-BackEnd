namespace Bookstore.Application.Exceptions
{
    public class DistributorNotFoundException : Exception
    {
        public DistributorNotFoundException() : base() { }
        public DistributorNotFoundException(string message) : base(message) { }
        public DistributorNotFoundException(string message, Exception exc) : base(message, exc) { }
    }
}
