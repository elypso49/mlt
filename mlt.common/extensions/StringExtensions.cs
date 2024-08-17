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
}