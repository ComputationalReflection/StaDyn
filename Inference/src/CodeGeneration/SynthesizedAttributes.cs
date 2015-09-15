////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SynthesizedAttributes.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class encapsulates several synthesized attributes used in code     //
// generation process.                                                        //
// -------------------------------------------------------------------------- //
// Create date: 28-07-2007                                                    //
// Modification date: 28-07-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using AST;

namespace CodeGeneration
{
   /// <summary>
   /// This class encapsulates several inherited attributes used in code
   /// generation process.
   /// </summary>
   struct SynthesizedAttributes
   {
      #region Fields

      //SingleIdentifierExpression or ArrayAccessExpression
      /// <summary>
      /// Stores the current expression to use in store instruction in assignment node.
      /// </summary>
      private Expression identifier;

      /// <summary>
      /// Stores the identifier mode (Instance, UserType, Namespace)
      /// </summary>
      private IdentifierMode idMode;

      /// <summary>
      /// True if it is necessary to create an auxiliar variable. Otherwise, false.
      /// </summary>
      private bool createAuxiliarVar;

      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets the current expression to use in store instruction in assignment node.
      /// </summary>
      public Expression Identifier
      {
         get { return this.identifier; }
         set { this.identifier = value; }
      }

      /// <summary>
      /// Gets or sets the identifier mode (Instance, UserType, Namespace)
      /// </summary>
      public IdentifierMode IdentifierExpressionMode
      {
         get { return this.idMode; }
         set { this.idMode = value; }
      }

      /// <summary>
      /// Gets or sets true if it is necessary to create an auxiliar variable. Otherwise, false.
      /// </summary>
      public bool CreateAuxiliarVar
      {
         get { return this.createAuxiliarVar; }
         set { this.createAuxiliarVar = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of SynthesizedAttributes
      /// </summary>
      public SynthesizedAttributes(Expression exp)
      {
         this.identifier = exp;
         this.idMode = IdentifierMode.Instance;
         if (exp is SingleIdentifierExpression)
            this.idMode = ((SingleIdentifierExpression)exp).IdMode;
         this.createAuxiliarVar = true;
      }

      #endregion
   }
}
