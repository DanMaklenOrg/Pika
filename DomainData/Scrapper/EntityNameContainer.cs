using System.Net;
using HtmlAgilityPack;

namespace Pika.DomainData.Scrapper;

public class EntityNameContainer
{
    private readonly HashSet<string> _registeredNames = new HashSet<string>();

    public string RegisterAndNormalize(string name, string discriminator)
    {
        var normalizedName = Normalize(name);
        var discriminatedName = Discriminate(normalizedName, discriminator);

        _registeredNames.Add(normalizedName);
        return discriminatedName;
    }

    public static string Normalize(string name)
    {
        return WebUtility.HtmlDecode(name.Trim());
    }

    private string Discriminate(string name, string discriminator)
    {
        return _registeredNames.Contains(name) ? $"{name} ({discriminator})" : name;
    }
}
