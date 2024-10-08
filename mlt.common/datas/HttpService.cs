﻿using System.Net.Http.Headers;
using System.Text;
using mlt.common.helpers;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace mlt.common.datas;

public abstract class HttpService
{
    private readonly HttpClient _client = new(new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true });

    protected HttpService(string baseAddress, string? bearerToken = null)
    {
        _client.BaseAddress = new Uri(baseAddress);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
    }

    protected async Task<TResponse?> GetAsync<TResponse>(string endpoint)
        where TResponse : class
        => await HttpRequestHelper.DoActionWithRetry(async () =>
                                                 {
                                                     var response = await _client.GetAsync(endpoint);

                                                     response.EnsureSuccessStatusCode();

                                                     var json = await response.Content.ReadAsStringAsync();

                                                     return JsonConvert.DeserializeObject<TResponse>(json)!;
                                                 });

    protected async Task<TResponse?> PutAsync<TResponse>(string endpoint, string fileUrl)
        where TResponse : class
        => await HttpRequestHelper.DoActionWithRetry(async () =>
                                                 {
                                                     using var downloadResponse = await _client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
                                                     downloadResponse.EnsureSuccessStatusCode();

                                                     await using var contentStream = await downloadResponse.Content.ReadAsStreamAsync();
                                                     using var requestContent = new StreamContent(contentStream);
                                                     requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                                                     using var response = await _client.PutAsync(endpoint, requestContent);
                                                     response.EnsureSuccessStatusCode();

                                                     var responseJson = await response.Content.ReadAsStringAsync();

                                                     return string.IsNullOrWhiteSpace(responseJson) ? null : JsonConvert.DeserializeObject<TResponse>(responseJson)!;
                                                 });

    protected Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        where TResponse : class
        => DoPostAsync<TResponse?>(endpoint, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));

    protected Task<TResponse?> PostAsync<TResponse>(string endpoint, string data)
        where TResponse : class
        => DoPostAsync<TResponse?>(endpoint, new StringContent(data, Encoding.UTF8, "text/plain"));

    protected Task<TResponse?> PostAsync<TResponse>(string endpoint, Dictionary<string, string> data)
        where TResponse : class
    {
        var formContent = new MultipartFormDataContent();

        foreach (var item in data)
            formContent.Add(new StringContent(item.Value), item.Key);

        return DoPostAsync<TResponse?>(endpoint, formContent);
    }

    private async Task<TResponse?> DoPostAsync<TResponse>(string endpoint, HttpContent content)
        where TResponse : class?
        => await HttpRequestHelper.DoActionWithRetry(async () =>
                                                 {
                                                     var response = await _client.PostAsync(endpoint, content);

                                                     response.EnsureSuccessStatusCode();

                                                     var responseJson = await response.Content.ReadAsStringAsync();

                                                     return string.IsNullOrWhiteSpace(responseJson) ? null : JsonConvert.DeserializeObject<TResponse>(responseJson)!;
                                                 });
}