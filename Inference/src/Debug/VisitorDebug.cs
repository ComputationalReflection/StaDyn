////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorDebug.cs                                                      //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class shows the information of the abstract syntax tree.           //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 11-12-2006                                                    //
// Modification date: 10-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AST;
using TypeSystem;
using Tools;

namespace Debugger
{
   /// <summary>
   /// This class shows the information of the abstract syntax tree.
   /// </summary>
   /// <remarks>
   /// Inheritance: VisitorAdapter.
   /// Implements Visitor pattern [Concrete Visitor].
   /// </remarks>
   class VisitorDebug : VisitorAdapter
   {
      #region Fields

      /// <summary>
      /// Represents the place where we want to write de information of the tree.
      /// </summary>
      private TextWriter output;

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of VisitorDebug
      /// </summary>
      /// <param name="writer">Writer to write the information of the tree.</param>
      public VisitorDebug(TextWriter writer)
      {
         this.output = writer;
      }

      #endregion

      #region printIndentation

      /// <summary>
      /// Prints indentation.
      /// </summary>
      /// <param name="lenght">Lenght of the indentation.</param>
      private void printIndentation(int lenght)
      {
         for (int i = 0; i < lenght * 2; i++)
         {
            this.output.Write(" ");
         }
         this.output.Write("|_ ");
      }

      #endregion

      #region Visit(SourceFile node, Object obj)

      public override Object Visit(SourceFile node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Source code [{0}:{1}]", node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Usings");
         for (int i = 0; i < node.Usings.Count; i++)
         {
            this.printIndentation(indent + 2);
            this.output.WriteLine(node.Usings[i]);
         }

         foreach (string key in node.Namespacekeys)
         {
            int count = node.GetNamespaceDefinitionCount(key);
            for (int i = 0; i < count; i++)
            {
               node.GetNamespaceDeclarationElement(key, i).Accept(this, indent + 1);
            }
         }

         for (int i = 0; i < node.DeclarationCount; i++)
         {
            this.printIndentation(indent + 1);
            this.output.WriteLine("Definition");
            node.GetDeclarationElement(i).Accept(this, indent + 2);
         }
         return null;
      }

      #endregion

      #region Visit(Namespace node, Object obj)

      public override Object Visit(Namespace node, Object obj)
      {
         int indent = Convert.ToInt32(obj);

         this.printIndentation(indent);
         this.output.WriteLine("Namespace: {0} [{1}:{2}]", node.Identifier.Identifier, node.Location.Line, node.Location.Column);

         for (int i = 0; i < node.NamespaceMembersCount; i++)
         {
            this.printIndentation(indent + 1);
            this.output.WriteLine("Definition");
            node.GetDeclarationElement(i).Accept(this, indent + 2);
         }

         return null;

      }

      #endregion

      #region printType()

      private string printType(TypeExpression type)
      {
         if (type != null)
            return type.ToString();
         else
            return "ERROR!! NO ASIGNADO";
         //return "";
      }

      #endregion

      #region Visit(IdDeclaration node, Object obj

      public override Object Visit(IdDeclaration node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         if (node.IdentifierExp.IndexOfSSA != -1)
            this.output.WriteLine("Declaration: {0}{1} [{2}:{3}]", node.Identifier, node.IdentifierExp.IndexOfSSA, node.Location.Line, node.Location.Column);
         else
            this.output.WriteLine("Declaration: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));
         return null;
      }

      #endregion 

      #region Visit(PropertyDefinition node, Object obj)

      public override Object Visit(PropertyDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Property: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         if (node.GetBlock != null)
         {
            this.printIndentation(indent + 1);
            this.output.WriteLine("GetAccesor");
            node.GetBlock.Accept(this, indent + 2);
         }
         if (node.SetBlock != null)
         {
            this.printIndentation(indent + 1);
            this.output.WriteLine("SetAccesor");
            node.SetBlock.Accept(this, indent + 2);
         }
         return null;
      }

      #endregion

      #region Visit(Definition node, Object obj)

      public override Object Visit(Definition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         if (node.IdentifierExp.IndexOfSSA != -1)
            this.output.WriteLine("Definition: {0}{1} [{2}:{3}]", node.Identifier, node.IdentifierExp.IndexOfSSA, node.Location.Line, node.Location.Column);
         else
            this.output.WriteLine("Definition: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));
         return node.Init.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(ConstantDefinition node, Object obj)

      public override Object Visit(ConstantDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         if (node.IdentifierExp.IndexOfSSA != -1)
            this.output.WriteLine("Constant Definition: {0}{1} [{2}:{3}]", node.Identifier, node.IdentifierExp.IndexOfSSA, node.Location.Line, node.Location.Column);
         else
            this.output.WriteLine("Constant Definition: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         return node.Init.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(ClassDefinition node, Object obj)

      public override Object Visit(ClassDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Class: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         for (int i = 0; i < node.MemberCount; i++)
         {
            node.GetMemberElement(i).Accept(this, indent + 1);
         }
         return null;
      }

      #endregion

      #region Visit(InterfaceDefinition node, Object obj)

      public override Object Visit(InterfaceDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Interface: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         for (int i = 0; i < node.MemberCount; i++)
         {
            node.GetMemberElement(i).Accept(this, indent + 1);
         }
         return null;
      }

      #endregion

      #region Visit(FieldDeclaration node, Object obj)

      public override Object Visit(FieldDeclaration node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Field: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         return null;
      }

      #endregion

      #region Visit(FieldDefinition node, Object obj)

      public override Object Visit(FieldDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Field: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         return node.Init.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(ConstantFieldDefinition node, Object obj)

      public override Object Visit(ConstantFieldDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Constant Field: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         return node.Init.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(MethodDeclaration node, Object obj)

      public override Object Visit(MethodDeclaration node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Method: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         return null;
      }

      #endregion

      #region Visit(MethodDefinition node, Object obj)

      public override Object Visit(MethodDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Method: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         return node.Body.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(ConstructorDefinition node, Object obj)

      public override Object Visit(ConstructorDefinition node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Constructor: {0} [{1}:{2}]", node.Identifier, node.Location.Line, node.Location.Column);

         this.printIndentation(indent + 1);
         this.output.Write("Type: ");
         this.output.WriteLine(printType(node.TypeExpr));

         if (node.Initialization != null)
            node.Initialization.Accept(this, indent + 1);
         return node.Body.Accept(this, indent + 1);
      }

      #endregion

      // Expressions

      #region Visit(ArgumentExpression node, Object obj)

      public override Object Visit(ArgumentExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Argument [{0}:{1}]", node.Location.Line, node.Location.Column);
         return node.Argument.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(ArithmeticExpression node, Object obj)

      public override Object Visit(ArithmeticExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.Write("ArithmeticExpression ");
         switch (node.Operator)
         {
            case ArithmeticOperator.Minus: this.output.Write("-"); break;
            case ArithmeticOperator.Plus: this.output.Write("+"); break;
            case ArithmeticOperator.Mult: this.output.Write("*"); break;
            case ArithmeticOperator.Div: this.output.Write("/"); break;
            case ArithmeticOperator.Mod: this.output.Write("%"); break;
         }
         this.output.WriteLine(" Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.FirstOperand.Accept(this, indent + 1);
         node.SecondOperand.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(ArrayAccessExpression node, Object obj)

      public override Object Visit(ArrayAccessExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("ArrayAccessExpression Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.FirstOperand.Accept(this, indent + 1);
         node.SecondOperand.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(AssignmentExpression node, Object obj)

      public override Object Visit(AssignmentExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.Write("AssignmentExpression ");
         switch (node.Operator)
         {
            case AssignmentOperator.Assign: this.output.Write("="); break;
            case AssignmentOperator.PlusAssign: this.output.Write("+="); break;
            case AssignmentOperator.MinusAssign: this.output.Write("-="); break;
            case AssignmentOperator.MultAssign: this.output.Write("*="); break;
            case AssignmentOperator.DivAssign: this.output.Write("/="); break;
            case AssignmentOperator.ModAssign: this.output.Write("%="); break;
            case AssignmentOperator.ShiftRightAssign: this.output.Write(">>="); break;
            case AssignmentOperator.ShiftLeftAssign: this.output.Write("<<="); break;
            case AssignmentOperator.BitwiseAndAssign: this.output.Write("&="); break;
            case AssignmentOperator.BitwiseXOrAssign: this.output.Write("^="); break;
            case AssignmentOperator.BitwiseOrAssign: this.output.Write("|="); break;
         }
         this.output.WriteLine(" Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column); node.FirstOperand.Accept(this, indent + 1);
         node.SecondOperand.Accept(this, indent + 1);
         if (node.MoveStat != null)
            node.MoveStat.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(BaseCallExpression node, Object obj)

      public override Object Visit(BaseCallExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("BaseCallExpression Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return node.Arguments.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(BitwiseExpression node, Object obj)

      public override Object Visit(BitwiseExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.Write("BitwiseExpression ");
         switch (node.Operator)
         {
            case BitwiseOperator.BitwiseOr: this.output.Write("|"); break;
            case BitwiseOperator.BitwiseAnd: this.output.Write("&"); break;
            case BitwiseOperator.BitwiseXOr: this.output.Write("^"); break;
            case BitwiseOperator.ShiftLeft: this.output.Write("<<"); break;
            case BitwiseOperator.ShiftRight: this.output.Write(">>"); break;
         }
         this.output.WriteLine(" Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.FirstOperand.Accept(this, indent + 1);
         node.SecondOperand.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(CastExpression node, Object obj)

      public override Object Visit(CastExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("CastExpression Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return node.Expression.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(FieldAccessExpression node, Object obj)

      public override Object Visit(FieldAccessExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("FieldAccessExpression Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.Expression.Accept(this, indent + 1);
         node.FieldName.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(InvocationExpression node, Object obj)

      public override Object Visit(InvocationExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("InvocationExpression Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.Identifier.Accept(this, indent + 1);
         node.Arguments.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(IsExpression node, Object obj)

      public override Object Visit(IsExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("IsExpression Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.Expression.Accept(this, indent + 1);
         this.printIndentation(indent + 1);
         this.output.WriteLine(printType(node.TypeExpr));
         return null;
      }

      #endregion

      #region Visit(LogicalExpression node, Object obj)

      public override Object Visit(LogicalExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.Write("LogicalExpression ");
         switch (node.Operator)
         {
            case LogicalOperator.Or: this.output.Write("||"); break;
            case LogicalOperator.And: this.output.Write("&&"); break;
         }
         this.output.WriteLine(" Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.FirstOperand.Accept(this, indent + 1);
         node.SecondOperand.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(NewArrayExpression node, Object obj)

      public override Object Visit(NewArrayExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("NewArrayExpression: {0} rank: {1} Type: {2} [{3}:{4}]", node.TypeInfo, node.Rank, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         if (node.Size != null)
         {
            this.printIndentation(indent + 1);
            this.output.WriteLine("Size:");
            node.Size.Accept(this, indent + 2);
         }
         if (node.Init != null)
            node.Init.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(NewExpression node, Object obj)

      public override Object Visit(NewExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("NewExpression: {0} Type: {1} [{2}:{3}]", node.TypeInfo, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return node.Arguments.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(QualifiedIdentifierExpression node, Object obj)

      public override Object Visit(QualifiedIdentifierExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("QualifiedIdentifierExpression Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.IdName.Accept(this, indent + 1);
         node.IdExpression.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(RelationalExpression node, Object obj)

      public override Object Visit(RelationalExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.Write("RelationalExpression ");
         switch (node.Operator)
         {
            case RelationalOperator.NotEqual: this.output.Write("!="); break;
            case RelationalOperator.Equal: this.output.Write("=="); break;
            case RelationalOperator.LessThan: this.output.Write("<"); break;
            case RelationalOperator.GreaterThan: this.output.Write(">"); break;
            case RelationalOperator.LessThanOrEqual: this.output.Write("<="); break;
            case RelationalOperator.GreaterThanOrEqual: this.output.Write(">="); break;
         }
         this.output.WriteLine(" Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.FirstOperand.Accept(this, indent + 1);
         node.SecondOperand.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(TernaryExpression node, Object obj)

      public override Object Visit(TernaryExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("TernaryExpression ? Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.FirstOperand.Accept(this, indent + 1);
         node.SecondOperand.Accept(this, indent + 1);
         node.ThirdOperand.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(UnaryExpression node, Object obj)

      public override Object Visit(UnaryExpression node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.Write("UnaryExpression ");
         switch (node.Operator)
         {
            case UnaryOperator.BitwiseNot: this.output.Write("~"); break;
            case UnaryOperator.Minus: this.output.Write("-"); break;
            case UnaryOperator.Not: this.output.Write("!"); break;
            case UnaryOperator.Plus: this.output.Write("+"); break;
            case UnaryOperator.PostfixDecrement:
            case UnaryOperator.PrefixDecrement: this.output.Write("--"); break;
            case UnaryOperator.PostfixIncrement:
            case UnaryOperator.PrefixIncrement: this.output.Write("++"); break;
         }
         this.output.WriteLine("Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         node.Operand.Accept(this, indent + 1);

         //if (node.MoveStat != null)
         //   node.MoveStat.Accept(this, obj);
         return null;
      }

      #endregion

      // Literales

      #region Visit(BaseExpression node, Object obj)

      public override Object Visit(BaseExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("base Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region  Visit(BoolLiteralExpression node, Object obj)

      public override Object Visit(BoolLiteralExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("{0} Type: {1} [{2}:{3}]", node.BoolValue, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(CharLiteralExpression node, Object obj)

      public override Object Visit(CharLiteralExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("{0} Type: {1} [{2}:{3}]", node.CharValue, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(DoubleLiteralExpression node, Object obj)

      public override Object Visit(DoubleLiteralExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("{0} Type: {1} [{2}:{3}]", node.DoubleValue, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(IntLiteralExpression node, Object obj)

      public override Object Visit(IntLiteralExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("{0} Type: {1} [{2}:{3}]", node.IntValue, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(NullExpression node, Object obj)

      public override Object Visit(NullExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("null Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(SingleIdentifierExpression node, Object obj)

      public override Object Visit(SingleIdentifierExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         if (node.IndexOfSSA != -1)
             this.output.WriteLine("{0}{1} Type: {2} [{3}:{4}]", node.Identifier, node.IndexOfSSA, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         else
             this.output.WriteLine("{0} Type: {1} [{2}:{3}]", node.Identifier, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(StringLiteralExpression node, Object obj)

      public override Object Visit(StringLiteralExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("{0} Type: {1} [{2}:{3}]", node.StringValue, printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(ThisExpression node, Object obj)

      public override Object Visit(ThisExpression node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("this Type: {0} [{1}:{2}]", printType(node.ExpressionType), node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      // Statements

      #region Visit(BreakStatement node, Object obj)

      public override Object Visit(BreakStatement node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("break [{0}:{1}]", node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(CatchStatement node, Object obj)

      public override Object Visit(CatchStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("CatchStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         node.Exception.Accept(this, indent + 1);
         node.Statements.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(ContinueStatement node, Object obj)

      public override Object Visit(ContinueStatement node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.WriteLine("continue [{0}:{1}]", node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(DoStatement node, Object obj)

      public override Object Visit(DoStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("DoStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Init");
         for (int i = 0; i < node.InitDo.Count; i++)
         {
            node.InitDo[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Before Body");
         for (int i = 0; i < node.BeforeBody.Count; i++)
         {
            node.BeforeBody[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Statements");         
         node.Statements.Accept(this, indent + 2);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Condition");
         node.Condition.Accept(this, indent + 2);
         return null;
      }

      #endregion

      #region Visit(ForeachStatement node, Object obj)

      public override Object Visit(ForeachStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("ForeachStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         node.ForEachDeclaration.Accept(this, indent + 1);
         node.ForeachExp.Accept(this, indent + 1);
         node.ForeachBlock.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(ForStatement node, Object obj)

      public override Object Visit(ForStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("ForStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Initialization");
         for (int i = 0; i < node.InitializerCount; i++)
         {
            node.GetInitializerElement(i).Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("After initialization");
         for (int i = 0; i < node.AfterInit.Count; i++)
         {
            node.AfterInit[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Before condition");
         for (int i = 0; i < node.BeforeCondition.Count; i++)
			{
            node.BeforeCondition[i].Accept(this, indent + 2);
			}
         this.printIndentation(indent + 1);
         this.output.WriteLine("Condition");
         node.Condition.Accept(this, indent + 2);

         this.printIndentation(indent + 1);
         this.output.WriteLine("After condition");
         for (int i = 0; i < node.AfterCondition.Count; i++)
         {
            node.AfterCondition[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Statements");
         node.Statements.Accept(this, indent + 2);

         this.printIndentation(indent + 1);
         this.output.WriteLine("Iterators");
         for (int i = 0; i < node.IteratorCount; i++)
         {
            node.GetIteratorElement(i).Accept(this, indent + 2);
         }
         
         return null;
      }

      #endregion

      #region Visit(IfElseStatement node, Object obj)

      public override Object Visit(IfElseStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("IfElseStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Condition");
         node.Condition.Accept(this, indent + 2);
         this.printIndentation(indent + 1);
         this.output.WriteLine("After Condition");
         for (int i = 0; i < node.AfterCondition.Count; i++)
         {
            node.AfterCondition[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("True branch");
         node.TrueBranch.Accept(this, indent + 2);
         this.printIndentation(indent + 1);
         this.output.WriteLine("False branch");
         node.FalseBranch.Accept(this, indent + 2);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Theta functions");
         for (int i = 0; i < node.ThetaStatements.Count; i++)
         {
            node.ThetaStatements[i].Accept(this, indent + 2);
         }
         return null;
      }

      #endregion

      #region Visit(ReturnStatement node, Object obj)

      public override Object Visit(ReturnStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         node.Assigns.Accept(this, indent);
         this.printIndentation(indent);
         this.output.WriteLine("ReturnStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         if (node.ReturnExpression != null)
            return node.ReturnExpression.Accept(this, indent + 1);
         return null;
      }

      #endregion

      #region Visit(SwitchLabel node, Object obj)

      public override Object Visit(SwitchLabel node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         if (node.SwitchSectionType == SectionType.Case)
         {
             this.output.WriteLine("Case [{0}:{1}]", node.Location.Line, node.Location.Column);
            node.Condition.Accept(this, indent + 1);
         }
         else
             this.output.WriteLine("Default [{0}:{1}]", node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(SwitchSection node, Object obj)

      public override Object Visit(SwitchSection node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("Switch section [{0}:{1}]", node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Switch label");
         for (int i = 0; i < node.LabelSection.Count; i++)
         {
            node.LabelSection[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Statements");
         for (int i = 0; i < node.SwitchBlock.StatementCount; i++)
         {
            node.SwitchBlock.GetStatementElement(i).Accept(this, indent + 2);
         }
         return null;
      }

      #endregion

      #region Visit(SwitchStatement node, Object obj)

      public override Object Visit(SwitchStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("SwitchStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Condition");
         node.Condition.Accept(this, indent + 2);
         this.printIndentation(indent + 1);
         this.output.WriteLine("After Condition");
         for (int i = 0; i < node.AfterCondition.Count; i++)
         {
            node.AfterCondition[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Switch block");
         for (int i = 0; i < node.SwitchBlockCount; i++)
         {
            node.GetSwitchSectionElement(i).Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Theta functions");
         for (int i = 0; i < node.ThetaStatements.Count; i++)
         {
            node.ThetaStatements[i].Accept(this, indent + 2);
         }
         return null;
      }

      #endregion

      #region Visit(ThrowStatement node, Object obj)

      public override Object Visit(ThrowStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("ThrowStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         if (node.ThrowExpression == null)
             return null;
         return node.ThrowExpression.Accept(this, indent + 1);
      }

      #endregion

      #region Visit(ExceptionManagementStatement node, Object obj)

      public override Object Visit(ExceptionManagementStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("TryStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         node.TryBlock.Accept(this, indent + 1);
         this.printIndentation(indent);
         this.output.WriteLine("Catch [{0}:{1}]", node.Location.Line, node.Location.Column);
         for (int i = 0; i < node.CatchCount; i++)
         {
            node.GetCatchElement(i).Accept(this, indent + 1);
         }
         if (node.FinallyBlock != null) {
             this.printIndentation(indent);
             this.output.WriteLine("Finally [{0}:{1}]", node.Location.Line, node.Location.Column);
             node.FinallyBlock.Accept(this, indent + 1);
         }
         return null;
      }

      #endregion

      #region Visit(WhileStatement node, Object obj)

      public override Object Visit(WhileStatement node, Object obj)
      {
         int indent = Convert.ToInt32(obj);
         this.printIndentation(indent);
         this.output.WriteLine("WhileStatement [{0}:{1}]", node.Location.Line, node.Location.Column);
         this.printIndentation(indent + 1);
         this.output.WriteLine("Init");
         for (int i = 0; i < node.InitWhile.Count; i++)
         {
            node.InitWhile[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Before Condition");
         for (int i = 0; i < node.BeforeCondition.Count; i++)
         {
            node.BeforeCondition[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Condition");
         node.Condition.Accept(this, indent + 2);
         this.printIndentation(indent + 1);
         this.output.WriteLine("After Condition");
         for (int i = 0; i < node.AfterCondition.Count; i++)
         {
            node.AfterCondition[i].Accept(this, indent + 2);
         }
         this.printIndentation(indent + 1);
         this.output.WriteLine("Statements");
         node.Statements.Accept(this, indent + 2);
         return null;
      }

      #endregion

      #region Visit(MoveStatement node, Object obj)

      public override Object Visit(MoveStatement node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));

         if (node.LeftExp.IndexOfSSA != -1)
            this.output.Write("{0}{1}", node.LeftExp.Identifier, node.LeftExp.IndexOfSSA);
         else
            this.output.Write("{0}", node.LeftExp.Identifier);

         this.output.Write(" ({0})", printType(node.LeftExp.ExpressionType));
         this.output.Write(" <-- ");

         if (node.RightExp.IndexOfSSA != -1)
            this.output.Write("{0}{1}", node.RightExp.Identifier, node.RightExp.IndexOfSSA);
         else
            this.output.Write("{0}", node.RightExp.Identifier);

         this.output.WriteLine(" ({0})", printType(node.RightExp.ExpressionType));

         if (node.MoveStat != null)
            node.MoveStat.Accept(this, obj);
         return null;
      }

      #endregion

      #region Visit(ThetaStatement node, Object obj)

      public override Object Visit(ThetaStatement node, Object obj)
      {
         this.printIndentation(Convert.ToInt32(obj));
         this.output.Write("{0}{1} = 0(", node.ThetaId.Identifier, node.ThetaId.IndexOfSSA);
         for (int i = 0; i < node.ThetaList.Count - 1; i++)
         {
            if (node.ThetaList[i].IndexOfSSA != -1)
               this.output.Write("{0}{1}", node.ThetaList[i].Identifier, node.ThetaList[i].IndexOfSSA);
            else
               this.output.Write("{0}", node.ThetaList[i].Identifier);

            this.output.Write(" ({0}), ", printType(node.ThetaList[i].ExpressionType));
         }

         if (node.ThetaList[node.ThetaList.Count - 1].IndexOfSSA != -1)
            this.output.Write("{0}{1}", node.ThetaList[node.ThetaList.Count - 1].Identifier, node.ThetaList[node.ThetaList.Count - 1].IndexOfSSA);
         else
            this.output.Write("{0}", node.ThetaList[node.ThetaList.Count - 1].Identifier);

         this.output.WriteLine(" ({0}))", printType(node.ThetaList[node.ThetaList.Count - 1].ExpressionType));

         return null;
      }

      #endregion
   }
}
