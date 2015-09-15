////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: InheritedAttributes.cs                                               //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class encapsulates several inherited attributes used in code       //
// generation process.                                                        //
// -------------------------------------------------------------------------- //
// Create date: 27-07-2007                                                    //
// Modification date: 28-07-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using AST;
using TypeSystem;

namespace CodeGeneration
{
   /// <summary>
   /// This class encapsulates several inherited attributes used in code
   /// generation process.
   /// </summary>
   struct InheritedAttributes
   {
      #region Fields

      /// <summary>
      /// Stores the current method definition node.
      /// </summary>
      private MethodDefinition node;

      /// <summary>
      /// True if assignment expression found. Otherwise, false.
      /// </summary>
      private bool assignment;

      /// <summary>
      /// Represents the reference of a concrete identifier.
      /// </summary>
      private Expression reference;

      /// <summary>
      /// True if array access expression found. Otherwise, false.
      /// </summary>
      private bool arrayAccess;

      /// <summary>
      /// Stores the type expression of the current invocation. If not exists, their value is null.
      /// </summary>
      private MethodType actualMethodCalled;

      /// <summary>
      /// True if the parent node is an InvocationExpression. Otherwise, false.
      /// </summary>
      private bool isParentNodeAnInvocation;

      /// <summary>
      /// When suitable, the parent node of the node that is being visited.
      /// It is very important for the SingleIndentifier node, to distinguish between the implicit object and 
      /// when the ID is a parameter
      /// </summary>
      private AstNode parentNode;
      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets true if assignment expression found. Otherwise, gets or sets false.
      /// </summary>
      public bool Assignment
      {
         get { return assignment; }
         set { assignment = value; }
      }


       private bool isIntrospectiveInvocation;
      /// <summary>
      /// Gets or sets true the node is an Introspective Invocation. Otherwise, gets or sets false.
      /// </summary>
      public bool IsIntrospectiveInvocation
      {
          get { return isIntrospectiveInvocation; }
          set { isIntrospectiveInvocation = value; }
      }

      /// <summary>
      /// Gets or sets true if array access expression found. Otherwise, gets or sets false.
      /// </summary>
      public bool ArrayAccessFound
      {
         get { return arrayAccess; }
         set { arrayAccess = value; }
      }

      /// <summary>
      /// Gets or sets the current method definition node.
      /// </summary>
      public MethodDefinition CurrentMethod
      {
         get { return this.node; }
         set { this.node = value; }
      }

      /// <summary>
      /// Gets or sets the reference of a concrete identifier.
      /// </summary>
      public Expression Reference
      {
         get { return this.reference; }
         set { this.reference = value; }
      }

      /// <summary>
      /// Gets or sets the type expression of the current invocation expression. If not exists, their value is null.
      /// </summary>
      public MethodType ActualMethodCalled
      {
         get { return this.actualMethodCalled; }
         set { this.actualMethodCalled = value; }
      }

      /// <summary>
      /// Gets or sets true if the parent node is an InvocationExpression. Otherwise, false.
      /// </summary>
      public bool IsParentNodeAnInvocation
      {
         get { return this.isParentNodeAnInvocation; }
         set { this.isParentNodeAnInvocation = value; }
      }

       /// <summary>
       /// When suitable, the parent node of the node that is being visited.
       /// It is very important for the SingleIndentifier node, to distinguish between the implicit object and 
       /// when the ID is a parameter
       /// </summary>
      public AstNode ParentNode {
          get { return this.parentNode; }
      }
       /// <summary>
       /// Indicates wheter a message is passed to the current expression.
       /// </summary>
      private bool messagePassed;

      public bool MessagePassed {
          get { return this.messagePassed; }

          set { this.messagePassed = value; }
      }

      #endregion
       
       #region Constructor
       // <summary>
      /// Constructor of InheritedAttributes
      /// </summary>
        public InheritedAttributes(MethodDefinition methodInfo, bool assignmentFound, Expression idReference, bool arrayAccessFound, MethodType methodTypeExpression, bool invocationParentNode, AstNode parentNode, bool messagePassed) {
         this.node = methodInfo;
         this.assignment = assignmentFound;
         this.reference = idReference;
         this.arrayAccess = arrayAccessFound;
         this.actualMethodCalled = methodTypeExpression;
         this.isParentNodeAnInvocation = invocationParentNode;
         this.parentNode = parentNode;
         this.messagePassed = messagePassed;
          this.isIntrospectiveInvocation = false;
      }
        public InheritedAttributes(MethodDefinition methodInfo, bool assignmentFound, Expression idReference, bool arrayAccessFound, MethodType methodTypeExpression, bool invocationParentNode)
        :  this(methodInfo, assignmentFound, idReference, arrayAccessFound, methodTypeExpression, invocationParentNode, null, false) 
        { }

      #endregion
   }
}
