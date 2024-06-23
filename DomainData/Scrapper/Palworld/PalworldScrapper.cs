using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.Palworld;

public class PalworldScrapper(EntityNameContainer nameContainer) : IScrapper
{
    public DomainId DomainId => "palworld";
    public string OutputDirectory => "Palworld";
    public string FileName => DomainId.ToString();

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Palworld",
            Entities = await ScrapePals(),
        };
    }

    private async Task<List<Entity>> ScrapePals()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://palworld.wiki.gg/wiki/Pals");
        var nodes = doc.DocumentNode.SelectNodes("//td/span[@class='iconlink']/../..");
        return nodes.Select(ParsePal).Where(x => x is not null).Cast<Entity>().ToList();
    }

    private Entity? ParsePal(HtmlNode node)
    {
        var name = nameContainer.RegisterAndNormalize(node.SelectSingleNode("td/span").InnerText, "Pal");
        var index = node.SelectSingleNode("td[2]").InnerText.Trim();

        List<string> blacklist = ["Gumoss (Special)", "Gorirat Terra", "Chillet Ignis"];

        if (blacklist.Contains(name)) return null;
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = $"{index}: {name}",
            Classes = [new ResourceId("pal", DomainId)],
        };
    }
}
