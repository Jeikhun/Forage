using System.Net;

namespace Forage.Service.Exceptions
{
    public class NotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public NotFoundException(string parameterName) : base(parameterName)
        {
            ErrorMessage = $"Element Not Found: {parameterName}";
        }

        public NotFoundException(string errorMessage, Exception? innerException) : base(errorMessage, innerException)
        {
            ErrorMessage = errorMessage;
        }
    }
}
