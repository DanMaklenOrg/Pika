using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class VampireSurvivorsScrapper : IScrapper
{
    public DomainId DomainId => "vampire_survivors";
    public string OutputDirectory => "VampireSurvivors";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Entities =
            [
                ..await ScrapeCharacters(),
                ..await ScrapePowerUps(),
                ..await ScrapeRelics(),
                ..await ScrapePassiveItems(),
                ..await ScrapeWeapons(),
            ],
        };
    }

    private async Task<List<Entity>> ScrapeCharacters()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Characters");
        var nodes = doc.DocumentNode.SelectNodes("//div/b/a");
        return nodes.Select(ParseCharacter).ToList();
    }

    private Entity ParseCharacter(HtmlNode node)
    {
        var name = node.InnerText;
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new("character", DomainId)],
        };
    }

    private async Task<List<Entity>> ScrapePowerUps()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Powerups");
        var nodes = doc.DocumentNode.SelectNodes("//tr");
        return nodes.Skip(2).Select(ParsePowerUp).ToList();
    }

    private Entity ParsePowerUp(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[2]").InnerText.Trim();
        name = name switch
        {
            "MoveSpeed" => "Move Speed",
            _ => name,
        };

        var maxRank = node.SelectSingleNode(".//td[4]").InnerText.Trim();
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new($"power_up_{maxRank}", DomainId)],
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

    private async Task<List<Entity>> ScrapePassiveItems()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Passive_items");
        var nodes = doc.DocumentNode.SelectNodes("//tr");
        return nodes.Skip(2).Select(ParsePassiveItem).ToList();
    }

    private Entity ParsePassiveItem(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[2]").InnerText.Trim();
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new($"passive_item", DomainId)],
        };
    }

    private async Task<List<Entity>> ScrapeWeapons()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Weapons");
        var nodes =
            doc.DocumentNode.SelectNodes("//table[preceding-sibling::h3[span/.='Base Weapons'] and following-sibling::h3[span/.='Gifts']]/tbody/tr/td/..");
        return nodes.Select(ParseWeapon).ToList();
    }

    private Entity ParseWeapon(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[2]").InnerText.Trim();
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new($"weapon", DomainId)],
        };
    }
}
