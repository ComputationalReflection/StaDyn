////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Visitor.cs                                                           //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Abstract class to define different visits over the abstract syntax tree //
//    Implements Visitor pattern [Visitor].                                   //
// -------------------------------------------------------------------------- //
// Create date: 11-12-2006                                                    //
// Modification date: 25-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;

namespace Tools {
    /// <summary>
    /// Abstract class to define different visits over the abstract syntax tree.
    /// </summary>
    /// <remarks>
    /// Implements Visitor pattern [Visitor].
    /// </remarks>
    public abstract class Visitor {
        public abstract Object Visit(SourceFile node, Object obj);
        public abstract Object Visit(Namespace node, Object obj);
        public abstract Object Visit(DeclarationSet node, Object obj);
        public abstract Object Visit(FieldDeclarationSet node, Object obj);
        public abstract Object Visit(IdDeclaration node, Object obj);
        public abstract Object Visit(Definition node, Object obj);
        public abstract Object Visit(ConstantDefinition node, Object obj);
        public abstract Object Visit(ClassDefinition node, Object obj);
        public abstract Object Visit(InterfaceDefinition node, Object obj);
        public abstract Object Visit(PropertyDefinition node, Object obj);
        public abstract Object Visit(FieldDeclaration node, Object obj);
        public abstract Object Visit(FieldDefinition node, Object obj);
        public abstract Object Visit(ConstantFieldDefinition node, Object obj);
        public abstract Object Visit(MethodDeclaration node, Object obj);
        public abstract Object Visit(MethodDefinition node, Object obj);
        public abstract Object Visit(ConstructorDefinition node, Object obj);

        // Expressions
        public abstract Object Visit(ArgumentExpression node, Object obj);
        public abstract Object Visit(ArithmeticExpression node, Object obj);
        public abstract Object Visit(ArrayAccessExpression node, Object obj);
        public abstract Object Visit(AssignmentExpression node, Object obj);
        public abstract Object Visit(BaseCallExpression node, Object obj);
        public abstract Object Visit(BaseExpression node, Object obj);
        public abstract Object Visit(BinaryExpression node, Object obj);
        public abstract Object Visit(BitwiseExpression node, Object obj);
        public abstract Object Visit(BoolLiteralExpression node, Object obj);
        public abstract Object Visit(CastExpression node, Object obj);
        public abstract Object Visit(CharLiteralExpression node, Object obj);
        public abstract Object Visit(CompoundExpression node, Object obj);
        public abstract Object Visit(DoubleLiteralExpression node, Object obj);
        public abstract Object Visit(FieldAccessExpression node, Object obj);
        public abstract Object Visit(IntLiteralExpression node, Object obj);
        public abstract Object Visit(InvocationExpression node, Object obj);
        public abstract Object Visit(IsExpression node, Object obj);
        public abstract Object Visit(LogicalExpression node, Object obj);
        public abstract Object Visit(NewArrayExpression node, Object obj);
        public abstract Object Visit(NewExpression node, Object obj);
        public abstract Object Visit(NullExpression node, Object obj);
        public abstract Object Visit(QualifiedIdentifierExpression node, Object obj);
        public abstract Object Visit(RelationalExpression node, Object obj);
        public abstract Object Visit(SingleIdentifierExpression node, Object obj);
        public abstract Object Visit(StringLiteralExpression node, Object obj);
        public abstract Object Visit(TernaryExpression node, Object obj);
        public abstract Object Visit(ThisExpression node, Object obj);
        public abstract Object Visit(UnaryExpression node, Object obj);

        // Statements
        public abstract Object Visit(AssertStatement node, Object obj);
        public abstract Object Visit(Block node, Object obj);
        public abstract Object Visit(BreakStatement node, Object obj);
        public abstract Object Visit(CatchStatement node, Object obj);
        public abstract Object Visit(ContinueStatement node, Object obj);
        public abstract Object Visit(DoStatement node, Object obj);
        public abstract Object Visit(ForeachStatement node, Object obj);
        public abstract Object Visit(ForStatement node, Object obj);
        public abstract Object Visit(IfElseStatement node, Object obj);
        public abstract Object Visit(MoveStatement node, Object obj);
        public abstract Object Visit(ReturnStatement node, Object obj);
        public abstract Object Visit(SwitchLabel node, Object obj);
        public abstract Object Visit(SwitchSection node, Object obj);
        public abstract Object Visit(SwitchStatement node, Object obj);
        public abstract Object Visit(ThetaStatement node, Object obj);
        public abstract Object Visit(ThrowStatement node, Object obj);
        public abstract Object Visit(ExceptionManagementStatement node, Object obj);
        public abstract Object Visit(WhileStatement node, Object obj);
        


        #region getInheritedAttributes<T>()
        /// <summary>
        /// Generic helper function that takes the visitor parameter an converts it into the appropiate
        /// inherited attribute.
        /// </summary>
        /// <typeparam name="T">The expected type of the attribute</typeparam>
        /// <param name="attributes">The visitor parameter that encapsulates all the inherited attributes</param>
        /// <param name="index">The index of the attribute</param>
        /// <returns>The expected inherited attribute</returns>
        public static T getInheritedAttributes<T>(Object attributes, int index)  {
            bool isArray = attributes is System.Array;
            T attribute = default(T);
            if (!isArray && index > 0)
                throw new ArgumentException("The index parameter should be 0 the attributes parameter is not an array.");
            if (!isArray) {
                if (attributes is T)
                    attribute = (T)attributes;
                else
                    throw new ArgumentException("The expected type is not the same as the actual one.");
                return attribute;
            }
            Object[] array = attributes as Object[];
            if (array == null)
                throw new ArgumentException("The expected type is not the same as the actual one.");
            if (array[index] == null)
                return default(T);
            if (!(array[index] is T))
                throw new ArgumentException(String.Format("The {0} expected type is not the same as the actual one.", index));
            if (array[index] is T)
                attribute=(T)array[index];
            return attribute;
        }
        /// <summary>
        /// Overloaded version of the above method
        /// </summary>
        /// <typeparam name="T">The expected type of the attribute</typeparam>
        /// <param name="attributes">The visitor parameter that encapsulates the inherited attribute</param>
        /// <returns>The expected inherited attribute</returns>
        public static T getInheritedAttributes<T>(Object attributes)  {
            return getInheritedAttributes<T>(attributes, 0);
        }
        #endregion

    }
}