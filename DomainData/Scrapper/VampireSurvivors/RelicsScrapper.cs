using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class RelicsScrapper : IScrapper
{
    public DomainId DomainId => "relics.vampire_survivors";
    public string OutputDirectory => "VampireSurvivors";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Vampire Survivors: Relics",
            Entities = await ScrapeRelics(),
        };
    }

    private async Task<List<Entity>> ScrapeRelics()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Relics");
        var nodes = doc.DocumentNode.SelectNodes("//tr");
        return nodes.Skip(2).Select(ParseRelic).ToList();
    }

    private Entity ParseRelic(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[2]").InnerText.Trim();
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new($"relic", DomainId)],
        };
    }
}
