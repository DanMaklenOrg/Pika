using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.Hades;

public class HadesInfernalArms(EntityNameContainer nameContainer) : IScrapper
{
    public DomainId DomainId => "hades";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => "InfernalArms";

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
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Infernal_Arms");
        var nodes = doc.DocumentNode.SelectNodes("(//table[contains(@class,'wikitable')])[1]/tbody/tr/td[2]");
        return nodes.Select(n =>
        {
            var name = n.InnerText;
            name = name.Split('/').First().Replace("\"", "");
            name = nameContainer.RegisterAndNormalize(name, "Infernal Arms");
            return new Entity
            {
                Id = ResourceId.InduceFromName(name, DomainId),
                Name = name,
                Classes = [new ResourceId("infernal_arm", DomainId)],
            };
        }).ToList();
    }
}
