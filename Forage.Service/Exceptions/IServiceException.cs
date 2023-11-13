using System.Net;

namespace Forage.Service.Exceptions
{
    public interface IServiceException
    {
        HttpStatusCode StatusCode { get; }
        string ErrorMessage { get; }
        virtual string? ErrorDetail { get => "Not any error detail"; }
    }
}
