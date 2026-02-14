using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class WarframeScrapper(IHttpClientFactory httpClientFactory) : IScrapper
{
    private static readonly Regex SolarNodeNameRegex = new Regex(@"^(.*) \((.*)\)$");

    private readonly HttpClient _client = httpClientFactory.CreateClient();

    public ResourceId GameId => "warframe";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await ScrapeWarframes());
        game.Entities.AddRange(await ScrapeWeapons());
        game.Entities.AddRange(await ScrapeAmps());
        game.Entities.AddRange(await ScrapeKitGuns());
        game.Entities.AddRange(await ScrapeSentinels());
        game.Entities.AddRange(await ScrapeSentinelWeapons());
        game.Entities.AddRange(await ScrapePets());
        game.Entities.AddRange(await ScrapeVehicles());
        game.Entities.AddRange(await ScrapeSolarNodes());
    }

    private async Task<List<Entity>> ScrapeWarframes()
    {
        var resp = await GetFromApi("warframes?only=name,masterable,productCategory");
        return resp
            .Where(x => x["masterable"]!.GetValue<bool>() && x["name"]!.GetValue<string>() != "Excalibur Prime")
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var categoory = x["productCategory"]!.GetValue<string>() switch
                {
                    "Suits" => "warframe",
                    "MechSuits" => "necramech",
                    "SpaceSuits" => "archwing",
                    _ => throw new ArgumentOutOfRangeException(),
                };
                var id = ScrapperHelper.InduceIdFromName(name, categoory);
                return new Entity(id, name, categoory);
            }).ToList();
    }

    private async Task<List<Entity>> ScrapeWeapons()
    {
        var resp = await GetFromApi("weapons?only=name,category,uniqueName,type,masterable");
        return resp.Where(x => x["name"]!.GetValue<string>() is not "Braton Prime" and not "Lato Prime" and not "Skana Prime" and not "Sirocco"
                               && !x["uniqueName"]!.GetValue<string>().Contains("TnDoppelgangerGrimoire")
                               && x["masterable"]!.GetValue<bool>())
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var category = (x["category"]!.GetValue<string>(), x["type"]!.GetValue<string>()) switch
                {
                    ("Primary", _) => "weapon_primary",
                    ("Secondary", _) => "weapon_secondary",
                    ("Melee", "Zaw Component") => "weapon_zaw",
                    ("Melee", _) => "weapon_melee",
                    ("Arch-Gun", _) => "weapon_arch_gun",
                    ("Arch-Melee", _) => "weapon_arch_melee",
                    _ => throw new ArgumentOutOfRangeException((x["category"]!.GetValue<string>(), x["type"]!.GetValue<string>()).ToString()),
                };
                var id = ScrapperHelper.InduceIdFromName(name, category);
                return new Entity(id, name, category);
            }).ToList();
    }

    private async Task<List<Entity>> ScrapeAmps()
    {
        var resp = await GetFromApi("items/search/OperatorAmplifiers?by=uniqueName&only=name,uniqueName");
        return resp.Where(x => x["uniqueName"]!.GetValue<string>().Contains("/Barrel/"))
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var id = ScrapperHelper.InduceIdFromName(name, "amp");
                return new Entity(id, name, "amp");
            })
            .Append(new Entity("amp_sirocco", "Sirocco", "amp"))
            .Append(new Entity("amp_melee", "Mote Amp", "amp"))
            .ToList();
    }

    private async Task<List<Entity>> ScrapeKitGuns()
    {
        var kitGunResp = await GetFromApi("items/search/Kitgun Component?by=type&only=name,uniqueName");
        var kitGunRespFiltered = kitGunResp.Where(x => x["uniqueName"]!.GetValue<string>().Contains("/Barrel/"));

        var infKitGunResp = await GetFromApi("items/search/InfKitGun?by=uniqueName&only=name,uniqueName");
        var infKitGunsFiltered = infKitGunResp.Where(x => x["uniqueName"]!.GetValue<string>().Contains("/Barrels/"));

        return kitGunRespFiltered.Union(infKitGunsFiltered)
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var id = ScrapperHelper.InduceIdFromName(name, "weapon_kitgun");
                return new Entity(id, name, "weapon_kitgun");
            }).ToList();
    }

    private async Task<List<Entity>> ScrapeSentinels()
    {
        var resp = await GetFromApi("items/search/Sentinel?by=type&only=name");
        return resp
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var id = ScrapperHelper.InduceIdFromName(name, "companion_sentinel");
                return new Entity(id, name, "companion_sentinel");
            })
            .ToList();
    }

    private async Task<List<Entity>> ScrapeSentinelWeapons()
    {
        var resp = await GetFromApi("items/search/Companion Weapon?by=type&only=name,masterable");
        return resp.Where(x => x["masterable"]!.GetValue<bool>())
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var id = ScrapperHelper.InduceIdFromName(name, "weapon_sentinel");
                return new Entity(id, name, "weapon_sentinel");
            })
            .ToList();
    }

    private async Task<List<Entity>> ScrapePets()
    {
        var resp = await GetFromApi("items/search/pets?by=type&only=name,uniqueName");
        return resp
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var category = x["uniqueName"]!.GetValue<string>() switch
                {
                    var s when s.Contains("KubrowPet") => "companion_kubrew",
                    var s when s.Contains("CatbrowPet") => "companion_kavat",
                    var s when s.Contains("MoaPet") => "companion_moa",
                    var s when s.Contains("ZanukaPet") => "companion_hound",
                    _ => throw new ArgumentOutOfRangeException(),
                };
                var id = ScrapperHelper.InduceIdFromName(name, category);
                return new Entity(id, name, category);
            })
            .Append(new Entity("companion_kavat_venari", "Venari", "companion_kavat"))
            .Append(new Entity("companion_kavat_venari_prime", "Venari Prime", "companion_kavat"))
            .ToList();
    }

    private async Task<List<Entity>> ScrapeVehicles()
    {
        var resp = await GetFromApi("items/search/K-Drive Component?by=type&only=name,masterable");
        return resp.Where(x => x["masterable"]!.GetValue<bool>())
            .Select(x =>
            {
                var name = x["name"]!.GetValue<string>();
                var id = ScrapperHelper.InduceIdFromName(name, "kdrive");
                return new Entity(id, name, "kdrive");
            })
            .ToList();
    }

    private async Task<List<Entity>> ScrapeSolarNodes()
    {
        var resp = await GetFromApi("solNodes", objToArray: true);
        return resp
            .Where(k =>
                k.ContainsKey("type") &&
                !k["$$ID$$"]!.GetValue<string>().StartsWith("EventNode") &&
                k["value"]!.GetValue<string>() is not "Vesper Relay (Venus)" and not "Leonov Relay (Europa)" and not "Kuiper Relay (Eris)" && // Destroyed Relays
                k["value"]!.GetValue<string>() is not "Sortie Boss: Phorid" and not "Vesper (Venus)" and not "Tikoloshe (Sedna)" and not "Phithale (Sedna)" and not "Ganalen's Grave (Veil)" and not "Gian Point (Veil)" and not "Ruse War Field (Veil)" and not "Rya (Veil)" &&
                k["type"]!.GetValue<string>() is not "Conclave" and not "Ancient Retribution" and not "Hive Sabotage" and not "The Perita Rebellion")
            .Select(x =>
            {
                var rawName = x["value"]!.GetValue<string>();
                var match = SolarNodeNameRegex.Match(rawName);
                var name = match.Groups[1].Value;
                var tag = ScrapperHelper.InduceIdFromName(match.Groups[2].Value, "solar");
                if (x["$$ID$$"]!.GetValue<string>().StartsWith("CrewBattleNode"))
                    tag += "_proxima";
                var id = ScrapperHelper.InduceIdFromName(name, "solar_node");
                return new Entity(id, name, "solar_node") { Tags = [tag] };
            })
            .Append(new Entity("solar_node_index", "The Index: Endurance", "solar_node") { Tags = ["solar_neptune"] })
            .Append(new Entity("solar_node_aladv", "Mutalist Alad V Assassinate", "solar_node") { Tags = ["solar_eris"] })
            .Append(new Entity("solar_node_jordas_golem", "Jordas Golem Assassinate", "solar_node") { Tags = ["solar_eris"] })
            .Append(new Entity("solar_node_isleweaver", "Isleweaver", "solar_node") { Tags = ["solar_duviri"] })
            .Append(new Entity("solar_node_iron_wake", "Iron Wake", "solar_node") { Tags = ["solar_earth"] })
            .Append(new Entity("solar_node_cetus", "Cetus", "solar_node") { Tags = ["solar_earth"] })
            .Append(new Entity("solar_node_free flight", "Free Flight", "solar_node") { Tags = ["solar_earth_proxima"] })
            .ToList();
    }

    private async Task<JsonObject[]> GetFromApi(string path, bool objToArray = false)
    {
        if (objToArray)
        {
            return (await _client.GetFromJsonAsync<JsonObject>($"https://api.warframestat.us/{path}"))!
                .Select(k =>
                {
                    var res = k.Value!.AsObject();
                    res.Add("$$ID$$", k.Key);
                    return res;
                })
                .ToArray();

        }

        return (await _client.GetFromJsonAsync<JsonObject[]>($"https://api.warframestat.us/{path}"))!;
    }

    private async Task<JsonObject> GetJsonObject(string path)
    {
        return (await _client.GetFromJsonAsync<JsonObject>($"https://api.warframestat.us/{path}"))!;
    }
}
