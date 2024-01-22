using HtmlAgilityPack;
using Pika.Model;

namespace Pika.GameData.Scrapper.MinecraftATM9;

public class IronSpellsNSpellbooksScrapper : IScrapper
{
    public DomainId DomainId => "iron_spells_n_spellbooks.minecraft_atm9";

    public string OutputDirectory => $"MinecraftATM9";

    public async Task<Domain> Scrape()
    {
        var spells = await ScrapeSpells();
        return new Domain
        {
            Id = DomainId,
            Entities = spells,
        };
    }

    private async Task<List<Entity>> ScrapeSpells()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://iron.wiki/spells/");
        var spellNodes = doc.DocumentNode.SelectNodes("//div[@class='spell-container']");
        return spellNodes.Select(this.ParseSpell).ToList();
    }

    private Entity ParseSpell(HtmlNode node)
    {
        var name = node.SelectSingleNode("div[@class='spell-header']").InnerText;

        var levelNode = node.SelectSingleNode(".//tr[position()=1]/td[last()]");
        var levelStat = levelNode.InnerText switch
        {
            "1 to 1" => "level1",
            "1 to 3" => "level3",
            "1 to 4" => "level4",
            "1 to 5" => "level5",
            "1 to 6" => "level6",
            "1 to 8" => "level8",
            "1 to 10" => "level10",
            _ => throw new Exception("Unknown Spell Level Range..."),
        };

        return new Entity
        {
            Id = new ResourceId(IdUtilities.Normalize(name), DomainId),
            Name = name,
            Stats = new List<ResourceId>
            {
                "_/owned",
                new ResourceId(levelStat, DomainId),
            }
        };
    }
}
