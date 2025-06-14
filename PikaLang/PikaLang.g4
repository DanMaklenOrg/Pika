grammar PikaLang;

root: gameDecl importStmt* declStmt* EOF;

gameDecl: GAME namedIdentifier SEMICOLON;

declStmt
    : achievementDecl   #achievementDeclaration
    | categoryDecl      #categoryDeclaration
    | entityDecl        #entityDeclaration
    ;

importStmt: IMPORT STEAM OPEN_PARENTHESES INTEGER_LITERAL CLOSE_PARENTHESES SEMICOLON;

achievementDecl
    : ACHIEVEMENT namedIdentifier SEMICOLON
    | ACHIEVEMENT namedIdentifier OPEN_BRACES describtionDecl? objectiveDecl* criterionDecl? CLOSE_BRACES
    ;

objectiveDecl
    : OBJECTIVE namedIdentifier SEMICOLON
    | OBJECTIVE namedIdentifier OPEN_BRACES describtionDecl? criterionDecl? CLOSE_BRACES
    ;
criterionDecl: CRITERION IDENTIFIER SEMICOLON;
describtionDecl: DESCRIPTION STRING_LITERAL SEMICOLON;

categoryDecl: CATEGORY namedIdentifier SEMICOLON;
entityDecl: IDENTIFIER namedIdentifier SEMICOLON;

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
GAME: 'game';
ACHIEVEMENT: 'achievement';
OBJECTIVE: 'objective';
CRITERION: 'criterion';
CATEGORY: 'category';
DESCRIPTION: 'description';
IMPORT: 'import';
STEAM: 'steam';

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
