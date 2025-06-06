using Steam.Models;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;

namespace Pika.GameData;

public class SteamClient
{
    private const string SteamApiKey = "726E8C3ACCE9C195F7934EC0BCE1A68D";

    private readonly SteamUserStats _userStatsClient;

    public SteamClient()
    {
        var factory = new SteamWebInterfaceFactory(SteamApiKey);
        _userStatsClient = factory.CreateSteamWebInterface<SteamUserStats>();
    }

    public async Task<IList<SchemaGameAchievementModel>> GetAchievements(uint appId)
    {
        var response = await _userStatsClient.GetSchemaForGameAsync(appId);
        return response.Data.AvailableGameStats.Achievements.ToList();
    }
}
