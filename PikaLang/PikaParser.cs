using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Pika.Model;

namespace Pika.PikaLang;

public class PikaParser
{
    public Domain Parse(TextReader textReader)
    {
        var antlrStream = new AntlrInputStream(textReader);
        var lexer = new PikaLangLexer(antlrStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new PikaLangParser(tokenStream);
        var parseTree = parser.root();

        var domain = ParseDomain(parseTree);
        return domain;
    }

    private Domain ParseDomain(PikaLangParser.RootContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.domainDecl().namedIdentifier());
        var domain = new Domain(id, name);

        domain.Projects.AddRange(context.declStmt().OfType<PikaLangParser.ProjectDeclarationContext>().Select(ParseProject));
        domain.Classes.AddRange(context.declStmt().OfType<PikaLangParser.ClassDeclarationContext>().Select(ParseClass));
        domain.Entities.AddRange(context.declStmt().OfType<PikaLangParser.EntityDeclarationContext>().Select(ParseEntity));

        return domain;
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
            Stats = context.classDecl().statDecl().Select(ParseStat).ToList(),
        };
    }

    private Entity ParseEntity(PikaLangParser.EntityDeclarationContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.entityDecl().namedIdentifier());
        ResourceId classId = context.entityDecl().IDENTIFIER().GetText();
        var entity = new Entity(id, name, classId)
        {
            Stats = context.entityDecl().statDecl().Select(ParseStat).ToList(),
        };
        return entity;
    }

    private Stat ParseStat(PikaLangParser.StatDeclContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.namedIdentifier());
        return context.statType() switch
        {
            PikaLangParser.BoolStatTypeContext => new Stat(id, name, Stat.StatType.Boolean),
            PikaLangParser.IntRangeStatTypeContext intRangeContext => new Stat(id, name, Stat.StatType.IntegerRange)
            {
                Min = int.Parse(intRangeContext.INTEGER_LITERAL(0).GetText()),
                Max = int.Parse(intRangeContext.INTEGER_LITERAL(1).GetText()),
            },
            _ => throw new UnreachableException(),
        };
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
}
