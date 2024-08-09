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
        var name = ParseStringLiteral(context.projectDecl().STRING_LITERAL());
        return new Project(name)
        {
            Objectives = context.projectDecl().objectiveDecl().Select(ParseObjective).ToList(),
        };
    }

    private Objective ParseObjective(PikaLangParser.ObjectiveDeclContext context)
    {
        var name = ParseStringLiteral(context.STRING_LITERAL());
        return new Objective(name)
        {
            Requirements = [new (context.IDENTIFIER().GetText())],
        };
    }

    private Class ParseClass(PikaLangParser.ClassDeclarationContext context)
    {
        ResourceId id = context.classDecl().IDENTIFIER().GetText();
        return new Class(id)
        {
            Stats = context.classDecl().statDecl().Select(ParseStat).ToList(),
        };
    }

    private Entity ParseEntity(PikaLangParser.EntityDeclarationContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.entityDecl().namedIdentifier());
        ResourceId classId = context.entityDecl().IDENTIFIER().GetText();
        return new Entity(id, name, classId);
    }

    private Stat ParseStat(PikaLangParser.StatDeclContext context)
    {
        (ResourceId id, string name) = ParseNamedIdentifier(context.namedIdentifier());
        return context.statType() switch
        {
            PikaLangParser.BoolStatTypeContext => new Stat(id, name, StatType.Boolean),
            PikaLangParser.IntRangeStatTypeContext intRangeContext => new Stat(id, name, StatType.IntegerRange)
            {
                Min = int.Parse(intRangeContext.INTEGER_LITERAL(0).GetText()),
                Max = int.Parse(intRangeContext.INTEGER_LITERAL(1).GetText()),
            },
            _ => throw new UnreachableException(),
        };
    }

    private (ResourceId id, string name) ParseNamedIdentifier(PikaLangParser.NamedIdentifierContext context)
    {
        var name = ParseStringLiteral(context.STRING_LITERAL());
        var idNode = context.IDENTIFIER();
        return idNode is null
            ? (ResourceId.InduceFromName(name), name)
            : (idNode.GetText(), name);
    }

    private string ParseStringLiteral(IParseTree stringLiteral)
    {
        return stringLiteral.GetText().Trim('\'');
    }
}
