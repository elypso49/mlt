namespace mlt.dtos.responses;

public class ResponseError
{
    public string Message { get; set; }
    public string Code { get; set; }
    public string Source { get; set; }
    public string StackTrace { get; set; }
}