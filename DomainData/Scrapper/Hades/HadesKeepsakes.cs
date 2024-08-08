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
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Keepsakes");
        var nodes = doc.DocumentNode.SelectNodes("//table[@class='fandom-table']/tbody/tr/td[1]");
        return nodes.Select(n =>
        {
            var name = nameContainer.RegisterAndNormalize(n.InnerText, "Keepsake");
            return new Entity
            {
                Id = ResourceId.InduceFromName(name, DomainId),
                Name = name,
                Classes = [new ResourceId("keepsake", DomainId)],
            };
        }).ToList();
    }
}
