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
            Stats = [await ScrapeVillagerCareerLevelStat()],
            Entities = await ScrapeVillagerProfessions(),
        };
    }

    private async Task<Stat> ScrapeVillagerCareerLevelStat()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://minecraft.fandom.com/wiki/Trading");
        var tableNode = doc.DocumentNode.SelectSingleNode("//table[@data-description='Villager trade levels']");
        var levelNodes = tableNode.SelectNodes(".//tr/td[3]");
        var careerLevels = levelNodes.Select(node => node.InnerText.Trim()).ToList();
        return new Stat
        {
            Id = new ResourceId("career_level", DomainId),
            Name = "Villager Career Level",
            Type = StatType.OrderedEnum,
            EnumValues = careerLevels,
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
            Stats =
            [
                new ResourceId("villager_moved", DomainId),
                new ResourceId("career_level", DomainId),
                new ResourceId("unzombified", DomainId)
            ]
        };
    }
}
