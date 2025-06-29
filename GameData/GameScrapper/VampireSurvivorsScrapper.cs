using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class VampireSurvivorsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 1794680;

    public ResourceId GameId => "vampire_survivors";

    public async Task ScrapeInto(Game game)
    {
        game.Achievements.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId));
        game.Entities.AddRange(await ScrapeCharacters());
        game.Entities.AddRange(await ScrapeRelics());
        game.Entities.AddRange(await ScrapePassiveItems());
        game.Entities.AddRange(await ScrapeWeapons());
        game.Entities.AddRange(await ScrapePowerUps());
        game.Entities.AddRange(await ScrapePickups());
        game.Entities.AddRange(await ScrapeArcanas());
        game.Entities.AddRange(await ScrapeDarkanas());
        game.Entities.AddRange(await ScrapeSecrets());
    }

    private async Task<List<Entity>> ScrapeCharacters()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Characters");
        var nodes = doc.DocumentNode.SelectNodes("//div/b/a");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "character");
            return new Entity(id, name, "character");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeRelics()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Relics");
        var nodes = doc.DocumentNode.SelectNodes("//tr/td[2]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "relic");
            return new Entity(id, name, "collection_entry");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapePowerUps()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Powerups");
        var nodes = doc.DocumentNode.SelectNodes("(//tbody)[1]/tr/td/..");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode(".//td[2]").InnerText);
            name = name == "MoveSpeed" ? "Move Speed" : name;
            var maxRank = ScrapperHelper.CleanName(n.SelectSingleNode(".//td[4]").InnerText.Trim());
            var id = ScrapperHelper.InduceIdFromName(name, "power_up");
            return new Entity(id, name, $"power_up_{maxRank}");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapePassiveItems()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Passive_items");
        var nodes = doc.DocumentNode.SelectNodes("//tr/td[2]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "passive_item");
            return new Entity(id, name, "collection_entry");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeWeapons()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Weapons");
        var nodes =
            doc.DocumentNode.SelectNodes("(//tbody)[position()>=2 and position() <= 4]/tr/td[2]");
        HashSet<string> blacklist = ["Anima of Mortaccio", "Profusione D'Amore", "Yatta Daikarin"];
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "weapon");
            return new Entity(id, name, "collection_entry");
        }).Where(e => !blacklist.Contains(e.Name)).ToList();
    }

    private async Task<List<Entity>> ScrapePickups()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Pickups");
        var nodes = doc.DocumentNode.SelectNodes("//tr/td[2]");
        HashSet<string> blacklist = ["Big Coin Bag", "Cheese", "Corn", "Little Heart", "Pie"];
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "pickup");
            return new Entity(id, name, "collection_entry");
        }).Where(e => !blacklist.Contains(e.Name)).ToList();
    }

    private async Task<List<Entity>> ScrapeArcanas()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Arcanas");
        var nodes = doc.DocumentNode.SelectNodes("(//tbody)[1]/tr/td[3]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "arcana");
            return new Entity(id, name, "collection_entry");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeDarkanas()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Arcanas");
        var nodes = doc.DocumentNode.SelectNodes("(//tbody)[2]/tr/td[3]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "darkana");
            return new Entity(id, name, "collection_entry");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeSecrets()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire-survivors.fandom.com/wiki/Secret");
        var nodes = doc.DocumentNode.SelectNodes("//tr/td[3]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "secret");
            return new Entity(id, name, "secret");
        }).ToList();
    }
}
