////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorTypeDefinition.cs                                             //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    This class visits the AST to store the type of each declaration in
//      the TypeExpr attribute and (indirectly) to build all 
//      the types in the TypeTable
//    Once all the classes and interfaces have been included in the 
//      TypeTable (VisitorTypeLoad), inheritance is resolved, adding type
//      referenceces between each class with its superclass and 
//      implemented interfaces.
//    Since all the type string representation have been decorated by the
//      parser in the TypeInfo attribute, this visitor resolves all the 
//      strings coverting them to TypeExpression references.
//    All the type references (clases'IDs to access static members, news, 
//      casts, is, this, base, namespaces...) are solved, 
//      assingning a TypeExpression reference to them
//    It also adds a binding between the MethodType and its regarding ASTNode 
// Includes methods, fields and inheritance.                                  //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 25-01-2007                                                    //
// Modification date: 26-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AST;
using ErrorManagement;
using TypeSystem;
using Tools;

namespace Semantic
{
   /// <summary>
   /// This class visits the AST to store the type of each declaration.
   /// Includes methods, fields and inheritance.
   /// </summary>
   /// <remarks>
   /// Inheritance: VisitorAdapter.
   /// Implements Visitor pattern [Concrete Visitor].
   /// </remarks>
   class VisitorTypeDefinition : VisitorAdapter
   {
      #region Fields

      /// <summary>
      /// Stores the name of the current namespace.
      /// </summary>
      private string currentNamespace;

      /// <summary>
      /// Stores scope identifiers
      /// </summary>
      private List<string> usings;

      /// <summary>
      /// Represents the type of the current class (this).
      /// </summary>
      private UserType thisType;

      /// <summary>
      /// Represents the base type of the current class (base).
      /// </summary>
      private UserType baseType;


      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of VisitorTypeDefinition
      /// </summary>
      public VisitorTypeDefinition()
      {
         this.currentNamespace = "";
         usings = new List<string>();
      }

      #endregion

      #region Visit(SourceFile node, Object obj)

      public override Object Visit(SourceFile node, Object obj)
      {
         this.currentFile = node.Location.FileName;

         for (int i = 0; i < node.Usings.Count; i++)
         {
            usings.Add(node.Usings[i]);
         }

         foreach (string key in node.Namespacekeys)
         {
            int count = node.GetNamespaceDefinitionCount(key);
            for (int i = 0; i < count; i++)
            {
               node.GetNamespaceDeclarationElement(key, i).Accept(this, obj);
            }
         }

         for (int i = 0; i < node.DeclarationCount; i++)
         {
            node.GetDeclarationElement(i).Accept(this, obj);
         }
         return null;
      }

      #endregion

      #region Visit(Namespace node, Object obj)

      public override Object Visit(Namespace node, Object obj)
      {
         this.currentNamespace = node.Identifier.Identifier;
         usings.Add(node.Identifier.Identifier);

         for (int i = 0; i < node.NamespaceMembersCount; i++)
         {
            node.GetDeclarationElement(i).Accept(this, obj);
         }

         this.currentNamespace = "";
         usings.Remove(node.Identifier.Identifier);

         return null;
      }

      #endregion

      #region Visit(ClassDefinition node, Object obj)

      public override Object Visit(ClassDefinition node, Object obj)
      {
         this.thisType = (UserType)node.TypeExpr;

         for (int i = 0; i < node.BaseClass.Count; i++)
         {
            UserType baseClassOrInterface = (UserType)this.searchType(node.BaseClass[i], node.Location.Line, node.Location.Column);
            ClassType klass = baseClassOrInterface as ClassType,
                nodeType = (ClassType)node.TypeExpr;
            if (klass != null)
            {
               nodeType.AddBaseClass(klass, new Location(this.currentFile, node.Location.Line, node.Location.Column));
               this.baseType = klass;
            }
            else
            {
               InterfaceType interfaze = baseClassOrInterface as InterfaceType;
               if (interfaze != null)
                  nodeType.AddBaseInterface((InterfaceType)baseClassOrInterface, new Location(this.currentFile, node.Location.Line, node.Location.Column));
               else
                  ErrorManager.Instance.NotifyError(new UnknownTypeError(node.BaseClass[i], node.Location));
            }
         }
         // * If node base class is defined, System.Object is added
         ClassType typeOfNode = node.TypeExpr as ClassType;
         if (typeOfNode != null && typeOfNode.BaseClass == null)
         {
            this.baseType = (ClassType)TypeTable.Instance.GetType("System.Object", node.Location);
            typeOfNode.AddBaseClass((ClassType)this.baseType, node.Location);
         }

         ((ClassType)node.TypeExpr).Members = builtClass(node, obj);
         node.TypeExpr.BuildTypeExpressionString(TypeExpression.MAX_DEPTH_LEVEL_TYPE_EXPRESSION);

         this.thisType = null;
         this.baseType = null;

         return null;
      }

      #endregion

      #region Visit(InterfaceDefinition node, Object obj)

      public override Object Visit(InterfaceDefinition node, Object obj)
      {
         for (int i = 0; i < node.BaseClass.Count; i++)
         {
             TypeExpression typeExpression = this.searchType(node.BaseClass[i], node.Location.Line, node.Location.Column);
            if (typeExpression == null)
               ErrorManager.Instance.NotifyError(new UnknownTypeError(node.BaseClass[i], node.Location));
            else
            {
               InterfaceType interfaze = typeExpression as InterfaceType;
               if (interfaze == null)
                  ErrorManager.Instance.NotifyError(new ExpectedInterfaceError(node.BaseClass[i], node.Location));
               else
                  ((InterfaceType)node.TypeExpr).AddBaseInterface(interfaze, node.Location);
            }
         }
         ((InterfaceType)node.TypeExpr).Members = builtClass(node, obj);
         node.TypeExpr.BuildTypeExpressionString(TypeExpression.MAX_DEPTH_LEVEL_TYPE_EXPRESSION);
         return null;
      }

      #endregion

      #region builtClass()

      private Dictionary<string, AccessModifier> builtClass(TypeDefinition node, Object obj)
      {
         Dictionary<string, AccessModifier> membersWithIntersectionTypes = new Dictionary<string, AccessModifier>();
         Dictionary<string, List<AccessModifier>> members = new Dictionary<string, List<AccessModifier>>();
         List<int> remove = new List<int>();
         AccessModifier accessModifier = null;
         int count = 0;

         for (int i = 0; i < node.MemberCount; i++)
         {
            accessModifier = (AccessModifier)(node.GetMemberElement(i).Accept(this, obj));
            if (accessModifier != null)
            {
               accessModifier.Class = (UserType)(node.TypeExpr);

               if (!(members.ContainsKey(accessModifier.MemberIdentifier)))
                  members.Add(accessModifier.MemberIdentifier, new List<AccessModifier>());
               // Insert element. The accessModifier list only can have one FieldType
               if (((members[accessModifier.MemberIdentifier].Count != 0) && (accessModifier.Type is FieldType)) ||
                    ((members[accessModifier.MemberIdentifier].Count == 1) && (members[accessModifier.MemberIdentifier][0].Type is FieldType)))
               {
                  remove.Add(i);
                  ErrorManager.Instance.NotifyError(new DeclarationFoundError(accessModifier.MemberIdentifier, new Location(this.currentFile, node.GetMemberElement(i).Location.Line, node.GetMemberElement(i).Location.Column)));
               }
               else
               {
                  members[accessModifier.MemberIdentifier].Add(accessModifier);
                  //accessModifier.WriteType.BuildTypeExpressionString(TypeExpression.MAX_DEPTH_LEVEL_TYPE_EXPRESSION);
               }
            }
         }
         // Removes incorrect declarations
         for (int i = 0; i < remove.Count; i++, count++)
         {
            node.RemoveMemberElement(remove[i] - count);
         }
         // * Converts method overload into intersection types
         foreach (KeyValuePair<string, List<AccessModifier>> pair in members)
            if (pair.Value.Count == 1)
               // * No intersection is necessary
               membersWithIntersectionTypes[pair.Key] = pair.Value[0];
            else
            { // * We add an intersection type
               // * An access modifier is taken
               AccessModifier am = pair.Value[0];
               // * We add each type to the intersection type
               IntersectionMemberType intersection = new IntersectionMemberType();
               foreach (AccessModifier accMod in pair.Value)
               {
                  MethodType method = (MethodType)accMod.Type;
                  if (!intersection.AddMethod(method))
                  {
                     AstNode astMethodNode = method.ASTNode;
                     ErrorManager.Instance.NotifyError(new OverloadError(am.MemberIdentifier, astMethodNode.Location));
                  }
               }
               am.Type = intersection;
               membersWithIntersectionTypes[pair.Key] = am;
            }
         return membersWithIntersectionTypes;
      }

      #endregion

      #region Visit(FieldDeclaration node, Object obj)

      public override Object Visit(FieldDeclaration node, Object obj)
      {
         return this.builtField(node);
      }

      #endregion

      #region Visit(FieldDefinition node, Object obj)

      public override Object Visit(FieldDefinition node, Object obj)
      {
         Object accessModifier = this.builtField(node);
         node.Init.Accept(this, obj);
         return accessModifier;
      }

      #endregion

      #region Visit(ConstantFieldDefinition node, Object obj)

      public override Object Visit(ConstantFieldDefinition node, Object obj)
      {
         return this.builtField(node);
      }

      #endregion

      #region builtField()

      private Object builtField(FieldDeclaration node)
      {
          TypeExpression ft = searchType(node.TypeInfo, node.Location.Line, node.Location.Column);
         FieldType type = new FieldType(ft);
         AccessModifier accessModifierInfo = new AccessModifier(node.ModifiersInfo, node.Identifier, type, false);
         type.MemberInfo = accessModifierInfo;
         node.TypeExpr = type;
         return accessModifierInfo;
      }

      #endregion

      #region Visit(MethodDeclaration node, Object obj)

      public override Object Visit(MethodDeclaration node, Object obj)
      {
         return builtMethod(node);
      }

      #endregion

      #region Visit(MethodDefinition node, Object obj)

      public override Object Visit(MethodDefinition node, Object obj)
      {
         Object modifiers = builtMethod(node);
         base.Visit(node, obj);
         return modifiers;
      }

      #endregion

      #region Visit(ConstructorDefinition node, Object obj)

      public override Object Visit(ConstructorDefinition node, Object obj)
      {
         Object modifiers = builtMethod(node);
         base.Visit(node, obj);
         return modifiers;
      }

      #endregion

      #region builtMethod()

      private Object builtMethod(MethodDeclaration node)
      {
         bool interfaceMember = false;
         if (!((node is MethodDefinition) || (node is ConstructorDefinition)))
            interfaceMember = true;

         TypeExpression ret = this.searchType(node.ReturnTypeInfo, node.Location.Line, node.Location.Column);
         if (ret == null)
         {
            ErrorManager.Instance.NotifyError(new UnknownTypeError(node.ReturnTypeInfo, node.Location));
            ret = this.searchType("void", node.Location.Line, node.Location.Column);
         }
         MethodType type = new MethodType(ret);
         type.ASTNode = node; // * Binds the MethodType with the appropriate AST node
         for (int i = 0; i < node.ParametersInfo.Count; i++)
         {
            TypeExpression paramType = this.searchType(node.ParametersInfo[i].ParamType, node.ParametersInfo[i].Line, node.ParametersInfo[i].Column);
            if (paramType == null)
                ErrorManager.Instance.NotifyError(new UnknownTypeError(node.ParametersInfo[i].ParamType, new Location(node.Location.FileName, node.ParametersInfo[i].Line, node.ParametersInfo[i].Column)));
            type.AddParameter(paramType);
         }
         AccessModifier accessModifierInfo = new AccessModifier(node.ModifiersInfo, node.Identifier, type, interfaceMember);
         type.MemberInfo = accessModifierInfo;
         node.TypeExpr = type;
         return accessModifierInfo;
      }

      #endregion

      #region Visit(BaseCallExpression node, Object obj)

      public override Object Visit(BaseCallExpression node, Object obj)
      {
         node.Arguments.Accept(this, obj);
         node.BaseType = searchType(this.baseType.Name, node.Location.Line, node.Location.Column);
         return null;
      }

      #endregion

      #region Visit(CastExpression node, Object obj)

      public override Object Visit(CastExpression node, Object obj)
      {
         node.Expression.Accept(this, obj);
         node.CastType = this.searchType(node.CastId, node.Location.Line, node.Location.Column);
         if (node.CastType == null)
            ErrorManager.Instance.NotifyError(new UnknownTypeError(node.CastId, new Location (this.currentFile, node.Location.Line, node.Location.Column)));
         return null;
      }

      #endregion

      #region Visit(IsExpression node, Object obj)

      public override Object Visit(IsExpression node, Object obj)
      {
         node.Expression.Accept(this, obj);
         node.TypeExpr = this.searchType(node.TypeId, node.Location.Line, node.Location.Column);
         return null;
      }
      #endregion

      #region Visit(NewArrayExpression node, Object obj)

      public override Object Visit(NewArrayExpression node, Object obj)
      {
         if (node.Size != null)
            node.Size.Accept(this, obj);
         if (node.Init != null)
            node.Init.Accept(this, obj);

         string typeIdentifier = node.TypeInfo;
         while (typeIdentifier.Contains("[]"))
            typeIdentifier = typeIdentifier.Substring(0, typeIdentifier.Length - 2);
         for (int i = 0; i < node.Rank; i++)
            typeIdentifier = typeIdentifier + "[]";

         node.ExpressionType = searchType(typeIdentifier, node.Location.Line, node.Location.Column);

         return null;
      }

      #endregion

      #region Visit(NewExpression node, Object obj)

      public override Object Visit(NewExpression node, Object obj)
      {
         node.Arguments.Accept(this, obj);
         node.NewType = searchType(node.TypeInfo, node.Location.Line, node.Location.Column);
         return null;
      }
      #endregion

      #region Visit(SingleIdentifierExpression node, Object obj)
      public override Object Visit(SingleIdentifierExpression node, Object obj)
      {
         // * Is the identifier a class, interface or namespace?
          node.ExpressionType = searchType(node.Identifier, node.Location.Line, node.Location.Column);
         if (node.ExpressionType is UserType)
         {
            node.IdMode = IdentifierMode.UserType;
            return null;
         }

         // * If the identifier is the starting name of all the BCL namespaces (System)
         //   a BCL namespace is created
         if (node.Identifier.Equals("System"))
         {
            node.IdMode = IdentifierMode.NameSpace;
            node.ExpressionType = new BCLNameSpaceType(node.Identifier);
            return null;
         }

         // * Is the identifier a namespace defined by the user?
         if (usings.Contains(node.Identifier))
         {
            node.IdMode = IdentifierMode.NameSpace;
            node.ExpressionType = new NameSpaceType(node.Identifier);
            return null;
         }

         // * Is the identifier the starting name of the user's namespaces?
         foreach (string userUsing in usings)
            if (userUsing.Contains("."))
            {
               string firstName = userUsing.Split(new char[] { '.' })[0];
               if (firstName.Equals(node.Identifier))
               {
                    node.IdMode = IdentifierMode.NameSpace;
                    node.ExpressionType = new NameSpaceType(firstName);
                    return null;
               }
            }

         // * Is the identifier contained in the name of the user's namespaces?
         foreach (string userUsing in usings)
         {
            if (userUsing.Contains("."+node.Identifier))
            {
                node.IdMode = IdentifierMode.NameSpace;
                node.ExpressionType = new NameSpaceType(node.Identifier, userUsing.Substring(0, userUsing.IndexOf("." + node.Identifier) + ("." + node.Identifier).Length));
                return null;
            }
        }

         // * Otherwise, it must be an instance
         node.IdMode = IdentifierMode.Instance;
         return null;
      }
      #endregion

      #region Visit(FieldAccessExpression node, Object obj)
      public override Object Visit(FieldAccessExpression node, Object obj)
      {
         node.Expression.Accept(this, obj);
         node.FieldName.Accept(this, obj);

         // * If the first name is a namespace, then the result could be...
         NameSpaceType nst = node.Expression.ExpressionType as NameSpaceType;
         if (nst != null)
         {
            // * ... another namepace or ...
            nst = nst.concat(node.FieldName.Identifier);
            // * ... a class
            TypeExpression userType = TypeTable.Instance.GetType(nst.Name, node.Location);
            if (userType != null)
               // The class
               node.ExpressionType = userType;
            else // The namespace
               node.ExpressionType = nst;
           // * We notify the following visitors that the type has been inferred in the type definition
           node.TypeInferredInVisitorTypeDefinition = true;
         }
         return null;
      }
      #endregion

      #region Visit(ThisExpression node, Object obj)
      public override Object Visit(ThisExpression node, Object obj)
      {
         node.ExpressionType = this.thisType;
         return null;
      }
      #endregion

      #region Visit(BaseExpression node, Object obj)
      public override Object Visit(BaseExpression node, Object obj)
      {
         node.ExpressionType = this.baseType;
         return null;
      }
      #endregion


      // Helper Methods

      #region searchType()

      /// <summary>
      /// Searches a type expression associated to the specified name.
      /// </summary>
      /// <param name="typeIdentifier">WriteType name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      /// <returns>WriteType expression associated to the name.</returns>
      private TypeExpression searchType(string typeIdentifier, int line, int column)
      {
         TypeExpression te = null;
         bool found = false;
         int rank = 0;
         Location location  = new Location(this.currentFile, line, column);

         // * Array WriteType

         if (typeIdentifier.Contains("[]"))
         {
            do
            {
               typeIdentifier = typeIdentifier.Substring(0, typeIdentifier.Length - 2);
               rank++;
            } while (typeIdentifier.Contains("[]"));
            te = TypeTable.Instance.GetType(typeIdentifier, location);
            if (te != null)
               found = true;
         }

         // * UserType (class or interface)
         if (!found)
            te = TypeTable.Instance.GetType(typeIdentifier, location);

         // * Try to locate the type with a namespace prefix
         if (te == null)
         {
            for (int i = 0; i < usings.Count; i++)
            {
               StringBuilder str = new StringBuilder();
               str.Append(usings[i]);
               str.Append(".");
               str.Append(typeIdentifier);

               te = TypeTable.Instance.GetType(str.ToString(), location);
               if (te != null)
                  break;
            }
         }

         if (rank != 0)
         {
            for (int i = 0; i < rank; i++)
            {
               te = new ArrayType(te);
               if (!TypeTable.Instance.ContainsType(te.FullName))
                  TypeTable.Instance.AddType(te.FullName, te, location);
            }
         }

         return te;
      }
      #endregion

   }
}
