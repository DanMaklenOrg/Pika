using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Pika.Model;

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

        if(context.importStmt().Length > 0) game.SteamAppId = (uint)ParseIntLiteral(context.importStmt()[0].INTEGER_LITERAL());

        game.Achievements.AddRange(context.declStmt().OfType<PikaLangParser.AchievementDeclarationContext>().Select(ParseAchievement));
        game.Categories.AddRange(context.declStmt().OfType<PikaLangParser.CategoryDeclarationContext>().Select(ParseCategory));
        game.Entities.AddRange(context.declStmt().OfType<PikaLangParser.EntityDeclarationContext>().Select(ParseEntity));

        return game;
    }

    private Achievement ParseAchievement(PikaLangParser.AchievementDeclarationContext context)
    {
        var ctx = context.achievementDecl();
        (ResourceId id, string name) = ParseNamedIdentifier(ctx.namedIdentifier());
        return new Achievement(id, name)
        {
            Description = ctx.describtionDecl() is null ? null : ParseStringLiteral(ctx.describtionDecl().STRING_LITERAL()),
            Objectives = ctx.objectiveDecl().Select(ParseObjective).ToList(),
            CriteriaCategory = ctx.criterionDecl() is null ? (ResourceId?)null : ctx.criterionDecl().IDENTIFIER().GetText(),
        };
    }

    private Objective ParseObjective(PikaLangParser.ObjectiveDeclContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.namedIdentifier());
        return new Objective(id, name)
        {
            Description = context.describtionDecl() is null ? null : ParseStringLiteral(context.describtionDecl().STRING_LITERAL()),
            CriteriaCategory = context.criterionDecl() is null ? (ResourceId?)null : context.criterionDecl().IDENTIFIER().GetText(),
        };
    }

    private Category ParseCategory(PikaLangParser.CategoryDeclarationContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.categoryDecl().namedIdentifier());
        return new Category(id, name);
    }

    private Entity ParseEntity(PikaLangParser.EntityDeclarationContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.entityDecl().namedIdentifier());
        ResourceId classId = context.entityDecl().IDENTIFIER().GetText();
        var entity = new Entity(id, name, classId);
        return entity;
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
                IdUtilities.Normalize(ParseStringLiteral(ctx.STRING_LITERAL())),
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
