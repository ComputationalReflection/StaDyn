////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ThrowStatement.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Throw statement of our programming languages.            //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 12-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Throw statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ThrowStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents the throw expression.
      /// </summary>
      private Expression throwExpression;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the throw expression.
      /// </summary>
      public Expression ThrowExpression  //can be null;
      {
         get { return throwExpression; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ThrowStatement
      /// </summary>
      /// <param name="throwExp">Throw expression. if null it will generate a retrhow statement in il code</param>
      ///<param name="Location"> </param>

      public ThrowStatement(Expression throwExp, Location location)
          : base(location) 
      {
         this.throwExpression = throwExp;
      }

      #endregion

      #region Accept()

      /// <summary>
      /// Accept method of a concrete visitor.
      /// </summary>
      /// <param name="v">Concrete visitor</param>
      /// <param name="o">Optional information to use in the visit.</param>
      /// <returns>Optional information to return</returns>
      public override Object Accept(Visitor v, Object o)
      {
         return v.Visit(this, o);
      }

      #endregion
   }
}
