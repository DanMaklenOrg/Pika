namespace Utility;

public static class ListExtensions
{
    public static List<TOutput>? ConvertAllOrNull<T, TOutput>(this List<T>? list, Converter<T, TOutput> func)
    {
        if (list is null || list.Count == 0) return null;
        return list.ConvertAll(func);
    }
}
