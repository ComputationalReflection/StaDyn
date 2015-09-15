////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorSymbolIdentification.cs                                       //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    This class visits the AST to assign symbol information to identifier    //
//    It also sets the Symbol's "dynamnic" property from the DynVarManager    //
// expression.                                                                //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 30-03-2007                                                    //
// Modification date: 22-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AST;
using ErrorManagement;
using Symbols;
using Tools;
using TypeSystem;
using DynVarManagement;

namespace Semantic
{
    /// <summary>
    /// This class visits the AST to assign symbol information to identifier
    /// expression.
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorAdapter.
    /// Implements Visitor pattern [Concrete Visitor].
    /// </remarks>
    class VisitorSymbolIdentification : VisitorAdapter
    {
        #region Fields

        /// <summary>
        /// References to the symbol table.
        /// </summary>
        private SymbolTable table;

        /// <summary>
        /// Stores scope identifiers
        /// </summary>
        private List<string> usings;

        /// <summary>
        /// Represents the type of the current class (this).
        /// </summary>
        private UserType thisType;

        /// <summary>
        /// Represents the identifier of the current Namespace.
        /// </summary>
        private string currentNamespace;

        /// <summary>
        /// Represents the identifier of the current Class.
        /// </summary>
        private string currentClass;

        /// <summary>
        /// Represents the identifier of the current Method.
        /// </summary>
        private string currentMethod;

        /// <summary>
        /// Represents the base type of the current class (base).
        /// </summary>
        private UserType baseType;

        /// <summary>
        /// References to the dynamic variables manager.
        /// </summary>
        private DynVarManagement.DynVarManager manager;

        /// <summary>
        /// Stores the current block to use in DynVarManager.
        /// </summary>
        private List<int> blockList;

        /// <summary>
        /// Index for block list;
        /// </summary>
        private int indexBlockList = 0;

        /// <summary>
        /// A mapping between each sort file name and its full directory name
        /// </summary>
        private IDictionary<string, string> directories;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of VisitorSymbolIdentification
        /// <param name="directories">A mapping between each sort file name and its full directory name</param>
        /// </summary>
        public VisitorSymbolIdentification(IDictionary<string, string> directories)
        {
            this.table = new SymbolTable();
            this.usings = new List<string>();
            this.manager = new DynVarManagement.DynVarManager();
            this.currentNamespace = "";
            this.currentClass = "";
            this.currentMethod = "";
            this.directories = directories;
        }

        #endregion

        #region loadDynVars

        /// <summary>
        /// Loads the info in the DynVarManager
        /// </summary>
        private void loadDynVars()
        {
            string dynFile = Path.ChangeExtension(this.currentFile, DynVarManagement.DynVarManager.DynVarFileExt);

            if (File.Exists(dynFile))
            {
                try
                {
                    manager.Load(dynFile);
                }
                catch (DynVarManagement.DynVarException e)
                {
                    ErrorManager.Instance.NotifyError(new LoadingDynVarsError(e.Message));
                }
            }
        }


        #endregion

        #region searchDynInfo()

        /// <summary>
        /// Searches a dynamic reference to the specified identifier.
        /// </summary>
        /// <param name="id">variable identifier.</param>
        /// <returns>True if the identifier is a dynamic variable, false otherwise.</returns>
        private bool searchDynInfo(string id)
        {
            if (manager.Ready)
            {
                if (this.currentMethod.Length == 0)
                    return manager.SearchDynVar(this.currentNamespace, this.currentClass, id);
                if ((this.blockList != null) && (this.blockList.Count != 0) && (this.indexBlockList > 0))
                    return manager.SearchDynVar(this.currentNamespace, this.currentClass, this.currentMethod, this.blockList.GetRange(0, this.indexBlockList), id);
                return manager.SearchDynVar(this.currentNamespace, this.currentClass, this.currentMethod, id);
            }
            return false;
        }

        #endregion

        #region Visit(SourceFile node, Object obj)

        public override Object Visit(SourceFile node, Object obj)
        {
            this.currentFile = node.Location.FileName;

            this.loadDynVars();

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
            this.table.Set();
            this.currentNamespace = node.Identifier.Identifier;

            usings.Add(node.Identifier.Identifier);

            for (int i = 0; i < node.NamespaceMembersCount; i++)
            {
                node.GetDeclarationElement(i).Accept(this, obj);
            }

            usings.Remove(node.Identifier.Identifier);

            this.currentNamespace = "";
            this.table.Reset();
            return null;
        }

        #endregion

        #region Visit(ClassDefinition node, Object obj)

        public override Object Visit(ClassDefinition node, Object obj)
        {
            this.table.Set();
            this.currentClass = node.Identifier;
            this.thisType = (UserType)node.TypeExpr;
            this.baseType = ((ClassType)node.TypeExpr).BaseClass;

            // first step
            for (int i = 0; i < node.MemberCount; i++)
            {
                if (node.GetMemberElement(i) is FieldDeclaration)
                    this.fieldDeclarationSymbol((FieldDeclaration)node.GetMemberElement(i));
            }

            // second step
            for (int i = 0; i < node.MemberCount; i++)
            {
                node.GetMemberElement(i).Accept(this, obj);
            }

            this.currentClass = "";
            this.thisType = null;
            this.baseType = null;
            this.table.Reset();
            return null;
        }

        #endregion

        #region Visit(IdDeclaration node, Object obj)

        public override Object Visit(IdDeclaration node, Object obj)
        {
            node.Symbol = declarationSymbol(node);
            return null;
        }

        #endregion

        #region Visit(Definition node, Object obj)

        public override Object Visit(Definition node, Object obj)
        {
            node.Init.Accept(this, obj);
            node.Symbol = declarationSymbol(node);
            return null;
        }

        #endregion

        #region Visit(ConstantDefinition node, Object obj)

        public override Object Visit(ConstantDefinition node, Object obj)
        {
            node.Init.Accept(this, obj);
            node.Symbol = declarationSymbol(node);
            return null;
        }

        #endregion

        #region declarationSymbol()

        /// <summary>
        /// Inserts the symbol associated to the declaration
        /// </summary>
        /// <param name="node">Declaration information.</param>
        /// <returns>The symbol inserted</returns>
        private Symbol declarationSymbol(IdDeclaration node)
        {
            TypeExpression te = this.searchType(node.FullName, node.Location.Line, node.Location.Column);
            if (te != null)
            {                
                node.TypeExpr = te;
                string id = node.Identifier;
                if (node.IdentifierExp.IndexOfSSA != -1)
                    id += node.IdentifierExp.IndexOfSSA;
                if (te is TypeVariable && te.IsDynamic)                
                    setDynamic(node.Identifier);                
                Symbol symbol = this.table.Insert(id, te,searchDynInfo(node.Identifier));
                if (symbol == null)
                    ErrorManager.Instance.NotifyError(new DeclarationFoundError(node.Identifier, new Location(this.currentFile, node.Location.Line, node.Location.Column)));
                return symbol;
            }
            ErrorManager.Instance.NotifyError(new UnknownTypeError(node.FullName, new Location(this.currentFile, node.Location.Line, node.Location.Column)));
            return null;
        }

        private void setDynamic(String identifier)
        {
            VarPath varPath = new VarPath();
            varPath.NamespaceName = currentNamespace;
            varPath.ClassName = currentClass;
            if (!String.IsNullOrEmpty(currentMethod))
                varPath.MethodName = currentMethod;
            varPath.VarName = identifier;
            if(manager.Ready)                
                manager.SetDynamic(varPath);
        }

        #endregion

        #region fieldDeclarationSymbol()

        /// <summary>
        /// Inserts the symbol associated to the field
        /// </summary>
        /// <param name="node">Field information.</param>
        private void fieldDeclarationSymbol(FieldDeclaration node)
        {
            if (node.TypeExpr == null)
            {
                ErrorManager.Instance.NotifyError(new UnknownTypeError(node.FullName, new Location(this.currentFile, node.Location.Line, node.Location.Column)));
                return;
            }
            if ((node.TypeExpr is FieldType) && ((FieldType)node.TypeExpr).FieldTypeExpression is TypeVariable && ((FieldType)node.TypeExpr).FieldTypeExpression.IsDynamic)
               setDynamic(node.Identifier);                            
            bool isDynamic = searchDynInfo(node.Identifier);
            if (this.table.Insert(node.Identifier, node.TypeExpr, isDynamic) == null)
            {
                ErrorManager.Instance.NotifyError(new DeclarationFoundError(node.Identifier, new Location(this.currentFile, node.Location.Line, node.Location.Column)));
                return;
            }
            DynVarOptions.Instance.AssignDynamism(this.thisType.Fields[node.Identifier].Type, isDynamic);
        }

        #endregion

        #region Visit(MethodDefinition node, Object obj)

        public override Object Visit(MethodDefinition node, Object obj)
        {
            this.blockList = new List<int>();
            this.indexBlockList = 0;

            this.table.Set();
            this.currentMethod = node.Identifier;
            node.IsReturnDynamic = this.searchDynInfo("");
            this.parameterSymbol(node);
            node.Body.Accept(this, node);
            this.currentMethod = "";
            this.table.Reset();
            return null;
        }

        #endregion

        #region Visit(ConstructorDefinition node, Object obj)

        public override Object Visit(ConstructorDefinition node, Object obj)
        {
            this.blockList = new List<int>();
            this.indexBlockList = 0;

            this.table.Set();
            this.currentMethod = node.Identifier;
            this.parameterSymbol(node);

            if (node.Initialization != null)
                node.Initialization.Accept(this, obj);

            node.Body.Accept(this, node);
            this.currentMethod = "";
            this.table.Reset();
            return null;
        }

        #endregion

        #region parameterSymbol()

        private void parameterSymbol(MethodDefinition node)
        {
            for (int i = 0; i < ((MethodType)node.TypeExpr).ParameterListCount; i++)
            {
                string id = node.ParametersInfo[i].Identifier;
                if (!(((MethodType)node.TypeExpr).GetParameter(i) is ArrayType))
                    id += "0";
                if (((MethodType)node.TypeExpr).GetParameter(i) is TypeVariable && ((MethodType)node.TypeExpr).GetParameter(i).IsDynamic)
                    setDynamic(node.ParametersInfo[i].Identifier);                 
                if (this.table.Insert(id, ((MethodType)node.TypeExpr).GetParameter(i), searchDynInfo(node.ParametersInfo[i].Identifier)) == null)
                    ErrorManager.Instance.NotifyError(new DeclarationFoundError(node.ParametersInfo[i].Identifier, new Location(this.currentFile, node.Location.Line, node.Location.Column)));
            }
        }

        #endregion

        // Literals

        #region Visit(SingleIdentifierExpression node, Object obj)

        public override Object Visit(SingleIdentifierExpression node, Object obj)
        {
            Symbol sym = null;
            if (node.IndexOfSSA != -1)
                sym = this.table.Search(node.Identifier + node.IndexOfSSA);
            else
                sym = this.table.Search(node.Identifier);

            if (sym != null)
                node.IdSymbol = (Symbol)sym;

            return null;
        }

        #endregion

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

            while ((typeIdentifier.Contains("[]")) && (!found))
            {
                te = TypeTable.Instance.GetType(typeIdentifier, new Location(this.currentFile, line, column));
                if (te == null)
                {
                    typeIdentifier = typeIdentifier.Substring(0, typeIdentifier.Length - 2);
                    rank++;
                }
                else
                    found = true;
            }

            if (!found)
                te = TypeTable.Instance.GetType(typeIdentifier, new Location(this.currentFile, line, column));

            if (te == null)
            {
                for (int i = 0; i < usings.Count; i++)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append(usings[i]);
                    str.Append(".");
                    str.Append(typeIdentifier);

                    te = TypeTable.Instance.GetType(str.ToString(), new Location(this.currentFile, line, column));
                    if (te != null)
                        break;
                }
            }

            if (te != null)
            {
                if (rank != 0)
                {
                    for (int i = 0; i < rank; i++)
                    {
                        te = new ArrayType(te);
                        if (!TypeTable.Instance.ContainsType(te.FullName))
                            TypeTable.Instance.AddType(te.FullName, te, new Location(this.currentFile, line, column));
                    }
                }
            }

            return te;
        }

        #endregion

        #region compoundExpressionToArray()

        /// <summary>
        /// Gets the type expression of the arguments.
        /// </summary>
        /// <param name="args">Arguments information.</param>
        /// <returns>Returns the argument type expressions </returns>
        private TypeExpression[] compoundExpressionToArray(CompoundExpression args)
        {
            TypeExpression[] aux = new TypeExpression[args.ExpressionCount];
            TypeExpression te;

            for (int i = 0; i < args.ExpressionCount; i++)
            {
                if ((te = args.GetExpressionElement(i).ExpressionType) != null)
                    aux[i] = te;
                else
                    return null;
            }
            return aux;
        }

        #endregion

        // Statements

        #region Visit(Block node, Object obj)

        public override Object Visit(Block node, Object obj)
        {
            if (node.StatementCount != 0)
            {
                if (!(obj is MethodDefinition)) // Set block
                {
                    if (this.indexBlockList >= this.blockList.Count)
                        this.blockList.Add(0);
                    else
                        this.blockList[indexBlockList] = this.blockList[indexBlockList] + 1;
                    this.indexBlockList++;
                }

                for (int i = 0; i < node.StatementCount; i++)
                    node.GetStatementElement(i).Accept(this, null);

                if (!(obj is MethodDefinition))
                {
                    for (int i = this.indexBlockList; i < this.blockList.Count; i++)
                        this.blockList.RemoveAt(i);
                    this.indexBlockList--;
                }
            }

            return null;
        }

        #endregion

        #region Visit(DoStatement node, Object obj)

        public override Object Visit(DoStatement node, Object obj)
        {
            for (int i = 0; i < node.InitDo.Count; i++)
            {
                node.InitDo[i].Accept(this, obj);
            }
            this.table.Set();
            for (int i = 0; i < node.BeforeBody.Count; i++)
            {
                node.BeforeBody[i].Accept(this, obj);
            }
            node.Statements.Accept(this, obj);
            this.table.Reset();
            node.Condition.Accept(this, obj);
            return null;
        }

        #endregion

        #region Visit(ForStatement node, Object obj)

        public override Object Visit(ForStatement node, Object obj)
        {
            this.table.Set();
            for (int i = 0; i < node.InitializerCount; i++)
            {
                node.GetInitializerElement(i).Accept(this, obj);
            }
            for (int i = 0; i < node.AfterInit.Count; i++)
            {
                node.AfterInit[i].Accept(this, obj);
            }
            for (int i = 0; i < node.BeforeCondition.Count; i++)
            {
                node.BeforeCondition[i].Accept(this, obj);
            }
            node.Condition.Accept(this, obj);
            for (int i = 0; i < node.AfterCondition.Count; i++)
            {
                node.AfterCondition[i].Accept(this, obj);
            }
            node.Statements.Accept(this, obj);
            for (int i = 0; i < node.IteratorCount; i++)
            {
                node.GetIteratorElement(i).Accept(this, obj);
            }
            this.table.Reset();
            return null;
        }

        #endregion

        #region Visit(IfElseStatement node, Object obj)

        public override Object Visit(IfElseStatement node, Object obj)
        {
            node.Condition.Accept(this, obj);
            for (int i = 0; i < node.AfterCondition.Count; i++)
            {
                node.AfterCondition[i].Accept(this, obj);
            }
            this.table.Set();
            node.TrueBranch.Accept(this, obj);
            this.table.Reset();
            this.table.Set();
            node.FalseBranch.Accept(this, obj);
            this.table.Reset();
            for (int i = 0; i < node.ThetaStatements.Count; i++)
            {
                node.ThetaStatements[i].Accept(this, obj);
            }
            return null;
        }

        #endregion

        #region Visit(WhileStatement node, Object obj)

        public override Object Visit(WhileStatement node, Object obj)
        {
            for (int i = 0; i < node.InitWhile.Count; i++)
            {
                node.InitWhile[i].Accept(this, obj);
            }
            for (int i = 0; i < node.BeforeCondition.Count; i++)
            {
                node.BeforeCondition[i].Accept(this, obj);
            }
            node.Condition.Accept(this, obj);
            for (int i = 0; i < node.AfterCondition.Count; i++)
            {
                node.AfterCondition[i].Accept(this, obj);
            }
            this.table.Set();
            node.Statements.Accept(this, obj);
            this.table.Reset();
            return null;
        }

        #endregion

        #region Visit(SwitchStatement node, Object obj)

        public override Object Visit(SwitchStatement node, Object obj)
        {
            node.Condition.Accept(this, obj);
            for (int i = 0; i < node.AfterCondition.Count; i++)
            {
                node.AfterCondition[i].Accept(this, obj);
            }

            for (int i = 0; i < node.SwitchBlockCount; i++)
            {
                this.table.Set();
                node.GetSwitchSectionElement(i).Accept(this, obj);
                this.table.Reset();
            }
            for (int i = 0; i < node.ThetaStatements.Count; i++)
            {
                node.ThetaStatements[i].Accept(this, obj);
            }
            return null;
        }

        #endregion
    }
}
