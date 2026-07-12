using System.Text.Json;
using System.Text.Json.Nodes;

namespace Pika.GameData.ScrapperHelpers;

public class JsScrapperHelper(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _client = httpClientFactory.CreateClient();

    // Assumptions:
    // - Back-to-back `var <name> = <json>;` declarations.
    // - Assumes the requested variable exists and its value is JSON parseable by System.Text.Json.
    public async Task<JsonObject[]> ScrapeJsVariable(string url, string variableName, bool objToArray = false)
    {
        var script = await _client.GetStringAsync(url);
        var json = ExtractVariableLiteral(script, variableName);

        if (objToArray)
        {
            return JsonSerializer.Deserialize<JsonObject>(json)!
                .Select(k =>
                {
                    var res = k.Value!.AsObject();
                    res.Add("$$ID$$", k.Key);
                    return res;
                })
                .ToArray();
        }

        return JsonSerializer.Deserialize<JsonObject[]>(json)!;
    }

    private static string ExtractVariableLiteral(string script, string variableName)
    {
        var marker = $"var {variableName} = ";
        var start = script.IndexOf(marker, StringComparison.Ordinal) + marker.Length;
        var end = script.IndexOf(';', start);
        return script[start..end];
    }
}
