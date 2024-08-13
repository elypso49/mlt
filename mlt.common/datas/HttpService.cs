namespace mlt.common.datas;

public abstract class HttpService(JsonSerializerOptions jsonOptions)
{
    private readonly HttpClient _client = new(new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true });

    protected void SetBearerToken(string token) => _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    protected async Task<TResponse> GetAsync<TResponse>(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(json, jsonOptions)!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }

    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseJson, jsonOptions)!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }
}