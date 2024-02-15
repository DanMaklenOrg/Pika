using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.MinecraftATM9;

public class MinecraftScrapper : IScrapper
{
    public DomainId DomainId => "minecraft_atm9";
    public string OutputDirectory => "MinecraftATM9";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Entities = await ScrapeVillagerProfessions(),
        };
    }

    private async Task<List<Entity>> ScrapeVillagerProfessions()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://minecraft.fandom.com/wiki/Trading");
        var nodes = doc.DocumentNode.SelectNodes("//h3[preceding-sibling::h2[1][span/.='Java Edition offers']]/span[1]");
        return nodes.Select(ParseVillagerProfession).ToList();
    }

    private Entity ParseVillagerProfession(HtmlNode node)
    {
        var name = $"Villager ({node.InnerText})";
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = new List<ResourceId> { new("villager", DomainId) },
        };
    }
}
