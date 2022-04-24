namespace Pika.Service.Utilities;

public static class Traverse
{
    public static IEnumerable<T> Dfs<T>(T node, Func<T, IEnumerable<T>> children)
    {
        yield return node;
        foreach (T descendant in children(node).SelectMany(n => Dfs(n, children))) yield return descendant;
    }
}
