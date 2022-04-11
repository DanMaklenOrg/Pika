namespace Pika.DataLayer;

public readonly struct DbConfig
{
    public string DatabaseName { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string Host { get; init; }
    public string Port { get; init; }
}
