using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.MinecraftATM9;

public class IntegratedDynamicsScrapper : IScrapper
{
    public DomainId DomainId => "integrated_dynamics.minecraft_atm9";
    public string OutputDirectory => $"MinecraftATM9";

    public async Task<Domain> Scrape()
    {
        var entityList = await ScrapeAdvancements("https://integrateddynamics.rubensworks.net/book/tutorials/");
        return new Domain
        {
            Id = DomainId,
            Entities = entityList,
        };
    }

    private async Task<List<Entity>> ScrapeAdvancements(string url)
    {
        var entities = new List<Entity>();
        var doc = await new HtmlWeb().LoadFromWebAsync(url);
        var advancementNodes = doc.DocumentNode.SelectNodes("//span[@class='advancement-title']");

        if (advancementNodes is not null)
            entities.AddRange(advancementNodes.Select(node => new Entity
            {
                Id = new ResourceId(IdUtilities.Normalize(node.InnerText), DomainId),
                Name = node.InnerText,
                Stats = new List<ResourceId>
                {
                    "_/quest_completed",
                },
            }));

        var subPages = doc.DocumentNode.SelectNodes("//li[preceding-sibling::h2]/a");

        if (subPages is not null)
            foreach (var page in subPages)
                entities.AddRange(await ScrapeAdvancements(page.Attributes["href"].Value));

        return entities;
    }
}
