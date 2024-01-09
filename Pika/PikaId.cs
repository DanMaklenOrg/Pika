using System.Text.RegularExpressions;

namespace Pika;

public readonly struct PikaId
{
    private static readonly Regex IdPattern = new(@"^[0-9a-z_]+$");
    private static readonly Regex NonIdCharacter = new(@"[^0-9a-zA-Z]+");

    public string Id { get; init; }

    public string Domain { get; init; }

    public string FullyQualifiedId => Domain is null ? Id : $"{Domain}/{Id}";

    public bool IsFullyQualified() => Domain is not null;

    public PikaId(string id, string domain)
    {
        Id = id;
        Domain = domain;
    }

    public override string ToString() => FullyQualifiedId;

    public static PikaId ParseId(string id)
    {
        var segments = id.Split('/');
        return id.Split('/') switch
        {
            [var seg1, var seg2] => new PikaId(seg1, seg2),
            _ => throw new ArgumentException("Id must be of form {{domain}}/{{id}} or {{id}}"),
        };
    }

    public static PikaId FromName(string name, string? domain = null) => new(NonIdCharacter.Replace(name, "_").ToLower(), domain);

    public static implicit operator PikaId(string id) => ParseId(id);

    public static bool IsValidId(string str) => IdPattern.IsMatch(str);
}
