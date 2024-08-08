using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.Hades;

public class HadesFates(EntityNameContainer nameContainer) : IScrapper
{
    public DomainId DomainId => "hades";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => "Fates";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Hades",
            Entities = await ScrapeEntities(),
        };
    }

    private async Task<List<Entity>> ScrapeEntities()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Fated_List_of_Minor_Prophecies");
        var nodes = doc.DocumentNode.SelectNodes("//table[contains(@class, 'wikitable')]/tbody/tr/td[1]");
        return nodes.Select(n =>
        {
            var name = nameContainer.RegisterAndNormalize(n.InnerText, "Fate");
            return new Entity
            {
                Id = ResourceId.InduceFromName(name, DomainId),
                Name = name,
                Classes = [new ResourceId("fate", DomainId)],
            };
        }).ToList();
    }
}
