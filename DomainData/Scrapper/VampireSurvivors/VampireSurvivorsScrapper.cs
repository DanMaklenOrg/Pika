using System.Net;
using HtmlAgilityPack;
using Pika.Model;
using Steam.Models;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class VampireSurvivorsScrapper(EntityNameContainer nameContainer, SteamClient steamClient) : IScrapper
{
    private readonly uint _steamAppId = 1794680;

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
        var inGameAchievements = await ScrapeInGameAchievements();
        var steamAchievements = await ScrapeSteamAchievements();

        // List of names that are part of another subdomain and not visible to the name container.
        HashSet<string> namesToAnnotate =
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
            "Polus Replica"
        ];

        var achievements = inGameAchievements.Union(steamAchievements).Select(rawName =>
        {
            var name = nameContainer.RegisterAndNormalize(rawName, "Achievement");
            if (namesToAnnotate.Contains(name)) name = $"{name} (Achievement)";

            List<ResourceId> classes = [];
            if (inGameAchievements.Contains(rawName)) classes.Add("_/achievement");
            if (steamAchievements.Contains(rawName)) classes.Add(new ResourceId("steam_achievement", DomainId));
            return new Entity
            {
                Id = ResourceId.InduceFromName(name, DomainId),
                Name = name,
                Classes = classes,
            };
        }).ToList();

        return achievements;
    }

    private async Task<HashSet<string>> ScrapeInGameAchievements()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Achievement");
        var nodes = doc.DocumentNode.SelectNodes("//tr/td/..");
        var achievements = nodes.Select(n => EntityNameContainer.Normalize(n.SelectSingleNode(".//td[2]").InnerText)).ToHashSet();
        achievements.Remove("Song Of Mana");
        achievements.Add("Song of Mana");
        achievements.Remove("Tiragisú");
        achievements.Add("Tirajisú");
        achievements.Add("EXTRA: Space Dude");
        achievements.Add("EXTRA: Glass Fandango");
        return achievements;
    }

    private async Task<HashSet<string>> ScrapeSteamAchievements()
    {
        var steamRawAchievements = await steamClient.GetAchievements(_steamAppId);
        var achievements = steamRawAchievements.Select(a => EntityNameContainer.Normalize(a.DisplayName)).ToHashSet();
        achievements.Remove("Song Of Mana");
        achievements.Add("Song of Mana");
        achievements.Remove("Tiragisú");
        achievements.Add("Tirajisú");
        achievements.Remove("Ebony WIngs");
        achievements.Add("Ebony Wings");
        return achievements;
    }
}
