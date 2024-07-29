using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.Hades;

public class HadesKeepsakes(EntityNameContainer nameContainer) : IScrapper
{
    public DomainId DomainId => "hades";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => "Keepsakes";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Hades",
            Entities = await ScrapeKeepsakes(),
        };
    }

    private async Task<List<Entity>> ScrapeKeepsakes()
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
