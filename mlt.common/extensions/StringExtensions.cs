namespace mlt.common.extensions;

public static class StringExtensions
{
    public static string ToUrlSafeString(this string destination)
        => Uri.EscapeDataString(destination);

    public static string RemoveUnsafeFolderCharacters(this string folderName)
    {
        char[] invalidChars = ['\\', ':', '*', '?', '"', '<', '>', '|', '\0'];
        var safeString = new string(folderName.Where(c => !invalidChars.Contains(c)).ToArray());

        return safeString.Replace("  ", " ");
    }
    
    
    public static string CleanTitle(this string title)
    {
        char[] invalidChars = ['-', ' '];
        var safeString = new string(title.Where(c => !invalidChars.Contains(c)).ToArray());

        return safeString.Replace("  ", " ").ToUpperInvariant();
    }
}