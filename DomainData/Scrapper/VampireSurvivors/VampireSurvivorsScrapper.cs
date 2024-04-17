using System.Net;
using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class VampireSurvivorsScrapper(EntityNameContainer nameContainer) : IScrapper
{
    public DomainId DomainId => "vampire_survivors";
    public string OutputDirectory => "VampireSurvivors";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Vampire Survivors",
            Entities =
            [
                ..await ScrapeCharacters(),
                ..await ScrapeRelics(),
                ..await ScrapePassiveItems(),
                ..await ScrapeWeapons(),
                ..await ScrapePowerUps(),
                ..await ScrapePickups(),
                ..await ScrapeArcana(),
                ..await ScrapeSecrets(),
                ..await ScrapeAchievements(),
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
        var name = nameContainer.RegisterAndNormalize(node.InnerText, "Character");
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new ResourceId("character", DomainId)],
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
        var name = node.SelectSingleNode(".//td[2]").InnerText;
        name = nameContainer.RegisterAndNormalize(name, "Relic");
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new ResourceId("collection_entry", DomainId)],
            Tags = [new ResourceId("relic", DomainId)],
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
        var name = node.SelectSingleNode(".//td[2]").InnerText;
        name = nameContainer.RegisterAndNormalize(name, "Power Up");
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
            Classes = [new ResourceId($"power_up_{maxRank}", DomainId)],
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
        var name = node.SelectSingleNode(".//td[2]").InnerText;
        name = nameContainer.RegisterAndNormalize(name, "Passive Item");
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new ResourceId("collection_entry", DomainId)],
            Tags = [new ResourceId("passive_item", DomainId)],
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
        var name = node.SelectSingleNode(".//td[2]").InnerText;
        name = nameContainer.RegisterAndNormalize(name, "Weapon");
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new ResourceId("collection_entry", DomainId)],
            Tags = [new ResourceId("weapon", DomainId)],
        };
    }

    private async Task<List<Entity>> ScrapePickups()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Pickups");
        var nodes = doc.DocumentNode.SelectNodes("//tr");
        return nodes.Skip(2).Select(ParsePickups).ToList();
    }

    private Entity ParsePickups(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[2]").InnerText;
        name = nameContainer.RegisterAndNormalize(name, "Pickup");
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new ResourceId("collection_entry", DomainId)],
            Tags = [new ResourceId("pickup", DomainId)],
        };
    }

    private async Task<List<Entity>> ScrapeArcana()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Arcanas");
        var nodes = doc.DocumentNode.SelectNodes("//tr");
        return nodes.Skip(1).Select(ParseArcana).ToList();
    }

    private Entity ParseArcana(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[2]").InnerText.Replace('—', ' ');
        name = nameContainer.RegisterAndNormalize(name, "Arcana");
        name = WebUtility.HtmlDecode(name);
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = [new ResourceId("collection_entry", DomainId)],
            Tags = [new ResourceId("arcana", DomainId)],
        };
    }

    private async Task<List<Entity>> ScrapeSecrets()
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

    private async Task<List<Entity>> ScrapeAchievements()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Achievement");
        var nodes = doc.DocumentNode.SelectNodes("//tr/td/..");
        return nodes.Select(ParseAchievement).ToList();
    }

    private Entity ParseAchievement(HtmlNode node)
    {
        var name = node.SelectSingleNode(".//td[2]").InnerText;
        if (name == "Song Of Mana") name = "Song of Mana";
        name = nameContainer.RegisterAndNormalize(name, "Achievement");
        List<string> x =
        [
            "Mad Forest",
            "Inlaid Library",
            "Dairy Plant",
            "Gallo Tower",
            "Cappella Magna",
            "Il Molise",
            "Moongolow",
            "Green Acres",
            "The Bone Zone",
            "Boss Rash",
            "Whiteout",
            "Space 54",
            "Bat Country",
            "Astral Stair",
            "Tiny Bridge",
            "Mt.Moonspell",
            "Lake Foscari",
            "Abyss Foscari",
            "Polus Replica",
            "Song Of Mana"
        ];
        if (x.Contains(name)) name = $"{name} (Achievement)";
        return new Entity
        {
            Id = ResourceId.InduceFromName(name, DomainId),
            Name = name,
            Classes = ["_/achievement"],
        };
    }
}
