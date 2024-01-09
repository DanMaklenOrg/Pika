using HtmlAgilityPack;

namespace Pika.GameData.Scrapper.MinecraftATM9;

public class IronSpellsNSpellbooksScrapper
{
    private const string SubDomainId = "iron_spells_n_spellbooks.minecraft_atm9";

    public async Task Scrape()
    {
        await ScrapeSpells();
    }

    private static async Task ScrapeSpells()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://iron431.github.io/Irons-Spellbooks-Docs/spells/");
        var spellNodes = doc.DocumentNode.SelectNodes("//div[@class='spell-container']");
        var spells = spellNodes.Select(ParseSpell).ToList();
    }

    private static Entity ParseSpell(HtmlNode node)
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
            Id = name,
            Name = name,
            Stats = new List<PikaId>
            {
                "_/owned",
                new PikaId(SubDomainId, levelStat),
            }
        };
    }
}
