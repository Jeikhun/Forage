namespace Forage.Service.Dtos.Common.ResponsesDtos
{
    public record GeneralResponse<T> : BaseReponse
    {
        public T Data { get; set; }
        public string? Url { get; set; }
    }
}
