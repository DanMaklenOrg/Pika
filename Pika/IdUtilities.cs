using System.Text.RegularExpressions;

namespace Pika;

public static class IdUtilities
{
    private static readonly Regex IdPattern = new(@"^[0-9a-z_]+$");
    private static readonly Regex NonIdCharacter = new(@"[^0-9a-zA-Z]+");

    public static string Normalize(string str)
    {
        str = str.Replace("\'", "");
        return NonIdCharacter.Replace(str, "_").Trim('_').ToLower();
    }

    public static bool IsValidId(string str) => IdPattern.IsMatch(str);
}
