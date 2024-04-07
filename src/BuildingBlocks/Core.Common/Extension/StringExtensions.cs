namespace Core.Common.Extension;

public static class StringExtensions
{
    public static List<string> AsList(this string value) => new List<string> { value };
}
