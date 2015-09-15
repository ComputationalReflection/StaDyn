// $ANTLR 2.7.6 (2005-12-22): "expandedCSharpPreprocessorHooverLexer.g" -> "CSharpPreprocessorHooverLexer.cs"$

	using System.IO;
	using System.Globalization;

	using TokenStreamSelector		        = antlr.TokenStreamSelector;

namespace Parser
{
	// Generate header specific to lexer CSharp file
	using System;
	using Stream                          = System.IO.Stream;
	using TextReader                      = System.IO.TextReader;
	using Hashtable                       = System.Collections.Hashtable;
	using Comparer                        = System.Collections.Comparer;
	
	using TokenStreamException            = antlr.TokenStreamException;
	using TokenStreamIOException          = antlr.TokenStreamIOException;
	using TokenStreamRecognitionException = antlr.TokenStreamRecognitionException;
	using CharStreamException             = antlr.CharStreamException;
	using CharStreamIOException           = antlr.CharStreamIOException;
	using ANTLRException                  = antlr.ANTLRException;
	using CharScanner                     = antlr.CharScanner;
	using InputBuffer                     = antlr.InputBuffer;
	using ByteBuffer                      = antlr.ByteBuffer;
	using CharBuffer                      = antlr.CharBuffer;
	using Token                           = antlr.Token;
	using IToken                          = antlr.IToken;
	using CommonToken                     = antlr.CommonToken;
	using SemanticException               = antlr.SemanticException;
	using RecognitionException            = antlr.RecognitionException;
	using NoViableAltForCharException     = antlr.NoViableAltForCharException;
	using MismatchedCharException         = antlr.MismatchedCharException;
	using TokenStream                     = antlr.TokenStream;
	using LexerSharedInputState           = antlr.LexerSharedInputState;
	using BitSet                          = antlr.collections.impl.BitSet;
	
	public 	class CSharpPreprocessorHooverLexer : antlr.CharScanner	, TokenStream
	 {
		public const int EOF = 1;
		public const int NULL_TREE_LOOKAHEAD = 3;
		public const int UNICODE_CLASS_Nl = 4;
		public const int UNICODE_CLASS_Lt = 5;
		public const int UNICODE_CLASS_Zs = 6;
		public const int UNICODE_CLASS_Ll = 7;
		public const int UNICODE_CLASS_Lu = 8;
		public const int UNICODE_CLASS_Lo = 9;
		public const int UNICODE_CLASS_Lm = 10;
		public const int UNICODE_CLASS_Mn = 11;
		public const int UNICODE_CLASS_Mc = 12;
		public const int UNICODE_CLASS_Nd = 13;
		public const int UNICODE_CLASS_Pc = 14;
		public const int UNICODE_CLASS_Cf = 15;
		public const int TRUE = 16;
		public const int FALSE = 17;
		public const int DEFAULT = 18;
		public const int PP_DEFINE = 19;
		public const int PP_UNDEFINE = 20;
		public const int PP_COND_IF = 21;
		public const int PP_COND_ELIF = 22;
		public const int PP_COND_ELSE = 23;
		public const int PP_COND_ENDIF = 24;
		public const int PP_LINE = 25;
		public const int PP_ERROR = 26;
		public const int PP_WARNING = 27;
		public const int PP_REGION = 28;
		public const int PP_ENDREGION = 29;
		public const int PP_FILENAME = 30;
		public const int PP_IDENT = 31;
		public const int PP_STRING = 32;
		public const int PP_NUMBER = 33;
		public const int WHITESPACE = 34;
		public const int QUOTE = 35;
		public const int OPEN_PAREN = 36;
		public const int CLOSE_PAREN = 37;
		public const int LOG_NOT = 38;
		public const int LOG_AND = 39;
		public const int LOG_OR = 40;
		public const int EQUAL = 41;
		public const int NOT_EQUAL = 42;
		public const int SL_COMMENT = 43;
		public const int NEWLINE = 44;
		public const int NOT_NEWLINE = 45;
		public const int NON_NEWLINE_WHITESPACE = 46;
		public const int UNICODE_ESCAPE_SEQUENCE = 47;
		public const int DECIMAL_DIGIT = 48;
		public const int HEX_DIGIT = 49;
		public const int LETTER_CHARACTER = 50;
		public const int DECIMAL_DIGIT_CHARACTER = 51;
		public const int CONNECTING_CHARACTER = 52;
		public const int COMBINING_CHARACTER = 53;
		public const int FORMATTING_CHARACTER = 54;
		
		
	
	/// <summary>
	///   A <see cref="TokenStreamSelector"> for switching between this Lexer and the C#-only Lexer.
	/// </summary>
	private TokenStreamSelector selector_;
	
	/// <summary>
	///   A <see cref="TokenStreamSelector"> for switching between this Lexer and the C#-only Lexer.
	/// </summary>
	public TokenStreamSelector Selector
	{
		get { return selector_;  }
		set { selector_ = value; }
	}

	private FileInfo	_fileinfo = null;

	/// <summary>
	/// Update _fileinfo attribute whenever filename changes.
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
	
		public CSharpPreprocessorHooverLexer(Stream ins) : this(new ByteBuffer(ins))
		{
		}
		
		public CSharpPreprocessorHooverLexer(TextReader r) : this(new CharBuffer(r))
		{
		}
		
		public CSharpPreprocessorHooverLexer(InputBuffer ib)		 : this(new LexerSharedInputState(ib))
		{
		}
		
		public CSharpPreprocessorHooverLexer(LexerSharedInputState state) : base(state)
		{
			initialize();
		}
		private void initialize()
		{
			caseSensitiveLiterals = true;
			setCaseSensitive(true);
			literals = new Hashtable(100, (float) 0.4, null, Comparer.Default);
			literals.Add("false", 17);
			literals.Add("true", 16);
			literals.Add("default", 18);
		}
		
		override public IToken nextToken()			//throws TokenStreamException
		{
			IToken theRetToken = null;
tryAgain:
			for (;;)
			{
				int _ttype = Token.INVALID_TYPE;
				resetText();
				try     // for char stream error handling
				{
					try     // for lexical error handling
					{
						if ((cached_LA1=='/'))
						{
							mSL_COMMENT(true);
							theRetToken = returnToken_;
						}
						else if ((cached_LA1=='\n'||cached_LA1=='\r'||cached_LA1=='\u2028'||cached_LA1=='\u2029')) {
							mNEWLINE(true);
							theRetToken = returnToken_;
						}
						else if ((tokenSet_0_.member(cached_LA1))) {
							mPP_STRING(true);
							theRetToken = returnToken_;
						}
						else
						{
							if (cached_LA1==EOF_CHAR) { uponEOF(); returnToken_ = makeToken(Token.EOF_TYPE); }
				else {throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());}
						}
						
						if ( null==returnToken_ ) goto tryAgain; // found SKIP token
						_ttype = returnToken_.Type;
						returnToken_.Type = _ttype;
						return returnToken_;
					}
					catch (RecognitionException e) {
							throw new TokenStreamRecognitionException(e);
					}
				}
				catch (CharStreamException cse) {
					if ( cse is CharStreamIOException ) {
						throw new TokenStreamIOException(((CharStreamIOException)cse).io);
					}
					else {
						throw new TokenStreamException(cse.Message);
					}
				}
			}
		}
		
	public void mSL_COMMENT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = SL_COMMENT;
		
		match('/');
		{
			if ((cached_LA1=='/'))
			{
				match('/');
				{    // ( ... )*
					for (;;)
					{
						if ((tokenSet_1_.member(cached_LA1)))
						{
							mNOT_NEWLINE(false);
						}
						else
						{
							goto _loop4_breakloop;
						}
						
					}
_loop4_breakloop:					;
				}    // ( ... )*
				{
					if ((cached_LA1=='\n'||cached_LA1=='\r'||cached_LA1=='\u2028'||cached_LA1=='\u2029'))
					{
						{
							if ((cached_LA1=='\r'))
							{
								match('\r');
								{
									if ((cached_LA1=='\n'))
									{
										match('\n');
									}
									else {
									}
									
								}
							}
							else if ((cached_LA1=='\n')) {
								match('\n');
							}
							else if ((cached_LA1=='\u2028')) {
								match('\u2028');
							}
							else if ((cached_LA1=='\u2029')) {
								match('\u2029');
							}
							else
							{
								throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
							}
							
						}
						newline();
					}
					else {
					}
					
				}
				selector_.pop();
			}
			else if ((tokenSet_0_.member(cached_LA1))) {
				{
					match(tokenSet_0_);
				}
				{    // ( ... )*
					for (;;)
					{
						if ((tokenSet_1_.member(cached_LA1)))
						{
							mNOT_NEWLINE(false);
						}
						else
						{
							goto _loop10_breakloop;
						}
						
					}
_loop10_breakloop:					;
				}    // ( ... )*
				_ttype = PP_STRING;
			}
			else
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mNOT_NEWLINE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = NOT_NEWLINE;
		
		{
			match(tokenSet_1_);
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mNEWLINE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = NEWLINE;
		
		{
			if ((cached_LA1=='\r'))
			{
				match('\r');
				{
					if ((cached_LA1=='\n'))
					{
						match('\n');
					}
					else {
					}
					
				}
			}
			else if ((cached_LA1=='\n')) {
				match('\n');
			}
			else if ((cached_LA1=='\u2028')) {
				match('\u2028');
			}
			else if ((cached_LA1=='\u2029')) {
				match('\u2029');
			}
			else
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			
		}
			newline();
					selector_.pop();
				
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mPP_STRING(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = PP_STRING;
		
		{
			match(tokenSet_0_);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_1_.member(cached_LA1)))
				{
					mNOT_NEWLINE(false);
				}
				else
				{
					goto _loop17_breakloop;
				}
				
			}
_loop17_breakloop:			;
		}    // ( ... )*
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mLOG_NOT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LOG_NOT;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mLOG_AND(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LOG_AND;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mLOG_OR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LOG_OR;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mEQUAL(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = EQUAL;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mNOT_EQUAL(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = NOT_EQUAL;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mQUOTE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = QUOTE;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mOPEN_PAREN(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = OPEN_PAREN;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mCLOSE_PAREN(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = CLOSE_PAREN;
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mNON_NEWLINE_WHITESPACE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = NON_NEWLINE_WHITESPACE;
		
		if ((cached_LA1=='\t'))
		{
			match('\t');
		}
		else if ((cached_LA1=='\u000c')) {
			match('\f');
		}
		else if ((cached_LA1=='\u000b')) {
			match('\u000B');
		}
		else if ((tokenSet_2_.member(cached_LA1))) {
			mUNICODE_CLASS_Zs(false);
		}
		else
		{
			throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
		}
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Zs(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Zs;
		
		{
			switch ( cached_LA1 )
			{
			case ' ':
			{
				match('\u0020');
				break;
			}
			case '\u00a0':
			{
				match('\u00A0');
				break;
			}
			case '\u1680':
			{
				match('\u1680');
				break;
			}
			case '\u2000':  case '\u2001':  case '\u2002':  case '\u2003':
			case '\u2004':  case '\u2005':  case '\u2006':  case '\u2007':
			case '\u2008':  case '\u2009':  case '\u200a':  case '\u200b':
			{
				matchRange('\u2000','\u200B');
				break;
			}
			case '\u202f':
			{
				match('\u202F');
				break;
			}
			case '\u205f':
			{
				match('\u205F');
				break;
			}
			case '\u3000':
			{
				match('\u3000');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_ESCAPE_SEQUENCE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_ESCAPE_SEQUENCE;
		
		match('\\');
		{
			if ((cached_LA1=='u'))
			{
				match('u');
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
			}
			else if ((cached_LA1=='U')) {
				match('U');
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
				mHEX_DIGIT(false);
			}
			else
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mHEX_DIGIT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = HEX_DIGIT;
		
		{
			if (((cached_LA1 >= '0' && cached_LA1 <= '9')))
			{
				matchRange('0','9');
			}
			else if (((cached_LA1 >= 'A' && cached_LA1 <= 'F'))) {
				matchRange('A','F');
			}
			else if (((cached_LA1 >= 'a' && cached_LA1 <= 'f'))) {
				matchRange('a','f');
			}
			else
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mDECIMAL_DIGIT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = DECIMAL_DIGIT;
		
		{
			matchRange('0','9');
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mLETTER_CHARACTER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = LETTER_CHARACTER;
		
		if ((tokenSet_3_.member(cached_LA1)))
		{
			mUNICODE_CLASS_Lu(false);
		}
		else if ((tokenSet_4_.member(cached_LA1))) {
			mUNICODE_CLASS_Ll(false);
		}
		else if ((tokenSet_5_.member(cached_LA1))) {
			mUNICODE_CLASS_Lt(false);
		}
		else if ((tokenSet_6_.member(cached_LA1))) {
			mUNICODE_CLASS_Lm(false);
		}
		else if ((tokenSet_7_.member(cached_LA1))) {
			mUNICODE_CLASS_Lo(false);
		}
		else if ((tokenSet_8_.member(cached_LA1))) {
			mUNICODE_CLASS_Nl(false);
		}
		else
		{
			throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
		}
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Lu(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Lu;
		
		{
			switch ( cached_LA1 )
			{
			case 'A':  case 'B':  case 'C':  case 'D':
			case 'E':  case 'F':  case 'G':  case 'H':
			case 'I':  case 'J':  case 'K':  case 'L':
			case 'M':  case 'N':  case 'O':  case 'P':
			case 'Q':  case 'R':  case 'S':  case 'T':
			case 'U':  case 'V':  case 'W':  case 'X':
			case 'Y':  case 'Z':
			{
				matchRange('\u0041','\u005A');
				break;
			}
			case '\u00c0':  case '\u00c1':  case '\u00c2':  case '\u00c3':
			case '\u00c4':  case '\u00c5':  case '\u00c6':  case '\u00c7':
			case '\u00c8':  case '\u00c9':  case '\u00ca':  case '\u00cb':
			case '\u00cc':  case '\u00cd':  case '\u00ce':  case '\u00cf':
			case '\u00d0':  case '\u00d1':  case '\u00d2':  case '\u00d3':
			case '\u00d4':  case '\u00d5':  case '\u00d6':
			{
				matchRange('\u00C0','\u00D6');
				break;
			}
			case '\u00d8':  case '\u00d9':  case '\u00da':  case '\u00db':
			case '\u00dc':  case '\u00dd':  case '\u00de':
			{
				matchRange('\u00D8','\u00DE');
				break;
			}
			case '\u0100':
			{
				match('\u0100');
				break;
			}
			case '\u0102':
			{
				match('\u0102');
				break;
			}
			case '\u0104':
			{
				match('\u0104');
				break;
			}
			case '\u0106':
			{
				match('\u0106');
				break;
			}
			case '\u0108':
			{
				match('\u0108');
				break;
			}
			case '\u010a':
			{
				match('\u010A');
				break;
			}
			case '\u010c':
			{
				match('\u010C');
				break;
			}
			case '\u010e':
			{
				match('\u010E');
				break;
			}
			case '\u0110':
			{
				match('\u0110');
				break;
			}
			case '\u0112':
			{
				match('\u0112');
				break;
			}
			case '\u0114':
			{
				match('\u0114');
				break;
			}
			case '\u0116':
			{
				match('\u0116');
				break;
			}
			case '\u0118':
			{
				match('\u0118');
				break;
			}
			case '\u011a':
			{
				match('\u011A');
				break;
			}
			case '\u011c':
			{
				match('\u011C');
				break;
			}
			case '\u011e':
			{
				match('\u011E');
				break;
			}
			case '\u0120':
			{
				match('\u0120');
				break;
			}
			case '\u0122':
			{
				match('\u0122');
				break;
			}
			case '\u0124':
			{
				match('\u0124');
				break;
			}
			case '\u0126':
			{
				match('\u0126');
				break;
			}
			case '\u0128':
			{
				match('\u0128');
				break;
			}
			case '\u012a':
			{
				match('\u012A');
				break;
			}
			case '\u012c':
			{
				match('\u012C');
				break;
			}
			case '\u012e':
			{
				match('\u012E');
				break;
			}
			case '\u0130':
			{
				match('\u0130');
				break;
			}
			case '\u0132':
			{
				match('\u0132');
				break;
			}
			case '\u0134':
			{
				match('\u0134');
				break;
			}
			case '\u0136':
			{
				match('\u0136');
				break;
			}
			case '\u0139':
			{
				match('\u0139');
				break;
			}
			case '\u013b':
			{
				match('\u013B');
				break;
			}
			case '\u013d':
			{
				match('\u013D');
				break;
			}
			case '\u013f':
			{
				match('\u013F');
				break;
			}
			case '\u0141':
			{
				match('\u0141');
				break;
			}
			case '\u0143':
			{
				match('\u0143');
				break;
			}
			case '\u0145':
			{
				match('\u0145');
				break;
			}
			case '\u0147':
			{
				match('\u0147');
				break;
			}
			case '\u014a':
			{
				match('\u014A');
				break;
			}
			case '\u014c':
			{
				match('\u014C');
				break;
			}
			case '\u014e':
			{
				match('\u014E');
				break;
			}
			case '\u0150':
			{
				match('\u0150');
				break;
			}
			case '\u0152':
			{
				match('\u0152');
				break;
			}
			case '\u0154':
			{
				match('\u0154');
				break;
			}
			case '\u0156':
			{
				match('\u0156');
				break;
			}
			case '\u0158':
			{
				match('\u0158');
				break;
			}
			case '\u015a':
			{
				match('\u015A');
				break;
			}
			case '\u015c':
			{
				match('\u015C');
				break;
			}
			case '\u015e':
			{
				match('\u015E');
				break;
			}
			case '\u0160':
			{
				match('\u0160');
				break;
			}
			case '\u0162':
			{
				match('\u0162');
				break;
			}
			case '\u0164':
			{
				match('\u0164');
				break;
			}
			case '\u0166':
			{
				match('\u0166');
				break;
			}
			case '\u0168':
			{
				match('\u0168');
				break;
			}
			case '\u016a':
			{
				match('\u016A');
				break;
			}
			case '\u016c':
			{
				match('\u016C');
				break;
			}
			case '\u016e':
			{
				match('\u016E');
				break;
			}
			case '\u0170':
			{
				match('\u0170');
				break;
			}
			case '\u0172':
			{
				match('\u0172');
				break;
			}
			case '\u0174':
			{
				match('\u0174');
				break;
			}
			case '\u0176':
			{
				match('\u0176');
				break;
			}
			case '\u0178':  case '\u0179':
			{
				matchRange('\u0178','\u0179');
				break;
			}
			case '\u017b':
			{
				match('\u017B');
				break;
			}
			case '\u017d':
			{
				match('\u017D');
				break;
			}
			case '\u0181':  case '\u0182':
			{
				matchRange('\u0181','\u0182');
				break;
			}
			case '\u0184':
			{
				match('\u0184');
				break;
			}
			case '\u0186':  case '\u0187':
			{
				matchRange('\u0186','\u0187');
				break;
			}
			case '\u0189':  case '\u018a':  case '\u018b':
			{
				matchRange('\u0189','\u018B');
				break;
			}
			case '\u018e':  case '\u018f':  case '\u0190':  case '\u0191':
			{
				matchRange('\u018E','\u0191');
				break;
			}
			case '\u0193':  case '\u0194':
			{
				matchRange('\u0193','\u0194');
				break;
			}
			case '\u0196':  case '\u0197':  case '\u0198':
			{
				matchRange('\u0196','\u0198');
				break;
			}
			case '\u019c':  case '\u019d':
			{
				matchRange('\u019C','\u019D');
				break;
			}
			case '\u019f':  case '\u01a0':
			{
				matchRange('\u019F','\u01A0');
				break;
			}
			case '\u01a2':
			{
				match('\u01A2');
				break;
			}
			case '\u01a4':
			{
				match('\u01A4');
				break;
			}
			case '\u01a6':  case '\u01a7':
			{
				matchRange('\u01A6','\u01A7');
				break;
			}
			case '\u01a9':
			{
				match('\u01A9');
				break;
			}
			case '\u01ac':
			{
				match('\u01AC');
				break;
			}
			case '\u01ae':  case '\u01af':
			{
				matchRange('\u01AE','\u01AF');
				break;
			}
			case '\u01b1':  case '\u01b2':  case '\u01b3':
			{
				matchRange('\u01B1','\u01B3');
				break;
			}
			case '\u01b5':
			{
				match('\u01B5');
				break;
			}
			case '\u01b7':  case '\u01b8':
			{
				matchRange('\u01B7','\u01B8');
				break;
			}
			case '\u01bc':
			{
				match('\u01BC');
				break;
			}
			case '\u01c4':
			{
				match('\u01C4');
				break;
			}
			case '\u01c7':
			{
				match('\u01C7');
				break;
			}
			case '\u01ca':
			{
				match('\u01CA');
				break;
			}
			case '\u01cd':
			{
				match('\u01CD');
				break;
			}
			case '\u01cf':
			{
				match('\u01CF');
				break;
			}
			case '\u01d1':
			{
				match('\u01D1');
				break;
			}
			case '\u01d3':
			{
				match('\u01D3');
				break;
			}
			case '\u01d5':
			{
				match('\u01D5');
				break;
			}
			case '\u01d7':
			{
				match('\u01D7');
				break;
			}
			case '\u01d9':
			{
				match('\u01D9');
				break;
			}
			case '\u01db':
			{
				match('\u01DB');
				break;
			}
			case '\u01de':
			{
				match('\u01DE');
				break;
			}
			case '\u01e0':
			{
				match('\u01E0');
				break;
			}
			case '\u01e2':
			{
				match('\u01E2');
				break;
			}
			case '\u01e4':
			{
				match('\u01E4');
				break;
			}
			case '\u01e6':
			{
				match('\u01E6');
				break;
			}
			case '\u01e8':
			{
				match('\u01E8');
				break;
			}
			case '\u01ea':
			{
				match('\u01EA');
				break;
			}
			case '\u01ec':
			{
				match('\u01EC');
				break;
			}
			case '\u01ee':
			{
				match('\u01EE');
				break;
			}
			case '\u01f1':
			{
				match('\u01F1');
				break;
			}
			case '\u01f4':
			{
				match('\u01F4');
				break;
			}
			case '\u01f6':  case '\u01f7':  case '\u01f8':
			{
				matchRange('\u01F6','\u01F8');
				break;
			}
			case '\u01fa':
			{
				match('\u01FA');
				break;
			}
			case '\u01fc':
			{
				match('\u01FC');
				break;
			}
			case '\u01fe':
			{
				match('\u01FE');
				break;
			}
			case '\u0200':
			{
				match('\u0200');
				break;
			}
			case '\u0202':
			{
				match('\u0202');
				break;
			}
			case '\u0204':
			{
				match('\u0204');
				break;
			}
			case '\u0206':
			{
				match('\u0206');
				break;
			}
			case '\u0208':
			{
				match('\u0208');
				break;
			}
			case '\u020a':
			{
				match('\u020A');
				break;
			}
			case '\u020c':
			{
				match('\u020C');
				break;
			}
			case '\u020e':
			{
				match('\u020E');
				break;
			}
			case '\u0210':
			{
				match('\u0210');
				break;
			}
			case '\u0212':
			{
				match('\u0212');
				break;
			}
			case '\u0214':
			{
				match('\u0214');
				break;
			}
			case '\u0216':
			{
				match('\u0216');
				break;
			}
			case '\u0218':
			{
				match('\u0218');
				break;
			}
			case '\u021a':
			{
				match('\u021A');
				break;
			}
			case '\u021c':
			{
				match('\u021C');
				break;
			}
			case '\u021e':
			{
				match('\u021E');
				break;
			}
			case '\u0220':
			{
				match('\u0220');
				break;
			}
			case '\u0222':
			{
				match('\u0222');
				break;
			}
			case '\u0224':
			{
				match('\u0224');
				break;
			}
			case '\u0226':
			{
				match('\u0226');
				break;
			}
			case '\u0228':
			{
				match('\u0228');
				break;
			}
			case '\u022a':
			{
				match('\u022A');
				break;
			}
			case '\u022c':
			{
				match('\u022C');
				break;
			}
			case '\u022e':
			{
				match('\u022E');
				break;
			}
			case '\u0230':
			{
				match('\u0230');
				break;
			}
			case '\u0232':
			{
				match('\u0232');
				break;
			}
			case '\u0386':
			{
				match('\u0386');
				break;
			}
			case '\u0388':  case '\u0389':  case '\u038a':
			{
				matchRange('\u0388','\u038A');
				break;
			}
			case '\u038c':
			{
				match('\u038C');
				break;
			}
			case '\u038e':  case '\u038f':
			{
				matchRange('\u038E','\u038F');
				break;
			}
			case '\u0391':  case '\u0392':  case '\u0393':  case '\u0394':
			case '\u0395':  case '\u0396':  case '\u0397':  case '\u0398':
			case '\u0399':  case '\u039a':  case '\u039b':  case '\u039c':
			case '\u039d':  case '\u039e':  case '\u039f':  case '\u03a0':
			case '\u03a1':
			{
				matchRange('\u0391','\u03A1');
				break;
			}
			case '\u03a3':  case '\u03a4':  case '\u03a5':  case '\u03a6':
			case '\u03a7':  case '\u03a8':  case '\u03a9':  case '\u03aa':
			case '\u03ab':
			{
				matchRange('\u03A3','\u03AB');
				break;
			}
			case '\u03d2':  case '\u03d3':  case '\u03d4':
			{
				matchRange('\u03D2','\u03D4');
				break;
			}
			case '\u03d8':
			{
				match('\u03D8');
				break;
			}
			case '\u03da':
			{
				match('\u03DA');
				break;
			}
			case '\u03dc':
			{
				match('\u03DC');
				break;
			}
			case '\u03de':
			{
				match('\u03DE');
				break;
			}
			case '\u03e0':
			{
				match('\u03E0');
				break;
			}
			case '\u03e2':
			{
				match('\u03E2');
				break;
			}
			case '\u03e4':
			{
				match('\u03E4');
				break;
			}
			case '\u03e6':
			{
				match('\u03E6');
				break;
			}
			case '\u03e8':
			{
				match('\u03E8');
				break;
			}
			case '\u03ea':
			{
				match('\u03EA');
				break;
			}
			case '\u03ec':
			{
				match('\u03EC');
				break;
			}
			case '\u03ee':
			{
				match('\u03EE');
				break;
			}
			case '\u03f4':
			{
				match('\u03F4');
				break;
			}
			case '\u0400':  case '\u0401':  case '\u0402':  case '\u0403':
			case '\u0404':  case '\u0405':  case '\u0406':  case '\u0407':
			case '\u0408':  case '\u0409':  case '\u040a':  case '\u040b':
			case '\u040c':  case '\u040d':  case '\u040e':  case '\u040f':
			case '\u0410':  case '\u0411':  case '\u0412':  case '\u0413':
			case '\u0414':  case '\u0415':  case '\u0416':  case '\u0417':
			case '\u0418':  case '\u0419':  case '\u041a':  case '\u041b':
			case '\u041c':  case '\u041d':  case '\u041e':  case '\u041f':
			case '\u0420':  case '\u0421':  case '\u0422':  case '\u0423':
			case '\u0424':  case '\u0425':  case '\u0426':  case '\u0427':
			case '\u0428':  case '\u0429':  case '\u042a':  case '\u042b':
			case '\u042c':  case '\u042d':  case '\u042e':  case '\u042f':
			{
				matchRange('\u0400','\u042F');
				break;
			}
			case '\u0460':
			{
				match('\u0460');
				break;
			}
			case '\u0462':
			{
				match('\u0462');
				break;
			}
			case '\u0464':
			{
				match('\u0464');
				break;
			}
			case '\u0466':
			{
				match('\u0466');
				break;
			}
			case '\u0468':
			{
				match('\u0468');
				break;
			}
			case '\u046a':
			{
				match('\u046A');
				break;
			}
			case '\u046c':
			{
				match('\u046C');
				break;
			}
			case '\u046e':
			{
				match('\u046E');
				break;
			}
			case '\u0470':
			{
				match('\u0470');
				break;
			}
			case '\u0472':
			{
				match('\u0472');
				break;
			}
			case '\u0474':
			{
				match('\u0474');
				break;
			}
			case '\u0476':
			{
				match('\u0476');
				break;
			}
			case '\u0478':
			{
				match('\u0478');
				break;
			}
			case '\u047a':
			{
				match('\u047A');
				break;
			}
			case '\u047c':
			{
				match('\u047C');
				break;
			}
			case '\u047e':
			{
				match('\u047E');
				break;
			}
			case '\u0480':
			{
				match('\u0480');
				break;
			}
			case '\u048a':
			{
				match('\u048A');
				break;
			}
			case '\u048c':
			{
				match('\u048C');
				break;
			}
			case '\u048e':
			{
				match('\u048E');
				break;
			}
			case '\u0490':
			{
				match('\u0490');
				break;
			}
			case '\u0492':
			{
				match('\u0492');
				break;
			}
			case '\u0494':
			{
				match('\u0494');
				break;
			}
			case '\u0496':
			{
				match('\u0496');
				break;
			}
			case '\u0498':
			{
				match('\u0498');
				break;
			}
			case '\u049a':
			{
				match('\u049A');
				break;
			}
			case '\u049c':
			{
				match('\u049C');
				break;
			}
			case '\u049e':
			{
				match('\u049E');
				break;
			}
			case '\u04a0':
			{
				match('\u04A0');
				break;
			}
			case '\u04a2':
			{
				match('\u04A2');
				break;
			}
			case '\u04a4':
			{
				match('\u04A4');
				break;
			}
			case '\u04a6':
			{
				match('\u04A6');
				break;
			}
			case '\u04a8':
			{
				match('\u04A8');
				break;
			}
			case '\u04aa':
			{
				match('\u04AA');
				break;
			}
			case '\u04ac':
			{
				match('\u04AC');
				break;
			}
			case '\u04ae':
			{
				match('\u04AE');
				break;
			}
			case '\u04b0':
			{
				match('\u04B0');
				break;
			}
			case '\u04b2':
			{
				match('\u04B2');
				break;
			}
			case '\u04b4':
			{
				match('\u04B4');
				break;
			}
			case '\u04b6':
			{
				match('\u04B6');
				break;
			}
			case '\u04b8':
			{
				match('\u04B8');
				break;
			}
			case '\u04ba':
			{
				match('\u04BA');
				break;
			}
			case '\u04bc':
			{
				match('\u04BC');
				break;
			}
			case '\u04be':
			{
				match('\u04BE');
				break;
			}
			case '\u04c0':  case '\u04c1':
			{
				matchRange('\u04C0','\u04C1');
				break;
			}
			case '\u04c3':
			{
				match('\u04C3');
				break;
			}
			case '\u04c5':
			{
				match('\u04C5');
				break;
			}
			case '\u04c7':
			{
				match('\u04C7');
				break;
			}
			case '\u04c9':
			{
				match('\u04C9');
				break;
			}
			case '\u04cb':
			{
				match('\u04CB');
				break;
			}
			case '\u04cd':
			{
				match('\u04CD');
				break;
			}
			case '\u04d0':
			{
				match('\u04D0');
				break;
			}
			case '\u04d2':
			{
				match('\u04D2');
				break;
			}
			case '\u04d4':
			{
				match('\u04D4');
				break;
			}
			case '\u04d6':
			{
				match('\u04D6');
				break;
			}
			case '\u04d8':
			{
				match('\u04D8');
				break;
			}
			case '\u04da':
			{
				match('\u04DA');
				break;
			}
			case '\u04dc':
			{
				match('\u04DC');
				break;
			}
			case '\u04de':
			{
				match('\u04DE');
				break;
			}
			case '\u04e0':
			{
				match('\u04E0');
				break;
			}
			case '\u04e2':
			{
				match('\u04E2');
				break;
			}
			case '\u04e4':
			{
				match('\u04E4');
				break;
			}
			case '\u04e6':
			{
				match('\u04E6');
				break;
			}
			case '\u04e8':
			{
				match('\u04E8');
				break;
			}
			case '\u04ea':
			{
				match('\u04EA');
				break;
			}
			case '\u04ec':
			{
				match('\u04EC');
				break;
			}
			case '\u04ee':
			{
				match('\u04EE');
				break;
			}
			case '\u04f0':
			{
				match('\u04F0');
				break;
			}
			case '\u04f2':
			{
				match('\u04F2');
				break;
			}
			case '\u04f4':
			{
				match('\u04F4');
				break;
			}
			case '\u04f8':
			{
				match('\u04F8');
				break;
			}
			case '\u0500':
			{
				match('\u0500');
				break;
			}
			case '\u0502':
			{
				match('\u0502');
				break;
			}
			case '\u0504':
			{
				match('\u0504');
				break;
			}
			case '\u0506':
			{
				match('\u0506');
				break;
			}
			case '\u0508':
			{
				match('\u0508');
				break;
			}
			case '\u050a':
			{
				match('\u050A');
				break;
			}
			case '\u050c':
			{
				match('\u050C');
				break;
			}
			case '\u050e':
			{
				match('\u050E');
				break;
			}
			case '\u0531':  case '\u0532':  case '\u0533':  case '\u0534':
			case '\u0535':  case '\u0536':  case '\u0537':  case '\u0538':
			case '\u0539':  case '\u053a':  case '\u053b':  case '\u053c':
			case '\u053d':  case '\u053e':  case '\u053f':  case '\u0540':
			case '\u0541':  case '\u0542':  case '\u0543':  case '\u0544':
			case '\u0545':  case '\u0546':  case '\u0547':  case '\u0548':
			case '\u0549':  case '\u054a':  case '\u054b':  case '\u054c':
			case '\u054d':  case '\u054e':  case '\u054f':  case '\u0550':
			case '\u0551':  case '\u0552':  case '\u0553':  case '\u0554':
			case '\u0555':  case '\u0556':
			{
				matchRange('\u0531','\u0556');
				break;
			}
			case '\u10a0':  case '\u10a1':  case '\u10a2':  case '\u10a3':
			case '\u10a4':  case '\u10a5':  case '\u10a6':  case '\u10a7':
			case '\u10a8':  case '\u10a9':  case '\u10aa':  case '\u10ab':
			case '\u10ac':  case '\u10ad':  case '\u10ae':  case '\u10af':
			case '\u10b0':  case '\u10b1':  case '\u10b2':  case '\u10b3':
			case '\u10b4':  case '\u10b5':  case '\u10b6':  case '\u10b7':
			case '\u10b8':  case '\u10b9':  case '\u10ba':  case '\u10bb':
			case '\u10bc':  case '\u10bd':  case '\u10be':  case '\u10bf':
			case '\u10c0':  case '\u10c1':  case '\u10c2':  case '\u10c3':
			case '\u10c4':  case '\u10c5':
			{
				matchRange('\u10A0','\u10C5');
				break;
			}
			case '\u1e00':
			{
				match('\u1E00');
				break;
			}
			case '\u1e02':
			{
				match('\u1E02');
				break;
			}
			case '\u1e04':
			{
				match('\u1E04');
				break;
			}
			case '\u1e06':
			{
				match('\u1E06');
				break;
			}
			case '\u1e08':
			{
				match('\u1E08');
				break;
			}
			case '\u1e0a':
			{
				match('\u1E0A');
				break;
			}
			case '\u1e0c':
			{
				match('\u1E0C');
				break;
			}
			case '\u1e0e':
			{
				match('\u1E0E');
				break;
			}
			case '\u1e10':
			{
				match('\u1E10');
				break;
			}
			case '\u1e12':
			{
				match('\u1E12');
				break;
			}
			case '\u1e14':
			{
				match('\u1E14');
				break;
			}
			case '\u1e16':
			{
				match('\u1E16');
				break;
			}
			case '\u1e18':
			{
				match('\u1E18');
				break;
			}
			case '\u1e1a':
			{
				match('\u1E1A');
				break;
			}
			case '\u1e1c':
			{
				match('\u1E1C');
				break;
			}
			case '\u1e1e':
			{
				match('\u1E1E');
				break;
			}
			case '\u1e20':
			{
				match('\u1E20');
				break;
			}
			case '\u1e22':
			{
				match('\u1E22');
				break;
			}
			case '\u1e24':
			{
				match('\u1E24');
				break;
			}
			case '\u1e26':
			{
				match('\u1E26');
				break;
			}
			case '\u1e28':
			{
				match('\u1E28');
				break;
			}
			case '\u1e2a':
			{
				match('\u1E2A');
				break;
			}
			case '\u1e2c':
			{
				match('\u1E2C');
				break;
			}
			case '\u1e2e':
			{
				match('\u1E2E');
				break;
			}
			case '\u1e30':
			{
				match('\u1E30');
				break;
			}
			case '\u1e32':
			{
				match('\u1E32');
				break;
			}
			case '\u1e34':
			{
				match('\u1E34');
				break;
			}
			case '\u1e36':
			{
				match('\u1E36');
				break;
			}
			case '\u1e38':
			{
				match('\u1E38');
				break;
			}
			case '\u1e3a':
			{
				match('\u1E3A');
				break;
			}
			case '\u1e3c':
			{
				match('\u1E3C');
				break;
			}
			case '\u1e3e':
			{
				match('\u1E3E');
				break;
			}
			case '\u1e40':
			{
				match('\u1E40');
				break;
			}
			case '\u1e42':
			{
				match('\u1E42');
				break;
			}
			case '\u1e44':
			{
				match('\u1E44');
				break;
			}
			case '\u1e46':
			{
				match('\u1E46');
				break;
			}
			case '\u1e48':
			{
				match('\u1E48');
				break;
			}
			case '\u1e4a':
			{
				match('\u1E4A');
				break;
			}
			case '\u1e4c':
			{
				match('\u1E4C');
				break;
			}
			case '\u1e4e':
			{
				match('\u1E4E');
				break;
			}
			case '\u1e50':
			{
				match('\u1E50');
				break;
			}
			case '\u1e52':
			{
				match('\u1E52');
				break;
			}
			case '\u1e54':
			{
				match('\u1E54');
				break;
			}
			case '\u1e56':
			{
				match('\u1E56');
				break;
			}
			case '\u1e58':
			{
				match('\u1E58');
				break;
			}
			case '\u1e5a':
			{
				match('\u1E5A');
				break;
			}
			case '\u1e5c':
			{
				match('\u1E5C');
				break;
			}
			case '\u1e5e':
			{
				match('\u1E5E');
				break;
			}
			case '\u1e60':
			{
				match('\u1E60');
				break;
			}
			case '\u1e62':
			{
				match('\u1E62');
				break;
			}
			case '\u1e64':
			{
				match('\u1E64');
				break;
			}
			case '\u1e66':
			{
				match('\u1E66');
				break;
			}
			case '\u1e68':
			{
				match('\u1E68');
				break;
			}
			case '\u1e6a':
			{
				match('\u1E6A');
				break;
			}
			case '\u1e6c':
			{
				match('\u1E6C');
				break;
			}
			case '\u1e6e':
			{
				match('\u1E6E');
				break;
			}
			case '\u1e70':
			{
				match('\u1E70');
				break;
			}
			case '\u1e72':
			{
				match('\u1E72');
				break;
			}
			case '\u1e74':
			{
				match('\u1E74');
				break;
			}
			case '\u1e76':
			{
				match('\u1E76');
				break;
			}
			case '\u1e78':
			{
				match('\u1E78');
				break;
			}
			case '\u1e7a':
			{
				match('\u1E7A');
				break;
			}
			case '\u1e7c':
			{
				match('\u1E7C');
				break;
			}
			case '\u1e7e':
			{
				match('\u1E7E');
				break;
			}
			case '\u1e80':
			{
				match('\u1E80');
				break;
			}
			case '\u1e82':
			{
				match('\u1E82');
				break;
			}
			case '\u1e84':
			{
				match('\u1E84');
				break;
			}
			case '\u1e86':
			{
				match('\u1E86');
				break;
			}
			case '\u1e88':
			{
				match('\u1E88');
				break;
			}
			case '\u1e8a':
			{
				match('\u1E8A');
				break;
			}
			case '\u1e8c':
			{
				match('\u1E8C');
				break;
			}
			case '\u1e8e':
			{
				match('\u1E8E');
				break;
			}
			case '\u1e90':
			{
				match('\u1E90');
				break;
			}
			case '\u1e92':
			{
				match('\u1E92');
				break;
			}
			case '\u1e94':
			{
				match('\u1E94');
				break;
			}
			case '\u1ea0':
			{
				match('\u1EA0');
				break;
			}
			case '\u1ea2':
			{
				match('\u1EA2');
				break;
			}
			case '\u1ea4':
			{
				match('\u1EA4');
				break;
			}
			case '\u1ea6':
			{
				match('\u1EA6');
				break;
			}
			case '\u1ea8':
			{
				match('\u1EA8');
				break;
			}
			case '\u1eaa':
			{
				match('\u1EAA');
				break;
			}
			case '\u1eac':
			{
				match('\u1EAC');
				break;
			}
			case '\u1eae':
			{
				match('\u1EAE');
				break;
			}
			case '\u1eb0':
			{
				match('\u1EB0');
				break;
			}
			case '\u1eb2':
			{
				match('\u1EB2');
				break;
			}
			case '\u1eb4':
			{
				match('\u1EB4');
				break;
			}
			case '\u1eb6':
			{
				match('\u1EB6');
				break;
			}
			case '\u1eb8':
			{
				match('\u1EB8');
				break;
			}
			case '\u1eba':
			{
				match('\u1EBA');
				break;
			}
			case '\u1ebc':
			{
				match('\u1EBC');
				break;
			}
			case '\u1ebe':
			{
				match('\u1EBE');
				break;
			}
			case '\u1ec0':
			{
				match('\u1EC0');
				break;
			}
			case '\u1ec2':
			{
				match('\u1EC2');
				break;
			}
			case '\u1ec4':
			{
				match('\u1EC4');
				break;
			}
			case '\u1ec6':
			{
				match('\u1EC6');
				break;
			}
			case '\u1ec8':
			{
				match('\u1EC8');
				break;
			}
			case '\u1eca':
			{
				match('\u1ECA');
				break;
			}
			case '\u1ecc':
			{
				match('\u1ECC');
				break;
			}
			case '\u1ece':
			{
				match('\u1ECE');
				break;
			}
			case '\u1ed0':
			{
				match('\u1ED0');
				break;
			}
			case '\u1ed2':
			{
				match('\u1ED2');
				break;
			}
			case '\u1ed4':
			{
				match('\u1ED4');
				break;
			}
			case '\u1ed6':
			{
				match('\u1ED6');
				break;
			}
			case '\u1ed8':
			{
				match('\u1ED8');
				break;
			}
			case '\u1eda':
			{
				match('\u1EDA');
				break;
			}
			case '\u1edc':
			{
				match('\u1EDC');
				break;
			}
			case '\u1ede':
			{
				match('\u1EDE');
				break;
			}
			case '\u1ee0':
			{
				match('\u1EE0');
				break;
			}
			case '\u1ee2':
			{
				match('\u1EE2');
				break;
			}
			case '\u1ee4':
			{
				match('\u1EE4');
				break;
			}
			case '\u1ee6':
			{
				match('\u1EE6');
				break;
			}
			case '\u1ee8':
			{
				match('\u1EE8');
				break;
			}
			case '\u1eea':
			{
				match('\u1EEA');
				break;
			}
			case '\u1eec':
			{
				match('\u1EEC');
				break;
			}
			case '\u1eee':
			{
				match('\u1EEE');
				break;
			}
			case '\u1ef0':
			{
				match('\u1EF0');
				break;
			}
			case '\u1ef2':
			{
				match('\u1EF2');
				break;
			}
			case '\u1ef4':
			{
				match('\u1EF4');
				break;
			}
			case '\u1ef6':
			{
				match('\u1EF6');
				break;
			}
			case '\u1ef8':
			{
				match('\u1EF8');
				break;
			}
			case '\u1f08':  case '\u1f09':  case '\u1f0a':  case '\u1f0b':
			case '\u1f0c':  case '\u1f0d':  case '\u1f0e':  case '\u1f0f':
			{
				matchRange('\u1F08','\u1F0F');
				break;
			}
			case '\u1f18':  case '\u1f19':  case '\u1f1a':  case '\u1f1b':
			case '\u1f1c':  case '\u1f1d':
			{
				matchRange('\u1F18','\u1F1D');
				break;
			}
			case '\u1f28':  case '\u1f29':  case '\u1f2a':  case '\u1f2b':
			case '\u1f2c':  case '\u1f2d':  case '\u1f2e':  case '\u1f2f':
			{
				matchRange('\u1F28','\u1F2F');
				break;
			}
			case '\u1f38':  case '\u1f39':  case '\u1f3a':  case '\u1f3b':
			case '\u1f3c':  case '\u1f3d':  case '\u1f3e':  case '\u1f3f':
			{
				matchRange('\u1F38','\u1F3F');
				break;
			}
			case '\u1f48':  case '\u1f49':  case '\u1f4a':  case '\u1f4b':
			case '\u1f4c':  case '\u1f4d':
			{
				matchRange('\u1F48','\u1F4D');
				break;
			}
			case '\u1f59':
			{
				match('\u1F59');
				break;
			}
			case '\u1f5b':
			{
				match('\u1F5B');
				break;
			}
			case '\u1f5d':
			{
				match('\u1F5D');
				break;
			}
			case '\u1f5f':
			{
				match('\u1F5F');
				break;
			}
			case '\u1f68':  case '\u1f69':  case '\u1f6a':  case '\u1f6b':
			case '\u1f6c':  case '\u1f6d':  case '\u1f6e':  case '\u1f6f':
			{
				matchRange('\u1F68','\u1F6F');
				break;
			}
			case '\u1fb8':  case '\u1fb9':  case '\u1fba':  case '\u1fbb':
			{
				matchRange('\u1FB8','\u1FBB');
				break;
			}
			case '\u1fc8':  case '\u1fc9':  case '\u1fca':  case '\u1fcb':
			{
				matchRange('\u1FC8','\u1FCB');
				break;
			}
			case '\u1fd8':  case '\u1fd9':  case '\u1fda':  case '\u1fdb':
			{
				matchRange('\u1FD8','\u1FDB');
				break;
			}
			case '\u1fe8':  case '\u1fe9':  case '\u1fea':  case '\u1feb':
			case '\u1fec':
			{
				matchRange('\u1FE8','\u1FEC');
				break;
			}
			case '\u1ff8':  case '\u1ff9':  case '\u1ffa':  case '\u1ffb':
			{
				matchRange('\u1FF8','\u1FFB');
				break;
			}
			case '\u2102':
			{
				match('\u2102');
				break;
			}
			case '\u2107':
			{
				match('\u2107');
				break;
			}
			case '\u210b':  case '\u210c':  case '\u210d':
			{
				matchRange('\u210B','\u210D');
				break;
			}
			case '\u2110':  case '\u2111':  case '\u2112':
			{
				matchRange('\u2110','\u2112');
				break;
			}
			case '\u2115':
			{
				match('\u2115');
				break;
			}
			case '\u2119':  case '\u211a':  case '\u211b':  case '\u211c':
			case '\u211d':
			{
				matchRange('\u2119','\u211D');
				break;
			}
			case '\u2124':
			{
				match('\u2124');
				break;
			}
			case '\u2126':
			{
				match('\u2126');
				break;
			}
			case '\u2128':
			{
				match('\u2128');
				break;
			}
			case '\u212a':  case '\u212b':  case '\u212c':  case '\u212d':
			{
				matchRange('\u212A','\u212D');
				break;
			}
			case '\u2130':  case '\u2131':
			{
				matchRange('\u2130','\u2131');
				break;
			}
			case '\u2133':
			{
				match('\u2133');
				break;
			}
			case '\u213e':  case '\u213f':
			{
				matchRange('\u213E','\u213F');
				break;
			}
			case '\u2145':
			{
				match('\u2145');
				break;
			}
			case '\uff21':  case '\uff22':  case '\uff23':  case '\uff24':
			case '\uff25':  case '\uff26':  case '\uff27':  case '\uff28':
			case '\uff29':  case '\uff2a':  case '\uff2b':  case '\uff2c':
			case '\uff2d':  case '\uff2e':  case '\uff2f':  case '\uff30':
			case '\uff31':  case '\uff32':  case '\uff33':  case '\uff34':
			case '\uff35':  case '\uff36':  case '\uff37':  case '\uff38':
			case '\uff39':  case '\uff3a':
			{
				matchRange('\uFF21','\uFF3A');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Ll(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Ll;
		
		{
			switch ( cached_LA1 )
			{
			case 'a':  case 'b':  case 'c':  case 'd':
			case 'e':  case 'f':  case 'g':  case 'h':
			case 'i':  case 'j':  case 'k':  case 'l':
			case 'm':  case 'n':  case 'o':  case 'p':
			case 'q':  case 'r':  case 's':  case 't':
			case 'u':  case 'v':  case 'w':  case 'x':
			case 'y':  case 'z':
			{
				matchRange('\u0061','\u007A');
				break;
			}
			case '\u00aa':
			{
				match('\u00AA');
				break;
			}
			case '\u00b5':
			{
				match('\u00B5');
				break;
			}
			case '\u00ba':
			{
				match('\u00BA');
				break;
			}
			case '\u00df':  case '\u00e0':  case '\u00e1':  case '\u00e2':
			case '\u00e3':  case '\u00e4':  case '\u00e5':  case '\u00e6':
			case '\u00e7':  case '\u00e8':  case '\u00e9':  case '\u00ea':
			case '\u00eb':  case '\u00ec':  case '\u00ed':  case '\u00ee':
			case '\u00ef':  case '\u00f0':  case '\u00f1':  case '\u00f2':
			case '\u00f3':  case '\u00f4':  case '\u00f5':  case '\u00f6':
			{
				matchRange('\u00DF','\u00F6');
				break;
			}
			case '\u00f8':  case '\u00f9':  case '\u00fa':  case '\u00fb':
			case '\u00fc':  case '\u00fd':  case '\u00fe':  case '\u00ff':
			{
				matchRange('\u00F8','\u00FF');
				break;
			}
			case '\u0101':
			{
				match('\u0101');
				break;
			}
			case '\u0103':
			{
				match('\u0103');
				break;
			}
			case '\u0105':
			{
				match('\u0105');
				break;
			}
			case '\u0107':
			{
				match('\u0107');
				break;
			}
			case '\u0109':
			{
				match('\u0109');
				break;
			}
			case '\u010b':
			{
				match('\u010B');
				break;
			}
			case '\u010d':
			{
				match('\u010D');
				break;
			}
			case '\u010f':
			{
				match('\u010F');
				break;
			}
			case '\u0111':
			{
				match('\u0111');
				break;
			}
			case '\u0113':
			{
				match('\u0113');
				break;
			}
			case '\u0115':
			{
				match('\u0115');
				break;
			}
			case '\u0117':
			{
				match('\u0117');
				break;
			}
			case '\u0119':
			{
				match('\u0119');
				break;
			}
			case '\u011b':
			{
				match('\u011B');
				break;
			}
			case '\u011d':
			{
				match('\u011D');
				break;
			}
			case '\u011f':
			{
				match('\u011F');
				break;
			}
			case '\u0121':
			{
				match('\u0121');
				break;
			}
			case '\u0123':
			{
				match('\u0123');
				break;
			}
			case '\u0125':
			{
				match('\u0125');
				break;
			}
			case '\u0127':
			{
				match('\u0127');
				break;
			}
			case '\u0129':
			{
				match('\u0129');
				break;
			}
			case '\u012b':
			{
				match('\u012B');
				break;
			}
			case '\u012d':
			{
				match('\u012D');
				break;
			}
			case '\u012f':
			{
				match('\u012F');
				break;
			}
			case '\u0131':
			{
				match('\u0131');
				break;
			}
			case '\u0133':
			{
				match('\u0133');
				break;
			}
			case '\u0135':
			{
				match('\u0135');
				break;
			}
			case '\u0137':  case '\u0138':
			{
				matchRange('\u0137','\u0138');
				break;
			}
			case '\u013a':
			{
				match('\u013A');
				break;
			}
			case '\u013c':
			{
				match('\u013C');
				break;
			}
			case '\u013e':
			{
				match('\u013E');
				break;
			}
			case '\u0140':
			{
				match('\u0140');
				break;
			}
			case '\u0142':
			{
				match('\u0142');
				break;
			}
			case '\u0144':
			{
				match('\u0144');
				break;
			}
			case '\u0146':
			{
				match('\u0146');
				break;
			}
			case '\u0148':  case '\u0149':
			{
				matchRange('\u0148','\u0149');
				break;
			}
			case '\u014b':
			{
				match('\u014B');
				break;
			}
			case '\u014d':
			{
				match('\u014D');
				break;
			}
			case '\u014f':
			{
				match('\u014F');
				break;
			}
			case '\u0151':
			{
				match('\u0151');
				break;
			}
			case '\u0153':
			{
				match('\u0153');
				break;
			}
			case '\u0155':
			{
				match('\u0155');
				break;
			}
			case '\u0157':
			{
				match('\u0157');
				break;
			}
			case '\u0159':
			{
				match('\u0159');
				break;
			}
			case '\u015b':
			{
				match('\u015B');
				break;
			}
			case '\u015d':
			{
				match('\u015D');
				break;
			}
			case '\u015f':
			{
				match('\u015F');
				break;
			}
			case '\u0161':
			{
				match('\u0161');
				break;
			}
			case '\u0163':
			{
				match('\u0163');
				break;
			}
			case '\u0165':
			{
				match('\u0165');
				break;
			}
			case '\u0167':
			{
				match('\u0167');
				break;
			}
			case '\u0169':
			{
				match('\u0169');
				break;
			}
			case '\u016b':
			{
				match('\u016B');
				break;
			}
			case '\u016d':
			{
				match('\u016D');
				break;
			}
			case '\u016f':
			{
				match('\u016F');
				break;
			}
			case '\u0171':
			{
				match('\u0171');
				break;
			}
			case '\u0173':
			{
				match('\u0173');
				break;
			}
			case '\u0175':
			{
				match('\u0175');
				break;
			}
			case '\u0177':
			{
				match('\u0177');
				break;
			}
			case '\u017a':
			{
				match('\u017A');
				break;
			}
			case '\u017c':
			{
				match('\u017C');
				break;
			}
			case '\u017e':  case '\u017f':  case '\u0180':
			{
				matchRange('\u017E','\u0180');
				break;
			}
			case '\u0183':
			{
				match('\u0183');
				break;
			}
			case '\u0185':
			{
				match('\u0185');
				break;
			}
			case '\u0188':
			{
				match('\u0188');
				break;
			}
			case '\u018c':  case '\u018d':
			{
				matchRange('\u018C','\u018D');
				break;
			}
			case '\u0192':
			{
				match('\u0192');
				break;
			}
			case '\u0195':
			{
				match('\u0195');
				break;
			}
			case '\u0199':  case '\u019a':  case '\u019b':
			{
				matchRange('\u0199','\u019B');
				break;
			}
			case '\u019e':
			{
				match('\u019E');
				break;
			}
			case '\u01a1':
			{
				match('\u01A1');
				break;
			}
			case '\u01a3':
			{
				match('\u01A3');
				break;
			}
			case '\u01a5':
			{
				match('\u01A5');
				break;
			}
			case '\u01a8':
			{
				match('\u01A8');
				break;
			}
			case '\u01aa':  case '\u01ab':
			{
				matchRange('\u01AA','\u01AB');
				break;
			}
			case '\u01ad':
			{
				match('\u01AD');
				break;
			}
			case '\u01b0':
			{
				match('\u01B0');
				break;
			}
			case '\u01b4':
			{
				match('\u01B4');
				break;
			}
			case '\u01b6':
			{
				match('\u01B6');
				break;
			}
			case '\u01b9':  case '\u01ba':
			{
				matchRange('\u01B9','\u01BA');
				break;
			}
			case '\u01bd':  case '\u01be':  case '\u01bf':
			{
				matchRange('\u01BD','\u01BF');
				break;
			}
			case '\u01c6':
			{
				match('\u01C6');
				break;
			}
			case '\u01c9':
			{
				match('\u01C9');
				break;
			}
			case '\u01cc':
			{
				match('\u01CC');
				break;
			}
			case '\u01ce':
			{
				match('\u01CE');
				break;
			}
			case '\u01d0':
			{
				match('\u01D0');
				break;
			}
			case '\u01d2':
			{
				match('\u01D2');
				break;
			}
			case '\u01d4':
			{
				match('\u01D4');
				break;
			}
			case '\u01d6':
			{
				match('\u01D6');
				break;
			}
			case '\u01d8':
			{
				match('\u01D8');
				break;
			}
			case '\u01da':
			{
				match('\u01DA');
				break;
			}
			case '\u01dc':  case '\u01dd':
			{
				matchRange('\u01DC','\u01DD');
				break;
			}
			case '\u01df':
			{
				match('\u01DF');
				break;
			}
			case '\u01e1':
			{
				match('\u01E1');
				break;
			}
			case '\u01e3':
			{
				match('\u01E3');
				break;
			}
			case '\u01e5':
			{
				match('\u01E5');
				break;
			}
			case '\u01e7':
			{
				match('\u01E7');
				break;
			}
			case '\u01e9':
			{
				match('\u01E9');
				break;
			}
			case '\u01eb':
			{
				match('\u01EB');
				break;
			}
			case '\u01ed':
			{
				match('\u01ED');
				break;
			}
			case '\u01ef':  case '\u01f0':
			{
				matchRange('\u01EF','\u01F0');
				break;
			}
			case '\u01f3':
			{
				match('\u01F3');
				break;
			}
			case '\u01f5':
			{
				match('\u01F5');
				break;
			}
			case '\u01f9':
			{
				match('\u01F9');
				break;
			}
			case '\u01fb':
			{
				match('\u01FB');
				break;
			}
			case '\u01fd':
			{
				match('\u01FD');
				break;
			}
			case '\u01ff':
			{
				match('\u01FF');
				break;
			}
			case '\u0201':
			{
				match('\u0201');
				break;
			}
			case '\u0203':
			{
				match('\u0203');
				break;
			}
			case '\u0205':
			{
				match('\u0205');
				break;
			}
			case '\u0207':
			{
				match('\u0207');
				break;
			}
			case '\u0209':
			{
				match('\u0209');
				break;
			}
			case '\u020b':
			{
				match('\u020B');
				break;
			}
			case '\u020d':
			{
				match('\u020D');
				break;
			}
			case '\u020f':
			{
				match('\u020F');
				break;
			}
			case '\u0211':
			{
				match('\u0211');
				break;
			}
			case '\u0213':
			{
				match('\u0213');
				break;
			}
			case '\u0215':
			{
				match('\u0215');
				break;
			}
			case '\u0217':
			{
				match('\u0217');
				break;
			}
			case '\u0219':
			{
				match('\u0219');
				break;
			}
			case '\u021b':
			{
				match('\u021B');
				break;
			}
			case '\u021d':
			{
				match('\u021D');
				break;
			}
			case '\u021f':
			{
				match('\u021F');
				break;
			}
			case '\u0223':
			{
				match('\u0223');
				break;
			}
			case '\u0225':
			{
				match('\u0225');
				break;
			}
			case '\u0227':
			{
				match('\u0227');
				break;
			}
			case '\u0229':
			{
				match('\u0229');
				break;
			}
			case '\u022b':
			{
				match('\u022B');
				break;
			}
			case '\u022d':
			{
				match('\u022D');
				break;
			}
			case '\u022f':
			{
				match('\u022F');
				break;
			}
			case '\u0231':
			{
				match('\u0231');
				break;
			}
			case '\u0233':
			{
				match('\u0233');
				break;
			}
			case '\u0250':  case '\u0251':  case '\u0252':  case '\u0253':
			case '\u0254':  case '\u0255':  case '\u0256':  case '\u0257':
			case '\u0258':  case '\u0259':  case '\u025a':  case '\u025b':
			case '\u025c':  case '\u025d':  case '\u025e':  case '\u025f':
			case '\u0260':  case '\u0261':  case '\u0262':  case '\u0263':
			case '\u0264':  case '\u0265':  case '\u0266':  case '\u0267':
			case '\u0268':  case '\u0269':  case '\u026a':  case '\u026b':
			case '\u026c':  case '\u026d':  case '\u026e':  case '\u026f':
			case '\u0270':  case '\u0271':  case '\u0272':  case '\u0273':
			case '\u0274':  case '\u0275':  case '\u0276':  case '\u0277':
			case '\u0278':  case '\u0279':  case '\u027a':  case '\u027b':
			case '\u027c':  case '\u027d':  case '\u027e':  case '\u027f':
			case '\u0280':  case '\u0281':  case '\u0282':  case '\u0283':
			case '\u0284':  case '\u0285':  case '\u0286':  case '\u0287':
			case '\u0288':  case '\u0289':  case '\u028a':  case '\u028b':
			case '\u028c':  case '\u028d':  case '\u028e':  case '\u028f':
			case '\u0290':  case '\u0291':  case '\u0292':  case '\u0293':
			case '\u0294':  case '\u0295':  case '\u0296':  case '\u0297':
			case '\u0298':  case '\u0299':  case '\u029a':  case '\u029b':
			case '\u029c':  case '\u029d':  case '\u029e':  case '\u029f':
			case '\u02a0':  case '\u02a1':  case '\u02a2':  case '\u02a3':
			case '\u02a4':  case '\u02a5':  case '\u02a6':  case '\u02a7':
			case '\u02a8':  case '\u02a9':  case '\u02aa':  case '\u02ab':
			case '\u02ac':  case '\u02ad':
			{
				matchRange('\u0250','\u02AD');
				break;
			}
			case '\u0390':
			{
				match('\u0390');
				break;
			}
			case '\u03ac':  case '\u03ad':  case '\u03ae':  case '\u03af':
			case '\u03b0':  case '\u03b1':  case '\u03b2':  case '\u03b3':
			case '\u03b4':  case '\u03b5':  case '\u03b6':  case '\u03b7':
			case '\u03b8':  case '\u03b9':  case '\u03ba':  case '\u03bb':
			case '\u03bc':  case '\u03bd':  case '\u03be':  case '\u03bf':
			case '\u03c0':  case '\u03c1':  case '\u03c2':  case '\u03c3':
			case '\u03c4':  case '\u03c5':  case '\u03c6':  case '\u03c7':
			case '\u03c8':  case '\u03c9':  case '\u03ca':  case '\u03cb':
			case '\u03cc':  case '\u03cd':  case '\u03ce':
			{
				matchRange('\u03AC','\u03CE');
				break;
			}
			case '\u03d0':  case '\u03d1':
			{
				matchRange('\u03D0','\u03D1');
				break;
			}
			case '\u03d5':  case '\u03d6':  case '\u03d7':
			{
				matchRange('\u03D5','\u03D7');
				break;
			}
			case '\u03d9':
			{
				match('\u03D9');
				break;
			}
			case '\u03db':
			{
				match('\u03DB');
				break;
			}
			case '\u03dd':
			{
				match('\u03DD');
				break;
			}
			case '\u03df':
			{
				match('\u03DF');
				break;
			}
			case '\u03e1':
			{
				match('\u03E1');
				break;
			}
			case '\u03e3':
			{
				match('\u03E3');
				break;
			}
			case '\u03e5':
			{
				match('\u03E5');
				break;
			}
			case '\u03e7':
			{
				match('\u03E7');
				break;
			}
			case '\u03e9':
			{
				match('\u03E9');
				break;
			}
			case '\u03eb':
			{
				match('\u03EB');
				break;
			}
			case '\u03ed':
			{
				match('\u03ED');
				break;
			}
			case '\u03ef':  case '\u03f0':  case '\u03f1':  case '\u03f2':
			case '\u03f3':
			{
				matchRange('\u03EF','\u03F3');
				break;
			}
			case '\u03f5':
			{
				match('\u03F5');
				break;
			}
			case '\u0430':  case '\u0431':  case '\u0432':  case '\u0433':
			case '\u0434':  case '\u0435':  case '\u0436':  case '\u0437':
			case '\u0438':  case '\u0439':  case '\u043a':  case '\u043b':
			case '\u043c':  case '\u043d':  case '\u043e':  case '\u043f':
			case '\u0440':  case '\u0441':  case '\u0442':  case '\u0443':
			case '\u0444':  case '\u0445':  case '\u0446':  case '\u0447':
			case '\u0448':  case '\u0449':  case '\u044a':  case '\u044b':
			case '\u044c':  case '\u044d':  case '\u044e':  case '\u044f':
			case '\u0450':  case '\u0451':  case '\u0452':  case '\u0453':
			case '\u0454':  case '\u0455':  case '\u0456':  case '\u0457':
			case '\u0458':  case '\u0459':  case '\u045a':  case '\u045b':
			case '\u045c':  case '\u045d':  case '\u045e':  case '\u045f':
			{
				matchRange('\u0430','\u045F');
				break;
			}
			case '\u0461':
			{
				match('\u0461');
				break;
			}
			case '\u0463':
			{
				match('\u0463');
				break;
			}
			case '\u0465':
			{
				match('\u0465');
				break;
			}
			case '\u0467':
			{
				match('\u0467');
				break;
			}
			case '\u0469':
			{
				match('\u0469');
				break;
			}
			case '\u046b':
			{
				match('\u046B');
				break;
			}
			case '\u046d':
			{
				match('\u046D');
				break;
			}
			case '\u046f':
			{
				match('\u046F');
				break;
			}
			case '\u0471':
			{
				match('\u0471');
				break;
			}
			case '\u0473':
			{
				match('\u0473');
				break;
			}
			case '\u0475':
			{
				match('\u0475');
				break;
			}
			case '\u0477':
			{
				match('\u0477');
				break;
			}
			case '\u0479':
			{
				match('\u0479');
				break;
			}
			case '\u047b':
			{
				match('\u047B');
				break;
			}
			case '\u047d':
			{
				match('\u047D');
				break;
			}
			case '\u047f':
			{
				match('\u047F');
				break;
			}
			case '\u0481':
			{
				match('\u0481');
				break;
			}
			case '\u048b':
			{
				match('\u048B');
				break;
			}
			case '\u048d':
			{
				match('\u048D');
				break;
			}
			case '\u048f':
			{
				match('\u048F');
				break;
			}
			case '\u0491':
			{
				match('\u0491');
				break;
			}
			case '\u0493':
			{
				match('\u0493');
				break;
			}
			case '\u0495':
			{
				match('\u0495');
				break;
			}
			case '\u0497':
			{
				match('\u0497');
				break;
			}
			case '\u0499':
			{
				match('\u0499');
				break;
			}
			case '\u049b':
			{
				match('\u049B');
				break;
			}
			case '\u049d':
			{
				match('\u049D');
				break;
			}
			case '\u049f':
			{
				match('\u049F');
				break;
			}
			case '\u04a1':
			{
				match('\u04A1');
				break;
			}
			case '\u04a3':
			{
				match('\u04A3');
				break;
			}
			case '\u04a5':
			{
				match('\u04A5');
				break;
			}
			case '\u04a7':
			{
				match('\u04A7');
				break;
			}
			case '\u04a9':
			{
				match('\u04A9');
				break;
			}
			case '\u04ab':
			{
				match('\u04AB');
				break;
			}
			case '\u04ad':
			{
				match('\u04AD');
				break;
			}
			case '\u04af':
			{
				match('\u04AF');
				break;
			}
			case '\u04b1':
			{
				match('\u04B1');
				break;
			}
			case '\u04b3':
			{
				match('\u04B3');
				break;
			}
			case '\u04b5':
			{
				match('\u04B5');
				break;
			}
			case '\u04b7':
			{
				match('\u04B7');
				break;
			}
			case '\u04b9':
			{
				match('\u04B9');
				break;
			}
			case '\u04bb':
			{
				match('\u04BB');
				break;
			}
			case '\u04bd':
			{
				match('\u04BD');
				break;
			}
			case '\u04bf':
			{
				match('\u04BF');
				break;
			}
			case '\u04c2':
			{
				match('\u04C2');
				break;
			}
			case '\u04c4':
			{
				match('\u04C4');
				break;
			}
			case '\u04c6':
			{
				match('\u04C6');
				break;
			}
			case '\u04c8':
			{
				match('\u04C8');
				break;
			}
			case '\u04ca':
			{
				match('\u04CA');
				break;
			}
			case '\u04cc':
			{
				match('\u04CC');
				break;
			}
			case '\u04ce':
			{
				match('\u04CE');
				break;
			}
			case '\u04d1':
			{
				match('\u04D1');
				break;
			}
			case '\u04d3':
			{
				match('\u04D3');
				break;
			}
			case '\u04d5':
			{
				match('\u04D5');
				break;
			}
			case '\u04d7':
			{
				match('\u04D7');
				break;
			}
			case '\u04d9':
			{
				match('\u04D9');
				break;
			}
			case '\u04db':
			{
				match('\u04DB');
				break;
			}
			case '\u04dd':
			{
				match('\u04DD');
				break;
			}
			case '\u04df':
			{
				match('\u04DF');
				break;
			}
			case '\u04e1':
			{
				match('\u04E1');
				break;
			}
			case '\u04e3':
			{
				match('\u04E3');
				break;
			}
			case '\u04e5':
			{
				match('\u04E5');
				break;
			}
			case '\u04e7':
			{
				match('\u04E7');
				break;
			}
			case '\u04e9':
			{
				match('\u04E9');
				break;
			}
			case '\u04eb':
			{
				match('\u04EB');
				break;
			}
			case '\u04ed':
			{
				match('\u04ED');
				break;
			}
			case '\u04ef':
			{
				match('\u04EF');
				break;
			}
			case '\u04f1':
			{
				match('\u04F1');
				break;
			}
			case '\u04f3':
			{
				match('\u04F3');
				break;
			}
			case '\u04f5':
			{
				match('\u04F5');
				break;
			}
			case '\u04f9':
			{
				match('\u04F9');
				break;
			}
			case '\u0501':
			{
				match('\u0501');
				break;
			}
			case '\u0503':
			{
				match('\u0503');
				break;
			}
			case '\u0505':
			{
				match('\u0505');
				break;
			}
			case '\u0507':
			{
				match('\u0507');
				break;
			}
			case '\u0509':
			{
				match('\u0509');
				break;
			}
			case '\u050b':
			{
				match('\u050B');
				break;
			}
			case '\u050d':
			{
				match('\u050D');
				break;
			}
			case '\u050f':
			{
				match('\u050F');
				break;
			}
			case '\u0561':  case '\u0562':  case '\u0563':  case '\u0564':
			case '\u0565':  case '\u0566':  case '\u0567':  case '\u0568':
			case '\u0569':  case '\u056a':  case '\u056b':  case '\u056c':
			case '\u056d':  case '\u056e':  case '\u056f':  case '\u0570':
			case '\u0571':  case '\u0572':  case '\u0573':  case '\u0574':
			case '\u0575':  case '\u0576':  case '\u0577':  case '\u0578':
			case '\u0579':  case '\u057a':  case '\u057b':  case '\u057c':
			case '\u057d':  case '\u057e':  case '\u057f':  case '\u0580':
			case '\u0581':  case '\u0582':  case '\u0583':  case '\u0584':
			case '\u0585':  case '\u0586':  case '\u0587':
			{
				matchRange('\u0561','\u0587');
				break;
			}
			case '\u1e01':
			{
				match('\u1E01');
				break;
			}
			case '\u1e03':
			{
				match('\u1E03');
				break;
			}
			case '\u1e05':
			{
				match('\u1E05');
				break;
			}
			case '\u1e07':
			{
				match('\u1E07');
				break;
			}
			case '\u1e09':
			{
				match('\u1E09');
				break;
			}
			case '\u1e0b':
			{
				match('\u1E0B');
				break;
			}
			case '\u1e0d':
			{
				match('\u1E0D');
				break;
			}
			case '\u1e0f':
			{
				match('\u1E0F');
				break;
			}
			case '\u1e11':
			{
				match('\u1E11');
				break;
			}
			case '\u1e13':
			{
				match('\u1E13');
				break;
			}
			case '\u1e15':
			{
				match('\u1E15');
				break;
			}
			case '\u1e17':
			{
				match('\u1E17');
				break;
			}
			case '\u1e19':
			{
				match('\u1E19');
				break;
			}
			case '\u1e1b':
			{
				match('\u1E1B');
				break;
			}
			case '\u1e1d':
			{
				match('\u1E1D');
				break;
			}
			case '\u1e1f':
			{
				match('\u1E1F');
				break;
			}
			case '\u1e21':
			{
				match('\u1E21');
				break;
			}
			case '\u1e23':
			{
				match('\u1E23');
				break;
			}
			case '\u1e25':
			{
				match('\u1E25');
				break;
			}
			case '\u1e27':
			{
				match('\u1E27');
				break;
			}
			case '\u1e29':
			{
				match('\u1E29');
				break;
			}
			case '\u1e2b':
			{
				match('\u1E2B');
				break;
			}
			case '\u1e2d':
			{
				match('\u1E2D');
				break;
			}
			case '\u1e2f':
			{
				match('\u1E2F');
				break;
			}
			case '\u1e31':
			{
				match('\u1E31');
				break;
			}
			case '\u1e33':
			{
				match('\u1E33');
				break;
			}
			case '\u1e35':
			{
				match('\u1E35');
				break;
			}
			case '\u1e37':
			{
				match('\u1E37');
				break;
			}
			case '\u1e39':
			{
				match('\u1E39');
				break;
			}
			case '\u1e3b':
			{
				match('\u1E3B');
				break;
			}
			case '\u1e3d':
			{
				match('\u1E3D');
				break;
			}
			case '\u1e3f':
			{
				match('\u1E3F');
				break;
			}
			case '\u1e41':
			{
				match('\u1E41');
				break;
			}
			case '\u1e43':
			{
				match('\u1E43');
				break;
			}
			case '\u1e45':
			{
				match('\u1E45');
				break;
			}
			case '\u1e47':
			{
				match('\u1E47');
				break;
			}
			case '\u1e49':
			{
				match('\u1E49');
				break;
			}
			case '\u1e4b':
			{
				match('\u1E4B');
				break;
			}
			case '\u1e4d':
			{
				match('\u1E4D');
				break;
			}
			case '\u1e4f':
			{
				match('\u1E4F');
				break;
			}
			case '\u1e51':
			{
				match('\u1E51');
				break;
			}
			case '\u1e53':
			{
				match('\u1E53');
				break;
			}
			case '\u1e55':
			{
				match('\u1E55');
				break;
			}
			case '\u1e57':
			{
				match('\u1E57');
				break;
			}
			case '\u1e59':
			{
				match('\u1E59');
				break;
			}
			case '\u1e5b':
			{
				match('\u1E5B');
				break;
			}
			case '\u1e5d':
			{
				match('\u1E5D');
				break;
			}
			case '\u1e5f':
			{
				match('\u1E5F');
				break;
			}
			case '\u1e61':
			{
				match('\u1E61');
				break;
			}
			case '\u1e63':
			{
				match('\u1E63');
				break;
			}
			case '\u1e65':
			{
				match('\u1E65');
				break;
			}
			case '\u1e67':
			{
				match('\u1E67');
				break;
			}
			case '\u1e69':
			{
				match('\u1E69');
				break;
			}
			case '\u1e6b':
			{
				match('\u1E6B');
				break;
			}
			case '\u1e6d':
			{
				match('\u1E6D');
				break;
			}
			case '\u1e6f':
			{
				match('\u1E6F');
				break;
			}
			case '\u1e71':
			{
				match('\u1E71');
				break;
			}
			case '\u1e73':
			{
				match('\u1E73');
				break;
			}
			case '\u1e75':
			{
				match('\u1E75');
				break;
			}
			case '\u1e77':
			{
				match('\u1E77');
				break;
			}
			case '\u1e79':
			{
				match('\u1E79');
				break;
			}
			case '\u1e7b':
			{
				match('\u1E7B');
				break;
			}
			case '\u1e7d':
			{
				match('\u1E7D');
				break;
			}
			case '\u1e7f':
			{
				match('\u1E7F');
				break;
			}
			case '\u1e81':
			{
				match('\u1E81');
				break;
			}
			case '\u1e83':
			{
				match('\u1E83');
				break;
			}
			case '\u1e85':
			{
				match('\u1E85');
				break;
			}
			case '\u1e87':
			{
				match('\u1E87');
				break;
			}
			case '\u1e89':
			{
				match('\u1E89');
				break;
			}
			case '\u1e8b':
			{
				match('\u1E8B');
				break;
			}
			case '\u1e8d':
			{
				match('\u1E8D');
				break;
			}
			case '\u1e8f':
			{
				match('\u1E8F');
				break;
			}
			case '\u1e91':
			{
				match('\u1E91');
				break;
			}
			case '\u1e93':
			{
				match('\u1E93');
				break;
			}
			case '\u1e95':  case '\u1e96':  case '\u1e97':  case '\u1e98':
			case '\u1e99':  case '\u1e9a':  case '\u1e9b':
			{
				matchRange('\u1E95','\u1E9B');
				break;
			}
			case '\u1ea1':
			{
				match('\u1EA1');
				break;
			}
			case '\u1ea3':
			{
				match('\u1EA3');
				break;
			}
			case '\u1ea5':
			{
				match('\u1EA5');
				break;
			}
			case '\u1ea7':
			{
				match('\u1EA7');
				break;
			}
			case '\u1ea9':
			{
				match('\u1EA9');
				break;
			}
			case '\u1eab':
			{
				match('\u1EAB');
				break;
			}
			case '\u1ead':
			{
				match('\u1EAD');
				break;
			}
			case '\u1eaf':
			{
				match('\u1EAF');
				break;
			}
			case '\u1eb1':
			{
				match('\u1EB1');
				break;
			}
			case '\u1eb3':
			{
				match('\u1EB3');
				break;
			}
			case '\u1eb5':
			{
				match('\u1EB5');
				break;
			}
			case '\u1eb7':
			{
				match('\u1EB7');
				break;
			}
			case '\u1eb9':
			{
				match('\u1EB9');
				break;
			}
			case '\u1ebb':
			{
				match('\u1EBB');
				break;
			}
			case '\u1ebd':
			{
				match('\u1EBD');
				break;
			}
			case '\u1ebf':
			{
				match('\u1EBF');
				break;
			}
			case '\u1ec1':
			{
				match('\u1EC1');
				break;
			}
			case '\u1ec3':
			{
				match('\u1EC3');
				break;
			}
			case '\u1ec5':
			{
				match('\u1EC5');
				break;
			}
			case '\u1ec7':
			{
				match('\u1EC7');
				break;
			}
			case '\u1ec9':
			{
				match('\u1EC9');
				break;
			}
			case '\u1ecb':
			{
				match('\u1ECB');
				break;
			}
			case '\u1ecd':
			{
				match('\u1ECD');
				break;
			}
			case '\u1ecf':
			{
				match('\u1ECF');
				break;
			}
			case '\u1ed1':
			{
				match('\u1ED1');
				break;
			}
			case '\u1ed3':
			{
				match('\u1ED3');
				break;
			}
			case '\u1ed5':
			{
				match('\u1ED5');
				break;
			}
			case '\u1ed7':
			{
				match('\u1ED7');
				break;
			}
			case '\u1ed9':
			{
				match('\u1ED9');
				break;
			}
			case '\u1edb':
			{
				match('\u1EDB');
				break;
			}
			case '\u1edd':
			{
				match('\u1EDD');
				break;
			}
			case '\u1edf':
			{
				match('\u1EDF');
				break;
			}
			case '\u1ee1':
			{
				match('\u1EE1');
				break;
			}
			case '\u1ee3':
			{
				match('\u1EE3');
				break;
			}
			case '\u1ee5':
			{
				match('\u1EE5');
				break;
			}
			case '\u1ee7':
			{
				match('\u1EE7');
				break;
			}
			case '\u1ee9':
			{
				match('\u1EE9');
				break;
			}
			case '\u1eeb':
			{
				match('\u1EEB');
				break;
			}
			case '\u1eed':
			{
				match('\u1EED');
				break;
			}
			case '\u1eef':
			{
				match('\u1EEF');
				break;
			}
			case '\u1ef1':
			{
				match('\u1EF1');
				break;
			}
			case '\u1ef3':
			{
				match('\u1EF3');
				break;
			}
			case '\u1ef5':
			{
				match('\u1EF5');
				break;
			}
			case '\u1ef7':
			{
				match('\u1EF7');
				break;
			}
			case '\u1ef9':
			{
				match('\u1EF9');
				break;
			}
			case '\u1f00':  case '\u1f01':  case '\u1f02':  case '\u1f03':
			case '\u1f04':  case '\u1f05':  case '\u1f06':  case '\u1f07':
			{
				matchRange('\u1F00','\u1F07');
				break;
			}
			case '\u1f10':  case '\u1f11':  case '\u1f12':  case '\u1f13':
			case '\u1f14':  case '\u1f15':
			{
				matchRange('\u1F10','\u1F15');
				break;
			}
			case '\u1f20':  case '\u1f21':  case '\u1f22':  case '\u1f23':
			case '\u1f24':  case '\u1f25':  case '\u1f26':  case '\u1f27':
			{
				matchRange('\u1F20','\u1F27');
				break;
			}
			case '\u1f30':  case '\u1f31':  case '\u1f32':  case '\u1f33':
			case '\u1f34':  case '\u1f35':  case '\u1f36':  case '\u1f37':
			{
				matchRange('\u1F30','\u1F37');
				break;
			}
			case '\u1f40':  case '\u1f41':  case '\u1f42':  case '\u1f43':
			case '\u1f44':  case '\u1f45':
			{
				matchRange('\u1F40','\u1F45');
				break;
			}
			case '\u1f50':  case '\u1f51':  case '\u1f52':  case '\u1f53':
			case '\u1f54':  case '\u1f55':  case '\u1f56':  case '\u1f57':
			{
				matchRange('\u1F50','\u1F57');
				break;
			}
			case '\u1f60':  case '\u1f61':  case '\u1f62':  case '\u1f63':
			case '\u1f64':  case '\u1f65':  case '\u1f66':  case '\u1f67':
			{
				matchRange('\u1F60','\u1F67');
				break;
			}
			case '\u1f70':  case '\u1f71':  case '\u1f72':  case '\u1f73':
			case '\u1f74':  case '\u1f75':  case '\u1f76':  case '\u1f77':
			case '\u1f78':  case '\u1f79':  case '\u1f7a':  case '\u1f7b':
			case '\u1f7c':  case '\u1f7d':
			{
				matchRange('\u1F70','\u1F7D');
				break;
			}
			case '\u1f80':  case '\u1f81':  case '\u1f82':  case '\u1f83':
			case '\u1f84':  case '\u1f85':  case '\u1f86':  case '\u1f87':
			{
				matchRange('\u1F80','\u1F87');
				break;
			}
			case '\u1f90':  case '\u1f91':  case '\u1f92':  case '\u1f93':
			case '\u1f94':  case '\u1f95':  case '\u1f96':  case '\u1f97':
			{
				matchRange('\u1F90','\u1F97');
				break;
			}
			case '\u1fa0':  case '\u1fa1':  case '\u1fa2':  case '\u1fa3':
			case '\u1fa4':  case '\u1fa5':  case '\u1fa6':  case '\u1fa7':
			{
				matchRange('\u1FA0','\u1FA7');
				break;
			}
			case '\u1fb0':  case '\u1fb1':  case '\u1fb2':  case '\u1fb3':
			case '\u1fb4':
			{
				matchRange('\u1FB0','\u1FB4');
				break;
			}
			case '\u1fb6':  case '\u1fb7':
			{
				matchRange('\u1FB6','\u1FB7');
				break;
			}
			case '\u1fbe':
			{
				match('\u1FBE');
				break;
			}
			case '\u1fc2':  case '\u1fc3':  case '\u1fc4':
			{
				matchRange('\u1FC2','\u1FC4');
				break;
			}
			case '\u1fc6':  case '\u1fc7':
			{
				matchRange('\u1FC6','\u1FC7');
				break;
			}
			case '\u1fd0':  case '\u1fd1':  case '\u1fd2':  case '\u1fd3':
			{
				matchRange('\u1FD0','\u1FD3');
				break;
			}
			case '\u1fd6':  case '\u1fd7':
			{
				matchRange('\u1FD6','\u1FD7');
				break;
			}
			case '\u1fe0':  case '\u1fe1':  case '\u1fe2':  case '\u1fe3':
			case '\u1fe4':  case '\u1fe5':  case '\u1fe6':  case '\u1fe7':
			{
				matchRange('\u1FE0','\u1FE7');
				break;
			}
			case '\u1ff2':  case '\u1ff3':  case '\u1ff4':
			{
				matchRange('\u1FF2','\u1FF4');
				break;
			}
			case '\u1ff6':  case '\u1ff7':
			{
				matchRange('\u1FF6','\u1FF7');
				break;
			}
			case '\u2071':
			{
				match('\u2071');
				break;
			}
			case '\u207f':
			{
				match('\u207F');
				break;
			}
			case '\u210a':
			{
				match('\u210A');
				break;
			}
			case '\u210e':  case '\u210f':
			{
				matchRange('\u210E','\u210F');
				break;
			}
			case '\u2113':
			{
				match('\u2113');
				break;
			}
			case '\u212f':
			{
				match('\u212F');
				break;
			}
			case '\u2134':
			{
				match('\u2134');
				break;
			}
			case '\u2139':
			{
				match('\u2139');
				break;
			}
			case '\u213d':
			{
				match('\u213D');
				break;
			}
			case '\u2146':  case '\u2147':  case '\u2148':  case '\u2149':
			{
				matchRange('\u2146','\u2149');
				break;
			}
			case '\ufb00':  case '\ufb01':  case '\ufb02':  case '\ufb03':
			case '\ufb04':  case '\ufb05':  case '\ufb06':
			{
				matchRange('\uFB00','\uFB06');
				break;
			}
			case '\ufb13':  case '\ufb14':  case '\ufb15':  case '\ufb16':
			case '\ufb17':
			{
				matchRange('\uFB13','\uFB17');
				break;
			}
			case '\uff41':  case '\uff42':  case '\uff43':  case '\uff44':
			case '\uff45':  case '\uff46':  case '\uff47':  case '\uff48':
			case '\uff49':  case '\uff4a':  case '\uff4b':  case '\uff4c':
			case '\uff4d':  case '\uff4e':  case '\uff4f':  case '\uff50':
			case '\uff51':  case '\uff52':  case '\uff53':  case '\uff54':
			case '\uff55':  case '\uff56':  case '\uff57':  case '\uff58':
			case '\uff59':  case '\uff5a':
			{
				matchRange('\uFF41','\uFF5A');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Lt(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Lt;
		
		{
			switch ( cached_LA1 )
			{
			case '\u01c5':
			{
				match('\u01C5');
				break;
			}
			case '\u01c8':
			{
				match('\u01C8');
				break;
			}
			case '\u01cb':
			{
				match('\u01CB');
				break;
			}
			case '\u01f2':
			{
				match('\u01F2');
				break;
			}
			case '\u1f88':  case '\u1f89':  case '\u1f8a':  case '\u1f8b':
			case '\u1f8c':  case '\u1f8d':  case '\u1f8e':  case '\u1f8f':
			{
				matchRange('\u1F88','\u1F8F');
				break;
			}
			case '\u1f98':  case '\u1f99':  case '\u1f9a':  case '\u1f9b':
			case '\u1f9c':  case '\u1f9d':  case '\u1f9e':  case '\u1f9f':
			{
				matchRange('\u1F98','\u1F9F');
				break;
			}
			case '\u1fa8':  case '\u1fa9':  case '\u1faa':  case '\u1fab':
			case '\u1fac':  case '\u1fad':  case '\u1fae':  case '\u1faf':
			{
				matchRange('\u1FA8','\u1FAF');
				break;
			}
			case '\u1fbc':
			{
				match('\u1FBC');
				break;
			}
			case '\u1fcc':
			{
				match('\u1FCC');
				break;
			}
			case '\u1ffc':
			{
				match('\u1FFC');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Lm(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Lm;
		
		{
			switch ( cached_LA1 )
			{
			case '\u02b0':  case '\u02b1':  case '\u02b2':  case '\u02b3':
			case '\u02b4':  case '\u02b5':  case '\u02b6':  case '\u02b7':
			case '\u02b8':
			{
				matchRange('\u02B0','\u02B8');
				break;
			}
			case '\u02bb':  case '\u02bc':  case '\u02bd':  case '\u02be':
			case '\u02bf':  case '\u02c0':  case '\u02c1':
			{
				matchRange('\u02BB','\u02C1');
				break;
			}
			case '\u02d0':  case '\u02d1':
			{
				matchRange('\u02D0','\u02D1');
				break;
			}
			case '\u02e0':  case '\u02e1':  case '\u02e2':  case '\u02e3':
			case '\u02e4':
			{
				matchRange('\u02E0','\u02E4');
				break;
			}
			case '\u02ee':
			{
				match('\u02EE');
				break;
			}
			case '\u037a':
			{
				match('\u037A');
				break;
			}
			case '\u0559':
			{
				match('\u0559');
				break;
			}
			case '\u0640':
			{
				match('\u0640');
				break;
			}
			case '\u06e5':  case '\u06e6':
			{
				matchRange('\u06E5','\u06E6');
				break;
			}
			case '\u0e46':
			{
				match('\u0E46');
				break;
			}
			case '\u0ec6':
			{
				match('\u0EC6');
				break;
			}
			case '\u17d7':
			{
				match('\u17D7');
				break;
			}
			case '\u1843':
			{
				match('\u1843');
				break;
			}
			case '\u3005':
			{
				match('\u3005');
				break;
			}
			case '\u3031':  case '\u3032':  case '\u3033':  case '\u3034':
			case '\u3035':
			{
				matchRange('\u3031','\u3035');
				break;
			}
			case '\u303b':
			{
				match('\u303B');
				break;
			}
			case '\u309d':  case '\u309e':
			{
				matchRange('\u309D','\u309E');
				break;
			}
			case '\u30fc':  case '\u30fd':  case '\u30fe':
			{
				matchRange('\u30FC','\u30FE');
				break;
			}
			case '\uff70':
			{
				match('\uFF70');
				break;
			}
			case '\uff9e':  case '\uff9f':
			{
				matchRange('\uFF9E','\uFF9F');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Lo(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Lo;
		
		{
			switch ( cached_LA1 )
			{
			case '\u01bb':
			{
				match('\u01BB');
				break;
			}
			case '\u01c0':  case '\u01c1':  case '\u01c2':  case '\u01c3':
			{
				matchRange('\u01C0','\u01C3');
				break;
			}
			case '\u05d0':  case '\u05d1':  case '\u05d2':  case '\u05d3':
			case '\u05d4':  case '\u05d5':  case '\u05d6':  case '\u05d7':
			case '\u05d8':  case '\u05d9':  case '\u05da':  case '\u05db':
			case '\u05dc':  case '\u05dd':  case '\u05de':  case '\u05df':
			case '\u05e0':  case '\u05e1':  case '\u05e2':  case '\u05e3':
			case '\u05e4':  case '\u05e5':  case '\u05e6':  case '\u05e7':
			case '\u05e8':  case '\u05e9':  case '\u05ea':
			{
				matchRange('\u05D0','\u05EA');
				break;
			}
			case '\u05f0':  case '\u05f1':  case '\u05f2':
			{
				matchRange('\u05F0','\u05F2');
				break;
			}
			case '\u0621':  case '\u0622':  case '\u0623':  case '\u0624':
			case '\u0625':  case '\u0626':  case '\u0627':  case '\u0628':
			case '\u0629':  case '\u062a':  case '\u062b':  case '\u062c':
			case '\u062d':  case '\u062e':  case '\u062f':  case '\u0630':
			case '\u0631':  case '\u0632':  case '\u0633':  case '\u0634':
			case '\u0635':  case '\u0636':  case '\u0637':  case '\u0638':
			case '\u0639':  case '\u063a':
			{
				matchRange('\u0621','\u063A');
				break;
			}
			case '\u0641':  case '\u0642':  case '\u0643':  case '\u0644':
			case '\u0645':  case '\u0646':  case '\u0647':  case '\u0648':
			case '\u0649':  case '\u064a':
			{
				matchRange('\u0641','\u064A');
				break;
			}
			case '\u066e':  case '\u066f':
			{
				matchRange('\u066E','\u066F');
				break;
			}
			case '\u0671':  case '\u0672':  case '\u0673':  case '\u0674':
			case '\u0675':  case '\u0676':  case '\u0677':  case '\u0678':
			case '\u0679':  case '\u067a':  case '\u067b':  case '\u067c':
			case '\u067d':  case '\u067e':  case '\u067f':  case '\u0680':
			case '\u0681':  case '\u0682':  case '\u0683':  case '\u0684':
			case '\u0685':  case '\u0686':  case '\u0687':  case '\u0688':
			case '\u0689':  case '\u068a':  case '\u068b':  case '\u068c':
			case '\u068d':  case '\u068e':  case '\u068f':  case '\u0690':
			case '\u0691':  case '\u0692':  case '\u0693':  case '\u0694':
			case '\u0695':  case '\u0696':  case '\u0697':  case '\u0698':
			case '\u0699':  case '\u069a':  case '\u069b':  case '\u069c':
			case '\u069d':  case '\u069e':  case '\u069f':  case '\u06a0':
			case '\u06a1':  case '\u06a2':  case '\u06a3':  case '\u06a4':
			case '\u06a5':  case '\u06a6':  case '\u06a7':  case '\u06a8':
			case '\u06a9':  case '\u06aa':  case '\u06ab':  case '\u06ac':
			case '\u06ad':  case '\u06ae':  case '\u06af':  case '\u06b0':
			case '\u06b1':  case '\u06b2':  case '\u06b3':  case '\u06b4':
			case '\u06b5':  case '\u06b6':  case '\u06b7':  case '\u06b8':
			case '\u06b9':  case '\u06ba':  case '\u06bb':  case '\u06bc':
			case '\u06bd':  case '\u06be':  case '\u06bf':  case '\u06c0':
			case '\u06c1':  case '\u06c2':  case '\u06c3':  case '\u06c4':
			case '\u06c5':  case '\u06c6':  case '\u06c7':  case '\u06c8':
			case '\u06c9':  case '\u06ca':  case '\u06cb':  case '\u06cc':
			case '\u06cd':  case '\u06ce':  case '\u06cf':  case '\u06d0':
			case '\u06d1':  case '\u06d2':  case '\u06d3':
			{
				matchRange('\u0671','\u06D3');
				break;
			}
			case '\u06d5':
			{
				match('\u06D5');
				break;
			}
			case '\u06fa':  case '\u06fb':  case '\u06fc':
			{
				matchRange('\u06FA','\u06FC');
				break;
			}
			case '\u0710':
			{
				match('\u0710');
				break;
			}
			case '\u0712':  case '\u0713':  case '\u0714':  case '\u0715':
			case '\u0716':  case '\u0717':  case '\u0718':  case '\u0719':
			case '\u071a':  case '\u071b':  case '\u071c':  case '\u071d':
			case '\u071e':  case '\u071f':  case '\u0720':  case '\u0721':
			case '\u0722':  case '\u0723':  case '\u0724':  case '\u0725':
			case '\u0726':  case '\u0727':  case '\u0728':  case '\u0729':
			case '\u072a':  case '\u072b':  case '\u072c':
			{
				matchRange('\u0712','\u072C');
				break;
			}
			case '\u0780':  case '\u0781':  case '\u0782':  case '\u0783':
			case '\u0784':  case '\u0785':  case '\u0786':  case '\u0787':
			case '\u0788':  case '\u0789':  case '\u078a':  case '\u078b':
			case '\u078c':  case '\u078d':  case '\u078e':  case '\u078f':
			case '\u0790':  case '\u0791':  case '\u0792':  case '\u0793':
			case '\u0794':  case '\u0795':  case '\u0796':  case '\u0797':
			case '\u0798':  case '\u0799':  case '\u079a':  case '\u079b':
			case '\u079c':  case '\u079d':  case '\u079e':  case '\u079f':
			case '\u07a0':  case '\u07a1':  case '\u07a2':  case '\u07a3':
			case '\u07a4':  case '\u07a5':
			{
				matchRange('\u0780','\u07A5');
				break;
			}
			case '\u07b1':
			{
				match('\u07B1');
				break;
			}
			case '\u0905':  case '\u0906':  case '\u0907':  case '\u0908':
			case '\u0909':  case '\u090a':  case '\u090b':  case '\u090c':
			case '\u090d':  case '\u090e':  case '\u090f':  case '\u0910':
			case '\u0911':  case '\u0912':  case '\u0913':  case '\u0914':
			case '\u0915':  case '\u0916':  case '\u0917':  case '\u0918':
			case '\u0919':  case '\u091a':  case '\u091b':  case '\u091c':
			case '\u091d':  case '\u091e':  case '\u091f':  case '\u0920':
			case '\u0921':  case '\u0922':  case '\u0923':  case '\u0924':
			case '\u0925':  case '\u0926':  case '\u0927':  case '\u0928':
			case '\u0929':  case '\u092a':  case '\u092b':  case '\u092c':
			case '\u092d':  case '\u092e':  case '\u092f':  case '\u0930':
			case '\u0931':  case '\u0932':  case '\u0933':  case '\u0934':
			case '\u0935':  case '\u0936':  case '\u0937':  case '\u0938':
			case '\u0939':
			{
				matchRange('\u0905','\u0939');
				break;
			}
			case '\u093d':
			{
				match('\u093D');
				break;
			}
			case '\u0950':
			{
				match('\u0950');
				break;
			}
			case '\u0958':  case '\u0959':  case '\u095a':  case '\u095b':
			case '\u095c':  case '\u095d':  case '\u095e':  case '\u095f':
			case '\u0960':  case '\u0961':
			{
				matchRange('\u0958','\u0961');
				break;
			}
			case '\u0985':  case '\u0986':  case '\u0987':  case '\u0988':
			case '\u0989':  case '\u098a':  case '\u098b':  case '\u098c':
			{
				matchRange('\u0985','\u098C');
				break;
			}
			case '\u098f':  case '\u0990':
			{
				matchRange('\u098F','\u0990');
				break;
			}
			case '\u0993':  case '\u0994':  case '\u0995':  case '\u0996':
			case '\u0997':  case '\u0998':  case '\u0999':  case '\u099a':
			case '\u099b':  case '\u099c':  case '\u099d':  case '\u099e':
			case '\u099f':  case '\u09a0':  case '\u09a1':  case '\u09a2':
			case '\u09a3':  case '\u09a4':  case '\u09a5':  case '\u09a6':
			case '\u09a7':  case '\u09a8':
			{
				matchRange('\u0993','\u09A8');
				break;
			}
			case '\u09aa':  case '\u09ab':  case '\u09ac':  case '\u09ad':
			case '\u09ae':  case '\u09af':  case '\u09b0':
			{
				matchRange('\u09AA','\u09B0');
				break;
			}
			case '\u09b2':
			{
				match('\u09B2');
				break;
			}
			case '\u09b6':  case '\u09b7':  case '\u09b8':  case '\u09b9':
			{
				matchRange('\u09B6','\u09B9');
				break;
			}
			case '\u09dc':  case '\u09dd':
			{
				matchRange('\u09DC','\u09DD');
				break;
			}
			case '\u09df':  case '\u09e0':  case '\u09e1':
			{
				matchRange('\u09DF','\u09E1');
				break;
			}
			case '\u09f0':  case '\u09f1':
			{
				matchRange('\u09F0','\u09F1');
				break;
			}
			case '\u0a05':  case '\u0a06':  case '\u0a07':  case '\u0a08':
			case '\u0a09':  case '\u0a0a':
			{
				matchRange('\u0A05','\u0A0A');
				break;
			}
			case '\u0a0f':  case '\u0a10':
			{
				matchRange('\u0A0F','\u0A10');
				break;
			}
			case '\u0a13':  case '\u0a14':  case '\u0a15':  case '\u0a16':
			case '\u0a17':  case '\u0a18':  case '\u0a19':  case '\u0a1a':
			case '\u0a1b':  case '\u0a1c':  case '\u0a1d':  case '\u0a1e':
			case '\u0a1f':  case '\u0a20':  case '\u0a21':  case '\u0a22':
			case '\u0a23':  case '\u0a24':  case '\u0a25':  case '\u0a26':
			case '\u0a27':  case '\u0a28':
			{
				matchRange('\u0A13','\u0A28');
				break;
			}
			case '\u0a2a':  case '\u0a2b':  case '\u0a2c':  case '\u0a2d':
			case '\u0a2e':  case '\u0a2f':  case '\u0a30':
			{
				matchRange('\u0A2A','\u0A30');
				break;
			}
			case '\u0a32':  case '\u0a33':
			{
				matchRange('\u0A32','\u0A33');
				break;
			}
			case '\u0a35':  case '\u0a36':
			{
				matchRange('\u0A35','\u0A36');
				break;
			}
			case '\u0a38':  case '\u0a39':
			{
				matchRange('\u0A38','\u0A39');
				break;
			}
			case '\u0a59':  case '\u0a5a':  case '\u0a5b':  case '\u0a5c':
			{
				matchRange('\u0A59','\u0A5C');
				break;
			}
			case '\u0a5e':
			{
				match('\u0A5E');
				break;
			}
			case '\u0a72':  case '\u0a73':  case '\u0a74':
			{
				matchRange('\u0A72','\u0A74');
				break;
			}
			case '\u0a85':  case '\u0a86':  case '\u0a87':  case '\u0a88':
			case '\u0a89':  case '\u0a8a':  case '\u0a8b':
			{
				matchRange('\u0A85','\u0A8B');
				break;
			}
			case '\u0a8d':
			{
				match('\u0A8D');
				break;
			}
			case '\u0a8f':  case '\u0a90':  case '\u0a91':
			{
				matchRange('\u0A8F','\u0A91');
				break;
			}
			case '\u0a93':  case '\u0a94':  case '\u0a95':  case '\u0a96':
			case '\u0a97':  case '\u0a98':  case '\u0a99':  case '\u0a9a':
			case '\u0a9b':  case '\u0a9c':  case '\u0a9d':  case '\u0a9e':
			case '\u0a9f':  case '\u0aa0':  case '\u0aa1':  case '\u0aa2':
			case '\u0aa3':  case '\u0aa4':  case '\u0aa5':  case '\u0aa6':
			case '\u0aa7':  case '\u0aa8':
			{
				matchRange('\u0A93','\u0AA8');
				break;
			}
			case '\u0aaa':  case '\u0aab':  case '\u0aac':  case '\u0aad':
			case '\u0aae':  case '\u0aaf':  case '\u0ab0':
			{
				matchRange('\u0AAA','\u0AB0');
				break;
			}
			case '\u0ab2':  case '\u0ab3':
			{
				matchRange('\u0AB2','\u0AB3');
				break;
			}
			case '\u0ab5':  case '\u0ab6':  case '\u0ab7':  case '\u0ab8':
			case '\u0ab9':
			{
				matchRange('\u0AB5','\u0AB9');
				break;
			}
			case '\u0abd':
			{
				match('\u0ABD');
				break;
			}
			case '\u0ad0':
			{
				match('\u0AD0');
				break;
			}
			case '\u0ae0':
			{
				match('\u0AE0');
				break;
			}
			case '\u0b05':  case '\u0b06':  case '\u0b07':  case '\u0b08':
			case '\u0b09':  case '\u0b0a':  case '\u0b0b':  case '\u0b0c':
			{
				matchRange('\u0B05','\u0B0C');
				break;
			}
			case '\u0b0f':  case '\u0b10':
			{
				matchRange('\u0B0F','\u0B10');
				break;
			}
			case '\u0b13':  case '\u0b14':  case '\u0b15':  case '\u0b16':
			case '\u0b17':  case '\u0b18':  case '\u0b19':  case '\u0b1a':
			case '\u0b1b':  case '\u0b1c':  case '\u0b1d':  case '\u0b1e':
			case '\u0b1f':  case '\u0b20':  case '\u0b21':  case '\u0b22':
			case '\u0b23':  case '\u0b24':  case '\u0b25':  case '\u0b26':
			case '\u0b27':  case '\u0b28':
			{
				matchRange('\u0B13','\u0B28');
				break;
			}
			case '\u0b2a':  case '\u0b2b':  case '\u0b2c':  case '\u0b2d':
			case '\u0b2e':  case '\u0b2f':  case '\u0b30':
			{
				matchRange('\u0B2A','\u0B30');
				break;
			}
			case '\u0b32':  case '\u0b33':
			{
				matchRange('\u0B32','\u0B33');
				break;
			}
			case '\u0b36':  case '\u0b37':  case '\u0b38':  case '\u0b39':
			{
				matchRange('\u0B36','\u0B39');
				break;
			}
			case '\u0b3d':
			{
				match('\u0B3D');
				break;
			}
			case '\u0b5c':  case '\u0b5d':
			{
				matchRange('\u0B5C','\u0B5D');
				break;
			}
			case '\u0b5f':  case '\u0b60':  case '\u0b61':
			{
				matchRange('\u0B5F','\u0B61');
				break;
			}
			case '\u0b83':
			{
				match('\u0B83');
				break;
			}
			case '\u0b85':  case '\u0b86':  case '\u0b87':  case '\u0b88':
			case '\u0b89':  case '\u0b8a':
			{
				matchRange('\u0B85','\u0B8A');
				break;
			}
			case '\u0b8e':  case '\u0b8f':  case '\u0b90':
			{
				matchRange('\u0B8E','\u0B90');
				break;
			}
			case '\u0b92':  case '\u0b93':  case '\u0b94':  case '\u0b95':
			{
				matchRange('\u0B92','\u0B95');
				break;
			}
			case '\u0b99':  case '\u0b9a':
			{
				matchRange('\u0B99','\u0B9A');
				break;
			}
			case '\u0b9c':
			{
				match('\u0B9C');
				break;
			}
			case '\u0b9e':  case '\u0b9f':
			{
				matchRange('\u0B9E','\u0B9F');
				break;
			}
			case '\u0ba3':  case '\u0ba4':
			{
				matchRange('\u0BA3','\u0BA4');
				break;
			}
			case '\u0ba8':  case '\u0ba9':  case '\u0baa':
			{
				matchRange('\u0BA8','\u0BAA');
				break;
			}
			case '\u0bae':  case '\u0baf':  case '\u0bb0':  case '\u0bb1':
			case '\u0bb2':  case '\u0bb3':  case '\u0bb4':  case '\u0bb5':
			{
				matchRange('\u0BAE','\u0BB5');
				break;
			}
			case '\u0bb7':  case '\u0bb8':  case '\u0bb9':
			{
				matchRange('\u0BB7','\u0BB9');
				break;
			}
			case '\u0c05':  case '\u0c06':  case '\u0c07':  case '\u0c08':
			case '\u0c09':  case '\u0c0a':  case '\u0c0b':  case '\u0c0c':
			{
				matchRange('\u0C05','\u0C0C');
				break;
			}
			case '\u0c0e':  case '\u0c0f':  case '\u0c10':
			{
				matchRange('\u0C0E','\u0C10');
				break;
			}
			case '\u0c12':  case '\u0c13':  case '\u0c14':  case '\u0c15':
			case '\u0c16':  case '\u0c17':  case '\u0c18':  case '\u0c19':
			case '\u0c1a':  case '\u0c1b':  case '\u0c1c':  case '\u0c1d':
			case '\u0c1e':  case '\u0c1f':  case '\u0c20':  case '\u0c21':
			case '\u0c22':  case '\u0c23':  case '\u0c24':  case '\u0c25':
			case '\u0c26':  case '\u0c27':  case '\u0c28':
			{
				matchRange('\u0C12','\u0C28');
				break;
			}
			case '\u0c2a':  case '\u0c2b':  case '\u0c2c':  case '\u0c2d':
			case '\u0c2e':  case '\u0c2f':  case '\u0c30':  case '\u0c31':
			case '\u0c32':  case '\u0c33':
			{
				matchRange('\u0C2A','\u0C33');
				break;
			}
			case '\u0c35':  case '\u0c36':  case '\u0c37':  case '\u0c38':
			case '\u0c39':
			{
				matchRange('\u0C35','\u0C39');
				break;
			}
			case '\u0c60':  case '\u0c61':
			{
				matchRange('\u0C60','\u0C61');
				break;
			}
			case '\u0c85':  case '\u0c86':  case '\u0c87':  case '\u0c88':
			case '\u0c89':  case '\u0c8a':  case '\u0c8b':  case '\u0c8c':
			{
				matchRange('\u0C85','\u0C8C');
				break;
			}
			case '\u0c8e':  case '\u0c8f':  case '\u0c90':
			{
				matchRange('\u0C8E','\u0C90');
				break;
			}
			case '\u0c92':  case '\u0c93':  case '\u0c94':  case '\u0c95':
			case '\u0c96':  case '\u0c97':  case '\u0c98':  case '\u0c99':
			case '\u0c9a':  case '\u0c9b':  case '\u0c9c':  case '\u0c9d':
			case '\u0c9e':  case '\u0c9f':  case '\u0ca0':  case '\u0ca1':
			case '\u0ca2':  case '\u0ca3':  case '\u0ca4':  case '\u0ca5':
			case '\u0ca6':  case '\u0ca7':  case '\u0ca8':
			{
				matchRange('\u0C92','\u0CA8');
				break;
			}
			case '\u0caa':  case '\u0cab':  case '\u0cac':  case '\u0cad':
			case '\u0cae':  case '\u0caf':  case '\u0cb0':  case '\u0cb1':
			case '\u0cb2':  case '\u0cb3':
			{
				matchRange('\u0CAA','\u0CB3');
				break;
			}
			case '\u0cb5':  case '\u0cb6':  case '\u0cb7':  case '\u0cb8':
			case '\u0cb9':
			{
				matchRange('\u0CB5','\u0CB9');
				break;
			}
			case '\u0cde':
			{
				match('\u0CDE');
				break;
			}
			case '\u0ce0':  case '\u0ce1':
			{
				matchRange('\u0CE0','\u0CE1');
				break;
			}
			case '\u0d05':  case '\u0d06':  case '\u0d07':  case '\u0d08':
			case '\u0d09':  case '\u0d0a':  case '\u0d0b':  case '\u0d0c':
			{
				matchRange('\u0D05','\u0D0C');
				break;
			}
			case '\u0d0e':  case '\u0d0f':  case '\u0d10':
			{
				matchRange('\u0D0E','\u0D10');
				break;
			}
			case '\u0d12':  case '\u0d13':  case '\u0d14':  case '\u0d15':
			case '\u0d16':  case '\u0d17':  case '\u0d18':  case '\u0d19':
			case '\u0d1a':  case '\u0d1b':  case '\u0d1c':  case '\u0d1d':
			case '\u0d1e':  case '\u0d1f':  case '\u0d20':  case '\u0d21':
			case '\u0d22':  case '\u0d23':  case '\u0d24':  case '\u0d25':
			case '\u0d26':  case '\u0d27':  case '\u0d28':
			{
				matchRange('\u0D12','\u0D28');
				break;
			}
			case '\u0d2a':  case '\u0d2b':  case '\u0d2c':  case '\u0d2d':
			case '\u0d2e':  case '\u0d2f':  case '\u0d30':  case '\u0d31':
			case '\u0d32':  case '\u0d33':  case '\u0d34':  case '\u0d35':
			case '\u0d36':  case '\u0d37':  case '\u0d38':  case '\u0d39':
			{
				matchRange('\u0D2A','\u0D39');
				break;
			}
			case '\u0d60':  case '\u0d61':
			{
				matchRange('\u0D60','\u0D61');
				break;
			}
			case '\u0d85':  case '\u0d86':  case '\u0d87':  case '\u0d88':
			case '\u0d89':  case '\u0d8a':  case '\u0d8b':  case '\u0d8c':
			case '\u0d8d':  case '\u0d8e':  case '\u0d8f':  case '\u0d90':
			case '\u0d91':  case '\u0d92':  case '\u0d93':  case '\u0d94':
			case '\u0d95':  case '\u0d96':
			{
				matchRange('\u0D85','\u0D96');
				break;
			}
			case '\u0d9a':  case '\u0d9b':  case '\u0d9c':  case '\u0d9d':
			case '\u0d9e':  case '\u0d9f':  case '\u0da0':  case '\u0da1':
			case '\u0da2':  case '\u0da3':  case '\u0da4':  case '\u0da5':
			case '\u0da6':  case '\u0da7':  case '\u0da8':  case '\u0da9':
			case '\u0daa':  case '\u0dab':  case '\u0dac':  case '\u0dad':
			case '\u0dae':  case '\u0daf':  case '\u0db0':  case '\u0db1':
			{
				matchRange('\u0D9A','\u0DB1');
				break;
			}
			case '\u0db3':  case '\u0db4':  case '\u0db5':  case '\u0db6':
			case '\u0db7':  case '\u0db8':  case '\u0db9':  case '\u0dba':
			case '\u0dbb':
			{
				matchRange('\u0DB3','\u0DBB');
				break;
			}
			case '\u0dbd':
			{
				match('\u0DBD');
				break;
			}
			case '\u0dc0':  case '\u0dc1':  case '\u0dc2':  case '\u0dc3':
			case '\u0dc4':  case '\u0dc5':  case '\u0dc6':
			{
				matchRange('\u0DC0','\u0DC6');
				break;
			}
			case '\u0e01':  case '\u0e02':  case '\u0e03':  case '\u0e04':
			case '\u0e05':  case '\u0e06':  case '\u0e07':  case '\u0e08':
			case '\u0e09':  case '\u0e0a':  case '\u0e0b':  case '\u0e0c':
			case '\u0e0d':  case '\u0e0e':  case '\u0e0f':  case '\u0e10':
			case '\u0e11':  case '\u0e12':  case '\u0e13':  case '\u0e14':
			case '\u0e15':  case '\u0e16':  case '\u0e17':  case '\u0e18':
			case '\u0e19':  case '\u0e1a':  case '\u0e1b':  case '\u0e1c':
			case '\u0e1d':  case '\u0e1e':  case '\u0e1f':  case '\u0e20':
			case '\u0e21':  case '\u0e22':  case '\u0e23':  case '\u0e24':
			case '\u0e25':  case '\u0e26':  case '\u0e27':  case '\u0e28':
			case '\u0e29':  case '\u0e2a':  case '\u0e2b':  case '\u0e2c':
			case '\u0e2d':  case '\u0e2e':  case '\u0e2f':  case '\u0e30':
			{
				matchRange('\u0E01','\u0E30');
				break;
			}
			case '\u0e32':  case '\u0e33':
			{
				matchRange('\u0E32','\u0E33');
				break;
			}
			case '\u0e40':  case '\u0e41':  case '\u0e42':  case '\u0e43':
			case '\u0e44':  case '\u0e45':
			{
				matchRange('\u0E40','\u0E45');
				break;
			}
			case '\u0e81':  case '\u0e82':
			{
				matchRange('\u0E81','\u0E82');
				break;
			}
			case '\u0e84':
			{
				match('\u0E84');
				break;
			}
			case '\u0e87':  case '\u0e88':
			{
				matchRange('\u0E87','\u0E88');
				break;
			}
			case '\u0e8a':
			{
				match('\u0E8A');
				break;
			}
			case '\u0e8d':
			{
				match('\u0E8D');
				break;
			}
			case '\u0e94':  case '\u0e95':  case '\u0e96':  case '\u0e97':
			{
				matchRange('\u0E94','\u0E97');
				break;
			}
			case '\u0e99':  case '\u0e9a':  case '\u0e9b':  case '\u0e9c':
			case '\u0e9d':  case '\u0e9e':  case '\u0e9f':
			{
				matchRange('\u0E99','\u0E9F');
				break;
			}
			case '\u0ea1':  case '\u0ea2':  case '\u0ea3':
			{
				matchRange('\u0EA1','\u0EA3');
				break;
			}
			case '\u0ea5':
			{
				match('\u0EA5');
				break;
			}
			case '\u0ea7':
			{
				match('\u0EA7');
				break;
			}
			case '\u0eaa':  case '\u0eab':
			{
				matchRange('\u0EAA','\u0EAB');
				break;
			}
			case '\u0ead':  case '\u0eae':  case '\u0eaf':  case '\u0eb0':
			{
				matchRange('\u0EAD','\u0EB0');
				break;
			}
			case '\u0eb2':  case '\u0eb3':
			{
				matchRange('\u0EB2','\u0EB3');
				break;
			}
			case '\u0ebd':
			{
				match('\u0EBD');
				break;
			}
			case '\u0ec0':  case '\u0ec1':  case '\u0ec2':  case '\u0ec3':
			case '\u0ec4':
			{
				matchRange('\u0EC0','\u0EC4');
				break;
			}
			case '\u0edc':  case '\u0edd':
			{
				matchRange('\u0EDC','\u0EDD');
				break;
			}
			case '\u0f00':
			{
				match('\u0F00');
				break;
			}
			case '\u0f40':  case '\u0f41':  case '\u0f42':  case '\u0f43':
			case '\u0f44':  case '\u0f45':  case '\u0f46':  case '\u0f47':
			{
				matchRange('\u0F40','\u0F47');
				break;
			}
			case '\u0f49':  case '\u0f4a':  case '\u0f4b':  case '\u0f4c':
			case '\u0f4d':  case '\u0f4e':  case '\u0f4f':  case '\u0f50':
			case '\u0f51':  case '\u0f52':  case '\u0f53':  case '\u0f54':
			case '\u0f55':  case '\u0f56':  case '\u0f57':  case '\u0f58':
			case '\u0f59':  case '\u0f5a':  case '\u0f5b':  case '\u0f5c':
			case '\u0f5d':  case '\u0f5e':  case '\u0f5f':  case '\u0f60':
			case '\u0f61':  case '\u0f62':  case '\u0f63':  case '\u0f64':
			case '\u0f65':  case '\u0f66':  case '\u0f67':  case '\u0f68':
			case '\u0f69':  case '\u0f6a':
			{
				matchRange('\u0F49','\u0F6A');
				break;
			}
			case '\u0f88':  case '\u0f89':  case '\u0f8a':  case '\u0f8b':
			{
				matchRange('\u0F88','\u0F8B');
				break;
			}
			case '\u1000':  case '\u1001':  case '\u1002':  case '\u1003':
			case '\u1004':  case '\u1005':  case '\u1006':  case '\u1007':
			case '\u1008':  case '\u1009':  case '\u100a':  case '\u100b':
			case '\u100c':  case '\u100d':  case '\u100e':  case '\u100f':
			case '\u1010':  case '\u1011':  case '\u1012':  case '\u1013':
			case '\u1014':  case '\u1015':  case '\u1016':  case '\u1017':
			case '\u1018':  case '\u1019':  case '\u101a':  case '\u101b':
			case '\u101c':  case '\u101d':  case '\u101e':  case '\u101f':
			case '\u1020':  case '\u1021':
			{
				matchRange('\u1000','\u1021');
				break;
			}
			case '\u1023':  case '\u1024':  case '\u1025':  case '\u1026':
			case '\u1027':
			{
				matchRange('\u1023','\u1027');
				break;
			}
			case '\u1029':  case '\u102a':
			{
				matchRange('\u1029','\u102A');
				break;
			}
			case '\u1050':  case '\u1051':  case '\u1052':  case '\u1053':
			case '\u1054':  case '\u1055':
			{
				matchRange('\u1050','\u1055');
				break;
			}
			case '\u10d0':  case '\u10d1':  case '\u10d2':  case '\u10d3':
			case '\u10d4':  case '\u10d5':  case '\u10d6':  case '\u10d7':
			case '\u10d8':  case '\u10d9':  case '\u10da':  case '\u10db':
			case '\u10dc':  case '\u10dd':  case '\u10de':  case '\u10df':
			case '\u10e0':  case '\u10e1':  case '\u10e2':  case '\u10e3':
			case '\u10e4':  case '\u10e5':  case '\u10e6':  case '\u10e7':
			case '\u10e8':  case '\u10e9':  case '\u10ea':  case '\u10eb':
			case '\u10ec':  case '\u10ed':  case '\u10ee':  case '\u10ef':
			case '\u10f0':  case '\u10f1':  case '\u10f2':  case '\u10f3':
			case '\u10f4':  case '\u10f5':  case '\u10f6':  case '\u10f7':
			case '\u10f8':
			{
				matchRange('\u10D0','\u10F8');
				break;
			}
			case '\u1100':  case '\u1101':  case '\u1102':  case '\u1103':
			case '\u1104':  case '\u1105':  case '\u1106':  case '\u1107':
			case '\u1108':  case '\u1109':  case '\u110a':  case '\u110b':
			case '\u110c':  case '\u110d':  case '\u110e':  case '\u110f':
			case '\u1110':  case '\u1111':  case '\u1112':  case '\u1113':
			case '\u1114':  case '\u1115':  case '\u1116':  case '\u1117':
			case '\u1118':  case '\u1119':  case '\u111a':  case '\u111b':
			case '\u111c':  case '\u111d':  case '\u111e':  case '\u111f':
			case '\u1120':  case '\u1121':  case '\u1122':  case '\u1123':
			case '\u1124':  case '\u1125':  case '\u1126':  case '\u1127':
			case '\u1128':  case '\u1129':  case '\u112a':  case '\u112b':
			case '\u112c':  case '\u112d':  case '\u112e':  case '\u112f':
			case '\u1130':  case '\u1131':  case '\u1132':  case '\u1133':
			case '\u1134':  case '\u1135':  case '\u1136':  case '\u1137':
			case '\u1138':  case '\u1139':  case '\u113a':  case '\u113b':
			case '\u113c':  case '\u113d':  case '\u113e':  case '\u113f':
			case '\u1140':  case '\u1141':  case '\u1142':  case '\u1143':
			case '\u1144':  case '\u1145':  case '\u1146':  case '\u1147':
			case '\u1148':  case '\u1149':  case '\u114a':  case '\u114b':
			case '\u114c':  case '\u114d':  case '\u114e':  case '\u114f':
			case '\u1150':  case '\u1151':  case '\u1152':  case '\u1153':
			case '\u1154':  case '\u1155':  case '\u1156':  case '\u1157':
			case '\u1158':  case '\u1159':
			{
				matchRange('\u1100','\u1159');
				break;
			}
			case '\u115f':  case '\u1160':  case '\u1161':  case '\u1162':
			case '\u1163':  case '\u1164':  case '\u1165':  case '\u1166':
			case '\u1167':  case '\u1168':  case '\u1169':  case '\u116a':
			case '\u116b':  case '\u116c':  case '\u116d':  case '\u116e':
			case '\u116f':  case '\u1170':  case '\u1171':  case '\u1172':
			case '\u1173':  case '\u1174':  case '\u1175':  case '\u1176':
			case '\u1177':  case '\u1178':  case '\u1179':  case '\u117a':
			case '\u117b':  case '\u117c':  case '\u117d':  case '\u117e':
			case '\u117f':  case '\u1180':  case '\u1181':  case '\u1182':
			case '\u1183':  case '\u1184':  case '\u1185':  case '\u1186':
			case '\u1187':  case '\u1188':  case '\u1189':  case '\u118a':
			case '\u118b':  case '\u118c':  case '\u118d':  case '\u118e':
			case '\u118f':  case '\u1190':  case '\u1191':  case '\u1192':
			case '\u1193':  case '\u1194':  case '\u1195':  case '\u1196':
			case '\u1197':  case '\u1198':  case '\u1199':  case '\u119a':
			case '\u119b':  case '\u119c':  case '\u119d':  case '\u119e':
			case '\u119f':  case '\u11a0':  case '\u11a1':  case '\u11a2':
			{
				matchRange('\u115F','\u11A2');
				break;
			}
			case '\u11a8':  case '\u11a9':  case '\u11aa':  case '\u11ab':
			case '\u11ac':  case '\u11ad':  case '\u11ae':  case '\u11af':
			case '\u11b0':  case '\u11b1':  case '\u11b2':  case '\u11b3':
			case '\u11b4':  case '\u11b5':  case '\u11b6':  case '\u11b7':
			case '\u11b8':  case '\u11b9':  case '\u11ba':  case '\u11bb':
			case '\u11bc':  case '\u11bd':  case '\u11be':  case '\u11bf':
			case '\u11c0':  case '\u11c1':  case '\u11c2':  case '\u11c3':
			case '\u11c4':  case '\u11c5':  case '\u11c6':  case '\u11c7':
			case '\u11c8':  case '\u11c9':  case '\u11ca':  case '\u11cb':
			case '\u11cc':  case '\u11cd':  case '\u11ce':  case '\u11cf':
			case '\u11d0':  case '\u11d1':  case '\u11d2':  case '\u11d3':
			case '\u11d4':  case '\u11d5':  case '\u11d6':  case '\u11d7':
			case '\u11d8':  case '\u11d9':  case '\u11da':  case '\u11db':
			case '\u11dc':  case '\u11dd':  case '\u11de':  case '\u11df':
			case '\u11e0':  case '\u11e1':  case '\u11e2':  case '\u11e3':
			case '\u11e4':  case '\u11e5':  case '\u11e6':  case '\u11e7':
			case '\u11e8':  case '\u11e9':  case '\u11ea':  case '\u11eb':
			case '\u11ec':  case '\u11ed':  case '\u11ee':  case '\u11ef':
			case '\u11f0':  case '\u11f1':  case '\u11f2':  case '\u11f3':
			case '\u11f4':  case '\u11f5':  case '\u11f6':  case '\u11f7':
			case '\u11f8':  case '\u11f9':
			{
				matchRange('\u11A8','\u11F9');
				break;
			}
			case '\u1200':  case '\u1201':  case '\u1202':  case '\u1203':
			case '\u1204':  case '\u1205':  case '\u1206':
			{
				matchRange('\u1200','\u1206');
				break;
			}
			case '\u1208':  case '\u1209':  case '\u120a':  case '\u120b':
			case '\u120c':  case '\u120d':  case '\u120e':  case '\u120f':
			case '\u1210':  case '\u1211':  case '\u1212':  case '\u1213':
			case '\u1214':  case '\u1215':  case '\u1216':  case '\u1217':
			case '\u1218':  case '\u1219':  case '\u121a':  case '\u121b':
			case '\u121c':  case '\u121d':  case '\u121e':  case '\u121f':
			case '\u1220':  case '\u1221':  case '\u1222':  case '\u1223':
			case '\u1224':  case '\u1225':  case '\u1226':  case '\u1227':
			case '\u1228':  case '\u1229':  case '\u122a':  case '\u122b':
			case '\u122c':  case '\u122d':  case '\u122e':  case '\u122f':
			case '\u1230':  case '\u1231':  case '\u1232':  case '\u1233':
			case '\u1234':  case '\u1235':  case '\u1236':  case '\u1237':
			case '\u1238':  case '\u1239':  case '\u123a':  case '\u123b':
			case '\u123c':  case '\u123d':  case '\u123e':  case '\u123f':
			case '\u1240':  case '\u1241':  case '\u1242':  case '\u1243':
			case '\u1244':  case '\u1245':  case '\u1246':
			{
				matchRange('\u1208','\u1246');
				break;
			}
			case '\u1248':
			{
				match('\u1248');
				break;
			}
			case '\u124a':  case '\u124b':  case '\u124c':  case '\u124d':
			{
				matchRange('\u124A','\u124D');
				break;
			}
			case '\u1250':  case '\u1251':  case '\u1252':  case '\u1253':
			case '\u1254':  case '\u1255':  case '\u1256':
			{
				matchRange('\u1250','\u1256');
				break;
			}
			case '\u1258':
			{
				match('\u1258');
				break;
			}
			case '\u125a':  case '\u125b':  case '\u125c':  case '\u125d':
			{
				matchRange('\u125A','\u125D');
				break;
			}
			case '\u1260':  case '\u1261':  case '\u1262':  case '\u1263':
			case '\u1264':  case '\u1265':  case '\u1266':  case '\u1267':
			case '\u1268':  case '\u1269':  case '\u126a':  case '\u126b':
			case '\u126c':  case '\u126d':  case '\u126e':  case '\u126f':
			case '\u1270':  case '\u1271':  case '\u1272':  case '\u1273':
			case '\u1274':  case '\u1275':  case '\u1276':  case '\u1277':
			case '\u1278':  case '\u1279':  case '\u127a':  case '\u127b':
			case '\u127c':  case '\u127d':  case '\u127e':  case '\u127f':
			case '\u1280':  case '\u1281':  case '\u1282':  case '\u1283':
			case '\u1284':  case '\u1285':  case '\u1286':
			{
				matchRange('\u1260','\u1286');
				break;
			}
			case '\u1288':
			{
				match('\u1288');
				break;
			}
			case '\u128a':  case '\u128b':  case '\u128c':  case '\u128d':
			{
				matchRange('\u128A','\u128D');
				break;
			}
			case '\u1290':  case '\u1291':  case '\u1292':  case '\u1293':
			case '\u1294':  case '\u1295':  case '\u1296':  case '\u1297':
			case '\u1298':  case '\u1299':  case '\u129a':  case '\u129b':
			case '\u129c':  case '\u129d':  case '\u129e':  case '\u129f':
			case '\u12a0':  case '\u12a1':  case '\u12a2':  case '\u12a3':
			case '\u12a4':  case '\u12a5':  case '\u12a6':  case '\u12a7':
			case '\u12a8':  case '\u12a9':  case '\u12aa':  case '\u12ab':
			case '\u12ac':  case '\u12ad':  case '\u12ae':
			{
				matchRange('\u1290','\u12AE');
				break;
			}
			case '\u12b0':
			{
				match('\u12B0');
				break;
			}
			case '\u12b2':  case '\u12b3':  case '\u12b4':  case '\u12b5':
			{
				matchRange('\u12B2','\u12B5');
				break;
			}
			case '\u12b8':  case '\u12b9':  case '\u12ba':  case '\u12bb':
			case '\u12bc':  case '\u12bd':  case '\u12be':
			{
				matchRange('\u12B8','\u12BE');
				break;
			}
			case '\u12c0':
			{
				match('\u12C0');
				break;
			}
			case '\u12c2':  case '\u12c3':  case '\u12c4':  case '\u12c5':
			{
				matchRange('\u12C2','\u12C5');
				break;
			}
			case '\u12c8':  case '\u12c9':  case '\u12ca':  case '\u12cb':
			case '\u12cc':  case '\u12cd':  case '\u12ce':
			{
				matchRange('\u12C8','\u12CE');
				break;
			}
			case '\u12d0':  case '\u12d1':  case '\u12d2':  case '\u12d3':
			case '\u12d4':  case '\u12d5':  case '\u12d6':
			{
				matchRange('\u12D0','\u12D6');
				break;
			}
			case '\u12d8':  case '\u12d9':  case '\u12da':  case '\u12db':
			case '\u12dc':  case '\u12dd':  case '\u12de':  case '\u12df':
			case '\u12e0':  case '\u12e1':  case '\u12e2':  case '\u12e3':
			case '\u12e4':  case '\u12e5':  case '\u12e6':  case '\u12e7':
			case '\u12e8':  case '\u12e9':  case '\u12ea':  case '\u12eb':
			case '\u12ec':  case '\u12ed':  case '\u12ee':
			{
				matchRange('\u12D8','\u12EE');
				break;
			}
			case '\u12f0':  case '\u12f1':  case '\u12f2':  case '\u12f3':
			case '\u12f4':  case '\u12f5':  case '\u12f6':  case '\u12f7':
			case '\u12f8':  case '\u12f9':  case '\u12fa':  case '\u12fb':
			case '\u12fc':  case '\u12fd':  case '\u12fe':  case '\u12ff':
			case '\u1300':  case '\u1301':  case '\u1302':  case '\u1303':
			case '\u1304':  case '\u1305':  case '\u1306':  case '\u1307':
			case '\u1308':  case '\u1309':  case '\u130a':  case '\u130b':
			case '\u130c':  case '\u130d':  case '\u130e':
			{
				matchRange('\u12F0','\u130E');
				break;
			}
			case '\u1310':
			{
				match('\u1310');
				break;
			}
			case '\u1312':  case '\u1313':  case '\u1314':  case '\u1315':
			{
				matchRange('\u1312','\u1315');
				break;
			}
			case '\u1318':  case '\u1319':  case '\u131a':  case '\u131b':
			case '\u131c':  case '\u131d':  case '\u131e':
			{
				matchRange('\u1318','\u131E');
				break;
			}
			case '\u1320':  case '\u1321':  case '\u1322':  case '\u1323':
			case '\u1324':  case '\u1325':  case '\u1326':  case '\u1327':
			case '\u1328':  case '\u1329':  case '\u132a':  case '\u132b':
			case '\u132c':  case '\u132d':  case '\u132e':  case '\u132f':
			case '\u1330':  case '\u1331':  case '\u1332':  case '\u1333':
			case '\u1334':  case '\u1335':  case '\u1336':  case '\u1337':
			case '\u1338':  case '\u1339':  case '\u133a':  case '\u133b':
			case '\u133c':  case '\u133d':  case '\u133e':  case '\u133f':
			case '\u1340':  case '\u1341':  case '\u1342':  case '\u1343':
			case '\u1344':  case '\u1345':  case '\u1346':
			{
				matchRange('\u1320','\u1346');
				break;
			}
			case '\u1348':  case '\u1349':  case '\u134a':  case '\u134b':
			case '\u134c':  case '\u134d':  case '\u134e':  case '\u134f':
			case '\u1350':  case '\u1351':  case '\u1352':  case '\u1353':
			case '\u1354':  case '\u1355':  case '\u1356':  case '\u1357':
			case '\u1358':  case '\u1359':  case '\u135a':
			{
				matchRange('\u1348','\u135A');
				break;
			}
			case '\u13a0':  case '\u13a1':  case '\u13a2':  case '\u13a3':
			case '\u13a4':  case '\u13a5':  case '\u13a6':  case '\u13a7':
			case '\u13a8':  case '\u13a9':  case '\u13aa':  case '\u13ab':
			case '\u13ac':  case '\u13ad':  case '\u13ae':  case '\u13af':
			case '\u13b0':  case '\u13b1':  case '\u13b2':  case '\u13b3':
			case '\u13b4':  case '\u13b5':  case '\u13b6':  case '\u13b7':
			case '\u13b8':  case '\u13b9':  case '\u13ba':  case '\u13bb':
			case '\u13bc':  case '\u13bd':  case '\u13be':  case '\u13bf':
			case '\u13c0':  case '\u13c1':  case '\u13c2':  case '\u13c3':
			case '\u13c4':  case '\u13c5':  case '\u13c6':  case '\u13c7':
			case '\u13c8':  case '\u13c9':  case '\u13ca':  case '\u13cb':
			case '\u13cc':  case '\u13cd':  case '\u13ce':  case '\u13cf':
			case '\u13d0':  case '\u13d1':  case '\u13d2':  case '\u13d3':
			case '\u13d4':  case '\u13d5':  case '\u13d6':  case '\u13d7':
			case '\u13d8':  case '\u13d9':  case '\u13da':  case '\u13db':
			case '\u13dc':  case '\u13dd':  case '\u13de':  case '\u13df':
			case '\u13e0':  case '\u13e1':  case '\u13e2':  case '\u13e3':
			case '\u13e4':  case '\u13e5':  case '\u13e6':  case '\u13e7':
			case '\u13e8':  case '\u13e9':  case '\u13ea':  case '\u13eb':
			case '\u13ec':  case '\u13ed':  case '\u13ee':  case '\u13ef':
			case '\u13f0':  case '\u13f1':  case '\u13f2':  case '\u13f3':
			case '\u13f4':
			{
				matchRange('\u13A0','\u13F4');
				break;
			}
			case '\u166f':  case '\u1670':  case '\u1671':  case '\u1672':
			case '\u1673':  case '\u1674':  case '\u1675':  case '\u1676':
			{
				matchRange('\u166F','\u1676');
				break;
			}
			case '\u1681':  case '\u1682':  case '\u1683':  case '\u1684':
			case '\u1685':  case '\u1686':  case '\u1687':  case '\u1688':
			case '\u1689':  case '\u168a':  case '\u168b':  case '\u168c':
			case '\u168d':  case '\u168e':  case '\u168f':  case '\u1690':
			case '\u1691':  case '\u1692':  case '\u1693':  case '\u1694':
			case '\u1695':  case '\u1696':  case '\u1697':  case '\u1698':
			case '\u1699':  case '\u169a':
			{
				matchRange('\u1681','\u169A');
				break;
			}
			case '\u16a0':  case '\u16a1':  case '\u16a2':  case '\u16a3':
			case '\u16a4':  case '\u16a5':  case '\u16a6':  case '\u16a7':
			case '\u16a8':  case '\u16a9':  case '\u16aa':  case '\u16ab':
			case '\u16ac':  case '\u16ad':  case '\u16ae':  case '\u16af':
			case '\u16b0':  case '\u16b1':  case '\u16b2':  case '\u16b3':
			case '\u16b4':  case '\u16b5':  case '\u16b6':  case '\u16b7':
			case '\u16b8':  case '\u16b9':  case '\u16ba':  case '\u16bb':
			case '\u16bc':  case '\u16bd':  case '\u16be':  case '\u16bf':
			case '\u16c0':  case '\u16c1':  case '\u16c2':  case '\u16c3':
			case '\u16c4':  case '\u16c5':  case '\u16c6':  case '\u16c7':
			case '\u16c8':  case '\u16c9':  case '\u16ca':  case '\u16cb':
			case '\u16cc':  case '\u16cd':  case '\u16ce':  case '\u16cf':
			case '\u16d0':  case '\u16d1':  case '\u16d2':  case '\u16d3':
			case '\u16d4':  case '\u16d5':  case '\u16d6':  case '\u16d7':
			case '\u16d8':  case '\u16d9':  case '\u16da':  case '\u16db':
			case '\u16dc':  case '\u16dd':  case '\u16de':  case '\u16df':
			case '\u16e0':  case '\u16e1':  case '\u16e2':  case '\u16e3':
			case '\u16e4':  case '\u16e5':  case '\u16e6':  case '\u16e7':
			case '\u16e8':  case '\u16e9':  case '\u16ea':
			{
				matchRange('\u16A0','\u16EA');
				break;
			}
			case '\u1700':  case '\u1701':  case '\u1702':  case '\u1703':
			case '\u1704':  case '\u1705':  case '\u1706':  case '\u1707':
			case '\u1708':  case '\u1709':  case '\u170a':  case '\u170b':
			case '\u170c':
			{
				matchRange('\u1700','\u170C');
				break;
			}
			case '\u170e':  case '\u170f':  case '\u1710':  case '\u1711':
			{
				matchRange('\u170E','\u1711');
				break;
			}
			case '\u1720':  case '\u1721':  case '\u1722':  case '\u1723':
			case '\u1724':  case '\u1725':  case '\u1726':  case '\u1727':
			case '\u1728':  case '\u1729':  case '\u172a':  case '\u172b':
			case '\u172c':  case '\u172d':  case '\u172e':  case '\u172f':
			case '\u1730':  case '\u1731':
			{
				matchRange('\u1720','\u1731');
				break;
			}
			case '\u1740':  case '\u1741':  case '\u1742':  case '\u1743':
			case '\u1744':  case '\u1745':  case '\u1746':  case '\u1747':
			case '\u1748':  case '\u1749':  case '\u174a':  case '\u174b':
			case '\u174c':  case '\u174d':  case '\u174e':  case '\u174f':
			case '\u1750':  case '\u1751':
			{
				matchRange('\u1740','\u1751');
				break;
			}
			case '\u1760':  case '\u1761':  case '\u1762':  case '\u1763':
			case '\u1764':  case '\u1765':  case '\u1766':  case '\u1767':
			case '\u1768':  case '\u1769':  case '\u176a':  case '\u176b':
			case '\u176c':
			{
				matchRange('\u1760','\u176C');
				break;
			}
			case '\u176e':  case '\u176f':  case '\u1770':
			{
				matchRange('\u176E','\u1770');
				break;
			}
			case '\u1780':  case '\u1781':  case '\u1782':  case '\u1783':
			case '\u1784':  case '\u1785':  case '\u1786':  case '\u1787':
			case '\u1788':  case '\u1789':  case '\u178a':  case '\u178b':
			case '\u178c':  case '\u178d':  case '\u178e':  case '\u178f':
			case '\u1790':  case '\u1791':  case '\u1792':  case '\u1793':
			case '\u1794':  case '\u1795':  case '\u1796':  case '\u1797':
			case '\u1798':  case '\u1799':  case '\u179a':  case '\u179b':
			case '\u179c':  case '\u179d':  case '\u179e':  case '\u179f':
			case '\u17a0':  case '\u17a1':  case '\u17a2':  case '\u17a3':
			case '\u17a4':  case '\u17a5':  case '\u17a6':  case '\u17a7':
			case '\u17a8':  case '\u17a9':  case '\u17aa':  case '\u17ab':
			case '\u17ac':  case '\u17ad':  case '\u17ae':  case '\u17af':
			case '\u17b0':  case '\u17b1':  case '\u17b2':  case '\u17b3':
			{
				matchRange('\u1780','\u17B3');
				break;
			}
			case '\u17dc':
			{
				match('\u17DC');
				break;
			}
			case '\u1820':  case '\u1821':  case '\u1822':  case '\u1823':
			case '\u1824':  case '\u1825':  case '\u1826':  case '\u1827':
			case '\u1828':  case '\u1829':  case '\u182a':  case '\u182b':
			case '\u182c':  case '\u182d':  case '\u182e':  case '\u182f':
			case '\u1830':  case '\u1831':  case '\u1832':  case '\u1833':
			case '\u1834':  case '\u1835':  case '\u1836':  case '\u1837':
			case '\u1838':  case '\u1839':  case '\u183a':  case '\u183b':
			case '\u183c':  case '\u183d':  case '\u183e':  case '\u183f':
			case '\u1840':  case '\u1841':  case '\u1842':
			{
				matchRange('\u1820','\u1842');
				break;
			}
			case '\u1844':  case '\u1845':  case '\u1846':  case '\u1847':
			case '\u1848':  case '\u1849':  case '\u184a':  case '\u184b':
			case '\u184c':  case '\u184d':  case '\u184e':  case '\u184f':
			case '\u1850':  case '\u1851':  case '\u1852':  case '\u1853':
			case '\u1854':  case '\u1855':  case '\u1856':  case '\u1857':
			case '\u1858':  case '\u1859':  case '\u185a':  case '\u185b':
			case '\u185c':  case '\u185d':  case '\u185e':  case '\u185f':
			case '\u1860':  case '\u1861':  case '\u1862':  case '\u1863':
			case '\u1864':  case '\u1865':  case '\u1866':  case '\u1867':
			case '\u1868':  case '\u1869':  case '\u186a':  case '\u186b':
			case '\u186c':  case '\u186d':  case '\u186e':  case '\u186f':
			case '\u1870':  case '\u1871':  case '\u1872':  case '\u1873':
			case '\u1874':  case '\u1875':  case '\u1876':  case '\u1877':
			{
				matchRange('\u1844','\u1877');
				break;
			}
			case '\u1880':  case '\u1881':  case '\u1882':  case '\u1883':
			case '\u1884':  case '\u1885':  case '\u1886':  case '\u1887':
			case '\u1888':  case '\u1889':  case '\u188a':  case '\u188b':
			case '\u188c':  case '\u188d':  case '\u188e':  case '\u188f':
			case '\u1890':  case '\u1891':  case '\u1892':  case '\u1893':
			case '\u1894':  case '\u1895':  case '\u1896':  case '\u1897':
			case '\u1898':  case '\u1899':  case '\u189a':  case '\u189b':
			case '\u189c':  case '\u189d':  case '\u189e':  case '\u189f':
			case '\u18a0':  case '\u18a1':  case '\u18a2':  case '\u18a3':
			case '\u18a4':  case '\u18a5':  case '\u18a6':  case '\u18a7':
			case '\u18a8':
			{
				matchRange('\u1880','\u18A8');
				break;
			}
			case '\u2135':  case '\u2136':  case '\u2137':  case '\u2138':
			{
				matchRange('\u2135','\u2138');
				break;
			}
			case '\u3006':
			{
				match('\u3006');
				break;
			}
			case '\u303c':
			{
				match('\u303C');
				break;
			}
			case '\u3041':  case '\u3042':  case '\u3043':  case '\u3044':
			case '\u3045':  case '\u3046':  case '\u3047':  case '\u3048':
			case '\u3049':  case '\u304a':  case '\u304b':  case '\u304c':
			case '\u304d':  case '\u304e':  case '\u304f':  case '\u3050':
			case '\u3051':  case '\u3052':  case '\u3053':  case '\u3054':
			case '\u3055':  case '\u3056':  case '\u3057':  case '\u3058':
			case '\u3059':  case '\u305a':  case '\u305b':  case '\u305c':
			case '\u305d':  case '\u305e':  case '\u305f':  case '\u3060':
			case '\u3061':  case '\u3062':  case '\u3063':  case '\u3064':
			case '\u3065':  case '\u3066':  case '\u3067':  case '\u3068':
			case '\u3069':  case '\u306a':  case '\u306b':  case '\u306c':
			case '\u306d':  case '\u306e':  case '\u306f':  case '\u3070':
			case '\u3071':  case '\u3072':  case '\u3073':  case '\u3074':
			case '\u3075':  case '\u3076':  case '\u3077':  case '\u3078':
			case '\u3079':  case '\u307a':  case '\u307b':  case '\u307c':
			case '\u307d':  case '\u307e':  case '\u307f':  case '\u3080':
			case '\u3081':  case '\u3082':  case '\u3083':  case '\u3084':
			case '\u3085':  case '\u3086':  case '\u3087':  case '\u3088':
			case '\u3089':  case '\u308a':  case '\u308b':  case '\u308c':
			case '\u308d':  case '\u308e':  case '\u308f':  case '\u3090':
			case '\u3091':  case '\u3092':  case '\u3093':  case '\u3094':
			case '\u3095':  case '\u3096':
			{
				matchRange('\u3041','\u3096');
				break;
			}
			case '\u309f':
			{
				match('\u309F');
				break;
			}
			case '\u30a1':  case '\u30a2':  case '\u30a3':  case '\u30a4':
			case '\u30a5':  case '\u30a6':  case '\u30a7':  case '\u30a8':
			case '\u30a9':  case '\u30aa':  case '\u30ab':  case '\u30ac':
			case '\u30ad':  case '\u30ae':  case '\u30af':  case '\u30b0':
			case '\u30b1':  case '\u30b2':  case '\u30b3':  case '\u30b4':
			case '\u30b5':  case '\u30b6':  case '\u30b7':  case '\u30b8':
			case '\u30b9':  case '\u30ba':  case '\u30bb':  case '\u30bc':
			case '\u30bd':  case '\u30be':  case '\u30bf':  case '\u30c0':
			case '\u30c1':  case '\u30c2':  case '\u30c3':  case '\u30c4':
			case '\u30c5':  case '\u30c6':  case '\u30c7':  case '\u30c8':
			case '\u30c9':  case '\u30ca':  case '\u30cb':  case '\u30cc':
			case '\u30cd':  case '\u30ce':  case '\u30cf':  case '\u30d0':
			case '\u30d1':  case '\u30d2':  case '\u30d3':  case '\u30d4':
			case '\u30d5':  case '\u30d6':  case '\u30d7':  case '\u30d8':
			case '\u30d9':  case '\u30da':  case '\u30db':  case '\u30dc':
			case '\u30dd':  case '\u30de':  case '\u30df':  case '\u30e0':
			case '\u30e1':  case '\u30e2':  case '\u30e3':  case '\u30e4':
			case '\u30e5':  case '\u30e6':  case '\u30e7':  case '\u30e8':
			case '\u30e9':  case '\u30ea':  case '\u30eb':  case '\u30ec':
			case '\u30ed':  case '\u30ee':  case '\u30ef':  case '\u30f0':
			case '\u30f1':  case '\u30f2':  case '\u30f3':  case '\u30f4':
			case '\u30f5':  case '\u30f6':  case '\u30f7':  case '\u30f8':
			case '\u30f9':  case '\u30fa':
			{
				matchRange('\u30A1','\u30FA');
				break;
			}
			case '\u30ff':
			{
				match('\u30FF');
				break;
			}
			case '\u3105':  case '\u3106':  case '\u3107':  case '\u3108':
			case '\u3109':  case '\u310a':  case '\u310b':  case '\u310c':
			case '\u310d':  case '\u310e':  case '\u310f':  case '\u3110':
			case '\u3111':  case '\u3112':  case '\u3113':  case '\u3114':
			case '\u3115':  case '\u3116':  case '\u3117':  case '\u3118':
			case '\u3119':  case '\u311a':  case '\u311b':  case '\u311c':
			case '\u311d':  case '\u311e':  case '\u311f':  case '\u3120':
			case '\u3121':  case '\u3122':  case '\u3123':  case '\u3124':
			case '\u3125':  case '\u3126':  case '\u3127':  case '\u3128':
			case '\u3129':  case '\u312a':  case '\u312b':  case '\u312c':
			{
				matchRange('\u3105','\u312C');
				break;
			}
			case '\u3131':  case '\u3132':  case '\u3133':  case '\u3134':
			case '\u3135':  case '\u3136':  case '\u3137':  case '\u3138':
			case '\u3139':  case '\u313a':  case '\u313b':  case '\u313c':
			case '\u313d':  case '\u313e':  case '\u313f':  case '\u3140':
			case '\u3141':  case '\u3142':  case '\u3143':  case '\u3144':
			case '\u3145':  case '\u3146':  case '\u3147':  case '\u3148':
			case '\u3149':  case '\u314a':  case '\u314b':  case '\u314c':
			case '\u314d':  case '\u314e':  case '\u314f':  case '\u3150':
			case '\u3151':  case '\u3152':  case '\u3153':  case '\u3154':
			case '\u3155':  case '\u3156':  case '\u3157':  case '\u3158':
			case '\u3159':  case '\u315a':  case '\u315b':  case '\u315c':
			case '\u315d':  case '\u315e':  case '\u315f':  case '\u3160':
			case '\u3161':  case '\u3162':  case '\u3163':  case '\u3164':
			case '\u3165':  case '\u3166':  case '\u3167':  case '\u3168':
			case '\u3169':  case '\u316a':  case '\u316b':  case '\u316c':
			case '\u316d':  case '\u316e':  case '\u316f':  case '\u3170':
			case '\u3171':  case '\u3172':  case '\u3173':  case '\u3174':
			case '\u3175':  case '\u3176':  case '\u3177':  case '\u3178':
			case '\u3179':  case '\u317a':  case '\u317b':  case '\u317c':
			case '\u317d':  case '\u317e':  case '\u317f':  case '\u3180':
			case '\u3181':  case '\u3182':  case '\u3183':  case '\u3184':
			case '\u3185':  case '\u3186':  case '\u3187':  case '\u3188':
			case '\u3189':  case '\u318a':  case '\u318b':  case '\u318c':
			case '\u318d':  case '\u318e':
			{
				matchRange('\u3131','\u318E');
				break;
			}
			case '\u31a0':  case '\u31a1':  case '\u31a2':  case '\u31a3':
			case '\u31a4':  case '\u31a5':  case '\u31a6':  case '\u31a7':
			case '\u31a8':  case '\u31a9':  case '\u31aa':  case '\u31ab':
			case '\u31ac':  case '\u31ad':  case '\u31ae':  case '\u31af':
			case '\u31b0':  case '\u31b1':  case '\u31b2':  case '\u31b3':
			case '\u31b4':  case '\u31b5':  case '\u31b6':  case '\u31b7':
			{
				matchRange('\u31A0','\u31B7');
				break;
			}
			case '\u31f0':  case '\u31f1':  case '\u31f2':  case '\u31f3':
			case '\u31f4':  case '\u31f5':  case '\u31f6':  case '\u31f7':
			case '\u31f8':  case '\u31f9':  case '\u31fa':  case '\u31fb':
			case '\u31fc':  case '\u31fd':  case '\u31fe':  case '\u31ff':
			{
				matchRange('\u31F0','\u31FF');
				break;
			}
			case '\u3400':
			{
				match('\u3400');
				break;
			}
			case '\u4db5':
			{
				match('\u4DB5');
				break;
			}
			case '\u4e00':
			{
				match('\u4E00');
				break;
			}
			case '\u9fa5':
			{
				match('\u9FA5');
				break;
			}
			case '\uac00':
			{
				match('\uAC00');
				break;
			}
			case '\ud7a3':
			{
				match('\uD7A3');
				break;
			}
			case '\ufa30':  case '\ufa31':  case '\ufa32':  case '\ufa33':
			case '\ufa34':  case '\ufa35':  case '\ufa36':  case '\ufa37':
			case '\ufa38':  case '\ufa39':  case '\ufa3a':  case '\ufa3b':
			case '\ufa3c':  case '\ufa3d':  case '\ufa3e':  case '\ufa3f':
			case '\ufa40':  case '\ufa41':  case '\ufa42':  case '\ufa43':
			case '\ufa44':  case '\ufa45':  case '\ufa46':  case '\ufa47':
			case '\ufa48':  case '\ufa49':  case '\ufa4a':  case '\ufa4b':
			case '\ufa4c':  case '\ufa4d':  case '\ufa4e':  case '\ufa4f':
			case '\ufa50':  case '\ufa51':  case '\ufa52':  case '\ufa53':
			case '\ufa54':  case '\ufa55':  case '\ufa56':  case '\ufa57':
			case '\ufa58':  case '\ufa59':  case '\ufa5a':  case '\ufa5b':
			case '\ufa5c':  case '\ufa5d':  case '\ufa5e':  case '\ufa5f':
			case '\ufa60':  case '\ufa61':  case '\ufa62':  case '\ufa63':
			case '\ufa64':  case '\ufa65':  case '\ufa66':  case '\ufa67':
			case '\ufa68':  case '\ufa69':  case '\ufa6a':
			{
				matchRange('\uFA30','\uFA6A');
				break;
			}
			case '\ufb1d':
			{
				match('\uFB1D');
				break;
			}
			case '\ufb1f':  case '\ufb20':  case '\ufb21':  case '\ufb22':
			case '\ufb23':  case '\ufb24':  case '\ufb25':  case '\ufb26':
			case '\ufb27':  case '\ufb28':
			{
				matchRange('\uFB1F','\uFB28');
				break;
			}
			case '\ufb2a':  case '\ufb2b':  case '\ufb2c':  case '\ufb2d':
			case '\ufb2e':  case '\ufb2f':  case '\ufb30':  case '\ufb31':
			case '\ufb32':  case '\ufb33':  case '\ufb34':  case '\ufb35':
			case '\ufb36':
			{
				matchRange('\uFB2A','\uFB36');
				break;
			}
			case '\ufb38':  case '\ufb39':  case '\ufb3a':  case '\ufb3b':
			case '\ufb3c':
			{
				matchRange('\uFB38','\uFB3C');
				break;
			}
			case '\ufb3e':
			{
				match('\uFB3E');
				break;
			}
			case '\ufb40':  case '\ufb41':
			{
				matchRange('\uFB40','\uFB41');
				break;
			}
			case '\ufb43':  case '\ufb44':
			{
				matchRange('\uFB43','\uFB44');
				break;
			}
			case '\ufb46':  case '\ufb47':  case '\ufb48':  case '\ufb49':
			case '\ufb4a':  case '\ufb4b':  case '\ufb4c':  case '\ufb4d':
			case '\ufb4e':  case '\ufb4f':  case '\ufb50':  case '\ufb51':
			case '\ufb52':  case '\ufb53':  case '\ufb54':  case '\ufb55':
			case '\ufb56':  case '\ufb57':  case '\ufb58':  case '\ufb59':
			case '\ufb5a':  case '\ufb5b':  case '\ufb5c':  case '\ufb5d':
			case '\ufb5e':  case '\ufb5f':  case '\ufb60':  case '\ufb61':
			case '\ufb62':  case '\ufb63':  case '\ufb64':  case '\ufb65':
			case '\ufb66':  case '\ufb67':  case '\ufb68':  case '\ufb69':
			case '\ufb6a':  case '\ufb6b':  case '\ufb6c':  case '\ufb6d':
			case '\ufb6e':  case '\ufb6f':  case '\ufb70':  case '\ufb71':
			case '\ufb72':  case '\ufb73':  case '\ufb74':  case '\ufb75':
			case '\ufb76':  case '\ufb77':  case '\ufb78':  case '\ufb79':
			case '\ufb7a':  case '\ufb7b':  case '\ufb7c':  case '\ufb7d':
			case '\ufb7e':  case '\ufb7f':  case '\ufb80':  case '\ufb81':
			case '\ufb82':  case '\ufb83':  case '\ufb84':  case '\ufb85':
			case '\ufb86':  case '\ufb87':  case '\ufb88':  case '\ufb89':
			case '\ufb8a':  case '\ufb8b':  case '\ufb8c':  case '\ufb8d':
			case '\ufb8e':  case '\ufb8f':  case '\ufb90':  case '\ufb91':
			case '\ufb92':  case '\ufb93':  case '\ufb94':  case '\ufb95':
			case '\ufb96':  case '\ufb97':  case '\ufb98':  case '\ufb99':
			case '\ufb9a':  case '\ufb9b':  case '\ufb9c':  case '\ufb9d':
			case '\ufb9e':  case '\ufb9f':  case '\ufba0':  case '\ufba1':
			case '\ufba2':  case '\ufba3':  case '\ufba4':  case '\ufba5':
			case '\ufba6':  case '\ufba7':  case '\ufba8':  case '\ufba9':
			case '\ufbaa':  case '\ufbab':  case '\ufbac':  case '\ufbad':
			case '\ufbae':  case '\ufbaf':  case '\ufbb0':  case '\ufbb1':
			{
				matchRange('\uFB46','\uFBB1');
				break;
			}
			case '\ufd50':  case '\ufd51':  case '\ufd52':  case '\ufd53':
			case '\ufd54':  case '\ufd55':  case '\ufd56':  case '\ufd57':
			case '\ufd58':  case '\ufd59':  case '\ufd5a':  case '\ufd5b':
			case '\ufd5c':  case '\ufd5d':  case '\ufd5e':  case '\ufd5f':
			case '\ufd60':  case '\ufd61':  case '\ufd62':  case '\ufd63':
			case '\ufd64':  case '\ufd65':  case '\ufd66':  case '\ufd67':
			case '\ufd68':  case '\ufd69':  case '\ufd6a':  case '\ufd6b':
			case '\ufd6c':  case '\ufd6d':  case '\ufd6e':  case '\ufd6f':
			case '\ufd70':  case '\ufd71':  case '\ufd72':  case '\ufd73':
			case '\ufd74':  case '\ufd75':  case '\ufd76':  case '\ufd77':
			case '\ufd78':  case '\ufd79':  case '\ufd7a':  case '\ufd7b':
			case '\ufd7c':  case '\ufd7d':  case '\ufd7e':  case '\ufd7f':
			case '\ufd80':  case '\ufd81':  case '\ufd82':  case '\ufd83':
			case '\ufd84':  case '\ufd85':  case '\ufd86':  case '\ufd87':
			case '\ufd88':  case '\ufd89':  case '\ufd8a':  case '\ufd8b':
			case '\ufd8c':  case '\ufd8d':  case '\ufd8e':  case '\ufd8f':
			{
				matchRange('\uFD50','\uFD8F');
				break;
			}
			case '\ufd92':  case '\ufd93':  case '\ufd94':  case '\ufd95':
			case '\ufd96':  case '\ufd97':  case '\ufd98':  case '\ufd99':
			case '\ufd9a':  case '\ufd9b':  case '\ufd9c':  case '\ufd9d':
			case '\ufd9e':  case '\ufd9f':  case '\ufda0':  case '\ufda1':
			case '\ufda2':  case '\ufda3':  case '\ufda4':  case '\ufda5':
			case '\ufda6':  case '\ufda7':  case '\ufda8':  case '\ufda9':
			case '\ufdaa':  case '\ufdab':  case '\ufdac':  case '\ufdad':
			case '\ufdae':  case '\ufdaf':  case '\ufdb0':  case '\ufdb1':
			case '\ufdb2':  case '\ufdb3':  case '\ufdb4':  case '\ufdb5':
			case '\ufdb6':  case '\ufdb7':  case '\ufdb8':  case '\ufdb9':
			case '\ufdba':  case '\ufdbb':  case '\ufdbc':  case '\ufdbd':
			case '\ufdbe':  case '\ufdbf':  case '\ufdc0':  case '\ufdc1':
			case '\ufdc2':  case '\ufdc3':  case '\ufdc4':  case '\ufdc5':
			case '\ufdc6':  case '\ufdc7':
			{
				matchRange('\uFD92','\uFDC7');
				break;
			}
			case '\ufdf0':  case '\ufdf1':  case '\ufdf2':  case '\ufdf3':
			case '\ufdf4':  case '\ufdf5':  case '\ufdf6':  case '\ufdf7':
			case '\ufdf8':  case '\ufdf9':  case '\ufdfa':  case '\ufdfb':
			{
				matchRange('\uFDF0','\uFDFB');
				break;
			}
			case '\ufe70':  case '\ufe71':  case '\ufe72':  case '\ufe73':
			case '\ufe74':
			{
				matchRange('\uFE70','\uFE74');
				break;
			}
			case '\uff66':  case '\uff67':  case '\uff68':  case '\uff69':
			case '\uff6a':  case '\uff6b':  case '\uff6c':  case '\uff6d':
			case '\uff6e':  case '\uff6f':
			{
				matchRange('\uFF66','\uFF6F');
				break;
			}
			case '\uff71':  case '\uff72':  case '\uff73':  case '\uff74':
			case '\uff75':  case '\uff76':  case '\uff77':  case '\uff78':
			case '\uff79':  case '\uff7a':  case '\uff7b':  case '\uff7c':
			case '\uff7d':  case '\uff7e':  case '\uff7f':  case '\uff80':
			case '\uff81':  case '\uff82':  case '\uff83':  case '\uff84':
			case '\uff85':  case '\uff86':  case '\uff87':  case '\uff88':
			case '\uff89':  case '\uff8a':  case '\uff8b':  case '\uff8c':
			case '\uff8d':  case '\uff8e':  case '\uff8f':  case '\uff90':
			case '\uff91':  case '\uff92':  case '\uff93':  case '\uff94':
			case '\uff95':  case '\uff96':  case '\uff97':  case '\uff98':
			case '\uff99':  case '\uff9a':  case '\uff9b':  case '\uff9c':
			case '\uff9d':
			{
				matchRange('\uFF71','\uFF9D');
				break;
			}
			case '\uffa0':  case '\uffa1':  case '\uffa2':  case '\uffa3':
			case '\uffa4':  case '\uffa5':  case '\uffa6':  case '\uffa7':
			case '\uffa8':  case '\uffa9':  case '\uffaa':  case '\uffab':
			case '\uffac':  case '\uffad':  case '\uffae':  case '\uffaf':
			case '\uffb0':  case '\uffb1':  case '\uffb2':  case '\uffb3':
			case '\uffb4':  case '\uffb5':  case '\uffb6':  case '\uffb7':
			case '\uffb8':  case '\uffb9':  case '\uffba':  case '\uffbb':
			case '\uffbc':  case '\uffbd':  case '\uffbe':
			{
				matchRange('\uFFA0','\uFFBE');
				break;
			}
			case '\uffc2':  case '\uffc3':  case '\uffc4':  case '\uffc5':
			case '\uffc6':  case '\uffc7':
			{
				matchRange('\uFFC2','\uFFC7');
				break;
			}
			case '\uffca':  case '\uffcb':  case '\uffcc':  case '\uffcd':
			case '\uffce':  case '\uffcf':
			{
				matchRange('\uFFCA','\uFFCF');
				break;
			}
			case '\uffd2':  case '\uffd3':  case '\uffd4':  case '\uffd5':
			case '\uffd6':  case '\uffd7':
			{
				matchRange('\uFFD2','\uFFD7');
				break;
			}
			case '\uffda':  case '\uffdb':  case '\uffdc':
			{
				matchRange('\uFFDA','\uFFDC');
				break;
			}
			default:
				if (((cached_LA1 >= '\u1401' && cached_LA1 <= '\u166c')))
				{
					matchRange('\u1401','\u166C');
				}
				else if (((cached_LA1 >= '\ua000' && cached_LA1 <= '\ua48c'))) {
					matchRange('\uA000','\uA48C');
				}
				else if (((cached_LA1 >= '\uf900' && cached_LA1 <= '\ufa2d'))) {
					matchRange('\uF900','\uFA2D');
				}
				else if (((cached_LA1 >= '\ufbd3' && cached_LA1 <= '\ufd3d'))) {
					matchRange('\uFBD3','\uFD3D');
				}
				else if (((cached_LA1 >= '\ufe76' && cached_LA1 <= '\ufefc'))) {
					matchRange('\uFE76','\uFEFC');
				}
			else
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			break; }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Nl(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Nl;
		
		{
			switch ( cached_LA1 )
			{
			case '\u16ee':  case '\u16ef':  case '\u16f0':
			{
				matchRange('\u16EE','\u16F0');
				break;
			}
			case '\u2160':  case '\u2161':  case '\u2162':  case '\u2163':
			case '\u2164':  case '\u2165':  case '\u2166':  case '\u2167':
			case '\u2168':  case '\u2169':  case '\u216a':  case '\u216b':
			case '\u216c':  case '\u216d':  case '\u216e':  case '\u216f':
			case '\u2170':  case '\u2171':  case '\u2172':  case '\u2173':
			case '\u2174':  case '\u2175':  case '\u2176':  case '\u2177':
			case '\u2178':  case '\u2179':  case '\u217a':  case '\u217b':
			case '\u217c':  case '\u217d':  case '\u217e':  case '\u217f':
			case '\u2180':  case '\u2181':  case '\u2182':  case '\u2183':
			{
				matchRange('\u2160','\u2183');
				break;
			}
			case '\u3007':
			{
				matchRange('\u3007','\u3007');
				break;
			}
			case '\u3021':  case '\u3022':  case '\u3023':  case '\u3024':
			case '\u3025':  case '\u3026':  case '\u3027':  case '\u3028':
			case '\u3029':
			{
				matchRange('\u3021','\u3029');
				break;
			}
			case '\u3038':  case '\u3039':  case '\u303a':
			{
				matchRange('\u3038','\u303A');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mDECIMAL_DIGIT_CHARACTER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = DECIMAL_DIGIT_CHARACTER;
		
		mUNICODE_CLASS_Nd(false);
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Nd(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Nd;
		
		{
			switch ( cached_LA1 )
			{
			case '0':  case '1':  case '2':  case '3':
			case '4':  case '5':  case '6':  case '7':
			case '8':  case '9':
			{
				matchRange('\u0030','\u0039');
				break;
			}
			case '\u0660':  case '\u0661':  case '\u0662':  case '\u0663':
			case '\u0664':  case '\u0665':  case '\u0666':  case '\u0667':
			case '\u0668':  case '\u0669':
			{
				matchRange('\u0660','\u0669');
				break;
			}
			case '\u06f0':  case '\u06f1':  case '\u06f2':  case '\u06f3':
			case '\u06f4':  case '\u06f5':  case '\u06f6':  case '\u06f7':
			case '\u06f8':  case '\u06f9':
			{
				matchRange('\u06F0','\u06F9');
				break;
			}
			case '\u0966':  case '\u0967':  case '\u0968':  case '\u0969':
			case '\u096a':  case '\u096b':  case '\u096c':  case '\u096d':
			case '\u096e':  case '\u096f':
			{
				matchRange('\u0966','\u096F');
				break;
			}
			case '\u09e6':  case '\u09e7':  case '\u09e8':  case '\u09e9':
			case '\u09ea':  case '\u09eb':  case '\u09ec':  case '\u09ed':
			case '\u09ee':  case '\u09ef':
			{
				matchRange('\u09E6','\u09EF');
				break;
			}
			case '\u0a66':  case '\u0a67':  case '\u0a68':  case '\u0a69':
			case '\u0a6a':  case '\u0a6b':  case '\u0a6c':  case '\u0a6d':
			case '\u0a6e':  case '\u0a6f':
			{
				matchRange('\u0A66','\u0A6F');
				break;
			}
			case '\u0ae6':  case '\u0ae7':  case '\u0ae8':  case '\u0ae9':
			case '\u0aea':  case '\u0aeb':  case '\u0aec':  case '\u0aed':
			case '\u0aee':  case '\u0aef':
			{
				matchRange('\u0AE6','\u0AEF');
				break;
			}
			case '\u0b66':  case '\u0b67':  case '\u0b68':  case '\u0b69':
			case '\u0b6a':  case '\u0b6b':  case '\u0b6c':  case '\u0b6d':
			case '\u0b6e':  case '\u0b6f':
			{
				matchRange('\u0B66','\u0B6F');
				break;
			}
			case '\u0be7':  case '\u0be8':  case '\u0be9':  case '\u0bea':
			case '\u0beb':  case '\u0bec':  case '\u0bed':  case '\u0bee':
			case '\u0bef':
			{
				matchRange('\u0BE7','\u0BEF');
				break;
			}
			case '\u0c66':  case '\u0c67':  case '\u0c68':  case '\u0c69':
			case '\u0c6a':  case '\u0c6b':  case '\u0c6c':  case '\u0c6d':
			case '\u0c6e':  case '\u0c6f':
			{
				matchRange('\u0C66','\u0C6F');
				break;
			}
			case '\u0ce6':  case '\u0ce7':  case '\u0ce8':  case '\u0ce9':
			case '\u0cea':  case '\u0ceb':  case '\u0cec':  case '\u0ced':
			case '\u0cee':  case '\u0cef':
			{
				matchRange('\u0CE6','\u0CEF');
				break;
			}
			case '\u0d66':  case '\u0d67':  case '\u0d68':  case '\u0d69':
			case '\u0d6a':  case '\u0d6b':  case '\u0d6c':  case '\u0d6d':
			case '\u0d6e':  case '\u0d6f':
			{
				matchRange('\u0D66','\u0D6F');
				break;
			}
			case '\u0e50':  case '\u0e51':  case '\u0e52':  case '\u0e53':
			case '\u0e54':  case '\u0e55':  case '\u0e56':  case '\u0e57':
			case '\u0e58':  case '\u0e59':
			{
				matchRange('\u0E50','\u0E59');
				break;
			}
			case '\u0ed0':  case '\u0ed1':  case '\u0ed2':  case '\u0ed3':
			case '\u0ed4':  case '\u0ed5':  case '\u0ed6':  case '\u0ed7':
			case '\u0ed8':  case '\u0ed9':
			{
				matchRange('\u0ED0','\u0ED9');
				break;
			}
			case '\u0f20':  case '\u0f21':  case '\u0f22':  case '\u0f23':
			case '\u0f24':  case '\u0f25':  case '\u0f26':  case '\u0f27':
			case '\u0f28':  case '\u0f29':
			{
				matchRange('\u0F20','\u0F29');
				break;
			}
			case '\u1040':  case '\u1041':  case '\u1042':  case '\u1043':
			case '\u1044':  case '\u1045':  case '\u1046':  case '\u1047':
			case '\u1048':  case '\u1049':
			{
				matchRange('\u1040','\u1049');
				break;
			}
			case '\u1369':  case '\u136a':  case '\u136b':  case '\u136c':
			case '\u136d':  case '\u136e':  case '\u136f':  case '\u1370':
			case '\u1371':
			{
				matchRange('\u1369','\u1371');
				break;
			}
			case '\u17e0':  case '\u17e1':  case '\u17e2':  case '\u17e3':
			case '\u17e4':  case '\u17e5':  case '\u17e6':  case '\u17e7':
			case '\u17e8':  case '\u17e9':
			{
				matchRange('\u17E0','\u17E9');
				break;
			}
			case '\u1810':  case '\u1811':  case '\u1812':  case '\u1813':
			case '\u1814':  case '\u1815':  case '\u1816':  case '\u1817':
			case '\u1818':  case '\u1819':
			{
				matchRange('\u1810','\u1819');
				break;
			}
			case '\uff10':  case '\uff11':  case '\uff12':  case '\uff13':
			case '\uff14':  case '\uff15':  case '\uff16':  case '\uff17':
			case '\uff18':  case '\uff19':
			{
				matchRange('\uFF10','\uFF19');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mCONNECTING_CHARACTER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = CONNECTING_CHARACTER;
		
		mUNICODE_CLASS_Pc(false);
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Pc(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Pc;
		
		{
			switch ( cached_LA1 )
			{
			case '_':
			{
				match('\u005F');
				break;
			}
			case '\u203f':  case '\u2040':
			{
				matchRange('\u203F','\u2040');
				break;
			}
			case '\u30fb':
			{
				match('\u30FB');
				break;
			}
			case '\ufe33':  case '\ufe34':
			{
				matchRange('\uFE33','\uFE34');
				break;
			}
			case '\ufe4d':  case '\ufe4e':  case '\ufe4f':
			{
				matchRange('\uFE4D','\uFE4F');
				break;
			}
			case '\uff3f':
			{
				match('\uFF3F');
				break;
			}
			case '\uff65':
			{
				match('\uFF65');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mCOMBINING_CHARACTER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = COMBINING_CHARACTER;
		
		if ((tokenSet_9_.member(cached_LA1)))
		{
			mUNICODE_CLASS_Mn(false);
		}
		else if ((tokenSet_10_.member(cached_LA1))) {
			mUNICODE_CLASS_Mc(false);
		}
		else
		{
			throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
		}
		
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Mn(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Mn;
		
		{
			switch ( cached_LA1 )
			{
			case '\u0300':  case '\u0301':  case '\u0302':  case '\u0303':
			case '\u0304':  case '\u0305':  case '\u0306':  case '\u0307':
			case '\u0308':  case '\u0309':  case '\u030a':  case '\u030b':
			case '\u030c':  case '\u030d':  case '\u030e':  case '\u030f':
			case '\u0310':  case '\u0311':  case '\u0312':  case '\u0313':
			case '\u0314':  case '\u0315':  case '\u0316':  case '\u0317':
			case '\u0318':  case '\u0319':  case '\u031a':  case '\u031b':
			case '\u031c':  case '\u031d':  case '\u031e':  case '\u031f':
			case '\u0320':  case '\u0321':  case '\u0322':  case '\u0323':
			case '\u0324':  case '\u0325':  case '\u0326':  case '\u0327':
			case '\u0328':  case '\u0329':  case '\u032a':  case '\u032b':
			case '\u032c':  case '\u032d':  case '\u032e':  case '\u032f':
			case '\u0330':  case '\u0331':  case '\u0332':  case '\u0333':
			case '\u0334':  case '\u0335':  case '\u0336':  case '\u0337':
			case '\u0338':  case '\u0339':  case '\u033a':  case '\u033b':
			case '\u033c':  case '\u033d':  case '\u033e':  case '\u033f':
			case '\u0340':  case '\u0341':  case '\u0342':  case '\u0343':
			case '\u0344':  case '\u0345':  case '\u0346':  case '\u0347':
			case '\u0348':  case '\u0349':  case '\u034a':  case '\u034b':
			case '\u034c':  case '\u034d':  case '\u034e':  case '\u034f':
			{
				matchRange('\u0300','\u034F');
				break;
			}
			case '\u0360':  case '\u0361':  case '\u0362':  case '\u0363':
			case '\u0364':  case '\u0365':  case '\u0366':  case '\u0367':
			case '\u0368':  case '\u0369':  case '\u036a':  case '\u036b':
			case '\u036c':  case '\u036d':  case '\u036e':  case '\u036f':
			{
				matchRange('\u0360','\u036F');
				break;
			}
			case '\u0483':  case '\u0484':  case '\u0485':  case '\u0486':
			{
				matchRange('\u0483','\u0486');
				break;
			}
			case '\u0591':  case '\u0592':  case '\u0593':  case '\u0594':
			case '\u0595':  case '\u0596':  case '\u0597':  case '\u0598':
			case '\u0599':  case '\u059a':  case '\u059b':  case '\u059c':
			case '\u059d':  case '\u059e':  case '\u059f':  case '\u05a0':
			case '\u05a1':
			{
				matchRange('\u0591','\u05A1');
				break;
			}
			case '\u05a3':  case '\u05a4':  case '\u05a5':  case '\u05a6':
			case '\u05a7':  case '\u05a8':  case '\u05a9':  case '\u05aa':
			case '\u05ab':  case '\u05ac':  case '\u05ad':  case '\u05ae':
			case '\u05af':  case '\u05b0':  case '\u05b1':  case '\u05b2':
			case '\u05b3':  case '\u05b4':  case '\u05b5':  case '\u05b6':
			case '\u05b7':  case '\u05b8':  case '\u05b9':
			{
				matchRange('\u05A3','\u05B9');
				break;
			}
			case '\u05bb':  case '\u05bc':  case '\u05bd':
			{
				matchRange('\u05BB','\u05BD');
				break;
			}
			case '\u05bf':
			{
				match('\u05BF');
				break;
			}
			case '\u05c1':  case '\u05c2':
			{
				matchRange('\u05C1','\u05C2');
				break;
			}
			case '\u05c4':
			{
				match('\u05C4');
				break;
			}
			case '\u064b':  case '\u064c':  case '\u064d':  case '\u064e':
			case '\u064f':  case '\u0650':  case '\u0651':  case '\u0652':
			case '\u0653':  case '\u0654':  case '\u0655':
			{
				matchRange('\u064B','\u0655');
				break;
			}
			case '\u0670':
			{
				match('\u0670');
				break;
			}
			case '\u06d6':  case '\u06d7':  case '\u06d8':  case '\u06d9':
			case '\u06da':  case '\u06db':  case '\u06dc':
			{
				matchRange('\u06D6','\u06DC');
				break;
			}
			case '\u06df':  case '\u06e0':  case '\u06e1':  case '\u06e2':
			case '\u06e3':  case '\u06e4':
			{
				matchRange('\u06DF','\u06E4');
				break;
			}
			case '\u06e7':  case '\u06e8':
			{
				matchRange('\u06E7','\u06E8');
				break;
			}
			case '\u06ea':  case '\u06eb':  case '\u06ec':  case '\u06ed':
			{
				matchRange('\u06EA','\u06ED');
				break;
			}
			case '\u0711':
			{
				match('\u0711');
				break;
			}
			case '\u0730':  case '\u0731':  case '\u0732':  case '\u0733':
			case '\u0734':  case '\u0735':  case '\u0736':  case '\u0737':
			case '\u0738':  case '\u0739':  case '\u073a':  case '\u073b':
			case '\u073c':  case '\u073d':  case '\u073e':  case '\u073f':
			case '\u0740':  case '\u0741':  case '\u0742':  case '\u0743':
			case '\u0744':  case '\u0745':  case '\u0746':  case '\u0747':
			case '\u0748':  case '\u0749':  case '\u074a':
			{
				matchRange('\u0730','\u074A');
				break;
			}
			case '\u07a6':  case '\u07a7':  case '\u07a8':  case '\u07a9':
			case '\u07aa':  case '\u07ab':  case '\u07ac':  case '\u07ad':
			case '\u07ae':  case '\u07af':  case '\u07b0':
			{
				matchRange('\u07A6','\u07B0');
				break;
			}
			case '\u0901':  case '\u0902':
			{
				matchRange('\u0901','\u0902');
				break;
			}
			case '\u093c':
			{
				match('\u093C');
				break;
			}
			case '\u0941':  case '\u0942':  case '\u0943':  case '\u0944':
			case '\u0945':  case '\u0946':  case '\u0947':  case '\u0948':
			{
				matchRange('\u0941','\u0948');
				break;
			}
			case '\u094d':
			{
				match('\u094D');
				break;
			}
			case '\u0951':  case '\u0952':  case '\u0953':  case '\u0954':
			{
				matchRange('\u0951','\u0954');
				break;
			}
			case '\u0962':  case '\u0963':
			{
				matchRange('\u0962','\u0963');
				break;
			}
			case '\u0981':
			{
				match('\u0981');
				break;
			}
			case '\u09bc':
			{
				match('\u09BC');
				break;
			}
			case '\u09c1':  case '\u09c2':  case '\u09c3':  case '\u09c4':
			{
				matchRange('\u09C1','\u09C4');
				break;
			}
			case '\u09cd':
			{
				match('\u09CD');
				break;
			}
			case '\u09e2':  case '\u09e3':
			{
				matchRange('\u09E2','\u09E3');
				break;
			}
			case '\u0a02':
			{
				match('\u0A02');
				break;
			}
			case '\u0a3c':
			{
				match('\u0A3C');
				break;
			}
			case '\u0a41':  case '\u0a42':
			{
				matchRange('\u0A41','\u0A42');
				break;
			}
			case '\u0a47':  case '\u0a48':
			{
				matchRange('\u0A47','\u0A48');
				break;
			}
			case '\u0a4b':  case '\u0a4c':  case '\u0a4d':
			{
				matchRange('\u0A4B','\u0A4D');
				break;
			}
			case '\u0a70':  case '\u0a71':
			{
				matchRange('\u0A70','\u0A71');
				break;
			}
			case '\u0a81':  case '\u0a82':
			{
				matchRange('\u0A81','\u0A82');
				break;
			}
			case '\u0abc':
			{
				match('\u0ABC');
				break;
			}
			case '\u0ac1':  case '\u0ac2':  case '\u0ac3':  case '\u0ac4':
			case '\u0ac5':
			{
				matchRange('\u0AC1','\u0AC5');
				break;
			}
			case '\u0ac7':  case '\u0ac8':
			{
				matchRange('\u0AC7','\u0AC8');
				break;
			}
			case '\u0acd':
			{
				match('\u0ACD');
				break;
			}
			case '\u0b01':
			{
				match('\u0B01');
				break;
			}
			case '\u0b3c':
			{
				match('\u0B3C');
				break;
			}
			case '\u0b3f':
			{
				match('\u0B3F');
				break;
			}
			case '\u0b41':  case '\u0b42':  case '\u0b43':
			{
				matchRange('\u0B41','\u0B43');
				break;
			}
			case '\u0b4d':
			{
				match('\u0B4D');
				break;
			}
			case '\u0b56':
			{
				match('\u0B56');
				break;
			}
			case '\u0b82':
			{
				match('\u0B82');
				break;
			}
			case '\u0bc0':
			{
				match('\u0BC0');
				break;
			}
			case '\u0bcd':
			{
				match('\u0BCD');
				break;
			}
			case '\u0c3e':  case '\u0c3f':  case '\u0c40':
			{
				matchRange('\u0C3E','\u0C40');
				break;
			}
			case '\u0c46':  case '\u0c47':  case '\u0c48':
			{
				matchRange('\u0C46','\u0C48');
				break;
			}
			case '\u0c4a':  case '\u0c4b':  case '\u0c4c':  case '\u0c4d':
			{
				matchRange('\u0C4A','\u0C4D');
				break;
			}
			case '\u0c55':  case '\u0c56':
			{
				matchRange('\u0C55','\u0C56');
				break;
			}
			case '\u0cbf':
			{
				match('\u0CBF');
				break;
			}
			case '\u0cc6':
			{
				match('\u0CC6');
				break;
			}
			case '\u0ccc':  case '\u0ccd':
			{
				matchRange('\u0CCC','\u0CCD');
				break;
			}
			case '\u0d41':  case '\u0d42':  case '\u0d43':
			{
				matchRange('\u0D41','\u0D43');
				break;
			}
			case '\u0d4d':
			{
				match('\u0D4D');
				break;
			}
			case '\u0dca':
			{
				match('\u0DCA');
				break;
			}
			case '\u0dd2':  case '\u0dd3':  case '\u0dd4':
			{
				matchRange('\u0DD2','\u0DD4');
				break;
			}
			case '\u0dd6':
			{
				match('\u0DD6');
				break;
			}
			case '\u0e31':
			{
				match('\u0E31');
				break;
			}
			case '\u0e34':  case '\u0e35':  case '\u0e36':  case '\u0e37':
			case '\u0e38':  case '\u0e39':  case '\u0e3a':
			{
				matchRange('\u0E34','\u0E3A');
				break;
			}
			case '\u0e47':  case '\u0e48':  case '\u0e49':  case '\u0e4a':
			case '\u0e4b':  case '\u0e4c':  case '\u0e4d':  case '\u0e4e':
			{
				matchRange('\u0E47','\u0E4E');
				break;
			}
			case '\u0eb1':
			{
				match('\u0EB1');
				break;
			}
			case '\u0eb4':  case '\u0eb5':  case '\u0eb6':  case '\u0eb7':
			case '\u0eb8':  case '\u0eb9':
			{
				matchRange('\u0EB4','\u0EB9');
				break;
			}
			case '\u0ebb':  case '\u0ebc':
			{
				matchRange('\u0EBB','\u0EBC');
				break;
			}
			case '\u0ec8':  case '\u0ec9':  case '\u0eca':  case '\u0ecb':
			case '\u0ecc':  case '\u0ecd':
			{
				matchRange('\u0EC8','\u0ECD');
				break;
			}
			case '\u0f18':  case '\u0f19':
			{
				matchRange('\u0F18','\u0F19');
				break;
			}
			case '\u0f35':
			{
				match('\u0F35');
				break;
			}
			case '\u0f37':
			{
				match('\u0F37');
				break;
			}
			case '\u0f39':
			{
				match('\u0F39');
				break;
			}
			case '\u0f71':  case '\u0f72':  case '\u0f73':  case '\u0f74':
			case '\u0f75':  case '\u0f76':  case '\u0f77':  case '\u0f78':
			case '\u0f79':  case '\u0f7a':  case '\u0f7b':  case '\u0f7c':
			case '\u0f7d':  case '\u0f7e':
			{
				matchRange('\u0F71','\u0F7E');
				break;
			}
			case '\u0f80':  case '\u0f81':  case '\u0f82':  case '\u0f83':
			case '\u0f84':
			{
				matchRange('\u0F80','\u0F84');
				break;
			}
			case '\u0f86':  case '\u0f87':
			{
				matchRange('\u0F86','\u0F87');
				break;
			}
			case '\u0f90':  case '\u0f91':  case '\u0f92':  case '\u0f93':
			case '\u0f94':  case '\u0f95':  case '\u0f96':  case '\u0f97':
			{
				matchRange('\u0F90','\u0F97');
				break;
			}
			case '\u0f99':  case '\u0f9a':  case '\u0f9b':  case '\u0f9c':
			case '\u0f9d':  case '\u0f9e':  case '\u0f9f':  case '\u0fa0':
			case '\u0fa1':  case '\u0fa2':  case '\u0fa3':  case '\u0fa4':
			case '\u0fa5':  case '\u0fa6':  case '\u0fa7':  case '\u0fa8':
			case '\u0fa9':  case '\u0faa':  case '\u0fab':  case '\u0fac':
			case '\u0fad':  case '\u0fae':  case '\u0faf':  case '\u0fb0':
			case '\u0fb1':  case '\u0fb2':  case '\u0fb3':  case '\u0fb4':
			case '\u0fb5':  case '\u0fb6':  case '\u0fb7':  case '\u0fb8':
			case '\u0fb9':  case '\u0fba':  case '\u0fbb':  case '\u0fbc':
			{
				matchRange('\u0F99','\u0FBC');
				break;
			}
			case '\u0fc6':
			{
				match('\u0FC6');
				break;
			}
			case '\u102d':  case '\u102e':  case '\u102f':  case '\u1030':
			{
				matchRange('\u102D','\u1030');
				break;
			}
			case '\u1032':
			{
				match('\u1032');
				break;
			}
			case '\u1036':  case '\u1037':
			{
				matchRange('\u1036','\u1037');
				break;
			}
			case '\u1039':
			{
				match('\u1039');
				break;
			}
			case '\u1058':  case '\u1059':
			{
				matchRange('\u1058','\u1059');
				break;
			}
			case '\u1712':  case '\u1713':  case '\u1714':
			{
				matchRange('\u1712','\u1714');
				break;
			}
			case '\u1732':  case '\u1733':  case '\u1734':
			{
				matchRange('\u1732','\u1734');
				break;
			}
			case '\u1752':  case '\u1753':
			{
				matchRange('\u1752','\u1753');
				break;
			}
			case '\u1772':  case '\u1773':
			{
				matchRange('\u1772','\u1773');
				break;
			}
			case '\u17b7':  case '\u17b8':  case '\u17b9':  case '\u17ba':
			case '\u17bb':  case '\u17bc':  case '\u17bd':
			{
				matchRange('\u17B7','\u17BD');
				break;
			}
			case '\u17c6':
			{
				match('\u17C6');
				break;
			}
			case '\u17c9':  case '\u17ca':  case '\u17cb':  case '\u17cc':
			case '\u17cd':  case '\u17ce':  case '\u17cf':  case '\u17d0':
			case '\u17d1':  case '\u17d2':  case '\u17d3':
			{
				matchRange('\u17C9','\u17D3');
				break;
			}
			case '\u180b':  case '\u180c':  case '\u180d':
			{
				matchRange('\u180B','\u180D');
				break;
			}
			case '\u18a9':
			{
				match('\u18A9');
				break;
			}
			case '\u20d0':  case '\u20d1':  case '\u20d2':  case '\u20d3':
			case '\u20d4':  case '\u20d5':  case '\u20d6':  case '\u20d7':
			case '\u20d8':  case '\u20d9':  case '\u20da':  case '\u20db':
			case '\u20dc':
			{
				matchRange('\u20D0','\u20DC');
				break;
			}
			case '\u20e1':
			{
				match('\u20E1');
				break;
			}
			case '\u20e5':  case '\u20e6':  case '\u20e7':  case '\u20e8':
			case '\u20e9':  case '\u20ea':
			{
				matchRange('\u20E5','\u20EA');
				break;
			}
			case '\u302a':  case '\u302b':  case '\u302c':  case '\u302d':
			case '\u302e':  case '\u302f':
			{
				matchRange('\u302A','\u302F');
				break;
			}
			case '\u3099':  case '\u309a':
			{
				matchRange('\u3099','\u309A');
				break;
			}
			case '\ufb1e':
			{
				match('\uFB1E');
				break;
			}
			case '\ufe00':  case '\ufe01':  case '\ufe02':  case '\ufe03':
			case '\ufe04':  case '\ufe05':  case '\ufe06':  case '\ufe07':
			case '\ufe08':  case '\ufe09':  case '\ufe0a':  case '\ufe0b':
			case '\ufe0c':  case '\ufe0d':  case '\ufe0e':  case '\ufe0f':
			{
				matchRange('\uFE00','\uFE0F');
				break;
			}
			case '\ufe20':  case '\ufe21':  case '\ufe22':  case '\ufe23':
			{
				matchRange('\uFE20','\uFE23');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Mc(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Mc;
		
		{
			switch ( cached_LA1 )
			{
			case '\u0903':
			{
				match('\u0903');
				break;
			}
			case '\u093e':  case '\u093f':  case '\u0940':
			{
				matchRange('\u093E','\u0940');
				break;
			}
			case '\u0949':  case '\u094a':  case '\u094b':  case '\u094c':
			{
				matchRange('\u0949','\u094C');
				break;
			}
			case '\u0982':  case '\u0983':
			{
				matchRange('\u0982','\u0983');
				break;
			}
			case '\u09be':  case '\u09bf':  case '\u09c0':
			{
				matchRange('\u09BE','\u09C0');
				break;
			}
			case '\u09c7':  case '\u09c8':
			{
				matchRange('\u09C7','\u09C8');
				break;
			}
			case '\u09cb':  case '\u09cc':
			{
				matchRange('\u09CB','\u09CC');
				break;
			}
			case '\u09d7':
			{
				match('\u09D7');
				break;
			}
			case '\u0a3e':  case '\u0a3f':  case '\u0a40':
			{
				matchRange('\u0A3E','\u0A40');
				break;
			}
			case '\u0a83':
			{
				match('\u0A83');
				break;
			}
			case '\u0abe':  case '\u0abf':  case '\u0ac0':
			{
				matchRange('\u0ABE','\u0AC0');
				break;
			}
			case '\u0ac9':
			{
				match('\u0AC9');
				break;
			}
			case '\u0acb':  case '\u0acc':
			{
				matchRange('\u0ACB','\u0ACC');
				break;
			}
			case '\u0b02':  case '\u0b03':
			{
				matchRange('\u0B02','\u0B03');
				break;
			}
			case '\u0b3e':
			{
				match('\u0B3E');
				break;
			}
			case '\u0b40':
			{
				match('\u0B40');
				break;
			}
			case '\u0b47':  case '\u0b48':
			{
				matchRange('\u0B47','\u0B48');
				break;
			}
			case '\u0b4b':  case '\u0b4c':
			{
				matchRange('\u0B4B','\u0B4C');
				break;
			}
			case '\u0b57':
			{
				match('\u0B57');
				break;
			}
			case '\u0bbe':  case '\u0bbf':
			{
				matchRange('\u0BBE','\u0BBF');
				break;
			}
			case '\u0bc1':  case '\u0bc2':
			{
				matchRange('\u0BC1','\u0BC2');
				break;
			}
			case '\u0bc6':  case '\u0bc7':  case '\u0bc8':
			{
				matchRange('\u0BC6','\u0BC8');
				break;
			}
			case '\u0bca':  case '\u0bcb':  case '\u0bcc':
			{
				matchRange('\u0BCA','\u0BCC');
				break;
			}
			case '\u0bd7':
			{
				match('\u0BD7');
				break;
			}
			case '\u0c01':  case '\u0c02':  case '\u0c03':
			{
				matchRange('\u0C01','\u0C03');
				break;
			}
			case '\u0c41':  case '\u0c42':  case '\u0c43':  case '\u0c44':
			{
				matchRange('\u0C41','\u0C44');
				break;
			}
			case '\u0c82':  case '\u0c83':
			{
				matchRange('\u0C82','\u0C83');
				break;
			}
			case '\u0cbe':
			{
				match('\u0CBE');
				break;
			}
			case '\u0cc0':  case '\u0cc1':  case '\u0cc2':  case '\u0cc3':
			case '\u0cc4':
			{
				matchRange('\u0CC0','\u0CC4');
				break;
			}
			case '\u0cc7':  case '\u0cc8':
			{
				matchRange('\u0CC7','\u0CC8');
				break;
			}
			case '\u0cca':  case '\u0ccb':
			{
				matchRange('\u0CCA','\u0CCB');
				break;
			}
			case '\u0cd5':  case '\u0cd6':
			{
				matchRange('\u0CD5','\u0CD6');
				break;
			}
			case '\u0d02':  case '\u0d03':
			{
				matchRange('\u0D02','\u0D03');
				break;
			}
			case '\u0d3e':  case '\u0d3f':  case '\u0d40':
			{
				matchRange('\u0D3E','\u0D40');
				break;
			}
			case '\u0d46':  case '\u0d47':  case '\u0d48':
			{
				matchRange('\u0D46','\u0D48');
				break;
			}
			case '\u0d4a':  case '\u0d4b':  case '\u0d4c':
			{
				matchRange('\u0D4A','\u0D4C');
				break;
			}
			case '\u0d57':
			{
				match('\u0D57');
				break;
			}
			case '\u0d82':  case '\u0d83':
			{
				matchRange('\u0D82','\u0D83');
				break;
			}
			case '\u0dcf':  case '\u0dd0':  case '\u0dd1':
			{
				matchRange('\u0DCF','\u0DD1');
				break;
			}
			case '\u0dd8':  case '\u0dd9':  case '\u0dda':  case '\u0ddb':
			case '\u0ddc':  case '\u0ddd':  case '\u0dde':  case '\u0ddf':
			{
				matchRange('\u0DD8','\u0DDF');
				break;
			}
			case '\u0df2':  case '\u0df3':
			{
				matchRange('\u0DF2','\u0DF3');
				break;
			}
			case '\u0f3e':  case '\u0f3f':
			{
				matchRange('\u0F3E','\u0F3F');
				break;
			}
			case '\u0f7f':
			{
				match('\u0F7F');
				break;
			}
			case '\u102c':
			{
				match('\u102C');
				break;
			}
			case '\u1031':
			{
				match('\u1031');
				break;
			}
			case '\u1038':
			{
				match('\u1038');
				break;
			}
			case '\u1056':  case '\u1057':
			{
				matchRange('\u1056','\u1057');
				break;
			}
			case '\u17b4':  case '\u17b5':  case '\u17b6':
			{
				matchRange('\u17B4','\u17B6');
				break;
			}
			case '\u17be':  case '\u17bf':  case '\u17c0':  case '\u17c1':
			case '\u17c2':  case '\u17c3':  case '\u17c4':  case '\u17c5':
			{
				matchRange('\u17BE','\u17C5');
				break;
			}
			case '\u17c7':  case '\u17c8':
			{
				matchRange('\u17C7','\u17C8');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mFORMATTING_CHARACTER(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = FORMATTING_CHARACTER;
		
		mUNICODE_CLASS_Cf(false);
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	protected void mUNICODE_CLASS_Cf(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = UNICODE_CLASS_Cf;
		
		{
			switch ( cached_LA1 )
			{
			case '\u06dd':
			{
				match('\u06DD');
				break;
			}
			case '\u070f':
			{
				match('\u070F');
				break;
			}
			case '\u180e':
			{
				match('\u180E');
				break;
			}
			case '\u200c':  case '\u200d':  case '\u200e':  case '\u200f':
			{
				matchRange('\u200C','\u200F');
				break;
			}
			case '\u202a':  case '\u202b':  case '\u202c':  case '\u202d':
			case '\u202e':
			{
				matchRange('\u202A','\u202E');
				break;
			}
			case '\u2060':  case '\u2061':  case '\u2062':  case '\u2063':
			{
				matchRange('\u2060','\u2063');
				break;
			}
			case '\u206a':  case '\u206b':  case '\u206c':  case '\u206d':
			case '\u206e':  case '\u206f':
			{
				matchRange('\u206A','\u206F');
				break;
			}
			case '\ufeff':
			{
				match('\uFEFF');
				break;
			}
			case '\ufff9':  case '\ufffa':  case '\ufffb':
			{
				matchRange('\uFFF9','\uFFFB');
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = new long[2048];
		data[0]=-140737488364545L;
		for (int i = 1; i<=127; i++) { data[i]=-1L; }
		data[128]=-3298534883329L;
		for (int i = 129; i<=1022; i++) { data[i]=-1L; }
		data[1023]=9223372036854775807L;
		for (int i = 1024; i<=2047; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = new long[2048];
		data[0]=-9217L;
		for (int i = 1; i<=127; i++) { data[i]=-1L; }
		data[128]=-3298534883329L;
		for (int i = 129; i<=1022; i++) { data[i]=-1L; }
		data[1023]=9223372036854775807L;
		for (int i = 1024; i<=2047; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = new long[1025];
		data[0]=4294967296L;
		data[1]=0L;
		data[2]=4294967296L;
		for (int i = 3; i<=89; i++) { data[i]=0L; }
		data[90]=1L;
		for (int i = 91; i<=127; i++) { data[i]=0L; }
		data[128]=140737488359423L;
		data[129]=2147483648L;
		for (int i = 130; i<=191; i++) { data[i]=0L; }
		data[192]=1L;
		for (int i = 193; i<=1024; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());
	private static long[] mk_tokenSet_3_()
	{
		long[] data = new long[2042];
		data[0]=0L;
		data[1]=134217726L;
		data[2]=0L;
		data[3]=2139095039L;
		data[4]=-6172933889249159851L;
		data[5]=3122495741643543722L;
		data[6]=1274187559846268630L;
		data[7]=6184099063146390672L;
		data[8]=1501199875790165L;
		for (int i = 9; i<=13; i++) { data[i]=0L; }
		data[14]=17575006099264L;
		data[15]=4597424615849984L;
		data[16]=281474976710655L;
		data[17]=6148914689804861440L;
		data[18]=6148914691236516865L;
		data[19]=78062393541077675L;
		data[20]=-562949953399467L;
		data[21]=8388607L;
		for (int i = 22; i<=65; i++) { data[i]=0L; }
		data[66]=-4294967296L;
		data[67]=63L;
		for (int i = 68; i<=119; i++) { data[i]=0L; }
		for (int i = 120; i<=121; i++) { data[i]=6148914691236517205L; }
		data[122]=6148914689806259541L;
		data[123]=96076792050570581L;
		data[124]=-71777217515815168L;
		data[125]=280378317225728L;
		data[126]=1080863910568919040L;
		data[127]=1080897995681042176L;
		for (int i = 128; i<=131; i++) { data[i]=0L; }
		data[132]=-4608522378834134908L;
		data[133]=32L;
		for (int i = 134; i<=1019; i++) { data[i]=0L; }
		data[1020]=576460743713488896L;
		for (int i = 1021; i<=2041; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_3_ = new BitSet(mk_tokenSet_3_());
	private static long[] mk_tokenSet_4_()
	{
		long[] data = new long[2044];
		data[0]=0L;
		data[1]=576460743713488896L;
		data[2]=297241973452963840L;
		data[3]=-36028799166447616L;
		data[4]=6172933889249159850L;
		data[5]=-3122495741643543723L;
		data[6]=-1850648312149692119L;
		data[7]=-6185224963053235648L;
		data[8]=3002391161645738L;
		data[9]=-65536L;
		data[10]=70368744177663L;
		for (int i = 11; i<=13; i++) { data[i]=0L; }
		data[14]=-17592185978880L;
		data[15]=13416973893599231L;
		data[16]=-281474976710656L;
		data[17]=-6148914689804861441L;
		data[18]=-6148914691236517886L;
		data[19]=156124787082155348L;
		data[20]=43690L;
		data[21]=-8589934592L;
		data[22]=255L;
		for (int i = 23; i<=119; i++) { data[i]=0L; }
		for (int i = 120; i<=121; i++) { data[i]=-6148914691236517206L; }
		data[122]=-6148914693832791382L;
		data[123]=192153584101141162L;
		data[124]=71777214282006783L;
		data[125]=4611405638684049471L;
		data[126]=4674456033467236607L;
		data[127]=61925590106570972L;
		data[128]=0L;
		data[129]=-9222809086901354496L;
		for (int i = 130; i<=131; i++) { data[i]=0L; }
		data[132]=2454602534405850112L;
		data[133]=960L;
		for (int i = 134; i<=1003; i++) { data[i]=0L; }
		data[1004]=16253055L;
		for (int i = 1005; i<=1020; i++) { data[i]=0L; }
		data[1021]=134217726L;
		for (int i = 1022; i<=2043; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_4_ = new BitSet(mk_tokenSet_4_());
	private static long[] mk_tokenSet_5_()
	{
		long[] data = new long[1025];
		for (int i = 0; i<=6; i++) { data[i]=0L; }
		data[7]=1125899906844960L;
		for (int i = 8; i<=125; i++) { data[i]=0L; }
		data[126]=1153201884350185216L;
		data[127]=1152921504606851072L;
		for (int i = 128; i<=1024; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_5_ = new BitSet(mk_tokenSet_5_());
	private static long[] mk_tokenSet_6_()
	{
		long[] data = new long[2046];
		for (int i = 0; i<=9; i++) { data[i]=0L; }
		data[10]=-432627039204278272L;
		data[11]=70501888360451L;
		data[12]=0L;
		data[13]=288230376151711744L;
		for (int i = 14; i<=20; i++) { data[i]=0L; }
		data[21]=33554432L;
		for (int i = 22; i<=24; i++) { data[i]=0L; }
		data[25]=1L;
		data[26]=0L;
		data[27]=412316860416L;
		for (int i = 28; i<=56; i++) { data[i]=0L; }
		data[57]=64L;
		data[58]=0L;
		data[59]=64L;
		for (int i = 60; i<=94; i++) { data[i]=0L; }
		data[95]=8388608L;
		data[96]=0L;
		data[97]=8L;
		for (int i = 98; i<=191; i++) { data[i]=0L; }
		data[192]=593912200859484192L;
		data[193]=0L;
		data[194]=1610612736L;
		data[195]=8070450532247928832L;
		for (int i = 196; i<=1020; i++) { data[i]=0L; }
		data[1021]=281474976710656L;
		data[1022]=3221225472L;
		for (int i = 1023; i<=2045; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_6_ = new BitSet(mk_tokenSet_6_());
	private static long[] mk_tokenSet_7_()
	{
		long[] data = new long[4088];
		for (int i = 0; i<=5; i++) { data[i]=0L; }
		data[6]=576460752303423488L;
		data[7]=15L;
		for (int i = 8; i<=22; i++) { data[i]=0L; }
		data[23]=1979120929931264L;
		data[24]=576460743713488896L;
		data[25]=-351843720886274L;
		data[26]=-1L;
		data[27]=2017612633065127935L;
		data[28]=35184371892224L;
		data[29]=0L;
		data[30]=563224831328255L;
		for (int i = 31; i<=35; i++) { data[i]=0L; }
		data[36]=2594073385365405664L;
		data[37]=17163157504L;
		data[38]=271902628478820320L;
		data[39]=844440767823872L;
		data[40]=247132830528276448L;
		data[41]=7881300924956672L;
		data[42]=2589004636761075680L;
		data[43]=4295032832L;
		data[44]=2579997437506199520L;
		data[45]=15837691904L;
		data[46]=270153412153034728L;
		data[47]=0L;
		data[48]=283724577500946400L;
		data[49]=12884901888L;
		data[50]=283724577500946400L;
		data[51]=13958643712L;
		data[52]=288228177128316896L;
		data[53]=12884901888L;
		data[54]=3457638613854978016L;
		data[55]=127L;
		data[56]=3940649673949182L;
		data[57]=63L;
		data[58]=2309762420256548246L;
		data[59]=805306399L;
		data[60]=1L;
		data[61]=8796093021951L;
		data[62]=3840L;
		data[63]=0L;
		data[64]=7679401525247L;
		data[65]=4128768L;
		data[66]=0L;
		data[67]=144115188075790336L;
		data[68]=-1L;
		data[69]=-2080374785L;
		data[70]=-1065151889409L;
		data[71]=288230376151711743L;
		data[72]=-129L;
		data[73]=-3263218305L;
		data[74]=9168625153884503423L;
		data[75]=-140737496776899L;
		data[76]=-2160230401L;
		data[77]=134217599L;
		data[78]=-4294967296L;
		data[79]=9007199254740991L;
		data[80]=-2L;
		for (int i = 81; i<=88; i++) { data[i]=-1L; }
		data[89]=35923243902697471L;
		data[90]=-4160749570L;
		data[91]=8796093022207L;
		data[92]=1125895612129279L;
		data[93]=527761286627327L;
		data[94]=4503599627370495L;
		data[95]=268435456L;
		data[96]=-4294967296L;
		data[97]=72057594037927927L;
		data[98]=2199023255551L;
		for (int i = 99; i<=131; i++) { data[i]=0L; }
		data[132]=135107988821114880L;
		for (int i = 133; i<=191; i++) { data[i]=0L; }
		data[192]=1152921504606847040L;
		data[193]=-2L;
		data[194]=-6434062337L;
		data[195]=-8646911284551352321L;
		data[196]=-527765581332512L;
		data[197]=-1L;
		data[198]=72057589742993407L;
		data[199]=-281474976710656L;
		for (int i = 200; i<=207; i++) { data[i]=0L; }
		data[208]=1L;
		for (int i = 209; i<=309; i++) { data[i]=0L; }
		data[310]=9007199254740992L;
		data[311]=0L;
		data[312]=1L;
		for (int i = 313; i<=637; i++) { data[i]=0L; }
		data[638]=137438953472L;
		data[639]=0L;
		for (int i = 640; i<=657; i++) { data[i]=-1L; }
		data[658]=8191L;
		for (int i = 659; i<=687; i++) { data[i]=0L; }
		data[688]=1L;
		for (int i = 689; i<=861; i++) { data[i]=0L; }
		data[862]=34359738368L;
		for (int i = 863; i<=995; i++) { data[i]=0L; }
		for (int i = 996; i<=999; i++) { data[i]=-1L; }
		data[1000]=-211106232532993L;
		data[1001]=8796093022207L;
		for (int i = 1002; i<=1003; i++) { data[i]=0L; }
		data[1004]=6881498029988249600L;
		data[1005]=-37L;
		data[1006]=1125899906842623L;
		data[1007]=-524288L;
		for (int i = 1008; i<=1011; i++) { data[i]=-1L; }
		data[1012]=4611686018427387903L;
		data[1013]=-65536L;
		data[1014]=-196609L;
		data[1015]=1152640029630136575L;
		data[1016]=0L;
		data[1017]=-9288674231451648L;
		data[1018]=-1L;
		data[1019]=2305843009213693951L;
		data[1020]=0L;
		data[1021]=-281749854617600L;
		data[1022]=9223372033633550335L;
		data[1023]=486341884L;
		for (int i = 1024; i<=4087; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_7_ = new BitSet(mk_tokenSet_7_());
	private static long[] mk_tokenSet_8_()
	{
		long[] data = new long[1025];
		for (int i = 0; i<=90; i++) { data[i]=0L; }
		data[91]=492581209243648L;
		for (int i = 92; i<=132; i++) { data[i]=0L; }
		data[133]=-4294967296L;
		data[134]=15L;
		for (int i = 135; i<=191; i++) { data[i]=0L; }
		data[192]=504407547722072192L;
		for (int i = 193; i<=1024; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_8_ = new BitSet(mk_tokenSet_8_());
	private static long[] mk_tokenSet_9_()
	{
		long[] data = new long[2034];
		for (int i = 0; i<=11; i++) { data[i]=0L; }
		data[12]=-1L;
		data[13]=281470681808895L;
		for (int i = 14; i<=17; i++) { data[i]=0L; }
		data[18]=120L;
		for (int i = 19; i<=21; i++) { data[i]=0L; }
		data[22]=-4899916411759099904L;
		data[23]=22L;
		data[24]=0L;
		data[25]=281474980902912L;
		data[26]=0L;
		data[27]=67755789254656L;
		data[28]=-281474976579584L;
		data[29]=2047L;
		data[30]=562675075514368L;
		for (int i = 31; i<=35; i++) { data[i]=0L; }
		data[36]=1152921504606846982L;
		data[37]=51541582334L;
		data[38]=1152921504606846978L;
		data[39]=51539615774L;
		data[40]=1152921504606846980L;
		data[41]=844424930146694L;
		data[42]=1152921504606846982L;
		data[43]=8638L;
		data[44]=-8070450532247928830L;
		data[45]=4202510L;
		data[46]=4L;
		data[47]=8193L;
		data[48]=-4611686018427387904L;
		data[49]=6307265L;
		data[50]=-9223372036854775808L;
		data[51]=12352L;
		data[52]=0L;
		data[53]=8206L;
		data[54]=0L;
		data[55]=6030336L;
		data[56]=572520102629474304L;
		data[57]=32640L;
		data[58]=2013671983388033024L;
		data[59]=16128L;
		data[60]=189151184399892480L;
		data[61]=9222809086901354496L;
		data[62]=2305843009196851423L;
		data[63]=64L;
		data[64]=199812049092476928L;
		data[65]=50331648L;
		for (int i = 66; i<=91; i++) { data[i]=0L; }
		data[92]=7881299349733376L;
		data[93]=3377699721314304L;
		data[94]=4575657221408423936L;
		data[95]=1048128L;
		data[96]=14336L;
		data[97]=0L;
		data[98]=2199023255552L;
		for (int i = 99; i<=130; i++) { data[i]=0L; }
		data[131]=8667780808704L;
		for (int i = 132; i<=191; i++) { data[i]=0L; }
		data[192]=277076930199552L;
		data[193]=0L;
		data[194]=100663296L;
		for (int i = 195; i<=1003; i++) { data[i]=0L; }
		data[1004]=1073741824L;
		for (int i = 1005; i<=1015; i++) { data[i]=0L; }
		data[1016]=64424574975L;
		for (int i = 1017; i<=2033; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_9_ = new BitSet(mk_tokenSet_9_());
	private static long[] mk_tokenSet_10_()
	{
		long[] data = new long[1025];
		for (int i = 0; i<=35; i++) { data[i]=0L; }
		data[36]=-4611686018427387896L;
		data[37]=7681L;
		data[38]=-4611686018427387892L;
		data[39]=8395137L;
		data[40]=-4611686018427387904L;
		data[41]=1L;
		data[42]=-4611686018427387896L;
		data[43]=6657L;
		data[44]=4611686018427387916L;
		data[45]=8395137L;
		data[46]=-4611686018427387904L;
		data[47]=8396230L;
		data[48]=14L;
		data[49]=30L;
		data[50]=4611686018427387916L;
		data[51]=6294943L;
		data[52]=-4611686018427387892L;
		data[53]=8396225L;
		data[54]=12L;
		data[55]=3377703998947328L;
		for (int i = 56; i<=59; i++) { data[i]=0L; }
		data[60]=-4611686018427387904L;
		data[61]=-9223372036854775808L;
		for (int i = 62; i<=63; i++) { data[i]=0L; }
		data[64]=72638136177393664L;
		data[65]=12582912L;
		for (int i = 66; i<=93; i++) { data[i]=0L; }
		data[94]=-4580160821035794432L;
		data[95]=447L;
		for (int i = 96; i<=1024; i++) { data[i]=0L; }
		return data;
	}
	public static readonly BitSet tokenSet_10_ = new BitSet(mk_tokenSet_10_());
	
}
}
