using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class VampireSurvivorsSecrets(EntityNameContainer nameContainer) : IScrapper
{
    public DomainId DomainId => "vampire_survivors";
    public string OutputDirectory => "VampireSurvivors";
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
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new ResourceId("secret_unlock", DomainId)],
        };
    }
}
