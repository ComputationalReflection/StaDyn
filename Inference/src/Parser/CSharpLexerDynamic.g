header
{
	using System.IO;
	using System.Globalization;

	using TokenStreamSelector		        = antlr.TokenStreamSelector;
}

options
{
	language 	= "CSharp";	
	namespace	= "Parser";	
}

class CSharpLexerDynamic extends CSharpLexer;

options
{
	importVocab							= CSharpLexer;
	exportVocab							= CSharpLexerDynamic;
	charVocabulary						= '\u0000'..'\uFFFE';	// All UNICODE characters except \uFFFF [and \u0000 to \u0002 used by ANTLR]
	k									= 3;					// two characters of lookahead
	testLiterals						= false;   				// don't automatically test for literals
	//defaultErrorHandler				= true;
	defaultErrorHandler					= false;
	codeGenMakeSwitchThreshold 			= 5;  // Some optimizations
	codeGenBitsetTestThreshold			= 5;
	classHeaderSuffix					= ICSharpLexer;
}

//======================================
// Section A.1.7 Keywords
//
tokens										// MUST be kept in sync with "keywordsTable" Hashtable below!!
{
	DYNAMIC 		= "dynamic";
}

{
	/// <summary>
	///   A <see cref="TokenStreamSelector"> for switching between this Lexer and the Preprocessor Lexer.
	/// </summary>
	private TokenStreamSelector selector_;
	
	/// <summary>
	///   A <see cref="TokenStreamSelector"> for switching between this Lexer and the Preprocessor Lexer.
	/// </summary>
	public TokenStreamSelector Selector
	{
		get { return selector_;  }
		set { selector_ = value; }
	}

	private FileInfo	_fileinfo = null;

	/// <summary>
	/// Update _fileinfo member whenever filename changes.
	/// </summary>
	public override void setFilename(string f)
	{
		base.setFilename(f);
		_fileinfo = new FileInfo(f);
	}
	
	/// <summary>
	///   Ensures all tokens have access to the source file's details.
	/// </summary>
	protected override IToken makeToken(int t)
	{
		IToken result = base.makeToken(t);
		CustomHiddenStreamToken customToken = result as CustomHiddenStreamToken;
		if ( customToken != null )
		{
			customToken.File = _fileinfo;
		}
		return result;
	}

	/// <summary>
	///   This table is used to keep a searchable list of keywords only. This is used mainly
	///   as for section "A.1.6 Identifers" for determining if an identifier is indeed a 
	///   VERBATIM_IDENTIFIER. It's contents MUST be kept in sync with the contents of section 
	///   "A.1.7 Keywords" above.
	/// </summary>
	///
	private static Hashtable keywordsTable = new Hashtable();
	
	static CSharpLexerDynamic()
	{
		keywordsTable.Add(ABSTRACT,  	"abstract");
		keywordsTable.Add(AS,        	"as");
		keywordsTable.Add(BASE,      	"base");
		keywordsTable.Add(BOOL,      	"bool");
		keywordsTable.Add(BREAK,     	"break");
		keywordsTable.Add(BYTE,      	"byte");
		keywordsTable.Add(CASE,      	"case");
		keywordsTable.Add(CATCH,     	"catch");
		keywordsTable.Add(CHAR,			"char");
		keywordsTable.Add(CHECKED,		"checked");
		keywordsTable.Add(CLASS,		"class");
		keywordsTable.Add(CONST,		"const");
		keywordsTable.Add(CONTINUE,		"continue");
		keywordsTable.Add(DECIMAL,		"decimal");
		keywordsTable.Add(DEFAULT,		"default");
		keywordsTable.Add(DELEGATE,		"delegate");
		keywordsTable.Add(DO,			"do");
		keywordsTable.Add(DOUBLE,		"double");
		keywordsTable.Add(ELSE,			"else");
		keywordsTable.Add(ENUM,			"enum");
		keywordsTable.Add(EVENT,		"event");
		keywordsTable.Add(EXPLICIT,		"explicit");
		keywordsTable.Add(EXTERN,		"extern");
		keywordsTable.Add(FALSE,		"false");
		keywordsTable.Add(FINALLY,		"finally");
		keywordsTable.Add(FIXED,		"fixed");
		keywordsTable.Add(FLOAT,		"float");
		keywordsTable.Add(FOR,			"for");
		keywordsTable.Add(FOREACH,		"foreach");
		keywordsTable.Add(GOTO,			"goto");
		keywordsTable.Add(IF,			"if");
		keywordsTable.Add(IMPLICIT,		"implicit");
		keywordsTable.Add(IN,			"in");
		keywordsTable.Add(INT,			"int");
		keywordsTable.Add(INTERFACE,	"interface");
		keywordsTable.Add(INTERNAL,		"internal");
		keywordsTable.Add(IS,			"is");
		keywordsTable.Add(LOCK,			"lock");
		keywordsTable.Add(LONG,			"long");
		keywordsTable.Add(NAMESPACE,	"namespace");
		keywordsTable.Add(NEW,			"new");
		keywordsTable.Add(NULL,			"null");
		keywordsTable.Add(OBJECT,		"object");
		keywordsTable.Add(OPERATOR,		"operator");
		keywordsTable.Add(OUT,			"out");
		keywordsTable.Add(OVERRIDE,		"override");
		keywordsTable.Add(PARAMS,		"params");
		keywordsTable.Add(PRIVATE,		"private");
		keywordsTable.Add(PROTECTED,	"protected");
		keywordsTable.Add(PUBLIC,		"public");
		keywordsTable.Add(READONLY,		"readonly");
		keywordsTable.Add(REF,			"ref");
		keywordsTable.Add(RETURN,		"return");
		keywordsTable.Add(SBYTE,		"sbyte");
		keywordsTable.Add(SEALED,		"sealed");
		keywordsTable.Add(SHORT,		"short");
		keywordsTable.Add(SIZEOF,		"sizeof");
		keywordsTable.Add(STACKALLOC,	"stackalloc");
		keywordsTable.Add(STATIC,		"static");
		keywordsTable.Add(STRING,		"string");
		keywordsTable.Add(STRUCT,		"struct");
		keywordsTable.Add(SWITCH,		"switch");
		keywordsTable.Add(THIS,			"this");
		keywordsTable.Add(THROW,		"throw");
		keywordsTable.Add(TRUE,			"true");
		keywordsTable.Add(TRY,			"try");
		keywordsTable.Add(TYPEOF,		"typeof");
		keywordsTable.Add(UINT,			"uint");
		keywordsTable.Add(ULONG,		"ulong");
		keywordsTable.Add(UNCHECKED,	"unchecked");
		keywordsTable.Add(UNSAFE,		"unsafe");
		keywordsTable.Add(USHORT,		"ushort");
		keywordsTable.Add(USING,		"using");
		keywordsTable.Add(VIRTUAL,		"virtual");
		keywordsTable.Add(VOID,			"void");
		keywordsTable.Add(WHILE,		"while");
		keywordsTable.Add(VAR,			"var");
		keywordsTable.Add(DYNAMIC,		"dynamic");
	}
	
	public bool IsLetterCharacter(string s)
	{
		return ( (UnicodeCategory.LowercaseLetter == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Ll
		         (UnicodeCategory.ModifierLetter  == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lm
		         (UnicodeCategory.OtherLetter     == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lo
		         (UnicodeCategory.TitlecaseLetter == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lt
		         (UnicodeCategory.UppercaseLetter == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lu
		         (UnicodeCategory.LetterNumber    == Char.GetUnicodeCategory(s, 1))     //UNICODE class Nl
		        );
	}

	public bool IsIdentifierCharacter(string s)
	{
		return ( (UnicodeCategory.LowercaseLetter      == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Ll
		         (UnicodeCategory.ModifierLetter       == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lm
		         (UnicodeCategory.OtherLetter          == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lo
		         (UnicodeCategory.TitlecaseLetter      == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lt
		         (UnicodeCategory.UppercaseLetter      == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Lu
		         (UnicodeCategory.LetterNumber         == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Nl		         
		         (UnicodeCategory.NonSpacingMark       == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Mn		         
		         (UnicodeCategory.SpacingCombiningMark == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Mc
		         (UnicodeCategory.DecimalDigitNumber   == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Nd
		         (UnicodeCategory.ConnectorPunctuation == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Pc
		         (UnicodeCategory.Format               == Char.GetUnicodeCategory(s, 1))     //UNICODE class Cf
		        );
	}

	public bool IsCombiningCharacter(string s)
	{
		return ( (UnicodeCategory.NonSpacingMark       == Char.GetUnicodeCategory(s, 1)) ||  //UNICODE class Mn
		         (UnicodeCategory.SpacingCombiningMark == Char.GetUnicodeCategory(s, 1))     //UNICODE class Mc
		        );
	}	
}

NEWLINE
	:	( '\r' 									// MacOS-style newline
		  ( options { generateAmbigWarnings=false; } 
		    : '\n' 								// DOS/Windows style newline
		  )?
		| '\n'									// UNIX-style newline
		| '\u2028'								// UNICODE line separator
		| '\u2029'								// UNICODE paragraph separator
		)					
		{	newline(); 
		}
	;