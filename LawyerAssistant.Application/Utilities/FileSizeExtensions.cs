namespace Application.Utilities;

public static class FileSizeExtensions
{
    public static string ToFileSizeString(this long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        int counter = 0;
        decimal number = bytes;

        while (Math.Round(number / 1024) >= 1)
        {
            number /= 1024;
            counter++;
        }

        return $"{number:n2} {suffixes[counter]}";
    }

    // Optional: For int values
    public static string ToFileSizeString(this int bytes)
    {
        return ((long)bytes).ToFileSizeString();
    }
}