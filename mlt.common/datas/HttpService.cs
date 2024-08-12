using System.Text;
using System.Text.Json;

namespace mlt.common.datas;

public class HttpService(JsonSerializerOptions jsonOptions)
{
    public async Task<TResponse> GetAsync<TResponse>(string url)
    {
        try
        {
            // Create a handler that ignores SSL certificate errors
            var handler = new HttpClientHandler
                          {
                              ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                          };

            // Create an HttpClient with the custom handler
            using var httpClient = new HttpClient(handler);
            var response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TResponse>(json, jsonOptions);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        try
        {
            // Create a handler that ignores SSL certificate errors
            var handler = new HttpClientHandler
                          {
                              ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                          };

            // Create an HttpClient with the custom handler
            using var httpClient = new HttpClient(handler);

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await httpClient.PostAsync(url, content);

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