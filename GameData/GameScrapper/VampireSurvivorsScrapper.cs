using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class VampireSurvivorsScrapper : IScrapper
{
    public ResourceId GameId => "vampire_survivors";

    public async Task ScrapeInto(Game game)
    {
        await ScrapePowerUps(game);
        await ScrapeCollectionEntries(game);
        await ScrapeSecrets(game);
        await ScrapeUnlocks(game);
        await ScrapeCharacters(game);
        await ScrapeStages(game);
        await ScrapeAdventures(game);
    }

    private async Task ScrapePowerUps(Game game)
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire.survivors.wiki/w/PowerUps");
        var nodes = doc.DocumentNode.SelectNodes("//td[3]");
        game.Entities.AddRange(nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "power_up");
            return new Entity(id, name, $"power_up");
        }));
    }

    private async Task ScrapeCollectionEntries(Game game)
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire.survivors.wiki/w/Collection");
        var entryTableNodeList = doc.DocumentNode.SelectNodes("//table");
        foreach (var entryTableNode in entryTableNodeList)
        {
            var tagNode = entryTableNode.SelectSingleNode("./preceding-sibling::h2[1]/span[1]");
            var tagName = ScrapperHelper.CleanName(tagNode.InnerText);
            var tagId = ScrapperHelper.InduceIdFromName(tagName, "collection_entry");
            var tag = new Tag(tagId, tagName);
            game.Tags.Add(tag);
            var entryNodeList = entryTableNode.SelectNodes(".//td");
            game.Entities.AddRange(entryNodeList.Select(n =>
            {
                var entryName = ScrapperHelper.CleanName(n.InnerText);
                var entryId = ScrapperHelper.InduceIdFromName(entryName, "collection_entry");
                return new Entity(entryId, entryName, "collection_entry") { Tags = [tag.Id] };
            }));
        }
    }

    private async Task ScrapeSecrets(Game game)
    {
        var ach = game.Achievements.Single(a => a.Id == "achievement_secrets");

        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire.survivors.wiki/w/Secrets");
        var nodes = doc.DocumentNode.SelectNodes("//table[contains(@class, 'wikitable')]//tr[position()>1]");
        ach.Objectives.AddRange(nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode("./td[1]").InnerText);
            var description = n.SelectSingleNode("./td[2]").InnerText.Trim();
            var id = ScrapperHelper.InduceIdFromName(name, "secret");
            return new Objective(id, name) { Description = description };
        }));
    }

    private async Task ScrapeUnlocks(Game game)
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire.survivors.wiki/w/Achievements");
        var entryTableNodeList = doc.DocumentNode.SelectNodes("//table[contains(@class, 'wikitable')]");
        foreach (var entryTableNode in entryTableNodeList)
        {
            var tagNode = entryTableNode.SelectSingleNode("./preceding-sibling::*[1]/span[1]");
            var tagName = ScrapperHelper.CleanName(tagNode.InnerText);
            var tagId = ScrapperHelper.InduceIdFromName(tagName, "unlock");
            var tag = new Tag(tagId, tagName);
            game.Tags.Add(tag);
            var entryNodeList = entryTableNode.SelectNodes(".//tr[position()>1]/td[2]");
            game.Entities.AddRange(entryNodeList.Select(n =>
            {
                var entryName = ScrapperHelper.CleanName(n.InnerText);
                var entryId = ScrapperHelper.InduceIdFromName(entryName, "unlock");
                return new Entity(entryId, entryName, "unlock") { Tags = [tag.Id] };
            }));
        }
    }

    private async Task ScrapeCharacters(Game game)
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire.survivors.wiki/w/Characters");
        var nodes = doc.DocumentNode.SelectNodes("//span[contains(@class, 'character-tooltip')]");
        game.Entities.AddRange(nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "character");
            return new Entity(id, name, "character");
        }));
    }

    private async Task ScrapeStages(Game game)
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire.survivors.wiki/w/Stages");
        var nodes = doc.DocumentNode.SelectNodes("//tr/td[1]/div/a");
        game.Entities.AddRange(nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "stage");
            return new Entity(id, name, "stage");
        }));
    }

    private async Task ScrapeAdventures(Game game)
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://vampire.survivors.wiki/w/Adventures");
        var nodes = doc.DocumentNode.SelectNodes("//h2/span[1]");
        game.Entities.AddRange(nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "adventure");
            return new Entity(id, name, "adventure");
        }));
    }
}
