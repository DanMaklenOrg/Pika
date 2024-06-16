using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class VampireSurvivorsAchievements(EntityNameContainer nameContainer, SteamClient steamClient) : IScrapper
{
    private readonly uint _steamAppId = 1794680;

    public DomainId DomainId => "vampire_survivors";
    public string OutputDirectory => "VampireSurvivors";
    public string FileName => "Achievements";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Vampire Survivors",
            Entities = await ScrapeAchievements(),
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
            return new Entity
            {
                Id = ResourceId.InduceFromName(name, DomainId),
                Name = name,
                Classes = ["_/achievement"],
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
        achievements.Add("EXTRA: Phas3r");
        achievements.Add("EXTRA: Space 54");
        achievements.Add("EXTRA: Whiteout");
        achievements.Add("EXTRA: Chaos Altemanna");
        achievements.Add("EXTRA: Pako Battiliar");
        achievements.Add("EXTRA: Bat Robbert");
        achievements.Add("EXTRA: Celestial Voulge");
        achievements.Add("EXTRA: Photonstorm");
        achievements.Add("EXTRA: She-Moon Eeta");
        achievements.Add("EXTRA: Laborratory");
        achievements.Add("EXTRA: Santa Javelin");
        achievements.Add("EXTRA: Arma Dio");
        achievements.Add("EXTRA: Carlo Cart");
        achievements.Add("EXTRA: Seraphic Cry");
        achievements.Add("EXTRA: Santa Ladonna");
        achievements.Add("EXTRA: Seal III");
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
