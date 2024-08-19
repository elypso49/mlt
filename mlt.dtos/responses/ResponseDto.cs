namespace mlt.dtos.responses;

public class ResponseDto<T>
    where T : class
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public List<ResponseError> Errors { get; set; } = new();
}