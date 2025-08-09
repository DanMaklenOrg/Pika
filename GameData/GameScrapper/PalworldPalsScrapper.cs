using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class PalworldPalsScrapper : IScrapper
{
    private static readonly Regex PalIndexIdRegex = new Regex(@"^#(\d{1,3})(\D)?$");

    public ResourceId GameId => "palworld";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await ScrapePals());
        game.Entities.AddRange(await ScrapeTerrariaCreatures());
        game.Entities.AddRange(await ScrapePassiveSkills());
    }

    private async Task<List<Entity>> ScrapePals()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pals");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='break border']/preceding-sibling::div");
        return nodes.Select(n =>
        {
            var palIndexRaw = ScrapperHelper.CleanName(n.SelectSingleNode(".//span").InnerText);
            var match = PalIndexIdRegex.Match(palIndexRaw);
            var palId = int.Parse(match.Groups[1].Value);
            var palVariant = match.Groups[2].Success ? match.Groups[2].Value : string.Empty;
            var palIndex = $"#{palId:D3}{palVariant}";
            var palName = ScrapperHelper.CleanName(n.SelectSingleNode(".//a[@class='itemname']").InnerText);
            var name = $"{palIndex}: {palName}";
            var id = ScrapperHelper.InduceIdFromName(name, "pal");
            return new Entity(id, name, "pal");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeTerrariaCreatures()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pals");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='break border']/following-sibling::div");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode(".//a[@class='itemname']").InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "terraria_creature");
            return new Entity(id, name, "terraria_creature");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapePassiveSkills()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Passive_Skills");
        var nodes = doc.DocumentNode.SelectNodes("//div[@id='PalPassiveSkills']//div[contains(@class, 'passive-rank')]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "passive_skill");
            return new Entity(id, name, "passive_skill");
        }).ToList();
    }
}
