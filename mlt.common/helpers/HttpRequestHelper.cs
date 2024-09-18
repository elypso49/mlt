namespace mlt.common.helpers;

public static class HttpRequestHelper
{
    private const int MaxRetries = 5;
    private const int InitialDelayMilliseconds = 1000;
    
    public static async Task<TResponse?> DoActionWithRetry<TResponse>(Func<Task<TResponse>> action)
        where TResponse : class?
    {
        var retries = 0;

        while (retries < MaxRetries)
        {
            try
            {
                return await action();
            }
            catch (HttpRequestException ex) when ((int)ex.StatusCode! == 429)
            {
                retries++;
                var delay = InitialDelayMilliseconds * (int)Math.Pow(2, retries);
                await Task.Delay(delay);
            }
        }

        throw new Exception("Failed to process batch after multiple retries.");
    }
}