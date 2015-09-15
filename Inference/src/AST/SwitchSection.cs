////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SwitchSection.cs                                                     //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Case statement of our programming languages.             //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 01-08-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;
namespace AST
{
   /// <summary>
   /// Encapsulates a Case statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class SwitchSection : Statement
   {
      #region Fields

      /// <summary>
      /// Represents if it is a case statement or a default case statement
      /// </summary>
      private List<SwitchLabel> labelSection;

      /// <summary>
      /// Represents the code block asociated to the Case statement.
      /// </summary>
      private Block caseBlock;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the code block of Case statement.
      /// </summary>
      public Block SwitchBlock
      {
         get { return this.caseBlock; }
      }

      /// <summary>
      /// Gets the type of the Case statement (case or default)
      /// </summary>
      public List<SwitchLabel> LabelSection
      {
         get { return this.labelSection; }
      }

      /// <summary>
      /// Gets the number of labels associated to the switch section.
      /// </summary>
      public int LabelCount
      {
         get { return this.labelSection.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of SwitchSection
      /// </summary>
      /// <param name="labels">Condition expressions of the Case statement.</param>
      /// <param name="stats">Block executed in the Case statement.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public SwitchSection(List<SwitchLabel> labels, List<Statement> stats, Location location): base(location)
      {
         this.labelSection = labels;
         this.caseBlock = new Block(stats, location);
      }

      #endregion

      #region IsDefaultCase()

      /// <summary>
      /// Checks if the switch section has a default case.
      /// </summary>
      /// <returns>True if the switch section has a default case. Otherwise, false.</returns>
      public bool IsDefaultCase()
      {
         for (int i = 0; i < this.labelSection.Count; i++)
         {
            if (this.labelSection[i].SwitchSectionType == SectionType.Default)
               return true;
         }
         return false;
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
