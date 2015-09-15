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
	public class CSharpTokenTypes
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
		
	}
}
