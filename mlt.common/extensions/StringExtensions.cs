namespace mlt.common.extensions;

public static class StringExtensions
{
    public static string ToUrlProof(this string destination)
        => destination.Replace("/", "%2F").Replace(" ", "%20");
}