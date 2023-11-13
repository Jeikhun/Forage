namespace Forage.Service.Dtos.Common.ResponsesDtos
{
    public record BaseReponse
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public Guid Id { get; set; }
    }
}
