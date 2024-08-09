using System.Net;

namespace Pika.DomainData.DomainScrapper;

public class EntityNameContainer
{
    private readonly HashSet<string> _registeredNames = new HashSet<string>();

    public string RegisterAndNormalize(string name, string discriminator)
    {
        var normalizedName = Normalize(name);
        var discriminatedName = Discriminate(normalizedName, discriminator);

        _registeredNames.Add(normalizedName.ToLower());
        return discriminatedName;
    }

    public static string Normalize(string name)
    {
        return WebUtility.HtmlDecode(name).Trim();
    }

    private string Discriminate(string name, string discriminator)
    {
        return _registeredNames.Contains(name.ToLower()) ? $"{name} ({discriminator})" : name;
    }
}
