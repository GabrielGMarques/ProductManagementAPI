namespace ProductManagement.Api.Dtos
{
    public class ResultDto<T>
    {
        public ResultDto(T? data, string? message = null)
        {
            Data = data;
            Message = message;
        }
        public T? Data { get; set; }

        public string? Message { get; set; }
    }
}
