////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SwitchLabel.cs                                                       //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a switch label (Case + condition or Default section).      //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 19-04-2007                                                    //
// Modification date: 19-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   #region CaseType

   /// <summary>
   /// Indicates if it is a case statement or a default case.
   /// </summary>
   public enum SectionType
   {
      Case,
      Default,
   }

   #endregion

   /// <summary>
   /// Encapsulates a switch label (Case + condition or Default section).
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class SwitchLabel : Statement
   {
      #region Fields

      /// <summary>
      /// Represents if it is a case statement or a default case statement
      /// </summary>
      private SectionType sectionType;

      /// <summary>
      /// Represents the condition expression of the Case statement.
      /// </summary>
      private Expression condition;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the condition expression of Case statement.
      /// </summary>
      public Expression Condition
      {
         get { return this.condition; }
      }

      /// <summary>
      /// Gets the type of the Case statement (case or default)
      /// </summary>
      public SectionType SwitchSectionType
      {
         get { return this.sectionType; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of SwitchLabel
      /// </summary>
      /// <param name="cond">Condition expression of the Case statement.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public SwitchLabel(Expression cond, Location location)
          : base(location) 
      {
         this.sectionType = SectionType.Case;
         this.condition = cond;
      }

      /// <summary>
      /// Constructor of SwitchLabel
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public SwitchLabel(Location location)
          : base(location) 
      {
         this.sectionType = SectionType.Default;
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
