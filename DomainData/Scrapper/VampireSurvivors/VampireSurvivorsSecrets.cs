using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class VampireSurvivorsSecrets(EntityNameContainer nameContainer) : IScrapper
{
    public ResourceId DomainId => "vampire_survivors";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => "Secrets";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Vampire Survivors",
            Entities = await ScrapeSecrets(),
        };
    }

    private async Task<List<Entity>> ScrapeSecrets()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Secret");
        var nodes = doc.DocumentNode.SelectNodes("//tr");
        return nodes.Skip(1).Select(ParseSecret).ToList();
    }

    private Entity ParseSecret(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[3]").InnerText;
        name = nameContainer.RegisterAndNormalize(name, "Secret");
        return new Entity
        {
            Id = ResourceId.InduceFromName(name),
            Name = name,
            Class = "secret_unlock",
        };
    }
}
