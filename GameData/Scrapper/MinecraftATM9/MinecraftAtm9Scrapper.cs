namespace Pika.GameData.Scrapper.MinecraftATM9;

public class MinecraftAtm9Scrapper
{
    private readonly IronSpellsNSpellbooksScrapper _ironSpellsNSpellbooksScrapper;

    public MinecraftAtm9Scrapper(IronSpellsNSpellbooksScrapper ironSpellsNSpellbooksScrapper)
    {
        _ironSpellsNSpellbooksScrapper = ironSpellsNSpellbooksScrapper;
    }

    public async Task Scrape()
    {
        Console.WriteLine("Scrapping Minecraft ATM9");
        await _ironSpellsNSpellbooksScrapper.Scrape();

    }
}
