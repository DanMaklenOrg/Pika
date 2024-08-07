using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class VampireSurvivorsAchievements(EntityNameContainer nameContainer, SteamClient steamClient) : IScrapper
{
    private readonly uint _steamAppId = 1794680;

    public ResourceId DomainId => "vampire_survivors";
    public string OutputDirectory => DomainId.ToString();
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
        var steamAchievements = await ScrapeSteamAchievements();

        // List of names that are part of another file and not visible to the name container.
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

        var achievements = steamAchievements.Select(rawName =>
        {
            var name = nameContainer.RegisterAndNormalize(rawName, "Achievement");
            if (namesToAnnotate.Contains(name)) name = $"{name} (Achievement)";
            return new Entity
            {
                Id = ResourceId.InduceFromName(name),
                Name = name,
                Class = "achievement",
            };
        }).ToList();

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
