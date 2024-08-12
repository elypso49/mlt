using System.Text.Json;

namespace mlt.common.helpers;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions
                                                                  {
                                                                      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                                                      PropertyNameCaseInsensitive = true
                                                                  };
}