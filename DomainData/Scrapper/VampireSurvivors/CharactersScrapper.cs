using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.VampireSurvivors;

public class CharactersScrapper : IScrapper
{
    public DomainId DomainId => "characters.vampire_survivors";
    public string OutputDirectory => "VampireSurvivors";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Vampire Survivors: Characters",
            Entities = await ScrapeCharacters(),
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
}
