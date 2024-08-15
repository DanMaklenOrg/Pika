grammar PikaLang;

root: domainDecl declStmt+ EOF;

domainDecl: DOMAIN namedIdentifier SEMICOLON;

declStmt
    : projectDecl   #projectDeclaration
    | classDecl     #classDeclaration
    | entityDecl    #entityDeclaration
    ;

projectDecl: PROJECT namedIdentifier OPEN_BRACES objectiveDecl+ CLOSE_BRACES;
objectiveDecl: OBJECTIVE namedIdentifier OPEN_BRACES requireDecl+ CLOSE_BRACES;
requireDecl: REQUIRE IDENTIFIER SEMICOLON;

classDecl: CLASS namedIdentifier OPEN_BRACES attrDecl* statDecl+ CLOSE_BRACES;

entityDecl
    : IDENTIFIER namedIdentifier SEMICOLON
    | IDENTIFIER namedIdentifier OPEN_BRACES attrDecl* statDecl* CLOSE_BRACES
    ;

statDecl: STAT OPEN_ANGULAR_BRACES statType CLOSE_ANGULAR_BRACES namedIdentifier SEMICOLON;
statType
    : BOOL                                                              #boolStatType
    | INT OPEN_PARENTHESES intOrAttribute COMMA intOrAttribute CLOSE_PARENTHESES    #intRangeStatType
    ;
intOrAttribute: INTEGER_LITERAL | IDENTIFIER;

attrDecl: ATTRIBUTE IDENTIFIER EQUALS INTEGER_LITERAL SEMICOLON;

namedIdentifier
    : IDENTIFIER WITH_NAME STRING_LITERAL   #idWithName
    | STRING_LITERAL                        #nameOnly
    | IDENTIFIER                            #idOnly
    ;

// Comments & Whitespaces
SINGLE_LINE_COMMENT: '//' ~[\n]* -> skip;
MULTI_LINE_COMMENT: '/*' .*? '*/' -> skip;
WHITESPACE: [ \n\t]+ -> skip;

// Keywords
DOMAIN: 'domain';
PROJECT: 'project';
OBJECTIVE: 'objective';
REQUIRE: 'require';
CLASS: 'class';
STAT: 'stat';
BOOL: 'bool';
INT: 'int';
ATTRIBUTE: 'attribute';

// Operators & Punctuations
SEMICOLON: ';';
OPEN_BRACES: '{';
CLOSE_BRACES: '}';
OPEN_ANGULAR_BRACES: '<';
CLOSE_ANGULAR_BRACES: '>';
OPEN_PARENTHESES: '(';
CLOSE_PARENTHESES: ')';
COMMA: ',';
WITH_NAME: '~>';
EQUALS: '=';

// Literals
STRING_LITERAL: '\'' (~[\n'] | '\\\'')* '\'';
INTEGER_LITERAL: [0-9]+;

// Identifiers
IDENTIFIER: [0-9a-z][0-9a-z_]*;
