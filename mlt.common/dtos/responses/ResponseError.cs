namespace mlt.common.dtos.responses;

public class ResponseError
{
    public string Message { get; set; } = null!;
    public string? Code { get; set; }
    public string? Source { get; set; }
    public string? StackTrace { get; set; }
}