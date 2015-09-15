header
{
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
}

options
{
	language = "CSharp";	
	namespace = "Parser";
}

class CSharpParserDynamic extends Parser;

options
{
	k = 2;                               // two tokens of lookahead
	importVocab = CSharpLexerDynamic;
	exportVocab = CSharpDynamic;
	buildAST = false;
	//codeGenMakeSwitchThreshold = 5;    // Some optimizations
	//codeGenBitsetTestThreshold = 50;
	defaultErrorHandler = false;
	//defaultErrorHandler = true;
	classHeaderSuffix					= ICSharpParser;
}

tokens
{
	COMPILATION_UNIT;
	USING_DIRECTIVES;
	USING_ALIAS_DIRECTIVE;
	USING_NAMESPACE_DIRECTIVE;
	GLOBAL_ATTRIBUTE_SECTIONS;
	GLOBAL_ATTRIBUTE_SECTION;
	ATTRIBUTE_SECTIONS;
	ATTRIBUTE_SECTION;
	ATTRIBUTE;
	QUALIFIED_IDENTIFIER;
	POSITIONAL_ARGLIST;
	POSITIONAL_ARG;
	NAMED_ARGLIST;
	NAMED_ARG;
	ARG_LIST;
	FORMAL_PARAMETER_LIST;
	PARAMETER_FIXED;
	PARAMETER_ARRAY;
	ATTRIB_ARGUMENT_EXPR;
	UNARY_MINUS;
	UNARY_PLUS;
	CLASS_BASE;
	STRUCT_BASE;
	INTERFACE_BASE;
	ENUM_BASE;
	TYPE_BODY;
	MEMBER_LIST;
	CONST_DECLARATOR;
	CTOR_DECL;
	STATIC_CTOR_DECL;
	DTOR_DECL;
	FIELD_DECL;
	METHOD_DECL;
	PROPERTY_DECL;
	INDEXER_DECL;
	UNARY_OP_DECL;	
	BINARY_OP_DECL;	
	CONV_OP_DECL;	
	
	TYPE;
	STARS;
	ARRAY_RANK;
	ARRAY_RANKS;
	ARRAY_INIT;
	VAR_INIT;
	VAR_INIT_LIST;
	VAR_DECLARATOR;
	LOCVAR_INIT;
	LOCVAR_INIT_LIST;
	LOCVAR_DECLS;
	LOCAL_CONST;
	
	EXPR;
	EXPR_LIST;
	MEMBER_ACCESS_EXPR;
	ELEMENT_ACCESS_EXPR;
	INVOCATION_EXPR;
	POST_INC_EXPR;
	POST_DEC_EXPR;
	PAREN_EXPR;
	OBJ_CREATE_EXPR;
	DLG_CREATE_EXPR;
	ARRAY_CREATE_EXPR;
	CAST_EXPR;

	PTR_ELEMENT_ACCESS_EXPR;
	PTR_INDIRECTION_EXPR;
	PTR_DECLARATOR;
	PTR_INIT;
	ADDRESS_OF_EXPR;
	
	MODIFIERS;
	NAMESPACE_BODY;	
	BLOCK;
	STMT_LIST;
	EMPTY_STMT;
	LABEL_STMT;
	EXPR_STMT;
	
	FOR_INIT;
	FOR_COND;
	FOR_ITER;
	
	SWITCH_SECTION;
	SWITCH_LABELS;
	SWITCH_LABEL;

	PP_DIRECTIVES;
	PP_EXPR;
	PP_MESSAGE;
	PP_BLOCK;
}

{
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
			(LA(lookAheadDepth) == DYNAMIC)    ||
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

}

//=============================================================================
// Start of RULES
//=============================================================================

//
// C# LANGUAGE SPECIFICATION
//
// A.2 Syntactic grammar 
//
// The start rule for this grammar is 'compilationUnit'
//

//
// A.2.1 Basic concepts
//

nonKeywordLiterals returns [SingleIdentifierExpression id = null]
	:	a:"add"       { id = new SingleIdentifierExpression(a.getText(), getLocation(a)); }
	|	r:"remove"    { id = new SingleIdentifierExpression(r.getText(), getLocation(r)); }
	|	g:"get"       { id = new SingleIdentifierExpression(g.getText(), getLocation(g)); }
	|	s:"set"       { id = new SingleIdentifierExpression(s.getText(), getLocation(s)); }
	|	ab:"assembly" { id = new SingleIdentifierExpression(ab.getText(), getLocation(ab)); }
	|	f:"field"     { id = new SingleIdentifierExpression(f.getText(), getLocation(f)); }
	|	m:"method"    { id = new SingleIdentifierExpression(m.getText(), getLocation(m)); }
	|	mo:"module"   { id = new SingleIdentifierExpression(mo.getText(), getLocation(mo)); }
	|	p:"param"     { id = new SingleIdentifierExpression(p.getText(), getLocation(p)); }
	|	pr:"property" { id = new SingleIdentifierExpression(pr.getText(), getLocation(pr)); }
	|	t:"type"      { id = new SingleIdentifierExpression(t.getText(), getLocation(t)); }
	;
	
identifier returns [SingleIdentifierExpression id = null]
	:	i:IDENTIFIER { id = new SingleIdentifierExpression(i.getText(), getLocation(i)); }
	|	id = nonKeywordLiterals
	;
	
qualifiedIdentifier returns [IdentifierExpression e = null] { SingleIdentifierExpression i = null; IdentifierExpression q = null; }
	:	i = identifier	
		(	options { greedy = true; } :
			DOT^ q = qualifiedIdentifier
		)?
		{
		   if (q != null)
		      e = new QualifiedIdentifierExpression(i, q, i.Location);
		   else 
		      e = i;
		}
	;

/*
//
// A.2.2 Types
//
*/
type returns [string name = ""; ] { IdentifierExpression qid = null; int rank = 0; }
	:	(
			( name = predefinedTypeName | qid = qualifiedIdentifier { name = qid.Identifier; } )	   // typeName
//			( STAR )*					// pointerType
		|	VOID { name = "void"; } // STAR
		)
		rank = rankSpecifiers 				//	arrayType := nonArrayType rankSpecifiers
		{
		   if (rank != 0)
		   {
		      for (int i = 0; i < rank; i++) { name += "[]"; }
		   }
		}
	;
	
// integralType
//	:	(	SBYTE
//		|	BYTE
//		|	SHORT
//		|	USHORT
//		|	INT
//		|	UINT
//		|	LONG
//		|	ULONG
//		|	CHAR
//		|   VAR
//		)
//	;
	
 //classType { IdentifierExpression qid = null; }
 //:	(	qid = qualifiedIdentifier		// typeName
//		|	OBJECT
//		|	STRING
//		|  VAR
// 		)
//	;

/*	
//
// A.2.4 Expressions
//
*/
argumentList returns [CompoundExpression ce = null] { Expression exp = null; }
	:	exp = argument { ce = new CompoundExpression(exp.Location); ce.AddExpression(exp); } 
	   ( COMMA exp = argument { ce.AddExpression(exp); } )*
	;
	
argument returns [ArgumentExpression arg = null] { Expression exp = null; }
	:	exp = expression { arg = new ArgumentExpression(exp, exp.Location); }
//	|	REF exp = expression { arg = new ArgumentExpression(exp, ArgumentOptions.Ref, exp.Location); }
//	|	OUT exp = expression { arg = new ArgumentExpression(exp, ArgumentOptions.Out, exp.Location); }
	;

constantExpression returns [Expression exp = null]
	:	exp = expression
	;
	
booleanExpression returns [Expression exp = null]
	:	exp = expression
	;
	
expressionList returns [CompoundExpression ce = null] { Expression exp = null; }
	:	exp = expression  { ce = new CompoundExpression(exp.Location); ce.AddExpression(exp); } 
	   ( COMMA exp = expression { ce.AddExpression(exp); } )*
	;

/*	
//======================================
// 14.2.1 Operator precedence and associativity
//
// The following table summarizes all operators in order of precedence from lowest to highest:
//
// PRECEDENCE     SECTION  CATEGORY                     OPERATORS
//  lowest  (14)  14.13    Assignment                   = *= /= %= += -= <<= >>= &= ^= |=
//          (13)  14.12    Conditional                  ?:
//          (12)  14.11    Conditional OR               ||
//          (11)  14.11    Conditional AND              &&
//          (10)  14.10    Logical OR                   |
//          ( 9)  14.10    Logical XOR                  ^
//          ( 8)  14.10    Logical AND                  &
//          ( 7)  14.9     Equality                     == !=
//          ( 6)  14.9     Relational and type-testing  < > <= >= is as
//          ( 5)  14.8     Shift                        << >>
//          ( 4)  14.7     Additive                     +{binary} -{binary}
//          ( 3)  14.7     Multiplicative               * / %
//          ( 2)  14.6     Unary                        +{unary} -{unary} ! ~ ++x --x (T)x
//  highest ( 1)  14.5     Primary                      x.y f(x) a[x] x++ x-- new
//                                                      typeof checked unchecked
//
// NOTE: In accordance with lessons gleaned from the "java.g" file supplied with ANTLR, 
//       I have applied the following pattern to the rules for expressions:
// 
//           thisLevelExpression :
//               nextHigherPrecedenceExpression (OPERATOR nextHigherPrecedenceExpression)*
//
//       which is a standard recursive definition for a parsing an expression.
//
*/

expression returns [Expression exp = null]
	:	exp = assignmentExpression
	;

assignmentExpression returns [Expression exp =  null] { AssignmentOperator op = 0; BitwiseOperator opBit = 0; ArithmeticOperator opArith = 0; Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = conditionalExpression 
		(	(	ASSIGN {op = AssignmentOperator.Assign; }

			|	PLUS_ASSIGN  {opArith = ArithmeticOperator.Plus; }
			|	MINUS_ASSIGN {opArith = ArithmeticOperator.Minus; }
			|	STAR_ASSIGN  {opArith = ArithmeticOperator.Mult; }
			|	DIV_ASSIGN   {opArith = ArithmeticOperator.Div; }
			|	MOD_ASSIGN   {opArith = ArithmeticOperator.Mod; }

			|	BIN_AND_ASSIGN {opBit = BitwiseOperator.BitwiseAnd; }
			|	BIN_OR_ASSIGN  {opBit = BitwiseOperator.BitwiseOr; }
			|	BIN_XOR_ASSIGN {opBit = BitwiseOperator.BitwiseXOr; }
			|	SHIFTL_ASSIGN  {opBit = BitwiseOperator.ShiftLeft; }
			|	SHIFTR_ASSIGN  {opBit = BitwiseOperator.ShiftRight; }
			) 
			exp2 = assignmentExpression 
		)?		
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
	;	


conditionalExpression returns [Expression exp = null] { Expression exp1 = null; Expression exp2 = null; Expression exp3 = null; }
:	exp1 = conditionalOrExpression 
   ( QUESTION exp2 = assignmentExpression COLON exp3 = conditionalExpression { exp = new TernaryExpression(exp1, exp2, exp3, TernaryOperator.Question, exp1.Location); } )?
   { if (exp == null) exp = exp1; }
;
	
conditionalOrExpression  returns [Expression exp = null] { Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = conditionalAndExpression 
	   ( LOG_OR exp2 = conditionalAndExpression 
        { exp1 = new LogicalExpression(exp1, exp2, LogicalOperator.Or, exp1.Location); } 	   
	   )*
	   { exp = exp1; }
	;

conditionalAndExpression returns [Expression exp = null] { Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = inclusiveOrExpression 
	   ( LOG_AND exp2 = inclusiveOrExpression 
	     { exp1 = new LogicalExpression(exp1, exp2, LogicalOperator.And, exp1.Location); } 	   
	   )*
	   { exp = exp1; }
	;
	
inclusiveOrExpression returns [Expression exp = null] { Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = exclusiveOrExpression 
	   ( BIN_OR exp2 = exclusiveOrExpression 
	     { exp1 = new BitwiseExpression(exp1, exp2, BitwiseOperator.BitwiseOr, exp1.Location); } 	   
	   )*
		{ exp = exp1; }
	;
	
exclusiveOrExpression returns [Expression exp = null] { Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = andExpression 
	   ( BIN_XOR exp2 = andExpression 
	     { exp1 = new BitwiseExpression(exp1, exp2, BitwiseOperator.BitwiseXOr, exp1.Location); } 
	   )*
	   { exp = exp1; }
	;
	
andExpression returns [Expression exp = null] { Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = equalityExpression 
	   ( BIN_AND exp2 = equalityExpression 
	     { exp1 = new BitwiseExpression(exp1, exp2, BitwiseOperator.BitwiseAnd, exp1.Location); } 
	   )*
	   { exp = exp1; }
	;
	
equalityExpression returns [Expression exp = null] { RelationalOperator op = 0; Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = relationalExpression 
	   ( 
	      ( EQUAL {op = RelationalOperator.Equal; }
	      | NOT_EQUAL {op = RelationalOperator.NotEqual; }
	      ) exp2 = relationalExpression { exp1 = new RelationalExpression(exp1, exp2, op, exp1.Location); }
	   )*
	   { exp = exp1; }
	;
	
relationalExpression returns [Expression exp = null] { RelationalOperator op = 0; Expression exp1 = null; Expression exp2 = null; string typeExp = ""; }
	:	exp1 = shiftExpression 
		( ( 
		     ( LTHAN { op = RelationalOperator.LessThan; }
		     | GTHAN { op = RelationalOperator.GreaterThan; }
		     | LTE   { op = RelationalOperator.LessThanOrEqual; }
		     | GTE   { op = RelationalOperator.GreaterThanOrEqual; }
		     ) exp2 = additiveExpression { exp1 = new RelationalExpression(exp1, exp2, op, exp1.Location); }
		  )* { exp = exp1; }
		| i:IS typeExp = type          // ( IS | AS ) typeExp = type
		  { exp = new IsExpression(exp1, typeExp, getLocation(i)); }
		)
	;
	
shiftExpression returns [Expression exp = null] { BitwiseOperator op = 0; Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = additiveExpression 
	   (
	      ( SHIFTL { op = BitwiseOperator.ShiftLeft; }
	      | SHIFTR { op = BitwiseOperator.ShiftRight; }
	      ) exp2 = additiveExpression { exp1 = new BitwiseExpression(exp1, exp2, op, exp1.Location); }
	   )*
	   { exp = exp1; }
	;
	
additiveExpression returns [Expression exp = null] { ArithmeticOperator op = 0; Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = multiplicativeExpression 
	   (
	      ( PLUS {op = ArithmeticOperator.Plus; }
	      | MINUS {op = ArithmeticOperator.Minus; }
	      ) exp2 = multiplicativeExpression { exp1 = new ArithmeticExpression(exp1, exp2, op, exp1.Location); }
	   )*
	   { exp = exp1; }
	;	

multiplicativeExpression returns [Expression exp = null] { ArithmeticOperator op = 0; Expression exp1 = null; Expression exp2 = null; }
	:	exp1 = unaryExpression 
	   ( 
	      ( STAR { op = ArithmeticOperator.Mult; } 
	      | DIV  { op = ArithmeticOperator.Div; } 
	      | MOD  { op = ArithmeticOperator.Mod; } 
	      ) exp2 = unaryExpression { exp1 = new ArithmeticExpression(exp1, exp2, op, exp1.Location); }
	   )*
	   { exp = exp1; }
	;
	
unaryExpression returns [Expression exp = null] {Expression e = null; string typeExp = ""; }
	:	// castExpression
		( OPEN_PAREN type CLOSE_PAREN unaryExpression )=> o:OPEN_PAREN typeExp = type CLOSE_PAREN e = unaryExpression
		{ exp = new CastExpression(typeExp, e, getLocation(o)); }
	|	// preIncrementExpression
		i:INC e = unaryExpression 
                { 
                   // ++a -> a = a + 1;
                   exp = new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, getLocation(i)), ArithmeticOperator.Plus, getLocation(i)), AssignmentOperator.Assign, getLocation(i));
                }

	|	// preDecrementExpression
		d:DEC e = unaryExpression 
                { 
                   // --a -> a = a - 1;
                   exp = new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, getLocation(d)), ArithmeticOperator.Minus, getLocation(d)), AssignmentOperator.Assign, getLocation(d));
                }

	|	p:PLUS    e = unaryExpression { exp = new UnaryExpression(e, UnaryOperator.Plus, getLocation(p)); }
	|	m:MINUS   e = unaryExpression { exp = new UnaryExpression(e, UnaryOperator.Minus, getLocation(m)); }
	|	l:LOG_NOT e = unaryExpression { exp = new UnaryExpression(e, UnaryOperator.Not, getLocation(l)); }
	|	b:BIN_NOT e = unaryExpression { exp = new UnaryExpression(e, UnaryOperator.BitwiseNot, getLocation(b)); }
//	|	STAR unaryExpression // pointerIndirectionExpression
//	|	BIN_AND unaryExpression // addressofExpression
	|	exp = primaryExpression
	;
	
basicPrimaryExpression returns [Expression exp = null] { SingleIdentifierExpression id = null; Expression exp1 = null; }
		// primaryNoArrayCreationExpression		
	:	(	exp = literal
		|	exp = identifier	// simpleName
		|	// parenthesizedExpression
			OPEN_PAREN exp = assignmentExpression CLOSE_PAREN
		|	t:THIS { exp = new ThisExpression(getLocation(t)); } // thisAccess
		|	b:BASE
			(	DOT id = identifier { exp = new FieldAccessExpression(new BaseExpression(getLocation(b)), id, getLocation(b)); } // baseAccess b.identifier
//			|	OPEN_BRACK expressionList CLOSE_BRACK  // baseAccess b[expression, expression]
			|	OPEN_BRACK exp1 = expression CLOSE_BRACK  // baseAccess b[expression]
			{  exp = new ArrayAccessExpression(new BaseExpression(getLocation(b)), exp1, getLocation(b)); }
			)			
		|	exp = newExpression
//		|	TYPEOF OPEN_PAREN
//			(	{ ((LA(1) == VOID) && (LA(2) == CLOSE_PAREN)) }? voidAsType CLOSE_PAREN	// typeofExpression
//			|	type CLOSE_PAREN						// typeofExpression
//			)
//		|	SIZEOF OPEN_PAREN qid = qualifiedIdentifier CLOSE_PAREN	// sizeofExpression
//		|	CHECKED OPEN_PAREN expression CLOSE_PAREN	               // checkedExpression
//		|	UNCHECKED OPEN_PAREN expression CLOSE_PAREN              // uncheckedExpression
//		|	predefinedType DOT id = identifier
		)
	;

primaryExpression returns [Expression exp = null] { Expression exp1 = null; CompoundExpression ce = null; Expression e = null; SingleIdentifierExpression id = null; }
	:	e = basicPrimaryExpression
		(	options { greedy = true; } : 
			(	// invocationExpression  ::= primaryExpression OPEN_PAREN ( argumentList )? CLOSE_PAREN
				o:OPEN_PAREN ( ce = argumentList )? CLOSE_PAREN
				{ e = new InvocationExpression(e, ce, getLocation(o)); }
			|	//	elementAccess		   ::= primaryNoArrayCreationExpression OPEN_BRACK expressionList CLOSE_BRACK
				//	pointerElementAccess ::= primaryNoArrayCreationExpression OPEN_BRACK expression     CLOSE_BRACK
				// OPEN_BRACK expressionList CLOSE_BRACK
				op:OPEN_BRACK exp1 = expression CLOSE_BRACK
			   { e = new ArrayAccessExpression(e, exp1, getLocation(op)); }
			|	//	memberAccess		 ::= primaryExpression DOT identifier
				d:DOT id = identifier
				{ e = new FieldAccessExpression(e, id, getLocation(d)); }

			|	i:INC // postIncrementExpression
                           { 
                              // a++ -> (a = a + 1) - 1
                              Location l = getLocation(i);
                              e = new ArithmeticExpression(new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, l), ArithmeticOperator.Plus, l), AssignmentOperator.Assign, l), new IntLiteralExpression(1, l), ArithmeticOperator.Minus, l);
                           } 

			|	de:DEC // postDecrementExpression
                           { 
                              // a-- -> (a = a - 1) + 1
                              Location l = getLocation(de);
                              e = new ArithmeticExpression(new AssignmentExpression(e, new ArithmeticExpression(e.CloneInit(), new IntLiteralExpression(1, l), ArithmeticOperator.Minus, l), AssignmentOperator.Assign, l), new IntLiteralExpression(1, l), ArithmeticOperator.Plus, l);
                           } 

//			|	DEREF identifier // pointerMemberAccess
			)
		)*	
		{ exp = e; }
	;

newExpression returns [Expression exp = null]{ CompoundExpression size = null; CompoundExpression ce = null; string typeExp = ""; int rank = 0; }
	:	n:NEW typeExp = type
		(	// objectCreationExpression ::= NEW type OPEN_PAREN ( argumentList )? CLOSE_PAREN
			// delegateCreationExpression ::= NEW delegateType OPEN_PAREN expression CLOSE_PAREN
			// NOTE: Will ALSO match 'delegateCreationExpression'
			OPEN_PAREN ( ce = argumentList )? CLOSE_PAREN
			{ exp = new NewExpression(typeExp, ce, getLocation(n)); }
			
		|	// arrayCreationExpression	::= NEW arrayType arrayInitializer
			ce = arrayInitializer
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
			
		|	// arrayCreationExpression ::= NEW nonArrayType OPEN_BRACK expressionList CLOSE_BRACK ( rankSpecifiers )? ( arrayInitializer )?
			OPEN_BRACK size = expressionList CLOSE_BRACK 
			rank = rankSpecifiers
			( ce = arrayInitializer )?
			{ 
			   exp = new NewArrayExpression(typeExp, getLocation(n)); 
			   if (size.ExpressionCount != 0)
			      ((NewArrayExpression)exp).Size = size.GetExpressionElement(0); 
			   ((NewArrayExpression)exp).Init = ce; ((NewArrayExpression)exp).Rank = rank + 1;
			}
		)
	;

literal returns [Expression exp = null]
	:	t:TRUE { exp = new BoolLiteralExpression(true, getLocation(t)); }  // BOOLEAN_LITERAL
	|	f:FALSE { exp = new BoolLiteralExpression(false, getLocation(f)); } // BOOLEAN_LITERAL
	|	i:INT_LITERAL {exp = new IntLiteralExpression(Convert.ToInt32(i.getText()), getLocation(i)); }
//	|	UINT_LITERAL
//	|	LONG_LITERAL
//	|	ULONG_LITERAL
//	|	DECIMAL_LITERAL
//	|	FLOAT_LITERAL
	|	d:DOUBLE_LITERAL 
	   {
	      NumberFormatInfo provider = new NumberFormatInfo();
	      provider.NumberDecimalSeparator =".";
	      exp = new DoubleLiteralExpression(Convert.ToDouble(d.getText(), provider), getLocation(d)); 
	   }
	|	c:CHAR_LITERAL 
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
	|	s:STRING_LITERAL {exp = new StringLiteralExpression(s.getText(), getLocation(s)); }
	|	n:NULL           {exp = new NullExpression(getLocation(n)); } // NULL_LITERAL
	;

predefinedType
	:	(	BOOL 
//		|	BYTE
		|	CHAR
//		|	DECIMAL
		|	DOUBLE
//		|	FLOAT
		|	INT
//		|	LONG
		|	OBJECT
//		|	SBYTE
//		|	SHORT
		|	STRING
//		|	UINT
//		|	ULONG
//		|	USHORT
		|  VAR
		|  DYNAMIC
		)
	;
	
predefinedTypeName returns [string typeExp = ""; ]
	:	BOOL      { typeExp = "bool"; }
//	|	BYTE
	|	CHAR      { typeExp = "char"; }
//	|	DECIMAL
	|	DOUBLE    { typeExp = "double"; }
//	|	FLOAT
	|	INT       { typeExp = "int"; }
//	|	LONG
	|	OBJECT    { typeExp = "object"; }
//	|	SBYTE
//	|	SHORT
	|	STRING    { typeExp = "string"; }
//	|	UINT
//	|	ULONG
//	|	USHORT
	|  	VAR       { typeExp = Convert.ToString(TypeVariable.NewTypeVariable); }
	|  	DYNAMIC   { typeExp = Convert.ToString(TypeVariable.NewDynamicTypeVariable); }
	;

/*
//
// A.2.5 Statements
//
*/
statement returns [Statement stat = null]
	// :	{ (IdentifierRuleIsPredictedByLA(1) && (LA(2) == COLON)) }? labeledStatement
	:	{ ((LA(1) == CONST) && TypeRuleIsPredictedByLA(2) && IdentifierRuleIsPredictedByLA(3)) ||
		  (TypeRuleIsPredictedByLA(1) && IdentifierRuleIsPredictedByLA(2)) }? stat = declarationStatement
	|	( ( CONST )? type identifier )=> stat = declarationStatement
	|	stat = embeddedStatement
//	|	preprocessorDirective[CodeMaskEnums.Statements]
	;
exception
 catch [RecognitionException ex] {
                ErrorManager.Instance.NotifyError(new ParserError(new Location(ex.fileName, ex.line, ex.column), ex.Message));
                // * We enter into panic mode (discarding tokens)
		   if (this.errorState) 
			this.match(NEWLINE);
		   else
                	this.errorState = true;
    }
	
embeddedStatement returns [Statement stat = null]
	:	stat = block
	|	s:SEMI { stat = new Block(getLocation(s)); } //emptyStatement
	|	stat = expressionStatement
	|	stat = selectionStatement
	|	stat = iterationStatement
	|	stat = jumpStatement
  	|	stat = tryStatement
//	|	checkedStatement
//	|	uncheckedStatement
//	|	lockStatement
//	|	usingStatement
//	|	unsafeStatement
//	|	fixedStatement
	;
	
body returns [Block stat = null]
	:	stat = block	
	|	s:SEMI { stat = new Block(getLocation(s)); }
	;

block returns [Block stat = null] { Statement s = null; }
	:	o:OPEN_CURLY { stat = new Block(getLocation(o)); } 
		( s=statement { stat.AddStatement(s); } 
		)* CLOSE_CURLY
	;
	
 statementList returns [List<Statement> stats = new List<Statement>()] { Statement stat = null;}
	:	( stat = statement { stats.Add(stat); } )+
	;
	
// labeledStatement
//	:	identifier COLON statement
//	;
	
declarationStatement returns [DeclarationSet decls = null] { List<Statement> ds = null; }
	:	ds = localVariableDeclaration SEMI { decls = new DeclarationSet(((Declaration)ds[0]).FullName, ds, ds[0].Location); }
	|	ds = localConstantDeclaration SEMI { decls = new DeclarationSet(((Declaration)ds[0]).FullName, ds, ds[0].Location); }
	;
	
localVariableDeclaration returns [List<Statement> statList = null] { string typeExp = ""; }
	:	typeExp = type statList = localVariableDeclarators[typeExp]
	;
	
localVariableDeclarators [string type] returns [List<Statement> statList = new List<Statement>()] { Statement s1 = null; }
	:	s1 = localVariableDeclarator[type] { statList.Add(s1); }
		( 
		   { // Creates a new VariableType.
		     type = TypeSystem.TypeTable.ObtainNewType(type); }
		   COMMA s1 = localVariableDeclarator[type] { statList.Add(s1); } 
		)*
	;
	
localVariableDeclarator [string type] returns [Declaration decl = null] { SingleIdentifierExpression id = null; Expression exp = null; }
	:	id = identifier ( ASSIGN exp = localVariableInitializer )?
	   {
	      if (exp == null)
	         decl = new IdDeclaration(id, type, id.Location);
	      else
	         decl = new Definition(id, type, exp, id.Location);
	   }
	;
	
localVariableInitializer returns [Expression exp = null;]
	:	(	exp = expression
		|	exp = arrayInitializer
		)
	;
	
localConstantDeclaration returns [List<Statement> statList = null] { string typeExp = ""; }
	:	CONST typeExp = type statList = localConstantDeclarators[typeExp]
	;
	
localConstantDeclarators [string type] returns [List<Statement> statList = new List<Statement>()] { Statement s1 = null; }
	:	s1 = localConstantDeclarator[type] { statList.Add(s1); }
		(
         { // Creates a new VariableType.
		     type = TypeSystem.TypeTable.ObtainNewType(type); }
 		   COMMA s1 = localConstantDeclarator[type] { statList.Add(s1); } 
		)*
	;
	
localConstantDeclarator [string type] returns [ConstantDefinition cd = null] { SingleIdentifierExpression id = null; Expression exp = null; }
	:	id = identifier ASSIGN exp = constantExpression
	   { cd = new ConstantDefinition(id, type, exp, id.Location); }
	;
		
constantDeclarators [string type, List<Modifier> mods] returns [List<FieldDeclaration> decls = new List<FieldDeclaration>()] { FieldDeclaration decl = null; }
	:	decl = constantDeclarator[type, mods] { decls.Add(decl); }
		( 
 		   { // Creates a new VariableType.
		     type = TypeSystem.TypeTable.ObtainNewType(type); }
		   COMMA decl = constantDeclarator[type, mods] { decls.Add(decl); } 
		)*
	;
	
constantDeclarator [string type, List<Modifier> mods] returns [ConstantFieldDefinition cd = null] { SingleIdentifierExpression id = null; Expression exp = null; }
	:	id = identifier ASSIGN exp = constantExpression
	   { cd = new ConstantFieldDefinition(id, type, exp, mods, id.Location); }
	;
	
expressionStatement returns [Statement stat = null]
	:	stat = statementExpression SEMI
	;
	
statementExpression returns [Expression exp = null]
	:	exp = assignmentExpression

/*
	:	invocationExpression
	|	objectCreationExpression
	|	assignmentExpression
	|	postIncrementExpression
	|	postDecrementExpression
	|	preIncrementExpression
	|	preDecrementExpression
*/
	;
	
selectionStatement returns [Statement stat = null]
	:	stat = ifStatement
	|	stat = switchStatement
	;
	
ifStatement returns [Statement st = null]{ Statement stat1 = null; Statement stat2 = null; Expression exp = null; }
	:	i:IF OPEN_PAREN exp = booleanExpression CLOSE_PAREN stat1 = embeddedStatement 
		( options { greedy = true; } : stat2 = elseStatement )?
		{
		   if (stat2 == null)
		      st = new IfElseStatement(exp, stat1, getLocation(i));
		   else
		      st = new IfElseStatement(exp, stat1, stat2, getLocation(i));
		}
	;
	
elseStatement returns [Statement st = null]
	:	ELSE st = embeddedStatement
	;
	
switchStatement returns [SwitchStatement stat = null] { Expression exp = null; List<SwitchSection> block = null; }
	:	s:SWITCH OPEN_PAREN exp = expression CLOSE_PAREN block = switchBlock
	   { stat = new SwitchStatement(exp, block, getLocation(s)); }
	;
	
 switchBlock returns [List<SwitchSection> sections = new List<SwitchSection>()]
	:	OPEN_CURLY ( sections = switchSections )? CLOSE_CURLY
	;
	
 switchSections returns [List<SwitchSection> sections = new List<SwitchSection>()] { SwitchSection section = null; }
	:	( section = switchSection { sections.Add(section); } )+
	;
	
 switchSection returns [SwitchSection section = null] { List<SwitchLabel> labels = null; List<Statement> stats = null; }
	:	labels = switchLabels stats = statementList { section = new SwitchSection(labels, stats, labels[0].Location); }
	;
	
 switchLabels returns [List<SwitchLabel> labels = new List<SwitchLabel>()] { SwitchLabel label = null; }
	:	( label = switchLabel { labels.Add(label); } )+
	;
	
 switchLabel returns [SwitchLabel label = null] { Expression exp = null; }
	:	c:CASE exp = constantExpression COLON  { label = new SwitchLabel(exp, getLocation(c)); }
	|	d:DEFAULT COLON                        { label = new SwitchLabel(getLocation(d)); }
	;
	
iterationStatement returns [Statement stat = null]
	:	stat = whileStatement
	|	stat = doStatement
	|	stat = forStatement
	|	stat = foreachStatement
	;
	
whileStatement returns [Statement s = null] { Expression exp = null; Statement stat = null; }
	:	w:WHILE OPEN_PAREN exp = booleanExpression CLOSE_PAREN stat = embeddedStatement
	{ s = new WhileStatement(exp, stat, getLocation(w)); }
	;
	
 doStatement returns [Statement s = null] { Expression exp = null; Statement stat = null; }
	:	d:DO stat = embeddedStatement WHILE OPEN_PAREN exp = booleanExpression CLOSE_PAREN SEMI
	{ s = new DoStatement(stat, exp, getLocation(d)); }  
	;
	
forStatement returns [Statement s = null] { Statement stat = null; Expression cond = null; List<Statement> stats1 = null; List<Statement> stats2 = null; }
	:	f:FOR OPEN_PAREN stats1 = forInitializer SEMI cond = forCondition SEMI stats2 = forIterator CLOSE_PAREN stat = embeddedStatement
	{ s = new ForStatement(stats1, cond, stats2, stat, getLocation(f)); }
	;
	
forInitializer returns [List<Statement> stat = null]
	:	(	{ (TypeRuleIsPredictedByLA(1) && IdentifierRuleIsPredictedByLA(2)) }? stat = localVariableDeclaration
		|	( type identifier )=> stat = localVariableDeclaration
		|	stat = statementExpressionList
		)?
	;
	
forCondition returns [Expression exp = null]
	:	(	exp = booleanExpression )?
	;
	
forIterator returns [List<Statement> statList = null]
	:	(	statList = statementExpressionList )?
	;
	
statementExpressionList returns [List<Statement> statList = null] { Statement st1 = null; }
	:	st1 = statementExpression { statList = new List<Statement>(); statList.Add(st1); } 
	   ( COMMA st1 = statementExpression { statList.Add(st1); } )*
	;
	
foreachStatement returns [Statement s = null] { string typeExp = ""; SingleIdentifierExpression id = null; Expression e = null; Statement stat = null; }
	:	f:FOREACH OPEN_PAREN typeExp = type id = identifier IN e = expression CLOSE_PAREN stat = embeddedStatement
	   { s = new ForeachStatement(typeExp, id, e, stat, getLocation(f)); }
	;
	
jumpStatement returns [Statement stat = null]
	:	stat = breakStatement
	|	stat = continueStatement
//	|	gotoStatement
	|	stat = returnStatement
	|	stat = throwStatement
	;
	
breakStatement returns [Statement stat = null]
	:	b:BREAK SEMI { stat = new BreakStatement(getLocation(b)); }
	;
	
continueStatement returns [Statement stat = null]
	:	c:CONTINUE SEMI { stat = new ContinueStatement(getLocation(c)); }
	;
	
// gotoStatement
//	:	GOTO
//		(	identifier SEMI
//		|	CASE constantExpression SEMI
//		|	DEFAULT SEMI
//		)
//	;
	
returnStatement returns [Statement stat = null] { Expression exp = null; }
	:	r:RETURN ( exp = expression )? SEMI
	   { stat = new ReturnStatement(exp, getLocation(r)); } 
	;
 throwStatement returns [Statement stat = null] { Expression exp = null; }
	:	t:THROW ( exp = expression )? SEMI
        { stat = new ThrowStatement(exp, getLocation(t)); } 
	;
	
tryStatement returns [Statement stat = null]{ ExceptionManagementStatement em = null; Block stats = null, f = null;  CatchStatement c = null; List<CatchStatement> l = new List<CatchStatement>();}
	: t:TRY stats = block  ( c = catchClause  { l.Add(c);} )* (f = finallyClause)?
		{ stat = em = new ExceptionManagementStatement (stats, l, f, getLocation(t)); }
	;

	
 catchClause returns [CatchStatement catchStatement = null] { SingleIdentifierExpression id = null; Block stats = null; IdentifierExpression e = null;}
	:	CATCH OPEN_PAREN e = qualifiedIdentifier ( id = identifier )? CLOSE_PAREN stats = block 
		{ 
		catchStatement  = new CatchStatement(new IdDeclaration(id, e.Identifier, id.Location), stats, id.Location ) ;
		}
    ;
			
   finallyClause returns [Block finallyBlock = null] 
	:	FINALLY finallyBlock = block 
	;
	
// checkedStatement
//	:	CHECKED block
//	;
	
// uncheckedStatement
//	:	UNCHECKED block
//	;
	
// lockStatement
//	:	LOCK OPEN_PAREN expression CLOSE_PAREN embeddedStatement
//	;
	
// usingStatement
//	:	USING OPEN_PAREN resourceAcquisition CLOSE_PAREN embeddedStatement
//	;
	
// unsafeStatement
//	:	UNSAFE block
//	;

// resourceAcquisition
//	:	{ (TypeRuleIsPredictedByLA(1) && IdentifierRuleIsPredictedByLA(2)) }? localVariableDeclaration
//	|	( type identifier )=> localVariableDeclaration
//	|	expression
//	;
	
compilationUnit returns [SourceFile sf = new SourceFile(new Location(fileinfo_.FullName, 0, 0))]
	:	// justPreprocessorDirectives
		usingDirectives [sf]
		// globalAttributes
		namespaceMemberDeclarations [sf]
	   EOF
	;
	
usingDirectives [SourceFile sf] { IdentifierExpression e = null; }
	:	(	options { greedy = true; } 
		:	{ !PPDirectiveIsPredictedByLA(1) }? e = usingDirective { sf.AddUsing(e.Identifier); } 
//		|	( preprocessorDirective[CodeMaskEnums.UsingDirectives] )=> preprocessorDirective[CodeMaskEnums.UsingDirectives]
		)*
	;
	
usingDirective returns [IdentifierExpression e = null] // IdentifierExpression can be SingleIdentifierExpression or QualifiedIdentifierExpression
	:	USING
		//(	// UsingAliasDirective
			// { (IdentifierRuleIsPredictedByLA(1) && (LA(2) == ASSIGN)) }? identifier ASSIGN! qualifiedIdentifier SEMI!
		//|	// UsingNamespaceDirective
			e = qualifiedIdentifier SEMI
		//)
	;
		
namespaceMemberDeclarations [SourceFile sf]
	:	(	options { greedy = true; } 
//		:	{ PPDirectiveIsPredictedByLA(1) }? preprocessorDirective[CodeMaskEnums.NamespaceMemberDeclarations] |
		:	namespaceMemberDeclaration [sf]
		)*
	;
	
namespaceMemberDeclaration [SourceFile sf] { Declaration decl = null; List<Modifier> mods = null; }
	:	namespaceDeclaration[sf]
	|	// attributes 
	   mods = modifiers decl = typeDeclaration[mods] { sf.AddDeclaration(decl); }
	;
	
typeDeclaration [List<Modifier> mods] returns [Declaration d = null]
	:	d = classDeclaration[mods]
//	|	structDeclaration
	|	d = interfaceDeclaration[mods]
//	|	enumDeclaration
//	|	delegateDeclaration
	;

namespaceDeclaration [SourceFile sf]{ IdentifierExpression qid = null; List<Declaration> decls = null; }
	:	NAMESPACE qid = qualifiedIdentifier decls = namespaceBody ( options { greedy = true; } : SEMI )?
	   { sf.AddNamespace(qid, decls); }
	;
	
namespaceBody returns [List<Declaration> decls = new List<Declaration>()] { Declaration decl = null; List<Modifier> mods = null; }
	:	OPEN_CURLY
			// usingDirectives 
			// namespaceMemberDeclarations // Do not use nested namespaces
			(mods = modifiers decl = typeDeclaration[mods] { decls.Add(decl); } )*
		CLOSE_CURLY
	;
	
modifiers returns [List<Modifier> mods = new List<Modifier>()] { Modifier m; }
	:	( m = modifier { mods.Add(m); } )*
	;

modifier returns [Modifier mod = 0;]
	:	(	ABSTRACT  { mod = Modifier.Abstract;  } 
		|	NEW       { mod = Modifier.New;       }
		|	OVERRIDE  { mod = Modifier.Override;  }
		|	PUBLIC    { mod = Modifier.Public;    }
		|	PROTECTED { mod = Modifier.Protected; }
		|	INTERNAL  { mod = Modifier.Internal;  } 
		|	PRIVATE   { mod = Modifier.Private;   }
//		|	SEALED
		|	STATIC    { mod = Modifier.Static;    }
		|	VIRTUAL   { mod = Modifier.Virtual;   }
//		|	EXTERN
//		|	READONLY
//		|	UNSAFE
//		|	VOLATILE
		)
	;

//	
// A.2.6 Classes
//

classDeclaration [List<Modifier> mods] returns [Declaration decl = null] { SingleIdentifierExpression id = null; List<string> bases = null; List<Declaration> decls = null; }
	:	c:CLASS id = identifier bases = classBase decls = classBody ( options { greedy = true; } : SEMI )?
	   { decl = new ClassDefinition(id, mods, bases, decls, getLocation(c)); }
	;
	
classBase returns [List<string> bases = new List<string>()] { string typeExp = ""; }
	:	( COLON typeExp = type { bases.Add(typeExp); } ( COMMA typeExp = type { bases.Add(typeExp); } )* )?
	;
	
classBody returns [List<Declaration> decls = new List<Declaration>()]
	:	OPEN_CURLY decls = classMemberDeclarations CLOSE_CURLY
	;
	
classMemberDeclarations	returns [List<Declaration> decls = new List<Declaration>()]	{ Declaration d = null; }
	:	(	options { greedy = true; } 
//		:	{ PPDirectiveIsPredictedByLA(1) }? preprocessorDirective[CodeMaskEnums.ClassMemberDeclarations] |
      :  d = classMemberDeclaration 
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
		)*
	;
	
classMemberDeclaration returns [Declaration decl = null] { List<Modifier> mods = null; }
	:	// attributes
	   mods = modifiers
		(	// destructorDeclaration |
			decl = typeMemberDeclaration[mods]
		)
	;
	
typeMemberDeclaration [List<Modifier> mods] returns [Declaration decl = null]{ Block stat = null; SingleIdentifierExpression id = null; string typeExp = ""; List<Parameter> pars = null; List<FieldDeclaration> decls = null; Statement []accessors = null; InvocationExpression iexp = null; }
	:	// constantDeclaration
		c:CONST typeExp = type decls = constantDeclarators[typeExp, mods] SEMI
      { decl = new FieldDeclarationSet(typeExp, decls, getLocation(c)); }
      
//	|	// eventDeclaration
//		EVENT type 
//		(	{ IdentifierRuleIsPredictedByLA(1) && (LA(2)==ASSIGN || LA(2)==SEMI ||LA(2)==COMMA) }?
//			variableDeclarators SEMI
//		|	qualifiedIdentifier OPEN_CURLY eventAccessorDeclarations CLOSE_CURLY
//		)
		
	|	// constructorDeclaration
		id = identifier OPEN_PAREN ( pars = formalParameterList )? CLOSE_PAREN 
		( iexp = constructorInitializer )? stat = constructorBody
		{ decl = new ConstructorDefinition(id, mods, pars, iexp, stat, id.Location); }

	|	// methodDeclaration
		{ ((LA(1) == VOID) && (LA(2) != STAR)) }? 
		typeExp = voidAsType id = identifier OPEN_PAREN ( pars = formalParameterList )? CLOSE_PAREN stat = methodBody
		{ decl = new MethodDefinition(id, stat, typeExp, pars, mods, id.Location); }
		
	|	typeExp = type
		(	
		   // unaryOperatorDeclarator or binaryOperatorDeclarator
//			OPERATOR overloadableOperator OPEN_PAREN
//				fixedOperatorParameter 
//				(	COMMA fixedOperatorParameter 
//				)?
//			CLOSE_PAREN
//			operatorBody
//			{}
//		|
			// fieldDeclaration
			{ IdentifierRuleIsPredictedByLA(1) && (LA(2)==ASSIGN || LA(2)==SEMI ||LA(2)==COMMA) }?
			decl = variableDeclarators[typeExp, mods] SEMI
			
		|	id = identifier
		
			(	// propertyDeclaration
				OPEN_CURLY
					accessors = accessorDeclarations 
				CLOSE_CURLY
				{ decl = new PropertyDefinition(id, typeExp, accessors[0], accessors[1], id.Location); }
				
			|	// methodDeclaration
				OPEN_PAREN ( pars = formalParameterList )? CLOSE_PAREN
				stat = methodBody
				{ decl = new MethodDefinition(id, stat, typeExp, pars, mods,  id.Location); }
				
//			|	// indexerDeclaration
//				DOT THIS OPEN_BRACK pars = formalParameterList CLOSE_BRACK
//				OPEN_CURLY accessorDeclarations CLOSE_CURLY
			)
			
//		|	// indexerDeclaration
//			THIS OPEN_BRACK pars = formalParameterList CLOSE_BRACK
//			OPEN_CURLY accessorDeclarations CLOSE_CURLY
		)
	
//	|	IMPLICIT OPERATOR type OPEN_PAREN oneOperatorParameter CLOSE_PAREN		// conversionOperatorDeclarator
//		operatorBody

//	|	EXPLICIT OPERATOR type OPEN_PAREN oneOperatorParameter CLOSE_PAREN		// conversionOperatorDeclarator
//		operatorBody
		
   // Do not use nested types
//	|	typeDeclaration
	;
	
variableDeclarators [string type, List<Modifier> mods] returns [Declaration fields = null] { List<FieldDeclaration> decls = new List<FieldDeclaration>(); FieldDeclaration d = null; }
	:	d = variableDeclarator[type, mods] { decls.Add(d); }
		( 
		   { // Creates a new VariableType.
		     type = TypeSystem.TypeTable.ObtainNewType(type); 
		   }
		   c:COMMA d = variableDeclarator[type, mods] { decls.Add(d); } 
		)*
		{ 
		   if (c == null)
		      fields = d;
		   else
		      fields = new FieldDeclarationSet(type, decls, getLocation(c)); 
		}
	;
	
variableDeclarator[string type, List<Modifier> mods] returns [FieldDeclaration d = null]{ Expression e = null; SingleIdentifierExpression id = null; }
	:	id = identifier ( ASSIGN e = variableInitializer )?
	   {
	      if (e == null)
	         d = new FieldDeclaration(id, type, mods, id.Location);
	      else
	         d = new FieldDefinition(id, e, type, mods, id.Location);
	   }
	;
	
variableInitializer returns [Expression exp = null]
	:	(	exp = expression
		|	exp = arrayInitializer
//		|	stackallocInitializer
		)
	;
		
returnType returns [string typeExp = ""; ]
	:	{ ((LA(1) == VOID) && (LA(2) != STAR)) }? typeExp = voidAsType
	|	typeExp = type
	;
	
methodBody returns [Block stat = null]
	:	stat = body
	;
	
formalParameterList returns [List<Parameter> pars = null]
	:	// attributes
		(	pars = fixedParameters 
//		   ( COMMA /* attributes */ parameterArray )?
//		|	parameterArray
		)
	;
	
fixedParameters returns [List<Parameter> pars = new List<Parameter>()] { Parameter p; }
	:	p = fixedParameter { pars.Add(p); } ( options { greedy = true; } : COMMA /* attributes */ p = fixedParameter { pars.Add(p); } )*
	;
	
fixedParameter returns [Parameter param = new Parameter()] { SingleIdentifierExpression id = null; string typeExp = ""; }
	:	//( parameterModifier )? 
	   typeExp = type id = identifier { param.Identifier = id.Identifier; param.ParamType = typeExp; param.Line = id.Location.Line; param.Column = id.Location.Column; } //param.Line = id.Line; param.Column = id.Column; }
	;
	
// parameterModifier
//	:	REF
//	|	OUT
//	;
	
// parameterArray
//	:	PARAMS! arrayType identifier // Commented
//	:	PARAMS type identifier
//	;
	
 accessorDeclarations returns [Statement []accessors = new Statement[2]] { Statement setStat = null; Statement getStat = null; }
	:	// attributes
		(	getStat = getAccessorDeclaration
			( // attributes 
			   setStat = setAccessorDeclaration
			)?
			{ accessors[0] = getStat; accessors[1] = setStat; }
		|	setStat = setAccessorDeclaration
			( // attributes 
			  getStat = getAccessorDeclaration
			)?
			{ accessors[0] = getStat; accessors[1] = setStat; }
		)
	;
	
 getAccessorDeclaration returns [Statement stat = null]
	:	"get" stat = accessorBody
	;
	
 setAccessorDeclaration returns [Statement stat = null]
	:	"set" stat = accessorBody
	;
	
 accessorBody returns [Statement stat = null]
	:	stat = body
	;
	
// eventAccessorDeclarations
//	:	// attributes
//		(	addAccessorDeclaration // attributes 
//		   removeAccessorDeclaration
//		|	removeAccessorDeclaration // attributes 
//		   addAccessorDeclaration
//		)
//	;
	
// addAccessorDeclaration { Statement stat = null; }
//	:	"add" stat = block
//	;
	
// removeAccessorDeclaration { Statement stat = null; }
//	:	"remove" stat = block
//	;

// overloadableOperator
//		// Unary-or-Binary Operators
//	:	PLUS
//	|	MINUS
//		// Unary-only Operators
//	|	LOG_NOT
//	|	BIN_NOT
//	|	INC
//	|	DEC
//	|	TRUE		//"true"
//	|	FALSE		//"false"
//		// Binary-only Operators
//	|	STAR 
//	|	DIV 
//	|	MOD 
//	|	BIN_AND 
//	|	BIN_OR 
//	|	BIN_XOR 
//	|	SHIFTL 
//	|	SHIFTR 
//	|	EQUAL 
//	|	NOT_EQUAL 
//	|	GTHAN
//	|	LTHAN 
//	|	GTE 
//	|	LTE 
//	;
	
// oneOperatorParameter
//	:	fixedOperatorParameter
//	;
	
// fixedOperatorParameter
//	:	type identifier
//	;
	
// operatorBody
//	:	body
//	;

constructorInitializer returns [InvocationExpression e = null] { CompoundExpression ce = null; }
	:	c:COLON
		(	b:BASE OPEN_PAREN ( ce = argumentList )? CLOSE_PAREN 
		   { e = new InvocationExpression(new BaseExpression(getLocation(b)), ce, getLocation(c)); }
		|	t:THIS OPEN_PAREN ( ce = argumentList )? CLOSE_PAREN
		   { e = new InvocationExpression(new ThisExpression(getLocation(t)), ce, getLocation(c)); }
		)
	;
	
constructorBody returns [Block stat = null]
	:	stat = body
	;

// destructorDeclaration
//	:	BIN_NOT identifier OPEN_PAREN CLOSE_PAREN destructorBody
//	;
	
// destructorBody
//	:	body
//	;
	
//
// A.2.7 Structs
//

// structDeclaration
//	:	STRUCT identifier structInterfaces structBody ( options { greedy = true; } : SEMI )?
//	;
	
// structInterfaces
//	:	( COLON type ( COMMA type )* )?
//	;
	
// structBody
//	:	OPEN_CURLY structMemberDeclarations CLOSE_CURLY
//;
	
//structMemberDeclarations
//	:	(	options { greedy = true; } 
//		:	{ PPDirectiveIsPredictedByLA(1) }? preprocessorDirective[CodeMaskEnums.StructMemberDeclarations] |
//		:	structMemberDeclaration
//		)*
//	;
	
//structMemberDeclaration
//	:	// attributes
//	   modifiers typeMemberDeclaration
//	;
	
//
// A.2.8 Arrays
//

nonArrayType returns [string typeExp = "";]
	:	typeExp = type
	;
	
rankSpecifiers returns [int rank = 0]
	:	// CONFLICT:	ANTLR says this about this line:
		//						ECMA-CSharp.g:1295: warning: nondeterminism upon
		//						ECMA-CSharp.g:1295: 	k==1:OPEN_BRACK
		//						ECMA-CSharp.g:1295: 	k==2:COMMA,CLOSE_BRACK
		//						ECMA-CSharp.g:1295: 	between alt 1 and exit branch of block
		//						!FIXME! -- if possible, can't see the problem right now.
		(	options { greedy = true; } : rankSpecifier { rank++; } )*
		//( rankSpecifier )+ // Commented
	;
	
rankSpecifier // Syntax used type[][] not type[,,]
	:	OPEN_BRACK ( options { greedy = true; } : COMMA )* CLOSE_BRACK 
	;
	
arrayInitializer returns [CompoundExpression ce = null]
	:	OPEN_CURLY
		(	CLOSE_CURLY
		|	ce = variableInitializerList (COMMA)? CLOSE_CURLY
		)
	;
	
variableInitializerList returns [CompoundExpression ce = null] { Expression e1 = null; }
	:	e1 = variableInitializer { ce = new CompoundExpression(e1.Location); ce.AddExpression(e1); } ( options { greedy = true; } : COMMA e1 = variableInitializer { ce.AddExpression(e1); } )*
	;
	
// 
// A.2.9 Interfaces
//

interfaceDeclaration [List<Modifier> mods] returns [Declaration decl = null] { SingleIdentifierExpression id = null; List<string> bases = null; List<Declaration> decls = null; }
	:	i:INTERFACE id = identifier bases = interfaceBase decls = interfaceBody ( options { greedy = true; } : SEMI )?
	   { decl = new InterfaceDefinition(id, mods, bases, decls, getLocation(i)); }
	;
	
interfaceBase returns [List<string> bases = new List<string>()] { string typeExp = ""; }
	:	( COLON typeExp = type { bases.Add(typeExp); } ( COMMA typeExp = type { bases.Add(typeExp); } )* )?
	;
	
interfaceBody returns [List<Declaration> decls = new List<Declaration>()]
	:	OPEN_CURLY decls = interfaceMemberDeclarations CLOSE_CURLY
	;
	
interfaceMemberDeclarations returns [List<Declaration> decls = new List<Declaration>()]	{ Declaration d = null; }
	:	(	options { greedy = true; } 
//		:	{ PPDirectiveIsPredictedByLA(1) }? preprocessorDirective[CodeMaskEnums.InterfaceMemberDeclarations] |
		:	d = interfaceMemberDeclaration { decls.Add(d); }
		)*
	;
	
interfaceMemberDeclaration returns [Declaration decl = null] { SingleIdentifierExpression id = null; string typeExp = ""; List<Parameter> pars = null; List<Modifier> mod = new List<Modifier>(); Statement []accessors = null; }
	:	// attributes
	   ( NEW { mod.Add(Modifier.New); } )? 	
		(	// interfaceMethodDeclaration
			{ ((LA(1) == VOID) && (LA(2) != STAR)) }?
			typeExp = voidAsType id = identifier OPEN_PAREN ( pars = formalParameterList )? CLOSE_PAREN SEMI
			{ decl = new MethodDeclaration(id, typeExp, pars, mod, id.Location); }

		|	typeExp = type 
			(	// interfaceIndexerDeclaration
//				THIS OPEN_BRACK formalParameterList CLOSE_BRACK
//				OPEN_CURLY interfaceAccessors CLOSE_CURLY
//			|	
			   id = identifier
				(	// interfaceMethodDeclaration
					OPEN_PAREN ( pars = formalParameterList )? CLOSE_PAREN SEMI
					{ decl = new MethodDeclaration(id, typeExp, pars, mod, id.Location); }
					
				|	// interfacePropertyDeclaration
					OPEN_CURLY accessors = interfaceAccessors CLOSE_CURLY
					{ decl = new PropertyDefinition(id, typeExp, accessors[0], accessors[1], id.Location); }
				)
			)
			
//		|	// interfaceEventDeclaration
//			EVENT type identifier SEMI
		)
	;
	

 interfaceAccessors returns [Statement []accessors = new Statement[2]] { Statement setStat = null; Statement getStat = null; }
	:	// attributes
		(	getStat = getAccessorDeclaration
			( // attributes 
			  setStat = setAccessorDeclaration
			)?
			{ accessors[0] = getStat; accessors[1] = setStat; }
		|	setStat = setAccessorDeclaration
			( // attributes 
			  getStat = getAccessorDeclaration
			)?
			{ accessors[0] = getStat; accessors[1] = setStat; }
		)
	;

//
//	A.2.10 Enums
//

// enumDeclaration 
//	:	ENUM identifier enumBase enumBody ( options { greedy = true; } : SEMI )?
//	;
	
//enumBase
//	:	( COLON integralType )?
//	;
	
// enumBody
//	:	OPEN_CURLY ( enumMemberDeclarations ( COMMA )? )? CLOSE_CURLY
//	;
	
// enumMemberDeclarations
//	:	// attributes
//	   enumMemberDeclaration 
//		(	options { greedy = true; } : 
//			COMMA  // attributes
//			enumMemberDeclaration
//		)*
//	;
 	
// enumMemberDeclaration
//	:	identifier ( ASSIGN constantExpression )?
//	;

//
// A.2.11 Delegates
//

// delegateDeclaration
//	:	DELEGATE 
//		( 	{ ((LA(1) == VOID) && IdentifierRuleIsPredictedByLA(2)) }? 
//			voidAsType
//		| 	type
//		) 
//		identifier OPEN_PAREN ( formalParameterList )? CLOSE_PAREN SEMI
//	;

//
// A.2.12 Attributes
//

// globalAttributes
//	:	(	options { greedy = true; } 
//		:	{ !PPDirectiveIsPredictedByLA(1) }? globalAttributeSection
//		|	( preprocessorDirective[CodeMaskEnums.GlobalAttributes] )=>
//			preprocessorDirective[CodeMaskEnums.GlobalAttributes]
//		)*
//	;
	
// globalAttributeSection
//	:	o:OPEN_BRACK^
//			"assembly"! COLON! attributeList ( COMMA! )? 
//		CLOSE_BRACK!
//	;

// attributes
//	:	(	options { greedy = true; } 
//		:	{ !PPDirectiveIsPredictedByLA(1) }? attributeSection 
//		|	( preprocessorDirective[CodeMaskEnums.Attributes] )=>
//			preprocessorDirective[CodeMaskEnums.Attributes]
//		)*
//	;
	
// attributeSection
//	:	o:OPEN_BRACK^
//			( attributeTarget COLON! )? attributeList ( COMMA! )? 
//		CLOSE_BRACK!
//	;
	
// attributeTarget
//	:	"field"
//	|	EVENT
//	|	"method"
//	|	"module"
//	|	"param"
//	|	"property"
//	|	RETURN
//	|	"type"
//	;

// attributeList
//	:	attribute ( options { greedy = true; } : COMMA! attribute )*
//	;
	
// attribute
//	:	( predefinedTypeName | qualifiedIdentifier ) ( attributeArguments )?
//	;
	
// attributeArguments
//	:	OPEN_PAREN!
//		(	CLOSE_PAREN!
//		|	{ (IdentifierRuleIsPredictedByLA(1) && (LA(2) == ASSIGN)) }? namedArgumentList CLOSE_PAREN!
//		|	positionalArgumentList ( COMMA! namedArgumentList )? CLOSE_PAREN!
//		)
//	;
	
// positionalArgumentList
//	:	positionalArgument 
//		(	// CONFLICT: ANTLR thinks this is ambiguous, because
//			//           in rule 'attributeArguments' a COMMA also
//			//				 separates positionalArgument & namedArgument. 
//			//           !FIXME! if possible.
//			options { greedy = true; }
//			:	COMMA! positionalArgument 
//		)*
//	;
	
// positionalArgument
//	:	attributeArgumentExpression
//	;
	
// namedArgumentList
//	:	namedArgument ( COMMA! namedArgument )*
//	;
	
// namedArgument
//	:	identifier ASSIGN! attributeArgumentExpression
//	;
	
// attributeArgumentExpression
//	:	expression
//	;

//
// A.3 Grammar extensions for unsafe code
// 

// fixedStatement
//	:	FIXED OPEN_PAREN pointerType fixedPointerDeclarators CLOSE_PAREN embeddedStatement // Commmeted
//	:	FIXED OPEN_PAREN typeExp = type fixedPointerDeclarators CLOSE_PAREN stat = embeddedStatement
//	;
	
// fixedPointerDeclarators
//	:	fixedPointerDeclarator ( COMMA fixedPointerDeclarator )*
//	;
	
// fixedPointerDeclarator { SingleIdentifierExpression id = null; }
//	:	id = identifier ASSIGN fixedPointerInitializer
//	;
	
// fixedPointerInitializer { Expression exp = null; }
//	:  exp = expression
//	;	
	
// stackallocInitializer { IdentifierExpression qid = null; Expression exp = null; }
//	:	STACKALLOC qid = qualifiedIdentifier OPEN_BRACK exp = expression CLOSE_BRACK
//	;

//
// A.1.10 Pre-processing directives
// 

// justPreprocessorDirectives
//	:	(	options { greedy = true; } 
//		:	{ SingleLinePPDirectiveIsPredictedByLA(1) }? singleLinePreprocessorDirective
//		|	( preprocessorDirective[CodeMaskEnums.PreprocessorDirectivesOnly] )=> 
//			preprocessorDirective[CodeMaskEnums.PreprocessorDirectivesOnly]
//		)*
//	;
	
// preprocessorDirective [CodeMaskEnums codeMask]
//	:	PP_DEFINE^   PP_IDENT
//	|	PP_UNDEFINE^ PP_IDENT
//	|	lineDirective
//	|	PP_ERROR^   ppMessage
//	|	PP_WARNING^ ppMessage
//	|	regionDirective[codeMask]
//	|	conditionalDirective[codeMask]
//	;
	
// singleLinePreprocessorDirective
//	:	PP_DEFINE^   PP_IDENT
//	|	PP_UNDEFINE^ PP_IDENT
//	|	lineDirective
//	|	PP_ERROR^   ppMessage
//	|	PP_WARNING^ ppMessage
//	; 
	
// lineDirective
//	:	PP_LINE^
//		(	DEFAULT
//		|	PP_NUMBER ( PP_FILENAME )?
//		)
//	;

// regionDirective! [CodeMaskEnums codeMask]
//	:	reg:PP_REGION^ msg1:ppMessage
//		drtv:directiveBlock[codeMask]
//		endreg:PP_ENDREGION msg2:ppMessage
//	;

// conditionalDirective! [CodeMaskEnums codeMask]
//	:	hashIF:PP_COND_IF^ exprIF:preprocessExpression
//		drtvIF:directiveBlock[codeMask]
//		(	hashELIF:PP_COND_ELIF exprELIF:preprocessExpression
//			drtvELIF:directiveBlock[codeMask]
//		)*
//		
//		(	hashELSE:PP_COND_ELSE
//			drtvELSE:directiveBlock[codeMask]
//		)?
//		
//		hashENDIF:PP_COND_ENDIF
//	;

// directiveBlock  [CodeMaskEnums codeMask]
//	:	
//		(	options { greedy = true; } 
//		:	preprocessorDirective[codeMask]
//		|	{ NotExcluded(codeMask, CodeMaskEnums.UsingDirectives) }? 			usingDirective
//		|	{ NotExcluded(codeMask, CodeMaskEnums.GlobalAttributes) }? 			globalAttributeSection
//		|	{ NotExcluded(codeMask, CodeMaskEnums.Attributes) }? 					attributeSection
//		|	{ NotExcluded(codeMask, CodeMaskEnums.NamespaceMemberDeclarations) }? 	namespaceMemberDeclaration
//		|	{ NotExcluded(codeMask, CodeMaskEnums.ClassMemberDeclarations) }? 		classMemberDeclaration
//		|	{ NotExcluded(codeMask, CodeMaskEnums.StructMemberDeclarations) }? 		structMemberDeclaration
//		|	{ NotExcluded(codeMask, CodeMaskEnums.InterfaceMemberDeclarations) }? 	interfaceMemberDeclaration
//		|	{ NotExcluded(codeMask, CodeMaskEnums.Statements) }? 					statement
//		)*
//	;
	
// ppMessage
//	:	( PP_IDENT | PP_STRING | PP_FILENAME | PP_NUMBER )*
//	;
	
	
//======================================
// 14.2.1 Operator precedence and associativity
//
// The following table summarizes all PP operators in order of precedence from lowest to highest:
//
// PRECEDENCE     SECTION  CATEGORY                     OPERATORS
//  lowest  ( 4)  14.11    Conditional OR               ||
//          ( 3)  14.11    Conditional AND              &&
//          ( 2)  14.9     Equality                     == !=
//  highest ( 1)  14.5     Primary                      (x) !x
//
// NOTE: In accordance with lessons gleaned from the "java.g" file supplied with ANTLR, I have
//       applied the following pattern to the rules for expressions:
// 
//           thisLevelExpression :
//               nextHigherPrecedenceExpression (OPERATOR nextHigherPrecedenceExpression)*
//
//       which is a standard recursive definition for a parsing an expression.
//

// preprocessExpression
//	:	preprocessOrExpression
//	;

// preprocessOrExpression
//	:	preprocessAndExpression (	LOG_OR^ preprocessAndExpression )*
//	;

// preprocessAndExpression
//	:	preprocessEqualityExpression (	LOG_AND^ preprocessEqualityExpression )*
//	;
	
// preprocessEqualityExpression
//	:	preprocessPrimaryExpression ( ( EQUAL^ | NOT_EQUAL^ ) preprocessPrimaryExpression )*
//	;
	
// preprocessPrimaryExpression
//	:	(	id:keywordExceptTrueAndFalse
//		|	PP_IDENT
//		|	TRUE
//		|	FALSE
//		|	LOG_NOT^ preprocessPrimaryExpression
//		|	o:OPEN_PAREN^ preprocessOrExpression CLOSE_PAREN!
//		)
//	;

// keywordExceptTrueAndFalse
//	:	ABSTRACT
//	|	AS
//	|	BASE
//	|	BOOL
//	|	BREAK
//	|	BYTE
//	|	CASE
//	|	CATCH
//	|	CHAR
//	|	CHECKED  | CLASS    | CONST   | CONTINUE | DECIMAL   | DEFAULT  | DELEGATE
//	|	DO       | DOUBLE   | ELSE    | ENUM     | EVENT     | EXPLICIT | EXTERN    
//	|	FINALLY  | FIXED    | FLOAT   |	FOR     | FOREACH   | GOTO     | IF      
//	|	IMPLICIT | IN       | INT     | INTERFACE| INTERNAL  | IS       | LOCK
//	|	LONG     | NAMESPACE| NEW     | NULL     | OBJECT    | OPERATOR | OUT	    
//	|	OVERRIDE | PARAMS   | PRIVATE | PROTECTED| PUBLIC    | READONLY       
//	|	REF      | RETURN   | SBYTE   | SEALED   | SHORT     | SIZEOF   | STACKALLOC 
//	|	STATIC   | STRING   | STRUCT  | SWITCH   | THIS      | THROW    | TRY     
//	|	TYPEOF   | UINT     | ULONG   | UNCHECKED| UNSAFE    | USHORT   | USING   
//	|	VIRTUAL  | VOID     | VOLATILE| WHILE    | VAR
//	;

voidAsType returns [string name = ""]
	:	VOID { name = "void"; }
	;