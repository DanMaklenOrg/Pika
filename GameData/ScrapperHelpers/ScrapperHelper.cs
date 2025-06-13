using System.Net;
using Pika.Model;

namespace Pika.GameData.ScrapperHelpers;

public static class ScrapperHelper
{
    public static ResourceId InduceIdFromName(string name, string? idPrefix = null)
    {
        var id = IdUtilities.Normalize(name);
        if (!string.IsNullOrWhiteSpace(idPrefix)) id = $"{idPrefix}_{id}";
        return id;
    }

    public static string CleanName(string name)
    {
        return WebUtility.HtmlDecode(name).Trim(' ', '\n', '\t', '\u00A0', '\u2014');
    }
}
