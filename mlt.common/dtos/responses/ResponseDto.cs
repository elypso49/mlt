namespace mlt.common.dtos.responses;

public class ResponseDto<T>
    where T : class?
{
    public T? Data { get; set; }
    public List<ResponseError> Errors { get; set; } = [];
    public bool IsSuccess => Errors.Any() == false;
}