using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Pika.Model;
using Attribute = Pika.Model.Attribute;

namespace Pika.PikaLang;

public class PikaParser
{
    public Game Parse(TextReader textReader)
    {
        var antlrStream = new AntlrInputStream(textReader);
        var lexer = new PikaLangLexer(antlrStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new PikaLangParser(tokenStream);
        var parseTree = parser.root();

        var game = ParseGame(parseTree);
        return game;
    }

    private Game ParseGame(PikaLangParser.RootContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.gameDecl().namedIdentifier());
        var game = new Game(id, name);

        game.Projects.AddRange(context.declStmt().OfType<PikaLangParser.ProjectDeclarationContext>().Select(ParseProject));
        game.Classes.AddRange(context.declStmt().OfType<PikaLangParser.ClassDeclarationContext>().Select(ParseClass));
        game.Entities.AddRange(context.declStmt().OfType<PikaLangParser.EntityDeclarationContext>().Select(ParseEntity));

        return game;
    }

    private Project ParseProject(PikaLangParser.ProjectDeclarationContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.projectDecl().namedIdentifier());
        return new Project(id, name)
        {
            Objectives = context.projectDecl().objectiveDecl().Select(ParseObjective).ToList(),
        };
    }

    private Objective ParseObjective(PikaLangParser.ObjectiveDeclContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.namedIdentifier());
        return new Objective(id, name)
        {
            Requirements = context.requireDecl().Select(r =>
            {
                ResourceId classId = r.IDENTIFIER().GetText();
                return new Objective.Requirement(classId);
            }).ToList(),
        };
    }

    private Class ParseClass(PikaLangParser.ClassDeclarationContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.classDecl().namedIdentifier());
        return new Class(id, name)
        {
            Attributes = context.classDecl().attrDecl().Select(ParseAttribute).ToList(),
            Stats = context.classDecl().statDecl().Select(ParseStat).ToList(),
        };
    }

    private Entity ParseEntity(PikaLangParser.EntityDeclarationContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.entityDecl().namedIdentifier());
        ResourceId classId = context.entityDecl().IDENTIFIER().GetText();
        var entity = new Entity(id, name, classId)
        {
            Attributes = context.entityDecl().attrDecl().Select(ParseAttribute).ToList(),
            Stats = context.entityDecl().statDecl().Select(ParseStat).ToList(),
        };
        return entity;
    }

    private Attribute ParseAttribute(PikaLangParser.AttrDeclContext context)
    {
        ResourceId id = context.IDENTIFIER().GetText();
        var val = ParseIntLiteral(context.INTEGER_LITERAL());
        return new Attribute(id, val);
    }

    private Stat ParseStat(PikaLangParser.StatDeclContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.namedIdentifier());
        return context.statType() switch
        {
            PikaLangParser.BoolStatTypeContext => new Stat(id, name, Stat.StatType.Boolean),
            PikaLangParser.IntRangeStatTypeContext intRangeContext => new Stat(id, name, Stat.StatType.IntegerRange)
            {
                Min = ParseIntOrAttribute(intRangeContext.intOrAttribute()[0]),
                Max = ParseIntOrAttribute(intRangeContext.intOrAttribute()[1]),
            },
            _ => throw new UnreachableException(),
        };
    }

    private Stat.IntOrAttribute ParseIntOrAttribute(PikaLangParser.IntOrAttributeContext context)
    {
        if (context.INTEGER_LITERAL() is not null) return ParseIntLiteral(context.INTEGER_LITERAL());
        if (context.IDENTIFIER() is not null) return (ResourceId)context.IDENTIFIER().GetText();
        throw new UnreachableException();
    }

    private (ResourceId id, string name) ParseNamedIdentifier(PikaLangParser.NamedIdentifierContext context)
    {
        return context switch
        {
            PikaLangParser.IdWithNameContext ctx => (
                ctx.IDENTIFIER().GetText(),
                ParseStringLiteral(ctx.STRING_LITERAL())
            ),
            PikaLangParser.NameOnlyContext ctx => (
                ResourceId.InduceFromName(ParseStringLiteral(ctx.STRING_LITERAL())),
                ParseStringLiteral(ctx.STRING_LITERAL())
            ),
            PikaLangParser.IdOnlyContext ctx => (
                ctx.IDENTIFIER().GetText(),
                ctx.IDENTIFIER().GetText()
            ),
            _ => throw new UnreachableException(),
        };
    }

    private string ParseStringLiteral(IParseTree stringLiteral)
    {
        var str = stringLiteral.GetText();
        str = str.Trim('\'');
        str = str.Replace("\\'", "'");
        return str;
    }

    private int ParseIntLiteral(IParseTree intLiteral)
    {
        return int.Parse(intLiteral.GetText());
    }
}
