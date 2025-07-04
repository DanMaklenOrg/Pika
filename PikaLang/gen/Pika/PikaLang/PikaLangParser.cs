//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/omarnasr/Documents/Projects/Personal/Pika/PikaLang/PikaLang.g4 by ANTLR 4.13.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Pika.PikaLang {
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public partial class PikaLangParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		SINGLE_LINE_COMMENT=1, MULTI_LINE_COMMENT=2, WHITESPACE=3, GAME=4, ACHIEVEMENT=5, 
		OBJECTIVE=6, CRITERION=7, CATEGORY=8, TAG=9, DESCRIPTION=10, IMPORT=11, 
		STEAM=12, SEMICOLON=13, OPEN_BRACES=14, CLOSE_BRACES=15, OPEN_ANGULAR_BRACES=16, 
		CLOSE_ANGULAR_BRACES=17, OPEN_PARENTHESES=18, CLOSE_PARENTHESES=19, COMMA=20, 
		WITH_NAME=21, EQUALS=22, HASH=23, STRING_LITERAL=24, INTEGER_LITERAL=25, 
		IDENTIFIER=26;
	public const int
		RULE_root = 0, RULE_gameDecl = 1, RULE_declStmt = 2, RULE_importStmt = 3, 
		RULE_achievementDecl = 4, RULE_objectiveDecl = 5, RULE_criterionDecl = 6, 
		RULE_describtionDecl = 7, RULE_categoryDecl = 8, RULE_tagDecl = 9, RULE_entityDecl = 10, 
		RULE_entityTags = 11, RULE_namedIdentifier = 12;
	public static readonly string[] ruleNames = {
		"root", "gameDecl", "declStmt", "importStmt", "achievementDecl", "objectiveDecl", 
		"criterionDecl", "describtionDecl", "categoryDecl", "tagDecl", "entityDecl", 
		"entityTags", "namedIdentifier"
	};

	private static readonly string[] _LiteralNames = {
		null, null, null, null, "'game'", "'achievement'", "'objective'", "'criterion'", 
		"'category'", "'tag'", "'description'", "'import'", "'steam'", "';'", 
		"'{'", "'}'", "'<'", "'>'", "'('", "')'", "','", "'~>'", "'='", "'#'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "SINGLE_LINE_COMMENT", "MULTI_LINE_COMMENT", "WHITESPACE", "GAME", 
		"ACHIEVEMENT", "OBJECTIVE", "CRITERION", "CATEGORY", "TAG", "DESCRIPTION", 
		"IMPORT", "STEAM", "SEMICOLON", "OPEN_BRACES", "CLOSE_BRACES", "OPEN_ANGULAR_BRACES", 
		"CLOSE_ANGULAR_BRACES", "OPEN_PARENTHESES", "CLOSE_PARENTHESES", "COMMA", 
		"WITH_NAME", "EQUALS", "HASH", "STRING_LITERAL", "INTEGER_LITERAL", "IDENTIFIER"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "PikaLang.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static PikaLangParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public PikaLangParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public PikaLangParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class RootContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public GameDeclContext gameDecl() {
			return GetRuleContext<GameDeclContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(PikaLangParser.Eof, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ImportStmtContext[] importStmt() {
			return GetRuleContexts<ImportStmtContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ImportStmtContext importStmt(int i) {
			return GetRuleContext<ImportStmtContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public DeclStmtContext[] declStmt() {
			return GetRuleContexts<DeclStmtContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public DeclStmtContext declStmt(int i) {
			return GetRuleContext<DeclStmtContext>(i);
		}
		public RootContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_root; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitRoot(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public RootContext root() {
		RootContext _localctx = new RootContext(Context, State);
		EnterRule(_localctx, 0, RULE_root);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 26;
			gameDecl();
			State = 30;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==IMPORT) {
				{
				{
				State = 27;
				importStmt();
				}
				}
				State = 32;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 36;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & 67109664L) != 0)) {
				{
				{
				State = 33;
				declStmt();
				}
				}
				State = 38;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 39;
			Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class GameDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode GAME() { return GetToken(PikaLangParser.GAME, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public GameDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_gameDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitGameDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public GameDeclContext gameDecl() {
		GameDeclContext _localctx = new GameDeclContext(Context, State);
		EnterRule(_localctx, 2, RULE_gameDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 41;
			Match(GAME);
			State = 42;
			namedIdentifier();
			State = 43;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DeclStmtContext : ParserRuleContext {
		public DeclStmtContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_declStmt; } }
	 
		public DeclStmtContext() { }
		public virtual void CopyFrom(DeclStmtContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class EntityDeclarationContext : DeclStmtContext {
		[System.Diagnostics.DebuggerNonUserCode] public EntityDeclContext entityDecl() {
			return GetRuleContext<EntityDeclContext>(0);
		}
		public EntityDeclarationContext(DeclStmtContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEntityDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class CategoryDeclarationContext : DeclStmtContext {
		[System.Diagnostics.DebuggerNonUserCode] public CategoryDeclContext categoryDecl() {
			return GetRuleContext<CategoryDeclContext>(0);
		}
		public CategoryDeclarationContext(DeclStmtContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCategoryDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class AchievementDeclarationContext : DeclStmtContext {
		[System.Diagnostics.DebuggerNonUserCode] public AchievementDeclContext achievementDecl() {
			return GetRuleContext<AchievementDeclContext>(0);
		}
		public AchievementDeclarationContext(DeclStmtContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAchievementDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class TagDeclarationContext : DeclStmtContext {
		[System.Diagnostics.DebuggerNonUserCode] public TagDeclContext tagDecl() {
			return GetRuleContext<TagDeclContext>(0);
		}
		public TagDeclarationContext(DeclStmtContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitTagDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DeclStmtContext declStmt() {
		DeclStmtContext _localctx = new DeclStmtContext(Context, State);
		EnterRule(_localctx, 4, RULE_declStmt);
		try {
			State = 49;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case ACHIEVEMENT:
				_localctx = new AchievementDeclarationContext(_localctx);
				EnterOuterAlt(_localctx, 1);
				{
				State = 45;
				achievementDecl();
				}
				break;
			case CATEGORY:
				_localctx = new CategoryDeclarationContext(_localctx);
				EnterOuterAlt(_localctx, 2);
				{
				State = 46;
				categoryDecl();
				}
				break;
			case TAG:
				_localctx = new TagDeclarationContext(_localctx);
				EnterOuterAlt(_localctx, 3);
				{
				State = 47;
				tagDecl();
				}
				break;
			case IDENTIFIER:
				_localctx = new EntityDeclarationContext(_localctx);
				EnterOuterAlt(_localctx, 4);
				{
				State = 48;
				entityDecl();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ImportStmtContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IMPORT() { return GetToken(PikaLangParser.IMPORT, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STEAM() { return GetToken(PikaLangParser.STEAM, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_PARENTHESES() { return GetToken(PikaLangParser.OPEN_PARENTHESES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INTEGER_LITERAL() { return GetToken(PikaLangParser.INTEGER_LITERAL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_PARENTHESES() { return GetToken(PikaLangParser.CLOSE_PARENTHESES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public ImportStmtContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_importStmt; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitImportStmt(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ImportStmtContext importStmt() {
		ImportStmtContext _localctx = new ImportStmtContext(Context, State);
		EnterRule(_localctx, 6, RULE_importStmt);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 51;
			Match(IMPORT);
			State = 52;
			Match(STEAM);
			State = 53;
			Match(OPEN_PARENTHESES);
			State = 54;
			Match(INTEGER_LITERAL);
			State = 55;
			Match(CLOSE_PARENTHESES);
			State = 56;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AchievementDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ACHIEVEMENT() { return GetToken(PikaLangParser.ACHIEVEMENT, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_BRACES() { return GetToken(PikaLangParser.OPEN_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_BRACES() { return GetToken(PikaLangParser.CLOSE_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public DescribtionDeclContext describtionDecl() {
			return GetRuleContext<DescribtionDeclContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ObjectiveDeclContext[] objectiveDecl() {
			return GetRuleContexts<ObjectiveDeclContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ObjectiveDeclContext objectiveDecl(int i) {
			return GetRuleContext<ObjectiveDeclContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public CriterionDeclContext criterionDecl() {
			return GetRuleContext<CriterionDeclContext>(0);
		}
		public AchievementDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_achievementDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAchievementDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AchievementDeclContext achievementDecl() {
		AchievementDeclContext _localctx = new AchievementDeclContext(Context, State);
		EnterRule(_localctx, 8, RULE_achievementDecl);
		int _la;
		try {
			State = 79;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,6,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 58;
				Match(ACHIEVEMENT);
				State = 59;
				namedIdentifier();
				State = 60;
				Match(SEMICOLON);
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 62;
				Match(ACHIEVEMENT);
				State = 63;
				namedIdentifier();
				State = 64;
				Match(OPEN_BRACES);
				State = 66;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				if (_la==DESCRIPTION) {
					{
					State = 65;
					describtionDecl();
					}
				}

				State = 71;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				while (_la==OBJECTIVE) {
					{
					{
					State = 68;
					objectiveDecl();
					}
					}
					State = 73;
					ErrorHandler.Sync(this);
					_la = TokenStream.LA(1);
				}
				State = 75;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				if (_la==CRITERION) {
					{
					State = 74;
					criterionDecl();
					}
				}

				State = 77;
				Match(CLOSE_BRACES);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ObjectiveDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OBJECTIVE() { return GetToken(PikaLangParser.OBJECTIVE, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_BRACES() { return GetToken(PikaLangParser.OPEN_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_BRACES() { return GetToken(PikaLangParser.CLOSE_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public DescribtionDeclContext describtionDecl() {
			return GetRuleContext<DescribtionDeclContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public CriterionDeclContext criterionDecl() {
			return GetRuleContext<CriterionDeclContext>(0);
		}
		public ObjectiveDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_objectiveDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitObjectiveDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ObjectiveDeclContext objectiveDecl() {
		ObjectiveDeclContext _localctx = new ObjectiveDeclContext(Context, State);
		EnterRule(_localctx, 10, RULE_objectiveDecl);
		int _la;
		try {
			State = 96;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,9,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 81;
				Match(OBJECTIVE);
				State = 82;
				namedIdentifier();
				State = 83;
				Match(SEMICOLON);
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 85;
				Match(OBJECTIVE);
				State = 86;
				namedIdentifier();
				State = 87;
				Match(OPEN_BRACES);
				State = 89;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				if (_la==DESCRIPTION) {
					{
					State = 88;
					describtionDecl();
					}
				}

				State = 92;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				if (_la==CRITERION) {
					{
					State = 91;
					criterionDecl();
					}
				}

				State = 94;
				Match(CLOSE_BRACES);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class CriterionDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CRITERION() { return GetToken(PikaLangParser.CRITERION, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public CriterionDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_criterionDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCriterionDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public CriterionDeclContext criterionDecl() {
		CriterionDeclContext _localctx = new CriterionDeclContext(Context, State);
		EnterRule(_localctx, 12, RULE_criterionDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 98;
			Match(CRITERION);
			State = 99;
			Match(IDENTIFIER);
			State = 100;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DescribtionDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode DESCRIPTION() { return GetToken(PikaLangParser.DESCRIPTION, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STRING_LITERAL() { return GetToken(PikaLangParser.STRING_LITERAL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public DescribtionDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_describtionDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDescribtionDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DescribtionDeclContext describtionDecl() {
		DescribtionDeclContext _localctx = new DescribtionDeclContext(Context, State);
		EnterRule(_localctx, 14, RULE_describtionDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 102;
			Match(DESCRIPTION);
			State = 103;
			Match(STRING_LITERAL);
			State = 104;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class CategoryDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CATEGORY() { return GetToken(PikaLangParser.CATEGORY, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public CategoryDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_categoryDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCategoryDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public CategoryDeclContext categoryDecl() {
		CategoryDeclContext _localctx = new CategoryDeclContext(Context, State);
		EnterRule(_localctx, 16, RULE_categoryDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 106;
			Match(CATEGORY);
			State = 107;
			namedIdentifier();
			State = 108;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class TagDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode TAG() { return GetToken(PikaLangParser.TAG, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public TagDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_tagDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitTagDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public TagDeclContext tagDecl() {
		TagDeclContext _localctx = new TagDeclContext(Context, State);
		EnterRule(_localctx, 18, RULE_tagDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 110;
			Match(TAG);
			State = 111;
			namedIdentifier();
			State = 112;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EntityDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public EntityTagsContext[] entityTags() {
			return GetRuleContexts<EntityTagsContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public EntityTagsContext entityTags(int i) {
			return GetRuleContext<EntityTagsContext>(i);
		}
		public EntityDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_entityDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEntityDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public EntityDeclContext entityDecl() {
		EntityDeclContext _localctx = new EntityDeclContext(Context, State);
		EnterRule(_localctx, 20, RULE_entityDecl);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 114;
			Match(IDENTIFIER);
			State = 115;
			namedIdentifier();
			State = 119;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==HASH) {
				{
				{
				State = 116;
				entityTags();
				}
				}
				State = 121;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 122;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EntityTagsContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode HASH() { return GetToken(PikaLangParser.HASH, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		public EntityTagsContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_entityTags; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEntityTags(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public EntityTagsContext entityTags() {
		EntityTagsContext _localctx = new EntityTagsContext(Context, State);
		EnterRule(_localctx, 22, RULE_entityTags);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 124;
			Match(HASH);
			State = 125;
			Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NamedIdentifierContext : ParserRuleContext {
		public NamedIdentifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_namedIdentifier; } }
	 
		public NamedIdentifierContext() { }
		public virtual void CopyFrom(NamedIdentifierContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class IdWithNameContext : NamedIdentifierContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode WITH_NAME() { return GetToken(PikaLangParser.WITH_NAME, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STRING_LITERAL() { return GetToken(PikaLangParser.STRING_LITERAL, 0); }
		public IdWithNameContext(NamedIdentifierContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitIdWithName(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class IdOnlyContext : NamedIdentifierContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		public IdOnlyContext(NamedIdentifierContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitIdOnly(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class NameOnlyContext : NamedIdentifierContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STRING_LITERAL() { return GetToken(PikaLangParser.STRING_LITERAL, 0); }
		public NameOnlyContext(NamedIdentifierContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNameOnly(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NamedIdentifierContext namedIdentifier() {
		NamedIdentifierContext _localctx = new NamedIdentifierContext(Context, State);
		EnterRule(_localctx, 24, RULE_namedIdentifier);
		try {
			State = 132;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,11,Context) ) {
			case 1:
				_localctx = new IdWithNameContext(_localctx);
				EnterOuterAlt(_localctx, 1);
				{
				State = 127;
				Match(IDENTIFIER);
				State = 128;
				Match(WITH_NAME);
				State = 129;
				Match(STRING_LITERAL);
				}
				break;
			case 2:
				_localctx = new NameOnlyContext(_localctx);
				EnterOuterAlt(_localctx, 2);
				{
				State = 130;
				Match(STRING_LITERAL);
				}
				break;
			case 3:
				_localctx = new IdOnlyContext(_localctx);
				EnterOuterAlt(_localctx, 3);
				{
				State = 131;
				Match(IDENTIFIER);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,26,135,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,6,2,7,
		7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,1,0,1,0,5,0,29,8,0,10,
		0,12,0,32,9,0,1,0,5,0,35,8,0,10,0,12,0,38,9,0,1,0,1,0,1,1,1,1,1,1,1,1,
		1,2,1,2,1,2,1,2,3,2,50,8,2,1,3,1,3,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,
		1,4,1,4,1,4,1,4,3,4,67,8,4,1,4,5,4,70,8,4,10,4,12,4,73,9,4,1,4,3,4,76,
		8,4,1,4,1,4,3,4,80,8,4,1,5,1,5,1,5,1,5,1,5,1,5,1,5,1,5,3,5,90,8,5,1,5,
		3,5,93,8,5,1,5,1,5,3,5,97,8,5,1,6,1,6,1,6,1,6,1,7,1,7,1,7,1,7,1,8,1,8,
		1,8,1,8,1,9,1,9,1,9,1,9,1,10,1,10,1,10,5,10,118,8,10,10,10,12,10,121,9,
		10,1,10,1,10,1,11,1,11,1,11,1,12,1,12,1,12,1,12,1,12,3,12,133,8,12,1,12,
		0,0,13,0,2,4,6,8,10,12,14,16,18,20,22,24,0,0,136,0,26,1,0,0,0,2,41,1,0,
		0,0,4,49,1,0,0,0,6,51,1,0,0,0,8,79,1,0,0,0,10,96,1,0,0,0,12,98,1,0,0,0,
		14,102,1,0,0,0,16,106,1,0,0,0,18,110,1,0,0,0,20,114,1,0,0,0,22,124,1,0,
		0,0,24,132,1,0,0,0,26,30,3,2,1,0,27,29,3,6,3,0,28,27,1,0,0,0,29,32,1,0,
		0,0,30,28,1,0,0,0,30,31,1,0,0,0,31,36,1,0,0,0,32,30,1,0,0,0,33,35,3,4,
		2,0,34,33,1,0,0,0,35,38,1,0,0,0,36,34,1,0,0,0,36,37,1,0,0,0,37,39,1,0,
		0,0,38,36,1,0,0,0,39,40,5,0,0,1,40,1,1,0,0,0,41,42,5,4,0,0,42,43,3,24,
		12,0,43,44,5,13,0,0,44,3,1,0,0,0,45,50,3,8,4,0,46,50,3,16,8,0,47,50,3,
		18,9,0,48,50,3,20,10,0,49,45,1,0,0,0,49,46,1,0,0,0,49,47,1,0,0,0,49,48,
		1,0,0,0,50,5,1,0,0,0,51,52,5,11,0,0,52,53,5,12,0,0,53,54,5,18,0,0,54,55,
		5,25,0,0,55,56,5,19,0,0,56,57,5,13,0,0,57,7,1,0,0,0,58,59,5,5,0,0,59,60,
		3,24,12,0,60,61,5,13,0,0,61,80,1,0,0,0,62,63,5,5,0,0,63,64,3,24,12,0,64,
		66,5,14,0,0,65,67,3,14,7,0,66,65,1,0,0,0,66,67,1,0,0,0,67,71,1,0,0,0,68,
		70,3,10,5,0,69,68,1,0,0,0,70,73,1,0,0,0,71,69,1,0,0,0,71,72,1,0,0,0,72,
		75,1,0,0,0,73,71,1,0,0,0,74,76,3,12,6,0,75,74,1,0,0,0,75,76,1,0,0,0,76,
		77,1,0,0,0,77,78,5,15,0,0,78,80,1,0,0,0,79,58,1,0,0,0,79,62,1,0,0,0,80,
		9,1,0,0,0,81,82,5,6,0,0,82,83,3,24,12,0,83,84,5,13,0,0,84,97,1,0,0,0,85,
		86,5,6,0,0,86,87,3,24,12,0,87,89,5,14,0,0,88,90,3,14,7,0,89,88,1,0,0,0,
		89,90,1,0,0,0,90,92,1,0,0,0,91,93,3,12,6,0,92,91,1,0,0,0,92,93,1,0,0,0,
		93,94,1,0,0,0,94,95,5,15,0,0,95,97,1,0,0,0,96,81,1,0,0,0,96,85,1,0,0,0,
		97,11,1,0,0,0,98,99,5,7,0,0,99,100,5,26,0,0,100,101,5,13,0,0,101,13,1,
		0,0,0,102,103,5,10,0,0,103,104,5,24,0,0,104,105,5,13,0,0,105,15,1,0,0,
		0,106,107,5,8,0,0,107,108,3,24,12,0,108,109,5,13,0,0,109,17,1,0,0,0,110,
		111,5,9,0,0,111,112,3,24,12,0,112,113,5,13,0,0,113,19,1,0,0,0,114,115,
		5,26,0,0,115,119,3,24,12,0,116,118,3,22,11,0,117,116,1,0,0,0,118,121,1,
		0,0,0,119,117,1,0,0,0,119,120,1,0,0,0,120,122,1,0,0,0,121,119,1,0,0,0,
		122,123,5,13,0,0,123,21,1,0,0,0,124,125,5,23,0,0,125,126,5,26,0,0,126,
		23,1,0,0,0,127,128,5,26,0,0,128,129,5,21,0,0,129,133,5,24,0,0,130,133,
		5,24,0,0,131,133,5,26,0,0,132,127,1,0,0,0,132,130,1,0,0,0,132,131,1,0,
		0,0,133,25,1,0,0,0,12,30,36,49,66,71,75,79,89,92,96,119,132
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Pika.PikaLang
