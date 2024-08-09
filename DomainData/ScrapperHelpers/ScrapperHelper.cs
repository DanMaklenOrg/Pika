using System.Net;
using Pika.Model;

namespace Pika.DomainData.ScrapperHelpers;

public static class ScrapperHelper
{
    public static ResourceId InduceIdFromName(string name, string? idPrefix = null)
    {
        var id = ResourceId.InduceFromName(name);
        if (!string.IsNullOrWhiteSpace(idPrefix)) id = $"{idPrefix}_{id}";
        return id;
    }

    public static string CleanName(string name)
    {
        return WebUtility.HtmlDecode(name).Trim();
    }
}
