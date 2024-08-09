//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/omarnasr/Documents/Projects/Personal/Pika/PikaLang/PikaLang.g4 by ANTLR 4.13.1

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

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class PikaLangParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		SINGLE_LINE_COMMENT=1, MULTI_LINE_COMMENT=2, WHITESPACE=3, DOMAIN=4, PROJECT=5, 
		OBJECTIVE=6, REQUIRE=7, CLASS=8, STAT=9, BOOL=10, INT=11, STRING_LITERAL=12, 
		INTEGER_LITERAL=13, SEMICOLON=14, OPEN_BRACES=15, CLOSE_BRACES=16, OPEN_ANGULAR_BRACES=17, 
		CLOSE_ANGULAR_BRACES=18, OPEN_PARENTHESES=19, CLOSE_PARENTHESES=20, COMMA=21, 
		WITH_NAME=22, IDENTIFIER=23;
	public const int
		RULE_root = 0, RULE_domainDecl = 1, RULE_declStmt = 2, RULE_projectDecl = 3, 
		RULE_objectiveDecl = 4, RULE_requireDecl = 5, RULE_classDecl = 6, RULE_entityDecl = 7, 
		RULE_statDecl = 8, RULE_statType = 9, RULE_namedIdentifier = 10;
	public static readonly string[] ruleNames = {
		"root", "domainDecl", "declStmt", "projectDecl", "objectiveDecl", "requireDecl", 
		"classDecl", "entityDecl", "statDecl", "statType", "namedIdentifier"
	};

	private static readonly string[] _LiteralNames = {
		null, null, null, null, "'domain'", "'project'", "'objective'", "'require'", 
		"'class'", "'stat'", "'bool'", "'int'", null, null, "';'", "'{'", "'}'", 
		"'<'", "'>'", "'('", "')'", "','", "'~>'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "SINGLE_LINE_COMMENT", "MULTI_LINE_COMMENT", "WHITESPACE", "DOMAIN", 
		"PROJECT", "OBJECTIVE", "REQUIRE", "CLASS", "STAT", "BOOL", "INT", "STRING_LITERAL", 
		"INTEGER_LITERAL", "SEMICOLON", "OPEN_BRACES", "CLOSE_BRACES", "OPEN_ANGULAR_BRACES", 
		"CLOSE_ANGULAR_BRACES", "OPEN_PARENTHESES", "CLOSE_PARENTHESES", "COMMA", 
		"WITH_NAME", "IDENTIFIER"
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
		[System.Diagnostics.DebuggerNonUserCode] public DomainDeclContext domainDecl() {
			return GetRuleContext<DomainDeclContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(PikaLangParser.Eof, 0); }
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
			State = 22;
			domainDecl();
			State = 24;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 23;
				declStmt();
				}
				}
				State = 26;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & 8388896L) != 0) );
			State = 28;
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

	public partial class DomainDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode DOMAIN() { return GetToken(PikaLangParser.DOMAIN, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public DomainDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_domainDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDomainDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DomainDeclContext domainDecl() {
		DomainDeclContext _localctx = new DomainDeclContext(Context, State);
		EnterRule(_localctx, 2, RULE_domainDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 30;
			Match(DOMAIN);
			State = 31;
			namedIdentifier();
			State = 32;
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
	public partial class ProjectDeclarationContext : DeclStmtContext {
		[System.Diagnostics.DebuggerNonUserCode] public ProjectDeclContext projectDecl() {
			return GetRuleContext<ProjectDeclContext>(0);
		}
		public ProjectDeclarationContext(DeclStmtContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProjectDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ClassDeclarationContext : DeclStmtContext {
		[System.Diagnostics.DebuggerNonUserCode] public ClassDeclContext classDecl() {
			return GetRuleContext<ClassDeclContext>(0);
		}
		public ClassDeclarationContext(DeclStmtContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClassDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DeclStmtContext declStmt() {
		DeclStmtContext _localctx = new DeclStmtContext(Context, State);
		EnterRule(_localctx, 4, RULE_declStmt);
		try {
			State = 37;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case PROJECT:
				_localctx = new ProjectDeclarationContext(_localctx);
				EnterOuterAlt(_localctx, 1);
				{
				State = 34;
				projectDecl();
				}
				break;
			case CLASS:
				_localctx = new ClassDeclarationContext(_localctx);
				EnterOuterAlt(_localctx, 2);
				{
				State = 35;
				classDecl();
				}
				break;
			case IDENTIFIER:
				_localctx = new EntityDeclarationContext(_localctx);
				EnterOuterAlt(_localctx, 3);
				{
				State = 36;
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

	public partial class ProjectDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode PROJECT() { return GetToken(PikaLangParser.PROJECT, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STRING_LITERAL() { return GetToken(PikaLangParser.STRING_LITERAL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_BRACES() { return GetToken(PikaLangParser.OPEN_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_BRACES() { return GetToken(PikaLangParser.CLOSE_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ObjectiveDeclContext[] objectiveDecl() {
			return GetRuleContexts<ObjectiveDeclContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ObjectiveDeclContext objectiveDecl(int i) {
			return GetRuleContext<ObjectiveDeclContext>(i);
		}
		public ProjectDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_projectDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProjectDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProjectDeclContext projectDecl() {
		ProjectDeclContext _localctx = new ProjectDeclContext(Context, State);
		EnterRule(_localctx, 6, RULE_projectDecl);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 39;
			Match(PROJECT);
			State = 40;
			Match(STRING_LITERAL);
			State = 41;
			Match(OPEN_BRACES);
			State = 43;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 42;
				objectiveDecl();
				}
				}
				State = 45;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==OBJECTIVE );
			State = 47;
			Match(CLOSE_BRACES);
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
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STRING_LITERAL() { return GetToken(PikaLangParser.STRING_LITERAL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_BRACES() { return GetToken(PikaLangParser.OPEN_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_BRACES() { return GetToken(PikaLangParser.CLOSE_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public RequireDeclContext[] requireDecl() {
			return GetRuleContexts<RequireDeclContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public RequireDeclContext requireDecl(int i) {
			return GetRuleContext<RequireDeclContext>(i);
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
		EnterRule(_localctx, 8, RULE_objectiveDecl);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 49;
			Match(OBJECTIVE);
			State = 50;
			Match(STRING_LITERAL);
			State = 51;
			Match(OPEN_BRACES);
			State = 53;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 52;
				requireDecl();
				}
				}
				State = 55;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==REQUIRE );
			State = 57;
			Match(CLOSE_BRACES);
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

	public partial class RequireDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode REQUIRE() { return GetToken(PikaLangParser.REQUIRE, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public RequireDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_requireDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitRequireDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public RequireDeclContext requireDecl() {
		RequireDeclContext _localctx = new RequireDeclContext(Context, State);
		EnterRule(_localctx, 10, RULE_requireDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 59;
			Match(REQUIRE);
			State = 60;
			Match(IDENTIFIER);
			State = 61;
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

	public partial class ClassDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLASS() { return GetToken(PikaLangParser.CLASS, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_BRACES() { return GetToken(PikaLangParser.OPEN_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_BRACES() { return GetToken(PikaLangParser.CLOSE_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public StatDeclContext[] statDecl() {
			return GetRuleContexts<StatDeclContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public StatDeclContext statDecl(int i) {
			return GetRuleContext<StatDeclContext>(i);
		}
		public ClassDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_classDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClassDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ClassDeclContext classDecl() {
		ClassDeclContext _localctx = new ClassDeclContext(Context, State);
		EnterRule(_localctx, 12, RULE_classDecl);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 63;
			Match(CLASS);
			State = 64;
			Match(IDENTIFIER);
			State = 65;
			Match(OPEN_BRACES);
			State = 67;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 66;
				statDecl();
				}
				}
				State = 69;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==STAT );
			State = 71;
			Match(CLOSE_BRACES);
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
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_BRACES() { return GetToken(PikaLangParser.OPEN_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_BRACES() { return GetToken(PikaLangParser.CLOSE_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public StatDeclContext[] statDecl() {
			return GetRuleContexts<StatDeclContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public StatDeclContext statDecl(int i) {
			return GetRuleContext<StatDeclContext>(i);
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
		EnterRule(_localctx, 14, RULE_entityDecl);
		int _la;
		try {
			State = 87;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,6,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 73;
				Match(IDENTIFIER);
				State = 74;
				namedIdentifier();
				State = 75;
				Match(SEMICOLON);
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 77;
				Match(IDENTIFIER);
				State = 78;
				namedIdentifier();
				State = 79;
				Match(OPEN_BRACES);
				State = 81;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				do {
					{
					{
					State = 80;
					statDecl();
					}
					}
					State = 83;
					ErrorHandler.Sync(this);
					_la = TokenStream.LA(1);
				} while ( _la==STAT );
				State = 85;
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

	public partial class StatDeclContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STAT() { return GetToken(PikaLangParser.STAT, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_ANGULAR_BRACES() { return GetToken(PikaLangParser.OPEN_ANGULAR_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public StatTypeContext statType() {
			return GetRuleContext<StatTypeContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_ANGULAR_BRACES() { return GetToken(PikaLangParser.CLOSE_ANGULAR_BRACES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public NamedIdentifierContext namedIdentifier() {
			return GetRuleContext<NamedIdentifierContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(PikaLangParser.SEMICOLON, 0); }
		public StatDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_statDecl; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStatDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StatDeclContext statDecl() {
		StatDeclContext _localctx = new StatDeclContext(Context, State);
		EnterRule(_localctx, 16, RULE_statDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 89;
			Match(STAT);
			State = 90;
			Match(OPEN_ANGULAR_BRACES);
			State = 91;
			statType();
			State = 92;
			Match(CLOSE_ANGULAR_BRACES);
			State = 93;
			namedIdentifier();
			State = 94;
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

	public partial class StatTypeContext : ParserRuleContext {
		public StatTypeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_statType; } }
	 
		public StatTypeContext() { }
		public virtual void CopyFrom(StatTypeContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class IntRangeStatTypeContext : StatTypeContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INT() { return GetToken(PikaLangParser.INT, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_PARENTHESES() { return GetToken(PikaLangParser.OPEN_PARENTHESES, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] INTEGER_LITERAL() { return GetTokens(PikaLangParser.INTEGER_LITERAL); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INTEGER_LITERAL(int i) {
			return GetToken(PikaLangParser.INTEGER_LITERAL, i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode COMMA() { return GetToken(PikaLangParser.COMMA, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_PARENTHESES() { return GetToken(PikaLangParser.CLOSE_PARENTHESES, 0); }
		public IntRangeStatTypeContext(StatTypeContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitIntRangeStatType(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class BoolStatTypeContext : StatTypeContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode BOOL() { return GetToken(PikaLangParser.BOOL, 0); }
		public BoolStatTypeContext(StatTypeContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitBoolStatType(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StatTypeContext statType() {
		StatTypeContext _localctx = new StatTypeContext(Context, State);
		EnterRule(_localctx, 18, RULE_statType);
		try {
			State = 103;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case BOOL:
				_localctx = new BoolStatTypeContext(_localctx);
				EnterOuterAlt(_localctx, 1);
				{
				State = 96;
				Match(BOOL);
				}
				break;
			case INT:
				_localctx = new IntRangeStatTypeContext(_localctx);
				EnterOuterAlt(_localctx, 2);
				{
				State = 97;
				Match(INT);
				State = 98;
				Match(OPEN_PARENTHESES);
				State = 99;
				Match(INTEGER_LITERAL);
				State = 100;
				Match(COMMA);
				State = 101;
				Match(INTEGER_LITERAL);
				State = 102;
				Match(CLOSE_PARENTHESES);
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

	public partial class NamedIdentifierContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(PikaLangParser.IDENTIFIER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode WITH_NAME() { return GetToken(PikaLangParser.WITH_NAME, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode STRING_LITERAL() { return GetToken(PikaLangParser.STRING_LITERAL, 0); }
		public NamedIdentifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_namedIdentifier; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IPikaLangVisitor<TResult> typedVisitor = visitor as IPikaLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNamedIdentifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NamedIdentifierContext namedIdentifier() {
		NamedIdentifierContext _localctx = new NamedIdentifierContext(Context, State);
		EnterRule(_localctx, 20, RULE_namedIdentifier);
		try {
			State = 109;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case IDENTIFIER:
				EnterOuterAlt(_localctx, 1);
				{
				State = 105;
				Match(IDENTIFIER);
				State = 106;
				Match(WITH_NAME);
				State = 107;
				Match(STRING_LITERAL);
				}
				break;
			case STRING_LITERAL:
				EnterOuterAlt(_localctx, 2);
				{
				State = 108;
				Match(STRING_LITERAL);
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

	private static int[] _serializedATN = {
		4,1,23,112,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,6,2,7,
		7,7,2,8,7,8,2,9,7,9,2,10,7,10,1,0,1,0,4,0,25,8,0,11,0,12,0,26,1,0,1,0,
		1,1,1,1,1,1,1,1,1,2,1,2,1,2,3,2,38,8,2,1,3,1,3,1,3,1,3,4,3,44,8,3,11,3,
		12,3,45,1,3,1,3,1,4,1,4,1,4,1,4,4,4,54,8,4,11,4,12,4,55,1,4,1,4,1,5,1,
		5,1,5,1,5,1,6,1,6,1,6,1,6,4,6,68,8,6,11,6,12,6,69,1,6,1,6,1,7,1,7,1,7,
		1,7,1,7,1,7,1,7,1,7,4,7,82,8,7,11,7,12,7,83,1,7,1,7,3,7,88,8,7,1,8,1,8,
		1,8,1,8,1,8,1,8,1,8,1,9,1,9,1,9,1,9,1,9,1,9,1,9,3,9,104,8,9,1,10,1,10,
		1,10,1,10,3,10,110,8,10,1,10,0,0,11,0,2,4,6,8,10,12,14,16,18,20,0,0,110,
		0,22,1,0,0,0,2,30,1,0,0,0,4,37,1,0,0,0,6,39,1,0,0,0,8,49,1,0,0,0,10,59,
		1,0,0,0,12,63,1,0,0,0,14,87,1,0,0,0,16,89,1,0,0,0,18,103,1,0,0,0,20,109,
		1,0,0,0,22,24,3,2,1,0,23,25,3,4,2,0,24,23,1,0,0,0,25,26,1,0,0,0,26,24,
		1,0,0,0,26,27,1,0,0,0,27,28,1,0,0,0,28,29,5,0,0,1,29,1,1,0,0,0,30,31,5,
		4,0,0,31,32,3,20,10,0,32,33,5,14,0,0,33,3,1,0,0,0,34,38,3,6,3,0,35,38,
		3,12,6,0,36,38,3,14,7,0,37,34,1,0,0,0,37,35,1,0,0,0,37,36,1,0,0,0,38,5,
		1,0,0,0,39,40,5,5,0,0,40,41,5,12,0,0,41,43,5,15,0,0,42,44,3,8,4,0,43,42,
		1,0,0,0,44,45,1,0,0,0,45,43,1,0,0,0,45,46,1,0,0,0,46,47,1,0,0,0,47,48,
		5,16,0,0,48,7,1,0,0,0,49,50,5,6,0,0,50,51,5,12,0,0,51,53,5,15,0,0,52,54,
		3,10,5,0,53,52,1,0,0,0,54,55,1,0,0,0,55,53,1,0,0,0,55,56,1,0,0,0,56,57,
		1,0,0,0,57,58,5,16,0,0,58,9,1,0,0,0,59,60,5,7,0,0,60,61,5,23,0,0,61,62,
		5,14,0,0,62,11,1,0,0,0,63,64,5,8,0,0,64,65,5,23,0,0,65,67,5,15,0,0,66,
		68,3,16,8,0,67,66,1,0,0,0,68,69,1,0,0,0,69,67,1,0,0,0,69,70,1,0,0,0,70,
		71,1,0,0,0,71,72,5,16,0,0,72,13,1,0,0,0,73,74,5,23,0,0,74,75,3,20,10,0,
		75,76,5,14,0,0,76,88,1,0,0,0,77,78,5,23,0,0,78,79,3,20,10,0,79,81,5,15,
		0,0,80,82,3,16,8,0,81,80,1,0,0,0,82,83,1,0,0,0,83,81,1,0,0,0,83,84,1,0,
		0,0,84,85,1,0,0,0,85,86,5,16,0,0,86,88,1,0,0,0,87,73,1,0,0,0,87,77,1,0,
		0,0,88,15,1,0,0,0,89,90,5,9,0,0,90,91,5,17,0,0,91,92,3,18,9,0,92,93,5,
		18,0,0,93,94,3,20,10,0,94,95,5,14,0,0,95,17,1,0,0,0,96,104,5,10,0,0,97,
		98,5,11,0,0,98,99,5,19,0,0,99,100,5,13,0,0,100,101,5,21,0,0,101,102,5,
		13,0,0,102,104,5,20,0,0,103,96,1,0,0,0,103,97,1,0,0,0,104,19,1,0,0,0,105,
		106,5,23,0,0,106,107,5,22,0,0,107,110,5,12,0,0,108,110,5,12,0,0,109,105,
		1,0,0,0,109,108,1,0,0,0,110,21,1,0,0,0,9,26,37,45,55,69,83,87,103,109
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Pika.PikaLang