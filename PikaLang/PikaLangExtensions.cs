using Microsoft.Extensions.DependencyInjection;

namespace Pika.PikaLang;

public static class PikaLangExtensions
{
    public static IServiceCollection AddPikaParser(this IServiceCollection collection)
    {
        collection.AddTransient<PikaParser>();
        return collection;
    }
}
