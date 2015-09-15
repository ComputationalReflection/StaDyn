// $ANTLR 2.7.6 (2005-12-22): "CSharpParser.g" -> "CSharpParser.cs"$

	using StringBuilder = System.Text.StringBuilder;
	using FileInfo = System.IO.FileInfo;
	using AST;
	using TypeSystem;
	using ErrorManagement;
	using System.Collections.Generic;
	using System.Globalization;

   #region

   /**param.Identifier = id.Identifier; param.ParamType = typeExp; param.Line = i
   [The "BSD licence"]
   Copyright (c) 2002-2005 Kunle Odutola
   All rights reserved.

   Redistribution and use in source and binary forms, with or without
   modification, are permitted provided that the following conditions
   are met:
   1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
   2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
   3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.

   THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
   IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
   OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
   IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
   INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
   NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
   DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
   THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
   (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
   THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


   /// <summary>
   /// A Parser for the C# language (including preprocessors directives).
   /// </summary>
   ///
   /// <remarks>
   /// <para>
   /// The Parser defined below is based on the "C# Language Specification" as documented in 
   /// the ECMA-334 standard dated December 2001.
   /// </para>
   ///
   /// <para>
   /// One notable feature of this parser is that it can handle input that includes "normalized"
   /// C# preprocessing directives. In the simplest sense, normalized C# preprocessing directives 
   /// are directives that can be safely deleted from a source file without triggering any parsing 
   /// errors due to incomplete statements etc.
   /// </para>
   ///
   /// <para>
   /// The Abstract Syntax Tree that this parser constructs has special nodes that represents
   //  all the C# preprocessor directives defined in the ECMA-334 standard.
   /// </para>
   ///
   /// <para>
   /// History
   /// </para>
   ///
   /// <para>
   /// 31-May-2002 kunle	  Started work in earnest
   /// 09-Feb-2003 kunle     Separated Parser from the original combined Lexer/Parser grammar file<br/>
   /// 20-Oct-2003 kunle     Removed STMT_LIST from inside BLOCK nodes. A BLOCK node now directly contains
   ///						  a list of statements. Finding the AST nodes that correspond to a selection
   ///						  should now be easier.<br/>
   /// </para>
   ///
   /// </remarks>

   */

   #endregion

namespace Parser
{
	// Generate the header common to all output files.
	using System;
	
	using TokenBuffer              = antlr.TokenBuffer;
	using TokenStreamException     = antlr.TokenStreamException;
	using TokenStreamIOException   = antlr.TokenStreamIOException;
	using ANTLRException           = antlr.ANTLRException;
	using LLkParser = antlr.LLkParser;
	using Token                    = antlr.Token;
	using IToken                   = antlr.IToken;
	using TokenStream              = antlr.TokenStream;
	using RecognitionException     = antlr.RecognitionException;
	using NoViableAltException     = antlr.NoViableAltException;
	using MismatchedTokenException = antlr.MismatchedTokenException;
	using SemanticException        = antlr.SemanticException;
	using ParserSharedInputState   = antlr.ParserSharedInputState;
	using BitSet                   = antlr.collections.impl.BitSet;
	
	public 	class CSharpParser : antlr.LLkParser
	              , ICSharpParser	{
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
		public const int ABSTRACT = 55;
		public const int AS = 56;
		public const int BASE = 57;
		public const int BOOL = 58;
		public const int BREAK = 59;
		public const int BYTE = 60;
		public const int CASE = 61;
		public const int CATCH = 62;
		public const int CHAR = 63;
		public const int CHECKED = 64;
		public const int CLASS = 65;
		public const int CONST = 66;
		public const int CONTINUE = 67;
		public const int DECIMAL = 68;
		public const int DELEGATE = 69;
		public const int DO = 70;
		public const int DOUBLE = 71;
		public const int ELSE = 72;
		public const int ENUM = 73;
		public const int EVENT = 74;
		public const int EXPLICIT = 75;
		public const int EXTERN = 76;
		public const int FINALLY = 77;
		public const int FIXED = 78;
		public const int FLOAT = 79;
		public const int FOR = 80;
		public const int FOREACH = 81;
		public const int GOTO = 82;
		public const int IF = 83;
		public const int IMPLICIT = 84;
		public const int IN = 85;
		public const int INT = 86;
		public const int INTERFACE = 87;
		public const int INTERNAL = 88;
		public const int IS = 89;
		public const int LOCK = 90;
		public const int LONG = 91;
		public const int NAMESPACE = 92;
		public const int NEW = 93;
		public const int NULL = 94;
		public const int OBJECT = 95;
		public const int OPERATOR = 96;
		public const int OUT = 97;
		public const int OVERRIDE = 98;
		public const int PARAMS = 99;
		public const int PRIVATE = 100;
		public const int PROTECTED = 101;
		public const int PUBLIC = 102;
		public const int READONLY = 103;
		public const int REF = 104;
		public const int RETURN = 105;
		public const int SBYTE = 106;
		public const int SEALED = 107;
		public const int SHORT = 108;
		public const int SIZEOF = 109;
		public const int STACKALLOC = 110;
		public const int STATIC = 111;
		public const int STRING = 112;
		public const int STRUCT = 113;
		public const int SWITCH = 114;
		public const int THIS = 115;
		public const int THROW = 116;
		public const int TRY = 117;
		public const int TYPEOF = 118;
		public const int UINT = 119;
		public const int ULONG = 120;
		public const int UNCHECKED = 121;
		public const int UNSAFE = 122;
		public const int USHORT = 123;
		public const int USING = 124;
		public const int VIRTUAL = 125;
		public const int VOID = 126;
		public const int VOLATILE = 127;
		public const int WHILE = 128;
		public const int VAR = 129;
		public const int DOT = 130;
		public const int UINT_LITERAL = 131;
		public const int LONG_LITERAL = 132;
		public const int ULONG_LITERAL = 133;
		public const int DECIMAL_LITERAL = 134;
		public const int FLOAT_LITERAL = 135;
		public const int DOUBLE_LITERAL = 136;
		public const int LITERAL_add = 137;
		public const int LITERAL_remove = 138;
		public const int LITERAL_get = 139;
		public const int LITERAL_set = 140;
		public const int LITERAL_assembly = 141;
		public const int LITERAL_field = 142;
		public const int LITERAL_method = 143;
		public const int LITERAL_module = 144;
		public const int LITERAL_param = 145;
		public const int LITERAL_property = 146;
		public const int LITERAL_type = 147;
		public const int ML_COMMENT = 148;
		public const int IDENTIFIER = 149;
		public const int INT_LITERAL = 150;
		public const int CHAR_LITERAL = 151;
		public const int STRING_LITERAL = 152;
		public const int ESCAPED_LITERAL = 153;
		public const int OPEN_CURLY = 154;
		public const int CLOSE_CURLY = 155;
		public const int OPEN_BRACK = 156;
		public const int CLOSE_BRACK = 157;
		public const int COMMA = 158;
		public const int COLON = 159;
		public const int SEMI = 160;
		public const int PLUS = 161;
		public const int MINUS = 162;
		public const int STAR = 163;
		public const int DIV = 164;
		public const int MOD = 165;
		public const int BIN_AND = 166;
		public const int BIN_OR = 167;
		public const int BIN_XOR = 168;
		public const int BIN_NOT = 169;
		public const int ASSIGN = 170;
		public const int LTHAN = 171;
		public const int GTHAN = 172;
		public const int QUESTION = 173;
		public const int INC = 174;
		public const int DEC = 175;
		public const int SHIFTL = 176;
		public const int SHIFTR = 177;
		public const int LTE = 178;
		public const int GTE = 179;
		public const int PLUS_ASSIGN = 180;
		public const int MINUS_ASSIGN = 181;
		public const int STAR_ASSIGN = 182;
		public const int DIV_ASSIGN = 183;
		public const int MOD_ASSIGN = 184;
		public const int BIN_AND_ASSIGN = 185;
		public const int BIN_OR_ASSIGN = 186;
		public const int BIN_XOR_ASSIGN = 187;
		public const int SHIFTL_ASSIGN = 188;
		public const int SHIFTR_ASSIGN = 189;
		public const int DEREF = 190;
		public const int PP_DIRECTIVE = 191;
		public const int COMPILATION_UNIT = 192;
		public const int USING_DIRECTIVES = 193;
		public const int USING_ALIAS_DIRECTIVE = 194;
		public const int USING_NAMESPACE_DIRECTIVE = 195;
		public const int GLOBAL_ATTRIBUTE_SECTIONS = 196;
		public const int GLOBAL_ATTRIBUTE_SECTION = 197;
		public const int ATTRIBUTE_SECTIONS = 198;
		public const int ATTRIBUTE_SECTION = 199;
		public const int ATTRIBUTE = 200;
		public const int QUALIFIED_IDENTIFIER = 201;
		public const int POSITIONAL_ARGLIST = 202;
		public const int POSITIONAL_ARG = 203;
		public const int NAMED_ARGLIST = 204;
		public const int NAMED_ARG = 205;
		public const int ARG_LIST = 206;
		public const int FORMAL_PARAMETER_LIST = 207;
		public const int PARAMETER_FIXED = 208;
		public const int PARAMETER_ARRAY = 209;
		public const int ATTRIB_ARGUMENT_EXPR = 210;
		public const int UNARY_MINUS = 211;
		public const int UNARY_PLUS = 212;
		public const int CLASS_BASE = 213;
		public const int STRUCT_BASE = 214;
		public const int INTERFACE_BASE = 215;
		public const int ENUM_BASE = 216;
		public const int TYPE_BODY = 217;
		public const int MEMBER_LIST = 218;
		public const int CONST_DECLARATOR = 219;
		public const int CTOR_DECL = 220;
		public const int STATIC_CTOR_DECL = 221;
		public const int DTOR_DECL = 222;
		public const int FIELD_DECL = 223;
		public const int METHOD_DECL = 224;
		public const int PROPERTY_DECL = 225;
		public const int INDEXER_DECL = 226;
		public const int UNARY_OP_DECL = 227;
		public const int BINARY_OP_DECL = 228;
		public const int CONV_OP_DECL = 229;
		public const int TYPE = 230;
		public const int STARS = 231;
		public const int ARRAY_RANK = 232;
		public const int ARRAY_RANKS = 233;
		public const int ARRAY_INIT = 234;
		public const int VAR_INIT = 235;
		public const int VAR_INIT_LIST = 236;
		public const int VAR_DECLARATOR = 237;
		public const int LOCVAR_INIT = 238;
		public const int LOCVAR_INIT_LIST = 239;
		public const int LOCVAR_DECLS = 240;
		public const int LOCAL_CONST = 241;
		public const int EXPR = 242;
		public const int EXPR_LIST = 243;
		public const int MEMBER_ACCESS_EXPR = 244;
		public const int ELEMENT_ACCESS_EXPR = 245;
		public const int INVOCATION_EXPR = 246;
		public const int POST_INC_EXPR = 247;
		public const int POST_DEC_EXPR = 248;
		public const int PAREN_EXPR = 249;
		public const int OBJ_CREATE_EXPR = 250;
		public const int DLG_CREATE_EXPR = 251;
		public const int ARRAY_CREATE_EXPR = 252;
		public const int CAST_EXPR = 253;
		public const int PTR_ELEMENT_ACCESS_EXPR = 254;
		public const int PTR_INDIRECTION_EXPR = 255;
		public const int PTR_DECLARATOR = 256;
		public const int PTR_INIT = 257;
		public const int ADDRESS_OF_EXPR = 258;
		public const int MODIFIERS = 259;
		public const int NAMESPACE_BODY = 260;
		public const int BLOCK = 261;
		public const int STMT_LIST = 262;
		public const int EMPTY_STMT = 263;
		public const int LABEL_STMT = 264;
		public const int EXPR_STMT = 265;
		public const int FOR_INIT = 266;
		public const int FOR_COND = 267;
		public const int FOR_ITER = 268;
		public const int SWITCH_SECTION = 269;
		public const int SWITCH_LABELS = 270;
		public const int SWITCH_LABEL = 271;
		public const int PP_DIRECTIVES = 272;
		public const int PP_EXPR = 273;
		public const int PP_MESSAGE = 274;
		public const int PP_BLOCK = 275;
		
		
	//---------------------------------------------------------------------
	// PRIVATE DATA MEMBERS
	//---------------------------------------------------------------------
	///Location: Encapsulates in one object the line, column and filename
    // the ocurrence of an intem in the text program
		
	private Location getLocation(IToken i) { return new Location(fileinfo_.FullName, i.getLine(), i.getColumn()); }

	private FileInfo fileinfo_;
	
		
	private bool NotExcluded(CodeMaskEnums codeMask, CodeMaskEnums construct)
	{
		return ((codeMask & construct) != 0 );
	}
	
	public override void setFilename(string filename)
	{
		base.setFilename(filename);
		fileinfo_ = new FileInfo(filename);
	}

	private bool SingleLinePPDirectiveIsPredictedByLA(int lookAheadDepth)
	{
		if ((LA(lookAheadDepth) == PP_WARNING)  ||
			(LA(lookAheadDepth) == PP_ERROR)    ||
			(LA(lookAheadDepth) == PP_LINE)     ||
			(LA(lookAheadDepth) == PP_UNDEFINE) ||
			(LA(lookAheadDepth) == PP_DEFINE))
		{
			return true;
		}
		return false;
	}
	
	private bool PPDirectiveIsPredictedByLA(int lookAheadDepth)
	{
		if ((LA(lookAheadDepth) == PP_REGION)   ||
			(LA(lookAheadDepth) == PP_COND_IF)  ||
			(LA(lookAheadDepth) == PP_WARNING)  ||
			(LA(lookAheadDepth) == PP_ERROR)    ||
			(LA(lookAheadDepth) == PP_LINE)     ||
			(LA(lookAheadDepth) == PP_UNDEFINE) ||
			(LA(lookAheadDepth) == PP_DEFINE))
		{
			return true;
		}
		return false;
	}
	
	private bool IdentifierRuleIsPredictedByLA(int lookAheadDepth)
	{
		if ((LA(lookAheadDepth) == IDENTIFIER)       ||
			(LA(lookAheadDepth) == LITERAL_add)      ||
			(LA(lookAheadDepth) == LITERAL_remove)   ||
			(LA(lookAheadDepth) == LITERAL_get)      ||
			(LA(lookAheadDepth) == LITERAL_set)      ||
			(LA(lookAheadDepth) == LITERAL_assembly) ||
			(LA(lookAheadDepth) == LITERAL_field)    ||
			(LA(lookAheadDepth) == LITERAL_method)   ||
			(LA(lookAheadDepth) == LITERAL_module)   ||
			(LA(lookAheadDepth) == LITERAL_param)    ||
			(LA(lookAheadDepth) == LITERAL_property) ||
			(LA(lookAheadDepth) == LITERAL_type))
		{
			return true;
		}
		return false;
	}
	
	private bool TypeRuleIsPredictedByLA(int lookAheadDepth)
	{
		if ((LA(lookAheadDepth) == DOT)        ||
			(LA(lookAheadDepth) == VOID)       ||
			(LA(lookAheadDepth) == IDENTIFIER) ||
			(LA(lookAheadDepth) == INT)        ||
			(LA(lookAheadDepth) == BOOL)       ||
			(LA(lookAheadDepth) == STRING)     ||
			(LA(lookAheadDepth) == OBJECT)     ||
			(LA(lookAheadDepth) == BYTE)       ||
			(LA(lookAheadDepth) == CHAR)       ||
			(LA(lookAheadDepth) == DECIMAL)    ||
			(LA(lookAheadDepth) == DOUBLE)     ||
			(LA(lookAheadDepth) == FLOAT)      ||
			(LA(lookAheadDepth) == LONG)       ||
			(LA(lookAheadDepth) == SBYTE)      ||
			(LA(lookAheadDepth) == SHORT)      ||
			(LA(lookAheadDepth) == UINT)       ||
			(LA(lookAheadDepth) == ULONG)      ||
			(LA(lookAheadDepth) == VAR)        ||
			(LA(lookAheadDepth) == USHORT))
		{
			return true;
		}
		return false;
	}


	// * Error recovery
	/// <summary>
      ///  True when the parser enters in panic mode
      /// </summary>
    	private bool errorState = false;

      // * Overrides the 
    	/// <summary>
    	/// Overrides the behavior of match. Discards tokens when the parser is in panic mode
    	/// </summary>
    	/// <param name="token">The token to match</param>
    	public override void match(int token) {
        if (!errorState)
            base.match(token);
        else {
            while (this.LA(0) != token && this.LA(0) != EOF)
                this.consume();
            errorState = false;
        }
    }

		
		protected void initialize()
		{
			tokenNames = tokenNames_;
		}
		
		
		protected CSharpParser(TokenBuffer tokenBuf, int k) : base(tokenBuf, k)
		{
			initialize();
		}
		
		public CSharpParser(TokenBuffer tokenBuf) : this(tokenBuf,2)
		{
		}
		
		protected CSharpParser(TokenStream lexer, int k) : base(lexer,k)
		{
			initialize();
		}
		
		public CSharpParser(TokenStream lexer) : this(lexer,2)
		{
		}
		
		public CSharpParser(ParserSharedInputState state) : base(state,2)
		{
			initialize();
		}
		
	public SingleIdentifierExpression  nonKeywordLiterals() //throws RecognitionException, TokenStreamException
{
		SingleIdentifierExpression id = null;
		
		IToken  a = null;
		IToken  r = null;
		IToken  g = null;
		IToken  s = null;
		IToken  ab = null;
		IToken  f = null;
		IToken  m = null;
		IToken  mo = null;
		IToken  p = null;
		IToken  pr = null;
		IToken  t = null;
		
		switch ( LA(1) )
		{
		case LITERAL_add:
		{
			a = LT(1);
			match(LITERAL_add);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(a.getText(), getLocation(a));
			}
			break;
		}
		case LITERAL_remove:
		{
			r = LT(1);
			match(LITERAL_remove);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(r.getText(), getLocation(r));
			}
			break;
		}
		case LITERAL_get:
		{
			g = LT(1);
			match(LITERAL_get);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(g.getText(), getLocation(g));
			}
			break;
		}
		case LITERAL_set:
		{
			s = LT(1);
			match(LITERAL_set);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(s.getText(), getLocation(s));
			}
			break;
		}
		case LITERAL_assembly:
		{
			ab = LT(1);
			match(LITERAL_assembly);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(ab.getText(), getLocation(ab));
			}
			break;
		}
		case LITERAL_field:
		{
			f = LT(1);
			match(LITERAL_field);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(f.getText(), getLocation(f));
			}
			break;
		}
		case LITERAL_method:
		{
			m = LT(1);
			match(LITERAL_method);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(m.getText(), getLocation(m));
			}
			break;
		}
		case LITERAL_module:
		{
			mo = LT(1);
			match(LITERAL_module);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(mo.getText(), getLocation(mo));
			}
			break;
		}
		case LITERAL_param:
		{
			p = LT(1);
			match(LITERAL_param);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(p.getText(), getLocation(p));
			}
			break;
		}
		case LITERAL_property:
		{
			pr = LT(1);
			match(LITERAL_property);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(pr.getText(), getLocation(pr));
			}
			break;
		}
		case LITERAL_type:
		{
			t = LT(1);
			match(LITERAL_type);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(t.getText(), getLocation(t));
			}
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return id;
	}
	
	public SingleIdentifierExpression  identifier() //throws RecognitionException, TokenStreamException
{
		SingleIdentifierExpression id = null;
		
		IToken  i = null;
		
		switch ( LA(1) )
		{
		case IDENTIFIER:
		{
			i = LT(1);
			match(IDENTIFIER);
			if (0==inputState.guessing)
			{
				id = new SingleIdentifierExpression(i.getText(), getLocation(i));
			}
			break;
		}
		case LITERAL_add:
		case LITERAL_remove:
		case LITERAL_get:
		case LITERAL_set:
		case LITERAL_assembly:
		case LITERAL_field:
		case LITERAL_method:
		case LITERAL_module:
		case LITERAL_param:
		case LITERAL_property:
		case LITERAL_type:
		{
			id=nonKeywordLiterals();
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return id;
	}
	
	public IdentifierExpression  qualifiedIdentifier() //throws RecognitionException, TokenStreamException
{
		IdentifierExpression e = null;
		
		SingleIdentifierExpression i = null; IdentifierExpression q = null;
		
		i=identifier();
		{
			switch ( LA(1) )
			{
			case DOT:
			{
				match(DOT);
				q=qualifiedIdentifier();
				break;
			}
			case EOF:
			case OPEN_PAREN:
			case CLOSE_PAREN:
			case LOG_AND:
			case LOG_OR:
			case EQUAL:
			case NOT_EQUAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case OPEN_CURLY:
			case CLOSE_CURLY:
			case OPEN_BRACK:
			case CLOSE_BRACK:
			case COMMA:
			case COLON:
			case SEMI:
			case BIN_AND:
			case BIN_OR:
			case BIN_XOR:
			case ASSIGN:
			case QUESTION:
			case PLUS_ASSIGN:
			case MINUS_ASSIGN:
			case STAR_ASSIGN:
			case DIV_ASSIGN:
			case MOD_ASSIGN:
			case BIN_AND_ASSIGN:
			case BIN_OR_ASSIGN:
			case BIN_XOR_ASSIGN:
			case SHIFTL_ASSIGN:
			case SHIFTR_ASSIGN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			
					   if (q != null)
					      e = new QualifiedIdentifierExpression(i, q, i.Location);
					   else 
					      e = i;
					
		}
		return e;
	}
	
	public string  type() //throws RecognitionException, TokenStreamException
{
		string name = ""; ;
		
		IdentifierExpression qid = null; int rank = 0;
		
		{
			switch ( LA(1) )
			{
			case BOOL:
			case CHAR:
			case DOUBLE:
			case INT:
			case OBJECT:
			case STRING:
			case VAR:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			{
				{
					switch ( LA(1) )
					{
					case BOOL:
					case CHAR:
					case DOUBLE:
					case INT:
					case OBJECT:
					case STRING:
					case VAR:
					{
						name=predefinedTypeName();
						break;
					}
					case LITERAL_add:
					case LITERAL_remove:
					case LITERAL_get:
					case LITERAL_set:
					case LITERAL_assembly:
					case LITERAL_field:
					case LITERAL_method:
					case LITERAL_module:
					case LITERAL_param:
					case LITERAL_property:
					case LITERAL_type:
					case IDENTIFIER:
					{
						qid=qualifiedIdentifier();
						if (0==inputState.guessing)
						{
							name = qid.Identifier;
						}
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				break;
			}
			case VOID:
			{
				match(VOID);
				if (0==inputState.guessing)
				{
					name = "void";
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		rank=rankSpecifiers();
		if (0==inputState.guessing)
		{
			
					   if (rank != 0)
					   {
					      for (int i = 0; i < rank; i++) { name += "[]"; }
					   }
					
		}
		return name;
	}
	
	public string  predefinedTypeName() //throws RecognitionException, TokenStreamException
{
		string typeExp = ""; ;
		
		
		switch ( LA(1) )
		{
		case BOOL:
		{
			match(BOOL);
			if (0==inputState.guessing)
			{
				typeExp = "bool";
			}
			break;
		}
		case CHAR:
		{
			match(CHAR);
			if (0==inputState.guessing)
			{
				typeExp = "char";
			}
			break;
		}
		case DOUBLE:
		{
			match(DOUBLE);
			if (0==inputState.guessing)
			{
				typeExp = "double";
			}
			break;
		}
		case INT:
		{
			match(INT);
			if (0==inputState.guessing)
			{
				typeExp = "int";
			}
			break;
		}
		case OBJECT:
		{
			match(OBJECT);
			if (0==inputState.guessing)
			{
				typeExp = "object";
			}
			break;
		}
		case STRING:
		{
			match(STRING);
			if (0==inputState.guessing)
			{
				typeExp = "string";
			}
			break;
		}
		case VAR:
		{
			match(VAR);
			if (0==inputState.guessing)
			{
				typeExp = Convert.ToString(TypeVariable.NewTypeVariable);
			}
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return typeExp;
	}
	
	public int  rankSpecifiers() //throws RecognitionException, TokenStreamException
{
		int rank = 0;
		
		
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==OPEN_BRACK) && (LA(2)==CLOSE_BRACK||LA(2)==COMMA))
				{
					rankSpecifier();
					if (0==inputState.guessing)
					{
						rank++;
					}
				}
				else
				{
					goto _loop224_breakloop;
				}
				
			}
_loop224_breakloop:			;
		}    // ( ... )*
		return rank;
	}
	
	public CompoundExpression  argumentList() //throws RecognitionException, TokenStreamException
{
		CompoundExpression ce = null;
		
		Expression exp = null;
		
		exp=argument();
		if (0==inputState.guessing)
		{
			ce = new CompoundExpression(exp.Location); ce.AddExpression(exp);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					match(COMMA);
					exp=argument();
					if (0==inputState.guessing)
					{
						ce.AddExpression(exp);
					}
				}
				else
				{
					goto _loop10_breakloop;
				}
				
			}
_loop10_breakloop:			;
		}    // ( ... )*
		return ce;
	}
	
	public ArgumentExpression  argument() //throws RecognitionException, TokenStreamException
{
		ArgumentExpression arg = null;
		
		Expression exp = null;
		
		exp=expression();
		if (0==inputState.guessing)
		{
			arg = new ArgumentExpression(exp, exp.Location);
		}
		return arg;
	}
	
	public Expression  expression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		
		exp=assignmentExpression();
		return exp;
	}
	
	public Expression  constantExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		
		exp=expression();
		return exp;
	}
	
	public Expression  booleanExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		
		exp=expression();
		return exp;
	}
	
	public CompoundExpression  expressionList() //throws RecognitionException, TokenStreamException
{
		CompoundExpression ce = null;
		
		Expression exp = null;
		
		exp=expression();
		if (0==inputState.guessing)
		{
			ce = new CompoundExpression(exp.Location); ce.AddExpression(exp);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					match(COMMA);
					exp=expression();
					if (0==inputState.guessing)
					{
						ce.AddExpression(exp);
					}
				}
				else
				{
					goto _loop16_breakloop;
				}
				
			}
_loop16_breakloop:			;
		}    // ( ... )*
		return ce;
	}
	
	public Expression  assignmentExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp =  null;
		
		AssignmentOperator op = 0; BitwiseOperator opBit = 0; ArithmeticOperator opArith = 0; Expression exp1 = null; Expression exp2 = null;
		
		exp1=conditionalExpression();
		{
			switch ( LA(1) )
			{
			case ASSIGN:
			case PLUS_ASSIGN:
			case MINUS_ASSIGN:
			case STAR_ASSIGN:
			case DIV_ASSIGN:
			case MOD_ASSIGN:
			case BIN_AND_ASSIGN:
			case BIN_OR_ASSIGN:
			case BIN_XOR_ASSIGN:
			case SHIFTL_ASSIGN:
			case SHIFTR_ASSIGN:
			{
				{
					switch ( LA(1) )
					{
					case ASSIGN:
					{
						match(ASSIGN);
						if (0==inputState.guessing)
						{
							op = AssignmentOperator.Assign;
						}
						break;
					}
					case PLUS_ASSIGN:
					{
						match(PLUS_ASSIGN);
						if (0==inputState.guessing)
						{
							opArith = ArithmeticOperator.Plus;
						}
						break;
					}
					case MINUS_ASSIGN:
					{
						match(MINUS_ASSIGN);
						if (0==inputState.guessing)
						{
							opArith = ArithmeticOperator.Minus;
						}
						break;
					}
					case STAR_ASSIGN:
					{
						match(STAR_ASSIGN);
						if (0==inputState.guessing)
						{
							opArith = ArithmeticOperator.Mult;
						}
						break;
					}
					case DIV_ASSIGN:
					{
						match(DIV_ASSIGN);
						if (0==inputState.guessing)
						{
							opArith = ArithmeticOperator.Div;
						}
						break;
					}
					case MOD_ASSIGN:
					{
						match(MOD_ASSIGN);
						if (0==inputState.guessing)
						{
							opArith = ArithmeticOperator.Mod;
						}
						break;
					}
					case BIN_AND_ASSIGN:
					{
						match(BIN_AND_ASSIGN);
						if (0==inputState.guessing)
						{
							opBit = BitwiseOperator.BitwiseAnd;
						}
						break;
					}
					case BIN_OR_ASSIGN:
					{
						match(BIN_OR_ASSIGN);
						if (0==inputState.guessing)
						{
							opBit = BitwiseOperator.BitwiseOr;
						}
						break;
					}
					case BIN_XOR_ASSIGN:
					{
						match(BIN_XOR_ASSIGN);
						if (0==inputState.guessing)
						{
							opBit = BitwiseOperator.BitwiseXOr;
						}
						break;
					}
					case SHIFTL_ASSIGN:
					{
						match(SHIFTL_ASSIGN);
						if (0==inputState.guessing)
						{
							opBit = BitwiseOperator.ShiftLeft;
						}
						break;
					}
					case SHIFTR_ASSIGN:
					{
						match(SHIFTR_ASSIGN);
						if (0==inputState.guessing)
						{
							opBit = BitwiseOperator.ShiftRight;
						}
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				exp2=assignmentExpression();
				break;
			}
			case CLOSE_PAREN:
			case CLOSE_CURLY:
			case CLOSE_BRACK:
			case COMMA:
			case COLON:
			case SEMI:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			
			if (exp2 != null)
			{
			// exp1 ?= exp2 --> exp1 = exp1 ? exp2
			if (op == AssignmentOperator.Assign)
			exp = new AssignmentExpression(exp1, exp2, op, exp1.Location);
			else
			{
			if (opArith != 0)
			exp = new AssignmentExpression(exp1, new ArithmeticExpression(exp1.CloneInit(), exp2, opArith, exp1.Location), AssignmentOperator.Assign, exp1.Location); //  creo qeu es un error poner exp1
			else
			exp = new AssignmentExpression(exp1, new BitwiseExpression(exp1.CloneInit(), exp2, opBit, exp1.Location), AssignmentOperator.Assign, exp1.Location);
			}
			}
					   else
					      exp = exp1;
					
		}
		return exp;
	}
	
	public Expression  conditionalExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		Expression exp1 = null; Expression exp2 = null; Expression exp3 = null;
		
		exp1=conditionalOrExpression();
		{
			switch ( LA(1) )
			{
			case QUESTION:
			{
				match(QUESTION);
				exp2=assignmentExpression();
				match(COLON);
				exp3=conditionalExpression();
				if (0==inputState.guessing)
				{
					exp = new TernaryExpression(exp1, exp2, exp3, TernaryOperator.Question, exp1.Location);
				}
				break;
			}
			case CLOSE_PAREN:
			case CLOSE_CURLY:
			case CLOSE_BRACK:
			case COMMA:
			case COLON:
			case SEMI:
			case ASSIGN:
			case PLUS_ASSIGN:
			case MINUS_ASSIGN:
			case STAR_ASSIGN:
			case DIV_ASSIGN:
			case MOD_ASSIGN:
			case BIN_AND_ASSIGN:
			case BIN_OR_ASSIGN:
			case BIN_XOR_ASSIGN:
			case SHIFTL_ASSIGN:
			case SHIFTR_ASSIGN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			if (exp == null) exp = exp1;
		}
		return exp;
	}
	
	public Expression  conditionalOrExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		Expression exp1 = null; Expression exp2 = null;
		
		exp1=conditionalAndExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==LOG_OR))
				{
					match(LOG_OR);
					exp2=conditionalAndExpression();
					if (0==inputState.guessing)
					{
						exp1 = new LogicalExpression(exp1, exp2, LogicalOperator.Or, exp1.Location);
					}
				}
				else
				{
					goto _loop25_breakloop;
				}
				
			}
_loop25_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  conditionalAndExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		Expression exp1 = null; Expression exp2 = null;
		
		exp1=inclusiveOrExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==LOG_AND))
				{
					match(LOG_AND);
					exp2=inclusiveOrExpression();
					if (0==inputState.guessing)
					{
						exp1 = new LogicalExpression(exp1, exp2, LogicalOperator.And, exp1.Location);
					}
				}
				else
				{
					goto _loop28_breakloop;
				}
				
			}
_loop28_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  inclusiveOrExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		Expression exp1 = null; Expression exp2 = null;
		
		exp1=exclusiveOrExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==BIN_OR))
				{
					match(BIN_OR);
					exp2=exclusiveOrExpression();
					if (0==inputState.guessing)
					{
						exp1 = new BitwiseExpression(exp1, exp2, BitwiseOperator.BitwiseOr, exp1.Location);
					}
				}
				else
				{
					goto _loop31_breakloop;
				}
				
			}
_loop31_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  exclusiveOrExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		Expression exp1 = null; Expression exp2 = null;
		
		exp1=andExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==BIN_XOR))
				{
					match(BIN_XOR);
					exp2=andExpression();
					if (0==inputState.guessing)
					{
						exp1 = new BitwiseExpression(exp1, exp2, BitwiseOperator.BitwiseXOr, exp1.Location);
					}
				}
				else
				{
					goto _loop34_breakloop;
				}
				
			}
_loop34_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  andExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		Expression exp1 = null; Expression exp2 = null;
		
		exp1=equalityExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==BIN_AND))
				{
					match(BIN_AND);
					exp2=equalityExpression();
					if (0==inputState.guessing)
					{
						exp1 = new BitwiseExpression(exp1, exp2, BitwiseOperator.BitwiseAnd, exp1.Location);
					}
				}
				else
				{
					goto _loop37_breakloop;
				}
				
			}
_loop37_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  equalityExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		RelationalOperator op = 0; Expression exp1 = null; Expression exp2 = null;
		
		exp1=relationalExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==EQUAL||LA(1)==NOT_EQUAL))
				{
					{
						switch ( LA(1) )
						{
						case EQUAL:
						{
							match(EQUAL);
							if (0==inputState.guessing)
							{
								op = RelationalOperator.Equal;
							}
							break;
						}
						case NOT_EQUAL:
						{
							match(NOT_EQUAL);
							if (0==inputState.guessing)
							{
								op = RelationalOperator.NotEqual;
							}
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					exp2=relationalExpression();
					if (0==inputState.guessing)
					{
						exp1 = new RelationalExpression(exp1, exp2, op, exp1.Location);
					}
				}
				else
				{
					goto _loop41_breakloop;
				}
				
			}
_loop41_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  relationalExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		IToken  i = null;
		RelationalOperator op = 0; Expression exp1 = null; Expression exp2 = null; string typeExp = "";
		
		exp1=shiftExpression();
		{
			switch ( LA(1) )
			{
			case CLOSE_PAREN:
			case LOG_AND:
			case LOG_OR:
			case EQUAL:
			case NOT_EQUAL:
			case CLOSE_CURLY:
			case CLOSE_BRACK:
			case COMMA:
			case COLON:
			case SEMI:
			case BIN_AND:
			case BIN_OR:
			case BIN_XOR:
			case ASSIGN:
			case LTHAN:
			case GTHAN:
			case QUESTION:
			case LTE:
			case GTE:
			case PLUS_ASSIGN:
			case MINUS_ASSIGN:
			case STAR_ASSIGN:
			case DIV_ASSIGN:
			case MOD_ASSIGN:
			case BIN_AND_ASSIGN:
			case BIN_OR_ASSIGN:
			case BIN_XOR_ASSIGN:
			case SHIFTL_ASSIGN:
			case SHIFTR_ASSIGN:
			{
				{    // ( ... )*
					for (;;)
					{
						if ((tokenSet_0_.member(LA(1))))
						{
							{
								switch ( LA(1) )
								{
								case LTHAN:
								{
									match(LTHAN);
									if (0==inputState.guessing)
									{
										op = RelationalOperator.LessThan;
									}
									break;
								}
								case GTHAN:
								{
									match(GTHAN);
									if (0==inputState.guessing)
									{
										op = RelationalOperator.GreaterThan;
									}
									break;
								}
								case LTE:
								{
									match(LTE);
									if (0==inputState.guessing)
									{
										op = RelationalOperator.LessThanOrEqual;
									}
									break;
								}
								case GTE:
								{
									match(GTE);
									if (0==inputState.guessing)
									{
										op = RelationalOperator.GreaterThanOrEqual;
									}
									break;
								}
								default:
								{
									throw new NoViableAltException(LT(1), getFilename());
								}
								 }
							}
							exp2=additiveExpression();
							if (0==inputState.guessing)
							{
								exp1 = new RelationalExpression(exp1, exp2, op, exp1.Location);
							}
						}
						else
						{
							goto _loop46_breakloop;
						}
						
					}
_loop46_breakloop:					;
				}    // ( ... )*
				if (0==inputState.guessing)
				{
					exp = exp1;
				}
				break;
			}
			case IS:
			{
				i = LT(1);
				match(IS);
				typeExp=type();
				if (0==inputState.guessing)
				{
					exp = new IsExpression(exp1, typeExp, getLocation(i));
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return exp;
	}
	
	public Expression  shiftExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		BitwiseOperator op = 0; Expression exp1 = null; Expression exp2 = null;
		
		exp1=additiveExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==SHIFTL||LA(1)==SHIFTR))
				{
					{
						switch ( LA(1) )
						{
						case SHIFTL:
						{
							match(SHIFTL);
							if (0==inputState.guessing)
							{
								op = BitwiseOperator.ShiftLeft;
							}
							break;
						}
						case SHIFTR:
						{
							match(SHIFTR);
							if (0==inputState.guessing)
							{
								op = BitwiseOperator.ShiftRight;
							}
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					exp2=additiveExpression();
					if (0==inputState.guessing)
					{
						exp1 = new BitwiseExpression(exp1, exp2, op, exp1.Location);
					}
				}
				else
				{
					goto _loop50_breakloop;
				}
				
			}
_loop50_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  additiveExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		ArithmeticOperator op = 0; Expression exp1 = null; Expression exp2 = null;
		
		exp1=multiplicativeExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==PLUS||LA(1)==MINUS))
				{
					{
						switch ( LA(1) )
						{
						case PLUS:
						{
							match(PLUS);
							if (0==inputState.guessing)
							{
								op = ArithmeticOperator.Plus;
							}
							break;
						}
						case MINUS:
						{
							match(MINUS);
							if (0==inputState.guessing)
							{
								op = ArithmeticOperator.Minus;
							}
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					exp2=multiplicativeExpression();
					if (0==inputState.guessing)
					{
						exp1 = new ArithmeticExpression(exp1, exp2, op, exp1.Location);
					}
				}
				else
				{
					goto _loop54_breakloop;
				}
				
			}
_loop54_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  multiplicativeExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		ArithmeticOperator op = 0; Expression exp1 = null; Expression exp2 = null;
		
		exp1=unaryExpression();
		{    // ( ... )*
			for (;;)
			{
				if (((LA(1) >= STAR && LA(1) <= MOD)))
				{
					{
						switch ( LA(1) )
						{
						case STAR:
						{
							match(STAR);
							if (0==inputState.guessing)
							{
								op = ArithmeticOperator.Mult;
							}
							break;
						}
						case DIV:
						{
							match(DIV);
							if (0==inputState.guessing)
							{
								op = ArithmeticOperator.Div;
							}
							break;
						}
						case MOD:
						{
							match(MOD);
							if (0==inputState.guessing)
							{
								op = ArithmeticOperator.Mod;
							}
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					exp2=unaryExpression();
					if (0==inputState.guessing)
					{
						exp1 = new ArithmeticExpression(exp1, exp2, op, exp1.Location);
					}
				}
				else
				{
					goto _loop58_breakloop;
				}
				
			}
_loop58_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = exp1;
		}
		return exp;
	}
	
	public Expression  unaryExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		IToken  o = null;
		IToken  i = null;
		IToken  d = null;
		IToken  p = null;
		IToken  m = null;
		IToken  l = null;
		IToken  b = null;
		Expression e = null; string typeExp = "";
		
		switch ( LA(1) )
		{
		case INC:
		{
			i = LT(1);
			match(INC);
			e=unaryExpression();
			if (0==inputState.guessing)
			{
				
				// ++a -> a = a + 1;
				exp = new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, getLocation(i)), ArithmeticOperator.Plus, getLocation(i)), AssignmentOperator.Assign, getLocation(i));
				
			}
			break;
		}
		case DEC:
		{
			d = LT(1);
			match(DEC);
			e=unaryExpression();
			if (0==inputState.guessing)
			{
				
				// --a -> a = a - 1;
				exp = new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, getLocation(d)), ArithmeticOperator.Minus, getLocation(d)), AssignmentOperator.Assign, getLocation(d));
				
			}
			break;
		}
		case PLUS:
		{
			p = LT(1);
			match(PLUS);
			e=unaryExpression();
			if (0==inputState.guessing)
			{
				exp = new UnaryExpression(e, UnaryOperator.Plus, getLocation(p));
			}
			break;
		}
		case MINUS:
		{
			m = LT(1);
			match(MINUS);
			e=unaryExpression();
			if (0==inputState.guessing)
			{
				exp = new UnaryExpression(e, UnaryOperator.Minus, getLocation(m));
			}
			break;
		}
		case LOG_NOT:
		{
			l = LT(1);
			match(LOG_NOT);
			e=unaryExpression();
			if (0==inputState.guessing)
			{
				exp = new UnaryExpression(e, UnaryOperator.Not, getLocation(l));
			}
			break;
		}
		case BIN_NOT:
		{
			b = LT(1);
			match(BIN_NOT);
			e=unaryExpression();
			if (0==inputState.guessing)
			{
				exp = new UnaryExpression(e, UnaryOperator.BitwiseNot, getLocation(b));
			}
			break;
		}
		default:
			bool synPredMatched61 = false;
			if (((LA(1)==OPEN_PAREN) && (tokenSet_1_.member(LA(2)))))
			{
				int _m61 = mark();
				synPredMatched61 = true;
				inputState.guessing++;
				try {
					{
						match(OPEN_PAREN);
						type();
						match(CLOSE_PAREN);
						unaryExpression();
					}
				}
				catch (RecognitionException)
				{
					synPredMatched61 = false;
				}
				rewind(_m61);
				inputState.guessing--;
			}
			if ( synPredMatched61 )
			{
				o = LT(1);
				match(OPEN_PAREN);
				typeExp=type();
				match(CLOSE_PAREN);
				e=unaryExpression();
				if (0==inputState.guessing)
				{
					exp = new CastExpression(typeExp, e, getLocation(o));
				}
			}
			else if ((tokenSet_2_.member(LA(1))) && (tokenSet_3_.member(LA(2)))) {
				exp=primaryExpression();
			}
		else
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		break; }
		return exp;
	}
	
	public Expression  primaryExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		IToken  o = null;
		IToken  op = null;
		IToken  d = null;
		IToken  i = null;
		IToken  de = null;
		Expression exp1 = null; CompoundExpression ce = null; Expression e = null; SingleIdentifierExpression id = null;
		
		e=basicPrimaryExpression();
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_4_.member(LA(1))))
				{
					{
						switch ( LA(1) )
						{
						case OPEN_PAREN:
						{
							o = LT(1);
							match(OPEN_PAREN);
							{
								switch ( LA(1) )
								{
								case TRUE:
								case FALSE:
								case OPEN_PAREN:
								case LOG_NOT:
								case BASE:
								case NEW:
								case NULL:
								case THIS:
								case DOUBLE_LITERAL:
								case LITERAL_add:
								case LITERAL_remove:
								case LITERAL_get:
								case LITERAL_set:
								case LITERAL_assembly:
								case LITERAL_field:
								case LITERAL_method:
								case LITERAL_module:
								case LITERAL_param:
								case LITERAL_property:
								case LITERAL_type:
								case IDENTIFIER:
								case INT_LITERAL:
								case CHAR_LITERAL:
								case STRING_LITERAL:
								case PLUS:
								case MINUS:
								case BIN_NOT:
								case INC:
								case DEC:
								{
									ce=argumentList();
									break;
								}
								case CLOSE_PAREN:
								{
									break;
								}
								default:
								{
									throw new NoViableAltException(LT(1), getFilename());
								}
								 }
							}
							match(CLOSE_PAREN);
							if (0==inputState.guessing)
							{
								e = new InvocationExpression(e, ce, getLocation(o));
							}
							break;
						}
						case OPEN_BRACK:
						{
							op = LT(1);
							match(OPEN_BRACK);
							exp1=expression();
							match(CLOSE_BRACK);
							if (0==inputState.guessing)
							{
								e = new ArrayAccessExpression(e, exp1, getLocation(op));
							}
							break;
						}
						case DOT:
						{
							d = LT(1);
							match(DOT);
							id=identifier();
							if (0==inputState.guessing)
							{
								e = new FieldAccessExpression(e, id, getLocation(d));
							}
							break;
						}
						case INC:
						{
							i = LT(1);
							match(INC);
							if (0==inputState.guessing)
							{
								
								// a++ -> (a = a + 1) - 1
								Location l = getLocation(i);
								e = new ArithmeticExpression(new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, l), ArithmeticOperator.Plus, l), AssignmentOperator.Assign, l), new IntLiteralExpression(1, l), ArithmeticOperator.Minus, l);
								
							}
							break;
						}
						case DEC:
						{
							de = LT(1);
							match(DEC);
							if (0==inputState.guessing)
							{
								
								// a-- -> (a = a - 1) + 1
								Location l = getLocation(de);
								e = new ArithmeticExpression(new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, l), ArithmeticOperator.Minus, l), AssignmentOperator.Assign, l), new IntLiteralExpression(1, l), ArithmeticOperator.Plus, l);
								
							}
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
				}
				else
				{
					goto _loop69_breakloop;
				}
				
			}
_loop69_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			exp = e;
		}
		return exp;
	}
	
	public Expression  basicPrimaryExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		IToken  t = null;
		IToken  b = null;
		SingleIdentifierExpression id = null; Expression exp1 = null;
		
		{
			switch ( LA(1) )
			{
			case TRUE:
			case FALSE:
			case NULL:
			case DOUBLE_LITERAL:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			{
				exp=literal();
				break;
			}
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			{
				exp=identifier();
				break;
			}
			case OPEN_PAREN:
			{
				match(OPEN_PAREN);
				exp=assignmentExpression();
				match(CLOSE_PAREN);
				break;
			}
			case THIS:
			{
				t = LT(1);
				match(THIS);
				if (0==inputState.guessing)
				{
					exp = new ThisExpression(getLocation(t));
				}
				break;
			}
			case BASE:
			{
				b = LT(1);
				match(BASE);
				{
					switch ( LA(1) )
					{
					case DOT:
					{
						match(DOT);
						id=identifier();
						if (0==inputState.guessing)
						{
							exp = new FieldAccessExpression(new BaseExpression(getLocation(b)), id, getLocation(b));
						}
						break;
					}
					case OPEN_BRACK:
					{
						match(OPEN_BRACK);
						exp1=expression();
						match(CLOSE_BRACK);
						if (0==inputState.guessing)
						{
							exp = new ArrayAccessExpression(new BaseExpression(getLocation(b)), exp1, getLocation(b));
						}
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				break;
			}
			case NEW:
			{
				exp=newExpression();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return exp;
	}
	
	public Expression  literal() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		IToken  t = null;
		IToken  f = null;
		IToken  i = null;
		IToken  d = null;
		IToken  c = null;
		IToken  s = null;
		IToken  n = null;
		
		switch ( LA(1) )
		{
		case TRUE:
		{
			t = LT(1);
			match(TRUE);
			if (0==inputState.guessing)
			{
				exp = new BoolLiteralExpression(true, getLocation(t));
			}
			break;
		}
		case FALSE:
		{
			f = LT(1);
			match(FALSE);
			if (0==inputState.guessing)
			{
				exp = new BoolLiteralExpression(false, getLocation(f));
			}
			break;
		}
		case INT_LITERAL:
		{
			i = LT(1);
			match(INT_LITERAL);
			if (0==inputState.guessing)
			{
				exp = new IntLiteralExpression(Convert.ToInt32(i.getText()), getLocation(i));
			}
			break;
		}
		case DOUBLE_LITERAL:
		{
			d = LT(1);
			match(DOUBLE_LITERAL);
			if (0==inputState.guessing)
			{
				
					      NumberFormatInfo provider = new NumberFormatInfo();
					      provider.NumberDecimalSeparator =".";
					      exp = new DoubleLiteralExpression(Convert.ToDouble(d.getText(), provider), getLocation(d)); 
					
			}
			break;
		}
		case CHAR_LITERAL:
		{
			c = LT(1);
			match(CHAR_LITERAL);
			if (0==inputState.guessing)
			{
				
				char literal = '\\';
				string aux = c.getText().Substring(1, c.getText().Length - 2);
				if (aux.Length == 1)
				{
				literal = aux[0];
				}
				else   // Escape characters
				{
				switch (aux)
				{
				case "\\\'": literal = '\''; break;
				case "\\\"": literal = '\"'; break;
				case "\\\\": literal = '\\'; break;
				case "\\0": literal = '\0'; break;
				case "\\a": literal = '\a'; break;
				case "\\b": literal = '\b'; break;
				case "\\f": literal = '\f'; break;
				case "\\n": literal = '\n'; break;
				case "\\r": literal = '\r'; break;
				case "\\t": literal = '\t'; break;
				default: ErrorManager.Instance.NotifyError(new LexicalError("The character " + c.getText() + " is not allowed.", getLocation(c))); break;
				}
				}
				exp = new CharLiteralExpression(literal, getLocation(c)); 
					
			}
			break;
		}
		case STRING_LITERAL:
		{
			s = LT(1);
			match(STRING_LITERAL);
			if (0==inputState.guessing)
			{
				exp = new StringLiteralExpression(s.getText(), getLocation(s));
			}
			break;
		}
		case NULL:
		{
			n = LT(1);
			match(NULL);
			if (0==inputState.guessing)
			{
				exp = new NullExpression(getLocation(n));
			}
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return exp;
	}
	
	public Expression  newExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		IToken  n = null;
		CompoundExpression size = null; CompoundExpression ce = null; string typeExp = ""; int rank = 0;
		
		n = LT(1);
		match(NEW);
		typeExp=type();
		{
			switch ( LA(1) )
			{
			case OPEN_PAREN:
			{
				match(OPEN_PAREN);
				{
					switch ( LA(1) )
					{
					case TRUE:
					case FALSE:
					case OPEN_PAREN:
					case LOG_NOT:
					case BASE:
					case NEW:
					case NULL:
					case THIS:
					case DOUBLE_LITERAL:
					case LITERAL_add:
					case LITERAL_remove:
					case LITERAL_get:
					case LITERAL_set:
					case LITERAL_assembly:
					case LITERAL_field:
					case LITERAL_method:
					case LITERAL_module:
					case LITERAL_param:
					case LITERAL_property:
					case LITERAL_type:
					case IDENTIFIER:
					case INT_LITERAL:
					case CHAR_LITERAL:
					case STRING_LITERAL:
					case PLUS:
					case MINUS:
					case BIN_NOT:
					case INC:
					case DEC:
					{
						ce=argumentList();
						break;
					}
					case CLOSE_PAREN:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				match(CLOSE_PAREN);
				if (0==inputState.guessing)
				{
					exp = new NewExpression(typeExp, ce, getLocation(n));
				}
				break;
			}
			case OPEN_CURLY:
			{
				ce=arrayInitializer();
				if (0==inputState.guessing)
				{
					
								   exp = new NewArrayExpression(typeExp, getLocation(n)); ((NewArrayExpression)exp).Init = ce; 
								   
					string typeIdentifier = typeExp;
					while (typeIdentifier.Contains("[]"))
					{
					typeIdentifier = typeIdentifier.Substring(0, typeIdentifier.Length - 2);
					rank++;
					}
					
					((NewArrayExpression)exp).Rank = rank;
								
				}
				break;
			}
			case OPEN_BRACK:
			{
				match(OPEN_BRACK);
				size=expressionList();
				match(CLOSE_BRACK);
				rank=rankSpecifiers();
				{
					switch ( LA(1) )
					{
					case OPEN_CURLY:
					{
						ce=arrayInitializer();
						break;
					}
					case OPEN_PAREN:
					case CLOSE_PAREN:
					case LOG_AND:
					case LOG_OR:
					case EQUAL:
					case NOT_EQUAL:
					case IS:
					case DOT:
					case CLOSE_CURLY:
					case OPEN_BRACK:
					case CLOSE_BRACK:
					case COMMA:
					case COLON:
					case SEMI:
					case PLUS:
					case MINUS:
					case STAR:
					case DIV:
					case MOD:
					case BIN_AND:
					case BIN_OR:
					case BIN_XOR:
					case ASSIGN:
					case LTHAN:
					case GTHAN:
					case QUESTION:
					case INC:
					case DEC:
					case SHIFTL:
					case SHIFTR:
					case LTE:
					case GTE:
					case PLUS_ASSIGN:
					case MINUS_ASSIGN:
					case STAR_ASSIGN:
					case DIV_ASSIGN:
					case MOD_ASSIGN:
					case BIN_AND_ASSIGN:
					case BIN_OR_ASSIGN:
					case BIN_XOR_ASSIGN:
					case SHIFTL_ASSIGN:
					case SHIFTR_ASSIGN:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				if (0==inputState.guessing)
				{
					
								   exp = new NewArrayExpression(typeExp, getLocation(n)); 
								   if (size.ExpressionCount != 0)
								      ((NewArrayExpression)exp).Size = size.GetExpressionElement(0); 
								   ((NewArrayExpression)exp).Init = ce; ((NewArrayExpression)exp).Rank = rank + 1;
								
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return exp;
	}
	
	public CompoundExpression  arrayInitializer() //throws RecognitionException, TokenStreamException
{
		CompoundExpression ce = null;
		
		
		match(OPEN_CURLY);
		{
			switch ( LA(1) )
			{
			case CLOSE_CURLY:
			{
				match(CLOSE_CURLY);
				break;
			}
			case TRUE:
			case FALSE:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case NEW:
			case NULL:
			case THIS:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case OPEN_CURLY:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				ce=variableInitializerList();
				{
					switch ( LA(1) )
					{
					case COMMA:
					{
						match(COMMA);
						break;
					}
					case CLOSE_CURLY:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				match(CLOSE_CURLY);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return ce;
	}
	
	public void predefinedType() //throws RecognitionException, TokenStreamException
{
		
		
		{
			switch ( LA(1) )
			{
			case BOOL:
			{
				match(BOOL);
				break;
			}
			case CHAR:
			{
				match(CHAR);
				break;
			}
			case DOUBLE:
			{
				match(DOUBLE);
				break;
			}
			case INT:
			{
				match(INT);
				break;
			}
			case OBJECT:
			{
				match(OBJECT);
				break;
			}
			case STRING:
			{
				match(STRING);
				break;
			}
			case VAR:
			{
				match(VAR);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
	}
	
	public Statement  statement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		try {      // for error handling
			if (((tokenSet_5_.member(LA(1))) && (tokenSet_6_.member(LA(2))))&&( ((LA(1) == CONST) && TypeRuleIsPredictedByLA(2) && IdentifierRuleIsPredictedByLA(3)) ||
		  (TypeRuleIsPredictedByLA(1) && IdentifierRuleIsPredictedByLA(2)) ))
			{
				stat=declarationStatement();
			}
			else {
				bool synPredMatched81 = false;
				if (((tokenSet_5_.member(LA(1))) && (tokenSet_6_.member(LA(2)))))
				{
					int _m81 = mark();
					synPredMatched81 = true;
					inputState.guessing++;
					try {
						{
							{
								switch ( LA(1) )
								{
								case CONST:
								{
									match(CONST);
									break;
								}
								case BOOL:
								case CHAR:
								case DOUBLE:
								case INT:
								case OBJECT:
								case STRING:
								case VOID:
								case VAR:
								case LITERAL_add:
								case LITERAL_remove:
								case LITERAL_get:
								case LITERAL_set:
								case LITERAL_assembly:
								case LITERAL_field:
								case LITERAL_method:
								case LITERAL_module:
								case LITERAL_param:
								case LITERAL_property:
								case LITERAL_type:
								case IDENTIFIER:
								{
									break;
								}
								default:
								{
									throw new NoViableAltException(LT(1), getFilename());
								}
								 }
							}
							type();
							identifier();
						}
					}
					catch (RecognitionException)
					{
						synPredMatched81 = false;
					}
					rewind(_m81);
					inputState.guessing--;
				}
				if ( synPredMatched81 )
				{
					stat=declarationStatement();
				}
				else if ((tokenSet_7_.member(LA(1))) && (tokenSet_8_.member(LA(2)))) {
					stat=embeddedStatement();
				}
				else
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
			}
			catch (RecognitionException ex)
			{
				if (0 == inputState.guessing)
				{
					
					ErrorManager.Instance.NotifyError(new ParserError(new Location(ex.fileName, ex.line, ex.column), ex.Message));
					// * We enter into panic mode (discarding tokens)
							   if (this.errorState) 
								this.match(NEWLINE);
							   else
						this.errorState = true;
					
				}
				else
				{
					throw;
				}
			}
			return stat;
		}
		
	public DeclarationSet  declarationStatement() //throws RecognitionException, TokenStreamException
{
		DeclarationSet decls = null;
		
		List<Statement> ds = null;
		
		switch ( LA(1) )
		{
		case BOOL:
		case CHAR:
		case DOUBLE:
		case INT:
		case OBJECT:
		case STRING:
		case VOID:
		case VAR:
		case LITERAL_add:
		case LITERAL_remove:
		case LITERAL_get:
		case LITERAL_set:
		case LITERAL_assembly:
		case LITERAL_field:
		case LITERAL_method:
		case LITERAL_module:
		case LITERAL_param:
		case LITERAL_property:
		case LITERAL_type:
		case IDENTIFIER:
		{
			ds=localVariableDeclaration();
			match(SEMI);
			if (0==inputState.guessing)
			{
				decls = new DeclarationSet(((Declaration)ds[0]).FullName, ds, ds[0].Location);
			}
			break;
		}
		case CONST:
		{
			ds=localConstantDeclaration();
			match(SEMI);
			if (0==inputState.guessing)
			{
				decls = new DeclarationSet(((Declaration)ds[0]).FullName, ds, ds[0].Location);
			}
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return decls;
	}
	
	public Statement  embeddedStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		IToken  s = null;
		
		switch ( LA(1) )
		{
		case OPEN_CURLY:
		{
			stat=block();
			break;
		}
		case SEMI:
		{
			s = LT(1);
			match(SEMI);
			if (0==inputState.guessing)
			{
				stat = new Block(getLocation(s));
			}
			break;
		}
		case TRUE:
		case FALSE:
		case OPEN_PAREN:
		case LOG_NOT:
		case BASE:
		case NEW:
		case NULL:
		case THIS:
		case DOUBLE_LITERAL:
		case LITERAL_add:
		case LITERAL_remove:
		case LITERAL_get:
		case LITERAL_set:
		case LITERAL_assembly:
		case LITERAL_field:
		case LITERAL_method:
		case LITERAL_module:
		case LITERAL_param:
		case LITERAL_property:
		case LITERAL_type:
		case IDENTIFIER:
		case INT_LITERAL:
		case CHAR_LITERAL:
		case STRING_LITERAL:
		case PLUS:
		case MINUS:
		case BIN_NOT:
		case INC:
		case DEC:
		{
			stat=expressionStatement();
			break;
		}
		case IF:
		case SWITCH:
		{
			stat=selectionStatement();
			break;
		}
		case DO:
		case FOR:
		case FOREACH:
		case WHILE:
		{
			stat=iterationStatement();
			break;
		}
		case BREAK:
		case CONTINUE:
		case RETURN:
		case THROW:
		{
			stat=jumpStatement();
			break;
		}
		case TRY:
		{
			stat=tryStatement();
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return stat;
	}
	
	public Block  block() //throws RecognitionException, TokenStreamException
{
		Block stat = null;
		
		IToken  o = null;
		Statement s = null;
		
		o = LT(1);
		match(OPEN_CURLY);
		if (0==inputState.guessing)
		{
			stat = new Block(getLocation(o));
		}
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_9_.member(LA(1))))
				{
					s=statement();
					if (0==inputState.guessing)
					{
						stat.AddStatement(s);
					}
				}
				else
				{
					goto _loop86_breakloop;
				}
				
			}
_loop86_breakloop:			;
		}    // ( ... )*
		match(CLOSE_CURLY);
		return stat;
	}
	
	public Statement  expressionStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		stat=statementExpression();
		match(SEMI);
		return stat;
	}
	
	public Statement  selectionStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		switch ( LA(1) )
		{
		case IF:
		{
			stat=ifStatement();
			break;
		}
		case SWITCH:
		{
			stat=switchStatement();
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return stat;
	}
	
	public Statement  iterationStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		switch ( LA(1) )
		{
		case WHILE:
		{
			stat=whileStatement();
			break;
		}
		case DO:
		{
			stat=doStatement();
			break;
		}
		case FOR:
		{
			stat=forStatement();
			break;
		}
		case FOREACH:
		{
			stat=foreachStatement();
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return stat;
	}
	
	public Statement  jumpStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		switch ( LA(1) )
		{
		case BREAK:
		{
			stat=breakStatement();
			break;
		}
		case CONTINUE:
		{
			stat=continueStatement();
			break;
		}
		case RETURN:
		{
			stat=returnStatement();
			break;
		}
		case THROW:
		{
			stat=throwStatement();
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return stat;
	}
	
	public Statement  tryStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		IToken  t = null;
		ExceptionManagementStatement em = null; Block stats = null, f = null;  CatchStatement c = null; List<CatchStatement> l = new List<CatchStatement>();
		
		t = LT(1);
		match(TRY);
		stats=block();
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==CATCH))
				{
					c=catchClause();
					if (0==inputState.guessing)
					{
						l.Add(c);
					}
				}
				else
				{
					goto _loop150_breakloop;
				}
				
			}
_loop150_breakloop:			;
		}    // ( ... )*
		{
			switch ( LA(1) )
			{
			case FINALLY:
			{
				f=finallyClause();
				break;
			}
			case TRUE:
			case FALSE:
			case DEFAULT:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case BOOL:
			case BREAK:
			case CASE:
			case CHAR:
			case CONST:
			case CONTINUE:
			case DO:
			case DOUBLE:
			case ELSE:
			case FOR:
			case FOREACH:
			case IF:
			case INT:
			case NEW:
			case NULL:
			case OBJECT:
			case RETURN:
			case STRING:
			case SWITCH:
			case THIS:
			case THROW:
			case TRY:
			case VOID:
			case WHILE:
			case VAR:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case OPEN_CURLY:
			case CLOSE_CURLY:
			case SEMI:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			stat = em = new ExceptionManagementStatement (stats, l, f, getLocation(t));
		}
		return stat;
	}
	
	public Block  body() //throws RecognitionException, TokenStreamException
{
		Block stat = null;
		
		IToken  s = null;
		
		switch ( LA(1) )
		{
		case OPEN_CURLY:
		{
			stat=block();
			break;
		}
		case SEMI:
		{
			s = LT(1);
			match(SEMI);
			if (0==inputState.guessing)
			{
				stat = new Block(getLocation(s));
			}
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return stat;
	}
	
	public List<Statement>  statementList() //throws RecognitionException, TokenStreamException
{
		List<Statement> stats = new List<Statement>();
		
		Statement stat = null;
		
		{ // ( ... )+
			int _cnt89=0;
			for (;;)
			{
				if ((tokenSet_9_.member(LA(1))))
				{
					stat=statement();
					if (0==inputState.guessing)
					{
						stats.Add(stat);
					}
				}
				else
				{
					if (_cnt89 >= 1) { goto _loop89_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
				}
				
				_cnt89++;
			}
_loop89_breakloop:			;
		}    // ( ... )+
		return stats;
	}
	
	public List<Statement>  localVariableDeclaration() //throws RecognitionException, TokenStreamException
{
		List<Statement> statList = null;
		
		string typeExp = "";
		
		typeExp=type();
		statList=localVariableDeclarators(typeExp);
		return statList;
	}
	
	public List<Statement>  localConstantDeclaration() //throws RecognitionException, TokenStreamException
{
		List<Statement> statList = null;
		
		string typeExp = "";
		
		match(CONST);
		typeExp=type();
		statList=localConstantDeclarators(typeExp);
		return statList;
	}
	
	public List<Statement>  localVariableDeclarators(
		string type
	) //throws RecognitionException, TokenStreamException
{
		List<Statement> statList = new List<Statement>();
		
		Statement s1 = null;
		
		s1=localVariableDeclarator(type);
		if (0==inputState.guessing)
		{
			statList.Add(s1);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					if (0==inputState.guessing)
					{
						// Creates a new VariableType.
								     type = TypeSystem.TypeTable.ObtainNewType(type);
					}
					match(COMMA);
					s1=localVariableDeclarator(type);
					if (0==inputState.guessing)
					{
						statList.Add(s1);
					}
				}
				else
				{
					goto _loop94_breakloop;
				}
				
			}
_loop94_breakloop:			;
		}    // ( ... )*
		return statList;
	}
	
	public Declaration  localVariableDeclarator(
		string type
	) //throws RecognitionException, TokenStreamException
{
		Declaration decl = null;
		
		SingleIdentifierExpression id = null; Expression exp = null;
		
		id=identifier();
		{
			switch ( LA(1) )
			{
			case ASSIGN:
			{
				match(ASSIGN);
				exp=localVariableInitializer();
				break;
			}
			case COMMA:
			case SEMI:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			
				      if (exp == null)
				         decl = new IdDeclaration(id, type, id.Location);
				      else
				         decl = new Definition(id, type, exp, id.Location);
				
		}
		return decl;
	}
	
	public Expression  localVariableInitializer() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;;
		
		
		{
			switch ( LA(1) )
			{
			case TRUE:
			case FALSE:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case NEW:
			case NULL:
			case THIS:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				exp=expression();
				break;
			}
			case OPEN_CURLY:
			{
				exp=arrayInitializer();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return exp;
	}
	
	public List<Statement>  localConstantDeclarators(
		string type
	) //throws RecognitionException, TokenStreamException
{
		List<Statement> statList = new List<Statement>();
		
		Statement s1 = null;
		
		s1=localConstantDeclarator(type);
		if (0==inputState.guessing)
		{
			statList.Add(s1);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					if (0==inputState.guessing)
					{
						// Creates a new VariableType.
								     type = TypeSystem.TypeTable.ObtainNewType(type);
					}
					match(COMMA);
					s1=localConstantDeclarator(type);
					if (0==inputState.guessing)
					{
						statList.Add(s1);
					}
				}
				else
				{
					goto _loop102_breakloop;
				}
				
			}
_loop102_breakloop:			;
		}    // ( ... )*
		return statList;
	}
	
	public ConstantDefinition  localConstantDeclarator(
		string type
	) //throws RecognitionException, TokenStreamException
{
		ConstantDefinition cd = null;
		
		SingleIdentifierExpression id = null; Expression exp = null;
		
		id=identifier();
		match(ASSIGN);
		exp=constantExpression();
		if (0==inputState.guessing)
		{
			cd = new ConstantDefinition(id, type, exp, id.Location);
		}
		return cd;
	}
	
	public List<FieldDeclaration>  constantDeclarators(
		string type, List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		List<FieldDeclaration> decls = new List<FieldDeclaration>();
		
		FieldDeclaration decl = null;
		
		decl=constantDeclarator(type, mods);
		if (0==inputState.guessing)
		{
			decls.Add(decl);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					if (0==inputState.guessing)
					{
						// Creates a new VariableType.
								     type = TypeSystem.TypeTable.ObtainNewType(type);
					}
					match(COMMA);
					decl=constantDeclarator(type, mods);
					if (0==inputState.guessing)
					{
						decls.Add(decl);
					}
				}
				else
				{
					goto _loop106_breakloop;
				}
				
			}
_loop106_breakloop:			;
		}    // ( ... )*
		return decls;
	}
	
	public ConstantFieldDefinition  constantDeclarator(
		string type, List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		ConstantFieldDefinition cd = null;
		
		SingleIdentifierExpression id = null; Expression exp = null;
		
		id=identifier();
		match(ASSIGN);
		exp=constantExpression();
		if (0==inputState.guessing)
		{
			cd = new ConstantFieldDefinition(id, type, exp, mods, id.Location);
		}
		return cd;
	}
	
	public Expression  statementExpression() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		
		exp=assignmentExpression();
		return exp;
	}
	
	public Statement  ifStatement() //throws RecognitionException, TokenStreamException
{
		Statement st = null;
		
		IToken  i = null;
		Statement stat1 = null; Statement stat2 = null; Expression exp = null;
		
		i = LT(1);
		match(IF);
		match(OPEN_PAREN);
		exp=booleanExpression();
		match(CLOSE_PAREN);
		stat1=embeddedStatement();
		{
			if ((LA(1)==ELSE) && (tokenSet_7_.member(LA(2))))
			{
				stat2=elseStatement();
			}
			else if ((tokenSet_10_.member(LA(1))) && (tokenSet_11_.member(LA(2)))) {
			}
			else
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		if (0==inputState.guessing)
		{
			
					   if (stat2 == null)
					      st = new IfElseStatement(exp, stat1, getLocation(i));
					   else
					      st = new IfElseStatement(exp, stat1, stat2, getLocation(i));
					
		}
		return st;
	}
	
	public SwitchStatement  switchStatement() //throws RecognitionException, TokenStreamException
{
		SwitchStatement stat = null;
		
		IToken  s = null;
		Expression exp = null; List<SwitchSection> block = null;
		
		s = LT(1);
		match(SWITCH);
		match(OPEN_PAREN);
		exp=expression();
		match(CLOSE_PAREN);
		block=switchBlock();
		if (0==inputState.guessing)
		{
			stat = new SwitchStatement(exp, block, getLocation(s));
		}
		return stat;
	}
	
	public Statement  elseStatement() //throws RecognitionException, TokenStreamException
{
		Statement st = null;
		
		
		match(ELSE);
		st=embeddedStatement();
		return st;
	}
	
	public List<SwitchSection>  switchBlock() //throws RecognitionException, TokenStreamException
{
		List<SwitchSection> sections = new List<SwitchSection>();
		
		
		match(OPEN_CURLY);
		{
			switch ( LA(1) )
			{
			case DEFAULT:
			case CASE:
			{
				sections=switchSections();
				break;
			}
			case CLOSE_CURLY:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		match(CLOSE_CURLY);
		return sections;
	}
	
	public List<SwitchSection>  switchSections() //throws RecognitionException, TokenStreamException
{
		List<SwitchSection> sections = new List<SwitchSection>();
		
		SwitchSection section = null;
		
		{ // ( ... )+
			int _cnt119=0;
			for (;;)
			{
				if ((LA(1)==DEFAULT||LA(1)==CASE))
				{
					section=switchSection();
					if (0==inputState.guessing)
					{
						sections.Add(section);
					}
				}
				else
				{
					if (_cnt119 >= 1) { goto _loop119_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
				}
				
				_cnt119++;
			}
_loop119_breakloop:			;
		}    // ( ... )+
		return sections;
	}
	
	public SwitchSection  switchSection() //throws RecognitionException, TokenStreamException
{
		SwitchSection section = null;
		
		List<SwitchLabel> labels = null; List<Statement> stats = null;
		
		labels=switchLabels();
		stats=statementList();
		if (0==inputState.guessing)
		{
			section = new SwitchSection(labels, stats, labels[0].Location);
		}
		return section;
	}
	
	public List<SwitchLabel>  switchLabels() //throws RecognitionException, TokenStreamException
{
		List<SwitchLabel> labels = new List<SwitchLabel>();
		
		SwitchLabel label = null;
		
		{ // ( ... )+
			int _cnt123=0;
			for (;;)
			{
				if ((LA(1)==DEFAULT||LA(1)==CASE))
				{
					label=switchLabel();
					if (0==inputState.guessing)
					{
						labels.Add(label);
					}
				}
				else
				{
					if (_cnt123 >= 1) { goto _loop123_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
				}
				
				_cnt123++;
			}
_loop123_breakloop:			;
		}    // ( ... )+
		return labels;
	}
	
	public SwitchLabel  switchLabel() //throws RecognitionException, TokenStreamException
{
		SwitchLabel label = null;
		
		IToken  c = null;
		IToken  d = null;
		Expression exp = null;
		
		switch ( LA(1) )
		{
		case CASE:
		{
			c = LT(1);
			match(CASE);
			exp=constantExpression();
			match(COLON);
			if (0==inputState.guessing)
			{
				label = new SwitchLabel(exp, getLocation(c));
			}
			break;
		}
		case DEFAULT:
		{
			d = LT(1);
			match(DEFAULT);
			match(COLON);
			if (0==inputState.guessing)
			{
				label = new SwitchLabel(getLocation(d));
			}
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return label;
	}
	
	public Statement  whileStatement() //throws RecognitionException, TokenStreamException
{
		Statement s = null;
		
		IToken  w = null;
		Expression exp = null; Statement stat = null;
		
		w = LT(1);
		match(WHILE);
		match(OPEN_PAREN);
		exp=booleanExpression();
		match(CLOSE_PAREN);
		stat=embeddedStatement();
		if (0==inputState.guessing)
		{
			s = new WhileStatement(exp, stat, getLocation(w));
		}
		return s;
	}
	
	public Statement  doStatement() //throws RecognitionException, TokenStreamException
{
		Statement s = null;
		
		IToken  d = null;
		Expression exp = null; Statement stat = null;
		
		d = LT(1);
		match(DO);
		stat=embeddedStatement();
		match(WHILE);
		match(OPEN_PAREN);
		exp=booleanExpression();
		match(CLOSE_PAREN);
		match(SEMI);
		if (0==inputState.guessing)
		{
			s = new DoStatement(stat, exp, getLocation(d));
		}
		return s;
	}
	
	public Statement  forStatement() //throws RecognitionException, TokenStreamException
{
		Statement s = null;
		
		IToken  f = null;
		Statement stat = null; Expression cond = null; List<Statement> stats1 = null; List<Statement> stats2 = null;
		
		f = LT(1);
		match(FOR);
		match(OPEN_PAREN);
		stats1=forInitializer();
		match(SEMI);
		cond=forCondition();
		match(SEMI);
		stats2=forIterator();
		match(CLOSE_PAREN);
		stat=embeddedStatement();
		if (0==inputState.guessing)
		{
			s = new ForStatement(stats1, cond, stats2, stat, getLocation(f));
		}
		return s;
	}
	
	public Statement  foreachStatement() //throws RecognitionException, TokenStreamException
{
		Statement s = null;
		
		IToken  f = null;
		string typeExp = ""; SingleIdentifierExpression id = null; Expression e = null; Statement stat = null;
		
		f = LT(1);
		match(FOREACH);
		match(OPEN_PAREN);
		typeExp=type();
		id=identifier();
		match(IN);
		e=expression();
		match(CLOSE_PAREN);
		stat=embeddedStatement();
		if (0==inputState.guessing)
		{
			s = new ForeachStatement(typeExp, id, e, stat, getLocation(f));
		}
		return s;
	}
	
	public List<Statement>  forInitializer() //throws RecognitionException, TokenStreamException
{
		List<Statement> stat = null;
		
		
		{
			if (((tokenSet_1_.member(LA(1))) && (tokenSet_12_.member(LA(2))))&&( (TypeRuleIsPredictedByLA(1) && IdentifierRuleIsPredictedByLA(2)) ))
			{
				stat=localVariableDeclaration();
			}
			else {
				bool synPredMatched132 = false;
				if (((tokenSet_1_.member(LA(1))) && (tokenSet_12_.member(LA(2)))))
				{
					int _m132 = mark();
					synPredMatched132 = true;
					inputState.guessing++;
					try {
						{
							type();
							identifier();
						}
					}
					catch (RecognitionException)
					{
						synPredMatched132 = false;
					}
					rewind(_m132);
					inputState.guessing--;
				}
				if ( synPredMatched132 )
				{
					stat=localVariableDeclaration();
				}
				else if ((tokenSet_13_.member(LA(1))) && (tokenSet_14_.member(LA(2)))) {
					stat=statementExpressionList();
				}
				else if ((LA(1)==SEMI)) {
				}
				else
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
			}
			return stat;
		}
		
	public Expression  forCondition() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		
		{
			switch ( LA(1) )
			{
			case TRUE:
			case FALSE:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case NEW:
			case NULL:
			case THIS:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				exp=booleanExpression();
				break;
			}
			case SEMI:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return exp;
	}
	
	public List<Statement>  forIterator() //throws RecognitionException, TokenStreamException
{
		List<Statement> statList = null;
		
		
		{
			switch ( LA(1) )
			{
			case TRUE:
			case FALSE:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case NEW:
			case NULL:
			case THIS:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				statList=statementExpressionList();
				break;
			}
			case CLOSE_PAREN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return statList;
	}
	
	public List<Statement>  statementExpressionList() //throws RecognitionException, TokenStreamException
{
		List<Statement> statList = null;
		
		Statement st1 = null;
		
		st1=statementExpression();
		if (0==inputState.guessing)
		{
			statList = new List<Statement>(); statList.Add(st1);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					match(COMMA);
					st1=statementExpression();
					if (0==inputState.guessing)
					{
						statList.Add(st1);
					}
				}
				else
				{
					goto _loop139_breakloop;
				}
				
			}
_loop139_breakloop:			;
		}    // ( ... )*
		return statList;
	}
	
	public Statement  breakStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		IToken  b = null;
		
		b = LT(1);
		match(BREAK);
		match(SEMI);
		if (0==inputState.guessing)
		{
			stat = new BreakStatement(getLocation(b));
		}
		return stat;
	}
	
	public Statement  continueStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		IToken  c = null;
		
		c = LT(1);
		match(CONTINUE);
		match(SEMI);
		if (0==inputState.guessing)
		{
			stat = new ContinueStatement(getLocation(c));
		}
		return stat;
	}
	
	public Statement  returnStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		IToken  r = null;
		Expression exp = null;
		
		r = LT(1);
		match(RETURN);
		{
			switch ( LA(1) )
			{
			case TRUE:
			case FALSE:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case NEW:
			case NULL:
			case THIS:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				exp=expression();
				break;
			}
			case SEMI:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		match(SEMI);
		if (0==inputState.guessing)
		{
			stat = new ReturnStatement(exp, getLocation(r));
		}
		return stat;
	}
	
	public Statement  throwStatement() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		IToken  t = null;
		Expression exp = null;
		
		t = LT(1);
		match(THROW);
		{
			switch ( LA(1) )
			{
			case TRUE:
			case FALSE:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case NEW:
			case NULL:
			case THIS:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				exp=expression();
				break;
			}
			case SEMI:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		match(SEMI);
		if (0==inputState.guessing)
		{
			stat = new ThrowStatement(exp, getLocation(t));
		}
		return stat;
	}
	
	public CatchStatement  catchClause() //throws RecognitionException, TokenStreamException
{
		CatchStatement catchStatement = null;
		
		SingleIdentifierExpression id = null; Block stats = null; IdentifierExpression e = null;
		
		match(CATCH);
		match(OPEN_PAREN);
		e=qualifiedIdentifier();
		{
			switch ( LA(1) )
			{
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			{
				id=identifier();
				break;
			}
			case CLOSE_PAREN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		match(CLOSE_PAREN);
		stats=block();
		if (0==inputState.guessing)
		{
			
					catchStatement  = new CatchStatement(new IdDeclaration(id, e.Identifier, id.Location), stats, id.Location ) ;
					
		}
		return catchStatement;
	}
	
	public Block  finallyClause() //throws RecognitionException, TokenStreamException
{
		Block finallyBlock = null;
		
		
		match(FINALLY);
		finallyBlock=block();
		return finallyBlock;
	}
	
	public SourceFile  compilationUnit() //throws RecognitionException, TokenStreamException
{
		SourceFile sf = new SourceFile(new Location(fileinfo_.FullName, 0, 0));
		
		
		usingDirectives(sf);
		namespaceMemberDeclarations(sf);
		match(Token.EOF_TYPE);
		return sf;
	}
	
	public void usingDirectives(
		SourceFile sf
	) //throws RecognitionException, TokenStreamException
{
		
		IdentifierExpression e = null;
		
		{    // ( ... )*
			for (;;)
			{
				if (((LA(1)==USING))&&( !PPDirectiveIsPredictedByLA(1) ))
				{
					e=usingDirective();
					if (0==inputState.guessing)
					{
						sf.AddUsing(e.Identifier);
					}
				}
				else
				{
					goto _loop158_breakloop;
				}
				
			}
_loop158_breakloop:			;
		}    // ( ... )*
	}
	
	public void namespaceMemberDeclarations(
		SourceFile sf
	) //throws RecognitionException, TokenStreamException
{
		
		
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_15_.member(LA(1))))
				{
					namespaceMemberDeclaration(sf);
				}
				else
				{
					goto _loop162_breakloop;
				}
				
			}
_loop162_breakloop:			;
		}    // ( ... )*
	}
	
	public IdentifierExpression  usingDirective() //throws RecognitionException, TokenStreamException
{
		IdentifierExpression e = null;
		
		
		match(USING);
		e=qualifiedIdentifier();
		match(SEMI);
		return e;
	}
	
	public void namespaceMemberDeclaration(
		SourceFile sf
	) //throws RecognitionException, TokenStreamException
{
		
		Declaration decl = null; List<Modifier> mods = null;
		
		switch ( LA(1) )
		{
		case NAMESPACE:
		{
			namespaceDeclaration(sf);
			break;
		}
		case ABSTRACT:
		case CLASS:
		case INTERFACE:
		case INTERNAL:
		case NEW:
		case OVERRIDE:
		case PRIVATE:
		case PROTECTED:
		case PUBLIC:
		case STATIC:
		case VIRTUAL:
		{
			mods=modifiers();
			decl=typeDeclaration(mods);
			if (0==inputState.guessing)
			{
				sf.AddDeclaration(decl);
			}
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
	}
	
	public void namespaceDeclaration(
		SourceFile sf
	) //throws RecognitionException, TokenStreamException
{
		
		IdentifierExpression qid = null; List<Declaration> decls = null;
		
		match(NAMESPACE);
		qid=qualifiedIdentifier();
		decls=namespaceBody();
		{
			switch ( LA(1) )
			{
			case SEMI:
			{
				match(SEMI);
				break;
			}
			case EOF:
			case ABSTRACT:
			case CLASS:
			case INTERFACE:
			case INTERNAL:
			case NAMESPACE:
			case NEW:
			case OVERRIDE:
			case PRIVATE:
			case PROTECTED:
			case PUBLIC:
			case STATIC:
			case VIRTUAL:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			sf.AddNamespace(qid, decls);
		}
	}
	
	public List<Modifier>  modifiers() //throws RecognitionException, TokenStreamException
{
		List<Modifier> mods = new List<Modifier>();
		
		Modifier m;
		
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_16_.member(LA(1))))
				{
					m=modifier();
					if (0==inputState.guessing)
					{
						mods.Add(m);
					}
				}
				else
				{
					goto _loop172_breakloop;
				}
				
			}
_loop172_breakloop:			;
		}    // ( ... )*
		return mods;
	}
	
	public Declaration  typeDeclaration(
		List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		Declaration d = null;
		
		
		switch ( LA(1) )
		{
		case CLASS:
		{
			d=classDeclaration(mods);
			break;
		}
		case INTERFACE:
		{
			d=interfaceDeclaration(mods);
			break;
		}
		default:
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		 }
		return d;
	}
	
	public Declaration  classDeclaration(
		List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		Declaration decl = null;
		
		IToken  c = null;
		SingleIdentifierExpression id = null; List<string> bases = null; List<Declaration> decls = null;
		
		c = LT(1);
		match(CLASS);
		id=identifier();
		bases=classBase();
		decls=classBody();
		{
			switch ( LA(1) )
			{
			case SEMI:
			{
				match(SEMI);
				break;
			}
			case EOF:
			case ABSTRACT:
			case CLASS:
			case INTERFACE:
			case INTERNAL:
			case NAMESPACE:
			case NEW:
			case OVERRIDE:
			case PRIVATE:
			case PROTECTED:
			case PUBLIC:
			case STATIC:
			case VIRTUAL:
			case CLOSE_CURLY:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			decl = new ClassDefinition(id, mods, bases, decls, getLocation(c));
		}
		return decl;
	}
	
	public Declaration  interfaceDeclaration(
		List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		Declaration decl = null;
		
		IToken  i = null;
		SingleIdentifierExpression id = null; List<string> bases = null; List<Declaration> decls = null;
		
		i = LT(1);
		match(INTERFACE);
		id=identifier();
		bases=interfaceBase();
		decls=interfaceBody();
		{
			switch ( LA(1) )
			{
			case SEMI:
			{
				match(SEMI);
				break;
			}
			case EOF:
			case ABSTRACT:
			case CLASS:
			case INTERFACE:
			case INTERNAL:
			case NAMESPACE:
			case NEW:
			case OVERRIDE:
			case PRIVATE:
			case PROTECTED:
			case PUBLIC:
			case STATIC:
			case VIRTUAL:
			case CLOSE_CURLY:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			decl = new InterfaceDefinition(id, mods, bases, decls, getLocation(i));
		}
		return decl;
	}
	
	public List<Declaration>  namespaceBody() //throws RecognitionException, TokenStreamException
{
		List<Declaration> decls = new List<Declaration>();
		
		Declaration decl = null; List<Modifier> mods = null;
		
		match(OPEN_CURLY);
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_17_.member(LA(1))))
				{
					mods=modifiers();
					decl=typeDeclaration(mods);
					if (0==inputState.guessing)
					{
						decls.Add(decl);
					}
				}
				else
				{
					goto _loop169_breakloop;
				}
				
			}
_loop169_breakloop:			;
		}    // ( ... )*
		match(CLOSE_CURLY);
		return decls;
	}
	
	public Modifier  modifier() //throws RecognitionException, TokenStreamException
{
		Modifier mod = 0;;
		
		
		{
			switch ( LA(1) )
			{
			case ABSTRACT:
			{
				match(ABSTRACT);
				if (0==inputState.guessing)
				{
					mod = Modifier.Abstract;
				}
				break;
			}
			case NEW:
			{
				match(NEW);
				if (0==inputState.guessing)
				{
					mod = Modifier.New;
				}
				break;
			}
			case OVERRIDE:
			{
				match(OVERRIDE);
				if (0==inputState.guessing)
				{
					mod = Modifier.Override;
				}
				break;
			}
			case PUBLIC:
			{
				match(PUBLIC);
				if (0==inputState.guessing)
				{
					mod = Modifier.Public;
				}
				break;
			}
			case PROTECTED:
			{
				match(PROTECTED);
				if (0==inputState.guessing)
				{
					mod = Modifier.Protected;
				}
				break;
			}
			case INTERNAL:
			{
				match(INTERNAL);
				if (0==inputState.guessing)
				{
					mod = Modifier.Internal;
				}
				break;
			}
			case PRIVATE:
			{
				match(PRIVATE);
				if (0==inputState.guessing)
				{
					mod = Modifier.Private;
				}
				break;
			}
			case STATIC:
			{
				match(STATIC);
				if (0==inputState.guessing)
				{
					mod = Modifier.Static;
				}
				break;
			}
			case VIRTUAL:
			{
				match(VIRTUAL);
				if (0==inputState.guessing)
				{
					mod = Modifier.Virtual;
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return mod;
	}
	
	public List<string>  classBase() //throws RecognitionException, TokenStreamException
{
		List<string> bases = new List<string>();
		
		string typeExp = "";
		
		{
			switch ( LA(1) )
			{
			case COLON:
			{
				match(COLON);
				typeExp=type();
				if (0==inputState.guessing)
				{
					bases.Add(typeExp);
				}
				{    // ( ... )*
					for (;;)
					{
						if ((LA(1)==COMMA))
						{
							match(COMMA);
							typeExp=type();
							if (0==inputState.guessing)
							{
								bases.Add(typeExp);
							}
						}
						else
						{
							goto _loop180_breakloop;
						}
						
					}
_loop180_breakloop:					;
				}    // ( ... )*
				break;
			}
			case OPEN_CURLY:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return bases;
	}
	
	public List<Declaration>  classBody() //throws RecognitionException, TokenStreamException
{
		List<Declaration> decls = new List<Declaration>();
		
		
		match(OPEN_CURLY);
		decls=classMemberDeclarations();
		match(CLOSE_CURLY);
		return decls;
	}
	
	public List<Declaration>  classMemberDeclarations() //throws RecognitionException, TokenStreamException
{
		List<Declaration> decls = new List<Declaration>();
		
		Declaration d = null;
		
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_18_.member(LA(1))))
				{
					d=classMemberDeclaration();
					if (0==inputState.guessing)
					{
						
						if (!(d is FieldDeclarationSet))
						{
						decls.Add(d); 
						}
						else
						{
						for (int i = 0; i < ((FieldDeclarationSet)d).Count; i++)
						{
						decls.Add(((FieldDeclarationSet)d).GetDeclarationElement(i));
						}
						}
						
					}
				}
				else
				{
					goto _loop184_breakloop;
				}
				
			}
_loop184_breakloop:			;
		}    // ( ... )*
		return decls;
	}
	
	public Declaration  classMemberDeclaration() //throws RecognitionException, TokenStreamException
{
		Declaration decl = null;
		
		List<Modifier> mods = null;
		
		mods=modifiers();
		{
			decl=typeMemberDeclaration(mods);
		}
		return decl;
	}
	
	public Declaration  typeMemberDeclaration(
		List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		Declaration decl = null;
		
		IToken  c = null;
		Block stat = null; SingleIdentifierExpression id = null; string typeExp = ""; List<Parameter> pars = null; List<FieldDeclaration> decls = null; Statement []accessors = null; InvocationExpression iexp = null;
		
		if ((LA(1)==CONST))
		{
			c = LT(1);
			match(CONST);
			typeExp=type();
			decls=constantDeclarators(typeExp, mods);
			match(SEMI);
			if (0==inputState.guessing)
			{
				decl = new FieldDeclarationSet(typeExp, decls, getLocation(c));
			}
		}
		else if ((tokenSet_19_.member(LA(1))) && (LA(2)==OPEN_PAREN)) {
			id=identifier();
			match(OPEN_PAREN);
			{
				switch ( LA(1) )
				{
				case BOOL:
				case CHAR:
				case DOUBLE:
				case INT:
				case OBJECT:
				case STRING:
				case VOID:
				case VAR:
				case LITERAL_add:
				case LITERAL_remove:
				case LITERAL_get:
				case LITERAL_set:
				case LITERAL_assembly:
				case LITERAL_field:
				case LITERAL_method:
				case LITERAL_module:
				case LITERAL_param:
				case LITERAL_property:
				case LITERAL_type:
				case IDENTIFIER:
				{
					pars=formalParameterList();
					break;
				}
				case CLOSE_PAREN:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(CLOSE_PAREN);
			{
				switch ( LA(1) )
				{
				case COLON:
				{
					iexp=constructorInitializer();
					break;
				}
				case OPEN_CURLY:
				case SEMI:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			stat=constructorBody();
			if (0==inputState.guessing)
			{
				decl = new ConstructorDefinition(id, mods, pars, iexp, stat, id.Location);
			}
		}
		else if (((LA(1)==VOID) && (tokenSet_19_.member(LA(2))))&&( ((LA(1) == VOID) && (LA(2) != STAR)) )) {
			typeExp=voidAsType();
			id=identifier();
			match(OPEN_PAREN);
			{
				switch ( LA(1) )
				{
				case BOOL:
				case CHAR:
				case DOUBLE:
				case INT:
				case OBJECT:
				case STRING:
				case VOID:
				case VAR:
				case LITERAL_add:
				case LITERAL_remove:
				case LITERAL_get:
				case LITERAL_set:
				case LITERAL_assembly:
				case LITERAL_field:
				case LITERAL_method:
				case LITERAL_module:
				case LITERAL_param:
				case LITERAL_property:
				case LITERAL_type:
				case IDENTIFIER:
				{
					pars=formalParameterList();
					break;
				}
				case CLOSE_PAREN:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(CLOSE_PAREN);
			stat=methodBody();
			if (0==inputState.guessing)
			{
				decl = new MethodDefinition(id, stat, typeExp, pars, mods, id.Location);
			}
		}
		else if ((tokenSet_1_.member(LA(1))) && (tokenSet_12_.member(LA(2)))) {
			typeExp=type();
			{
				if (((tokenSet_19_.member(LA(1))) && (LA(2)==COMMA||LA(2)==SEMI||LA(2)==ASSIGN))&&( IdentifierRuleIsPredictedByLA(1) && (LA(2)==ASSIGN || LA(2)==SEMI ||LA(2)==COMMA) ))
				{
					decl=variableDeclarators(typeExp, mods);
					match(SEMI);
				}
				else if ((tokenSet_19_.member(LA(1))) && (LA(2)==OPEN_PAREN||LA(2)==OPEN_CURLY)) {
					id=identifier();
					{
						switch ( LA(1) )
						{
						case OPEN_CURLY:
						{
							match(OPEN_CURLY);
							accessors=accessorDeclarations();
							match(CLOSE_CURLY);
							if (0==inputState.guessing)
							{
								decl = new PropertyDefinition(id, typeExp, accessors[0], accessors[1], id.Location);
							}
							break;
						}
						case OPEN_PAREN:
						{
							match(OPEN_PAREN);
							{
								switch ( LA(1) )
								{
								case BOOL:
								case CHAR:
								case DOUBLE:
								case INT:
								case OBJECT:
								case STRING:
								case VOID:
								case VAR:
								case LITERAL_add:
								case LITERAL_remove:
								case LITERAL_get:
								case LITERAL_set:
								case LITERAL_assembly:
								case LITERAL_field:
								case LITERAL_method:
								case LITERAL_module:
								case LITERAL_param:
								case LITERAL_property:
								case LITERAL_type:
								case IDENTIFIER:
								{
									pars=formalParameterList();
									break;
								}
								case CLOSE_PAREN:
								{
									break;
								}
								default:
								{
									throw new NoViableAltException(LT(1), getFilename());
								}
								 }
							}
							match(CLOSE_PAREN);
							stat=methodBody();
							if (0==inputState.guessing)
							{
								decl = new MethodDefinition(id, stat, typeExp, pars, mods,  id.Location);
							}
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
				}
				else
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				
			}
		}
		else
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		
		return decl;
	}
	
	public List<Parameter>  formalParameterList() //throws RecognitionException, TokenStreamException
{
		List<Parameter> pars = null;
		
		
		{
			pars=fixedParameters();
		}
		return pars;
	}
	
	public InvocationExpression  constructorInitializer() //throws RecognitionException, TokenStreamException
{
		InvocationExpression e = null;
		
		IToken  c = null;
		IToken  b = null;
		IToken  t = null;
		CompoundExpression ce = null;
		
		c = LT(1);
		match(COLON);
		{
			switch ( LA(1) )
			{
			case BASE:
			{
				b = LT(1);
				match(BASE);
				match(OPEN_PAREN);
				{
					switch ( LA(1) )
					{
					case TRUE:
					case FALSE:
					case OPEN_PAREN:
					case LOG_NOT:
					case BASE:
					case NEW:
					case NULL:
					case THIS:
					case DOUBLE_LITERAL:
					case LITERAL_add:
					case LITERAL_remove:
					case LITERAL_get:
					case LITERAL_set:
					case LITERAL_assembly:
					case LITERAL_field:
					case LITERAL_method:
					case LITERAL_module:
					case LITERAL_param:
					case LITERAL_property:
					case LITERAL_type:
					case IDENTIFIER:
					case INT_LITERAL:
					case CHAR_LITERAL:
					case STRING_LITERAL:
					case PLUS:
					case MINUS:
					case BIN_NOT:
					case INC:
					case DEC:
					{
						ce=argumentList();
						break;
					}
					case CLOSE_PAREN:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				match(CLOSE_PAREN);
				if (0==inputState.guessing)
				{
					e = new InvocationExpression(new BaseExpression(getLocation(b)), ce, getLocation(c));
				}
				break;
			}
			case THIS:
			{
				t = LT(1);
				match(THIS);
				match(OPEN_PAREN);
				{
					switch ( LA(1) )
					{
					case TRUE:
					case FALSE:
					case OPEN_PAREN:
					case LOG_NOT:
					case BASE:
					case NEW:
					case NULL:
					case THIS:
					case DOUBLE_LITERAL:
					case LITERAL_add:
					case LITERAL_remove:
					case LITERAL_get:
					case LITERAL_set:
					case LITERAL_assembly:
					case LITERAL_field:
					case LITERAL_method:
					case LITERAL_module:
					case LITERAL_param:
					case LITERAL_property:
					case LITERAL_type:
					case IDENTIFIER:
					case INT_LITERAL:
					case CHAR_LITERAL:
					case STRING_LITERAL:
					case PLUS:
					case MINUS:
					case BIN_NOT:
					case INC:
					case DEC:
					{
						ce=argumentList();
						break;
					}
					case CLOSE_PAREN:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				match(CLOSE_PAREN);
				if (0==inputState.guessing)
				{
					e = new InvocationExpression(new ThisExpression(getLocation(t)), ce, getLocation(c));
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return e;
	}
	
	public Block  constructorBody() //throws RecognitionException, TokenStreamException
{
		Block stat = null;
		
		
		stat=body();
		return stat;
	}
	
	public string  voidAsType() //throws RecognitionException, TokenStreamException
{
		string name = "";
		
		
		match(VOID);
		if (0==inputState.guessing)
		{
			name = "void";
		}
		return name;
	}
	
	public Block  methodBody() //throws RecognitionException, TokenStreamException
{
		Block stat = null;
		
		
		stat=body();
		return stat;
	}
	
	public Declaration  variableDeclarators(
		string type, List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		Declaration fields = null;
		
		IToken  c = null;
		List<FieldDeclaration> decls = new List<FieldDeclaration>(); FieldDeclaration d = null;
		
		d=variableDeclarator(type, mods);
		if (0==inputState.guessing)
		{
			decls.Add(d);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					if (0==inputState.guessing)
					{
						// Creates a new VariableType.
								     type = TypeSystem.TypeTable.ObtainNewType(type); 
								
					}
					c = LT(1);
					match(COMMA);
					d=variableDeclarator(type, mods);
					if (0==inputState.guessing)
					{
						decls.Add(d);
					}
				}
				else
				{
					goto _loop196_breakloop;
				}
				
			}
_loop196_breakloop:			;
		}    // ( ... )*
		if (0==inputState.guessing)
		{
			
					   if (c == null)
					      fields = d;
					   else
					      fields = new FieldDeclarationSet(type, decls, getLocation(c)); 
					
		}
		return fields;
	}
	
	public Statement [] accessorDeclarations() //throws RecognitionException, TokenStreamException
{
		Statement []accessors = new Statement[2];
		
		Statement setStat = null; Statement getStat = null;
		
		{
			switch ( LA(1) )
			{
			case LITERAL_get:
			{
				getStat=getAccessorDeclaration();
				{
					switch ( LA(1) )
					{
					case LITERAL_set:
					{
						setStat=setAccessorDeclaration();
						break;
					}
					case CLOSE_CURLY:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				if (0==inputState.guessing)
				{
					accessors[0] = getStat; accessors[1] = setStat;
				}
				break;
			}
			case LITERAL_set:
			{
				setStat=setAccessorDeclaration();
				{
					switch ( LA(1) )
					{
					case LITERAL_get:
					{
						getStat=getAccessorDeclaration();
						break;
					}
					case CLOSE_CURLY:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				if (0==inputState.guessing)
				{
					accessors[0] = getStat; accessors[1] = setStat;
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return accessors;
	}
	
	public FieldDeclaration  variableDeclarator(
		string type, List<Modifier> mods
	) //throws RecognitionException, TokenStreamException
{
		FieldDeclaration d = null;
		
		Expression e = null; SingleIdentifierExpression id = null;
		
		id=identifier();
		{
			switch ( LA(1) )
			{
			case ASSIGN:
			{
				match(ASSIGN);
				e=variableInitializer();
				break;
			}
			case COMMA:
			case SEMI:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		if (0==inputState.guessing)
		{
			
				      if (e == null)
				         d = new FieldDeclaration(id, type, mods, id.Location);
				      else
				         d = new FieldDefinition(id, e, type, mods, id.Location);
				
		}
		return d;
	}
	
	public Expression  variableInitializer() //throws RecognitionException, TokenStreamException
{
		Expression exp = null;
		
		
		{
			switch ( LA(1) )
			{
			case TRUE:
			case FALSE:
			case OPEN_PAREN:
			case LOG_NOT:
			case BASE:
			case NEW:
			case NULL:
			case THIS:
			case DOUBLE_LITERAL:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			case INT_LITERAL:
			case CHAR_LITERAL:
			case STRING_LITERAL:
			case PLUS:
			case MINUS:
			case BIN_NOT:
			case INC:
			case DEC:
			{
				exp=expression();
				break;
			}
			case OPEN_CURLY:
			{
				exp=arrayInitializer();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return exp;
	}
	
	public string  returnType() //throws RecognitionException, TokenStreamException
{
		string typeExp = ""; ;
		
		
		if (((LA(1)==VOID) && (LA(2)==EOF))&&( ((LA(1) == VOID) && (LA(2) != STAR)) ))
		{
			typeExp=voidAsType();
		}
		else if ((tokenSet_1_.member(LA(1))) && (LA(2)==EOF||LA(2)==DOT||LA(2)==OPEN_BRACK)) {
			typeExp=type();
		}
		else
		{
			throw new NoViableAltException(LT(1), getFilename());
		}
		
		return typeExp;
	}
	
	public List<Parameter>  fixedParameters() //throws RecognitionException, TokenStreamException
{
		List<Parameter> pars = new List<Parameter>();
		
		Parameter p;
		
		p=fixedParameter();
		if (0==inputState.guessing)
		{
			pars.Add(p);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					match(COMMA);
					p=fixedParameter();
					if (0==inputState.guessing)
					{
						pars.Add(p);
					}
				}
				else
				{
					goto _loop207_breakloop;
				}
				
			}
_loop207_breakloop:			;
		}    // ( ... )*
		return pars;
	}
	
	public Parameter  fixedParameter() //throws RecognitionException, TokenStreamException
{
		Parameter param = new Parameter();
		
		SingleIdentifierExpression id = null; string typeExp = "";
		
		typeExp=type();
		id=identifier();
		if (0==inputState.guessing)
		{
			param.Identifier = id.Identifier; param.ParamType = typeExp; param.Line = id.Location.Line; param.Column = id.Location.Column;
		}
		return param;
	}
	
	public Statement  getAccessorDeclaration() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		match(LITERAL_get);
		stat=accessorBody();
		return stat;
	}
	
	public Statement  setAccessorDeclaration() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		match(LITERAL_set);
		stat=accessorBody();
		return stat;
	}
	
	public Statement  accessorBody() //throws RecognitionException, TokenStreamException
{
		Statement stat = null;
		
		
		stat=body();
		return stat;
	}
	
	public string  nonArrayType() //throws RecognitionException, TokenStreamException
{
		string typeExp = "";;
		
		
		typeExp=type();
		return typeExp;
	}
	
	public void rankSpecifier() //throws RecognitionException, TokenStreamException
{
		
		
		match(OPEN_BRACK);
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA))
				{
					match(COMMA);
				}
				else
				{
					goto _loop227_breakloop;
				}
				
			}
_loop227_breakloop:			;
		}    // ( ... )*
		match(CLOSE_BRACK);
	}
	
	public CompoundExpression  variableInitializerList() //throws RecognitionException, TokenStreamException
{
		CompoundExpression ce = null;
		
		Expression e1 = null;
		
		e1=variableInitializer();
		if (0==inputState.guessing)
		{
			ce = new CompoundExpression(e1.Location); ce.AddExpression(e1);
		}
		{    // ( ... )*
			for (;;)
			{
				if ((LA(1)==COMMA) && (tokenSet_20_.member(LA(2))))
				{
					match(COMMA);
					e1=variableInitializer();
					if (0==inputState.guessing)
					{
						ce.AddExpression(e1);
					}
				}
				else
				{
					goto _loop233_breakloop;
				}
				
			}
_loop233_breakloop:			;
		}    // ( ... )*
		return ce;
	}
	
	public List<string>  interfaceBase() //throws RecognitionException, TokenStreamException
{
		List<string> bases = new List<string>();
		
		string typeExp = "";
		
		{
			switch ( LA(1) )
			{
			case COLON:
			{
				match(COLON);
				typeExp=type();
				if (0==inputState.guessing)
				{
					bases.Add(typeExp);
				}
				{    // ( ... )*
					for (;;)
					{
						if ((LA(1)==COMMA))
						{
							match(COMMA);
							typeExp=type();
							if (0==inputState.guessing)
							{
								bases.Add(typeExp);
							}
						}
						else
						{
							goto _loop239_breakloop;
						}
						
					}
_loop239_breakloop:					;
				}    // ( ... )*
				break;
			}
			case OPEN_CURLY:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return bases;
	}
	
	public List<Declaration>  interfaceBody() //throws RecognitionException, TokenStreamException
{
		List<Declaration> decls = new List<Declaration>();
		
		
		match(OPEN_CURLY);
		decls=interfaceMemberDeclarations();
		match(CLOSE_CURLY);
		return decls;
	}
	
	public List<Declaration>  interfaceMemberDeclarations() //throws RecognitionException, TokenStreamException
{
		List<Declaration> decls = new List<Declaration>();
		
		Declaration d = null;
		
		{    // ( ... )*
			for (;;)
			{
				if ((tokenSet_21_.member(LA(1))))
				{
					d=interfaceMemberDeclaration();
					if (0==inputState.guessing)
					{
						decls.Add(d);
					}
				}
				else
				{
					goto _loop243_breakloop;
				}
				
			}
_loop243_breakloop:			;
		}    // ( ... )*
		return decls;
	}
	
	public Declaration  interfaceMemberDeclaration() //throws RecognitionException, TokenStreamException
{
		Declaration decl = null;
		
		SingleIdentifierExpression id = null; string typeExp = ""; List<Parameter> pars = null; List<Modifier> mod = new List<Modifier>(); Statement []accessors = null;
		
		{
			switch ( LA(1) )
			{
			case NEW:
			{
				match(NEW);
				if (0==inputState.guessing)
				{
					mod.Add(Modifier.New);
				}
				break;
			}
			case BOOL:
			case CHAR:
			case DOUBLE:
			case INT:
			case OBJECT:
			case STRING:
			case VOID:
			case VAR:
			case LITERAL_add:
			case LITERAL_remove:
			case LITERAL_get:
			case LITERAL_set:
			case LITERAL_assembly:
			case LITERAL_field:
			case LITERAL_method:
			case LITERAL_module:
			case LITERAL_param:
			case LITERAL_property:
			case LITERAL_type:
			case IDENTIFIER:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		{
			if (((LA(1)==VOID) && (tokenSet_19_.member(LA(2))))&&( ((LA(1) == VOID) && (LA(2) != STAR)) ))
			{
				typeExp=voidAsType();
				id=identifier();
				match(OPEN_PAREN);
				{
					switch ( LA(1) )
					{
					case BOOL:
					case CHAR:
					case DOUBLE:
					case INT:
					case OBJECT:
					case STRING:
					case VOID:
					case VAR:
					case LITERAL_add:
					case LITERAL_remove:
					case LITERAL_get:
					case LITERAL_set:
					case LITERAL_assembly:
					case LITERAL_field:
					case LITERAL_method:
					case LITERAL_module:
					case LITERAL_param:
					case LITERAL_property:
					case LITERAL_type:
					case IDENTIFIER:
					{
						pars=formalParameterList();
						break;
					}
					case CLOSE_PAREN:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				match(CLOSE_PAREN);
				match(SEMI);
				if (0==inputState.guessing)
				{
					decl = new MethodDeclaration(id, typeExp, pars, mod, id.Location);
				}
			}
			else if ((tokenSet_1_.member(LA(1))) && (tokenSet_12_.member(LA(2)))) {
				typeExp=type();
				{
					id=identifier();
					{
						switch ( LA(1) )
						{
						case OPEN_PAREN:
						{
							match(OPEN_PAREN);
							{
								switch ( LA(1) )
								{
								case BOOL:
								case CHAR:
								case DOUBLE:
								case INT:
								case OBJECT:
								case STRING:
								case VOID:
								case VAR:
								case LITERAL_add:
								case LITERAL_remove:
								case LITERAL_get:
								case LITERAL_set:
								case LITERAL_assembly:
								case LITERAL_field:
								case LITERAL_method:
								case LITERAL_module:
								case LITERAL_param:
								case LITERAL_property:
								case LITERAL_type:
								case IDENTIFIER:
								{
									pars=formalParameterList();
									break;
								}
								case CLOSE_PAREN:
								{
									break;
								}
								default:
								{
									throw new NoViableAltException(LT(1), getFilename());
								}
								 }
							}
							match(CLOSE_PAREN);
							match(SEMI);
							if (0==inputState.guessing)
							{
								decl = new MethodDeclaration(id, typeExp, pars, mod, id.Location);
							}
							break;
						}
						case OPEN_CURLY:
						{
							match(OPEN_CURLY);
							accessors=interfaceAccessors();
							match(CLOSE_CURLY);
							if (0==inputState.guessing)
							{
								decl = new PropertyDefinition(id, typeExp, accessors[0], accessors[1], id.Location);
							}
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
				}
			}
			else
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		return decl;
	}
	
	public Statement [] interfaceAccessors() //throws RecognitionException, TokenStreamException
{
		Statement []accessors = new Statement[2];
		
		Statement setStat = null; Statement getStat = null;
		
		{
			switch ( LA(1) )
			{
			case LITERAL_get:
			{
				getStat=getAccessorDeclaration();
				{
					switch ( LA(1) )
					{
					case LITERAL_set:
					{
						setStat=setAccessorDeclaration();
						break;
					}
					case CLOSE_CURLY:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				if (0==inputState.guessing)
				{
					accessors[0] = getStat; accessors[1] = setStat;
				}
				break;
			}
			case LITERAL_set:
			{
				setStat=setAccessorDeclaration();
				{
					switch ( LA(1) )
					{
					case LITERAL_get:
					{
						getStat=getAccessorDeclaration();
						break;
					}
					case CLOSE_CURLY:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					 }
				}
				if (0==inputState.guessing)
				{
					accessors[0] = getStat; accessors[1] = setStat;
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			 }
		}
		return accessors;
	}
	
	private void initializeFactory()
	{
	}
	
	public static readonly string[] tokenNames_ = new string[] {
		@"""<0>""",
		@"""EOF""",
		@"""<2>""",
		@"""NULL_TREE_LOOKAHEAD""",
		@"""UNICODE_CLASS_Nl""",
		@"""UNICODE_CLASS_Lt""",
		@"""UNICODE_CLASS_Zs""",
		@"""UNICODE_CLASS_Ll""",
		@"""UNICODE_CLASS_Lu""",
		@"""UNICODE_CLASS_Lo""",
		@"""UNICODE_CLASS_Lm""",
		@"""UNICODE_CLASS_Mn""",
		@"""UNICODE_CLASS_Mc""",
		@"""UNICODE_CLASS_Nd""",
		@"""UNICODE_CLASS_Pc""",
		@"""UNICODE_CLASS_Cf""",
		@"""true""",
		@"""false""",
		@"""default""",
		@"""PP_DEFINE""",
		@"""PP_UNDEFINE""",
		@"""PP_COND_IF""",
		@"""PP_COND_ELIF""",
		@"""PP_COND_ELSE""",
		@"""PP_COND_ENDIF""",
		@"""PP_LINE""",
		@"""PP_ERROR""",
		@"""PP_WARNING""",
		@"""PP_REGION""",
		@"""PP_ENDREGION""",
		@"""PP_FILENAME""",
		@"""PP_IDENT""",
		@"""PP_STRING""",
		@"""PP_NUMBER""",
		@"""WHITESPACE""",
		@"""QUOTE""",
		@"""OPEN_PAREN""",
		@"""CLOSE_PAREN""",
		@"""LOG_NOT""",
		@"""LOG_AND""",
		@"""LOG_OR""",
		@"""EQUAL""",
		@"""NOT_EQUAL""",
		@"""SL_COMMENT""",
		@"""NEWLINE""",
		@"""NOT_NEWLINE""",
		@"""NON_NEWLINE_WHITESPACE""",
		@"""UNICODE_ESCAPE_SEQUENCE""",
		@"""DECIMAL_DIGIT""",
		@"""HEX_DIGIT""",
		@"""LETTER_CHARACTER""",
		@"""DECIMAL_DIGIT_CHARACTER""",
		@"""CONNECTING_CHARACTER""",
		@"""COMBINING_CHARACTER""",
		@"""FORMATTING_CHARACTER""",
		@"""abstract""",
		@"""as""",
		@"""base""",
		@"""bool""",
		@"""break""",
		@"""byte""",
		@"""case""",
		@"""catch""",
		@"""char""",
		@"""checked""",
		@"""class""",
		@"""const""",
		@"""continue""",
		@"""decimal""",
		@"""delegate""",
		@"""do""",
		@"""double""",
		@"""else""",
		@"""enum""",
		@"""event""",
		@"""explicit""",
		@"""extern""",
		@"""finally""",
		@"""fixed""",
		@"""float""",
		@"""for""",
		@"""foreach""",
		@"""goto""",
		@"""if""",
		@"""implicit""",
		@"""in""",
		@"""int""",
		@"""interface""",
		@"""internal""",
		@"""is""",
		@"""lock""",
		@"""long""",
		@"""namespace""",
		@"""new""",
		@"""null""",
		@"""object""",
		@"""operator""",
		@"""out""",
		@"""override""",
		@"""params""",
		@"""private""",
		@"""protected""",
		@"""public""",
		@"""readonly""",
		@"""ref""",
		@"""return""",
		@"""sbyte""",
		@"""sealed""",
		@"""short""",
		@"""sizeof""",
		@"""stackalloc""",
		@"""static""",
		@"""string""",
		@"""struct""",
		@"""switch""",
		@"""this""",
		@"""throw""",
		@"""try""",
		@"""typeof""",
		@"""uint""",
		@"""ulong""",
		@"""unchecked""",
		@"""unsafe""",
		@"""ushort""",
		@"""using""",
		@"""virtual""",
		@"""void""",
		@"""volatile""",
		@"""while""",
		@"""var""",
		@"""DOT""",
		@"""UINT_LITERAL""",
		@"""LONG_LITERAL""",
		@"""ULONG_LITERAL""",
		@"""DECIMAL_LITERAL""",
		@"""FLOAT_LITERAL""",
		@"""DOUBLE_LITERAL""",
		@"""add""",
		@"""remove""",
		@"""get""",
		@"""set""",
		@"""assembly""",
		@"""field""",
		@"""method""",
		@"""module""",
		@"""param""",
		@"""property""",
		@"""type""",
		@"""ML_COMMENT""",
		@"""IDENTIFIER""",
		@"""INT_LITERAL""",
		@"""CHAR_LITERAL""",
		@"""STRING_LITERAL""",
		@"""ESCAPED_LITERAL""",
		@"""OPEN_CURLY""",
		@"""CLOSE_CURLY""",
		@"""OPEN_BRACK""",
		@"""CLOSE_BRACK""",
		@"""COMMA""",
		@"""COLON""",
		@"""SEMI""",
		@"""PLUS""",
		@"""MINUS""",
		@"""STAR""",
		@"""DIV""",
		@"""MOD""",
		@"""BIN_AND""",
		@"""BIN_OR""",
		@"""BIN_XOR""",
		@"""BIN_NOT""",
		@"""ASSIGN""",
		@"""LTHAN""",
		@"""GTHAN""",
		@"""QUESTION""",
		@"""INC""",
		@"""DEC""",
		@"""SHIFTL""",
		@"""SHIFTR""",
		@"""LTE""",
		@"""GTE""",
		@"""PLUS_ASSIGN""",
		@"""MINUS_ASSIGN""",
		@"""STAR_ASSIGN""",
		@"""DIV_ASSIGN""",
		@"""MOD_ASSIGN""",
		@"""BIN_AND_ASSIGN""",
		@"""BIN_OR_ASSIGN""",
		@"""BIN_XOR_ASSIGN""",
		@"""SHIFTL_ASSIGN""",
		@"""SHIFTR_ASSIGN""",
		@"""DEREF""",
		@"""PP_DIRECTIVE""",
		@"""COMPILATION_UNIT""",
		@"""USING_DIRECTIVES""",
		@"""USING_ALIAS_DIRECTIVE""",
		@"""USING_NAMESPACE_DIRECTIVE""",
		@"""GLOBAL_ATTRIBUTE_SECTIONS""",
		@"""GLOBAL_ATTRIBUTE_SECTION""",
		@"""ATTRIBUTE_SECTIONS""",
		@"""ATTRIBUTE_SECTION""",
		@"""ATTRIBUTE""",
		@"""QUALIFIED_IDENTIFIER""",
		@"""POSITIONAL_ARGLIST""",
		@"""POSITIONAL_ARG""",
		@"""NAMED_ARGLIST""",
		@"""NAMED_ARG""",
		@"""ARG_LIST""",
		@"""FORMAL_PARAMETER_LIST""",
		@"""PARAMETER_FIXED""",
		@"""PARAMETER_ARRAY""",
		@"""ATTRIB_ARGUMENT_EXPR""",
		@"""UNARY_MINUS""",
		@"""UNARY_PLUS""",
		@"""CLASS_BASE""",
		@"""STRUCT_BASE""",
		@"""INTERFACE_BASE""",
		@"""ENUM_BASE""",
		@"""TYPE_BODY""",
		@"""MEMBER_LIST""",
		@"""CONST_DECLARATOR""",
		@"""CTOR_DECL""",
		@"""STATIC_CTOR_DECL""",
		@"""DTOR_DECL""",
		@"""FIELD_DECL""",
		@"""METHOD_DECL""",
		@"""PROPERTY_DECL""",
		@"""INDEXER_DECL""",
		@"""UNARY_OP_DECL""",
		@"""BINARY_OP_DECL""",
		@"""CONV_OP_DECL""",
		@"""TYPE""",
		@"""STARS""",
		@"""ARRAY_RANK""",
		@"""ARRAY_RANKS""",
		@"""ARRAY_INIT""",
		@"""VAR_INIT""",
		@"""VAR_INIT_LIST""",
		@"""VAR_DECLARATOR""",
		@"""LOCVAR_INIT""",
		@"""LOCVAR_INIT_LIST""",
		@"""LOCVAR_DECLS""",
		@"""LOCAL_CONST""",
		@"""EXPR""",
		@"""EXPR_LIST""",
		@"""MEMBER_ACCESS_EXPR""",
		@"""ELEMENT_ACCESS_EXPR""",
		@"""INVOCATION_EXPR""",
		@"""POST_INC_EXPR""",
		@"""POST_DEC_EXPR""",
		@"""PAREN_EXPR""",
		@"""OBJ_CREATE_EXPR""",
		@"""DLG_CREATE_EXPR""",
		@"""ARRAY_CREATE_EXPR""",
		@"""CAST_EXPR""",
		@"""PTR_ELEMENT_ACCESS_EXPR""",
		@"""PTR_INDIRECTION_EXPR""",
		@"""PTR_DECLARATOR""",
		@"""PTR_INIT""",
		@"""ADDRESS_OF_EXPR""",
		@"""MODIFIERS""",
		@"""NAMESPACE_BODY""",
		@"""BLOCK""",
		@"""STMT_LIST""",
		@"""EMPTY_STMT""",
		@"""LABEL_STMT""",
		@"""EXPR_STMT""",
		@"""FOR_INIT""",
		@"""FOR_COND""",
		@"""FOR_ITER""",
		@"""SWITCH_SECTION""",
		@"""SWITCH_LABELS""",
		@"""SWITCH_LABEL""",
		@"""PP_DIRECTIVES""",
		@"""PP_EXPR""",
		@"""PP_MESSAGE""",
		@"""PP_BLOCK"""
	};
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = { 0L, 0L, 3404087999594496L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = { -8935141660703064064L, 4611967495555776640L, 3145218L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = { 144115256795529216L, 2251801424297984L, 32505600L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());
	private static long[] mk_tokenSet_3_()
	{
		long[] data = { -8791017745253466112L, 4614219297013629056L, 4611686018325675782L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_3_ = new BitSet(mk_tokenSet_3_());
	private static long[] mk_tokenSet_4_()
	{
		long[] data = { 68719476736L, 0L, 211106500968452L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_4_ = new BitSet(mk_tokenSet_4_());
	private static long[] mk_tokenSet_5_()
	{
		long[] data = { -8935141660703064064L, 4611967495555776644L, 3145218L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_5_ = new BitSet(mk_tokenSet_5_());
	private static long[] mk_tokenSet_6_()
	{
		long[] data = { -8935141660703064064L, 4611967495555776640L, 271580678L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_6_ = new BitSet(mk_tokenSet_6_());
	private static long[] mk_tokenSet_7_()
	{
		long[] data = { 720576283976859648L, 16890699237228616L, 213335420174081L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_7_ = new BitSet(mk_tokenSet_7_());
	private static long[] mk_tokenSet_8_()
	{
		long[] data = { -5908714121175040000L, 4628858194826559692L, 4611686014634688263L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_8_ = new BitSet(mk_tokenSet_8_());
	private static long[] mk_tokenSet_9_()
	{
		long[] data = { -8214565376726204416L, 4628858194793005260L, 213335420174083L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_9_ = new BitSet(mk_tokenSet_9_());
	private static long[] mk_tokenSet_10_()
	{
		long[] data = { -5908722367512248320L, 4628858194793005516L, 213335554391811L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_10_ = new BitSet(mk_tokenSet_10_());
	private static long[] mk_tokenSet_11_()
	{
		long[] data = { -1260999305728688128L, 6934842439761600972L, 4611686016782171911L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_11_ = new BitSet(mk_tokenSet_11_());
	private static long[] mk_tokenSet_12_()
	{
		long[] data = { 0L, 0L, 271580676L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_12_ = new BitSet(mk_tokenSet_12_());
	private static long[] mk_tokenSet_13_()
	{
		long[] data = { 144115531673436160L, 2251801424297984L, 213331058097920L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_13_ = new BitSet(mk_tokenSet_13_());
	private static long[] mk_tokenSet_14_()
	{
		long[] data = { -8791017882692419584L, 4614219297013629056L, 4611686015507103494L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_14_ = new BitSet(mk_tokenSet_14_());
	private static long[] mk_tokenSet_15_()
	{
		long[] data = { 36028797018963968L, 2305984245748727810L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_15_ = new BitSet(mk_tokenSet_15_());
	private static long[] mk_tokenSet_16_()
	{
		long[] data = { 36028797018963968L, 2305984245471903744L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_16_ = new BitSet(mk_tokenSet_16_());
	private static long[] mk_tokenSet_17_()
	{
		long[] data = { 36028797018963968L, 2305984245480292354L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_17_ = new BitSet(mk_tokenSet_17_());
	private static long[] mk_tokenSet_18_()
	{
		long[] data = { -8899112863684100096L, 6917951741027680388L, 3145218L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_18_ = new BitSet(mk_tokenSet_18_());
	private static long[] mk_tokenSet_19_()
	{
		long[] data = { 0L, 0L, 3145216L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_19_ = new BitSet(mk_tokenSet_19_());
	private static long[] mk_tokenSet_20_()
	{
		long[] data = { 144115531673436160L, 2251801424297984L, 213331125206784L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_20_ = new BitSet(mk_tokenSet_20_());
	private static long[] mk_tokenSet_21_()
	{
		long[] data = { -8935141660703064064L, 4611967496092647552L, 3145218L, 0L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_21_ = new BitSet(mk_tokenSet_21_());
	
}
}
