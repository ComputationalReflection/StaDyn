////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SwitchStatement.cs                                                   //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Encapsulates a Switch statement of our programming languages.           //
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
   /// Encapsulates a Switch statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class SwitchStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents the condition expression to Switch statement.
      /// </summary>
      private Expression condition;

      /// <summary>
      /// Represents the cases of the Switch block.
      /// </summary>
      private List<SwitchSection> switchBlock;

      /// <summary>
      /// Represents the statements after condition.
      /// </summary>
      private List<MoveStatement> afterCondition;

      /// <summary>
      /// Represents a new block of ThetaStatement at the end of if-else statement.
      /// </summary>
      private List<ThetaStatement> thetaStats;

      /// <summary>
      /// The set of references used in each case body, including the default section.
      /// Used for SSA purposes.
      /// </summary>
      private IDictionary<Block, IList<SingleIdentifierExpression>> referencesUsedCases = new Dictionary<Block, IList<SingleIdentifierExpression>>();

      /// <summary>
      /// Stores the number of labels the switch statement has.
      /// </summary>
      private int labels = -1;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the condition expression of Switch Statement.
      /// </summary>
      public Expression Condition
      {
         get { return condition; }
         set { condition = value; }
      }

      /// <summary>
      /// Gets the number of cases used  in Switch statement.
      /// </summary>
      public int SwitchBlockCount
      {
         get { return this.switchBlock.Count; }
      }

       /// <summary>
      /// Gets or sets the statements after condition.
      /// </summary>
      public List<MoveStatement> AfterCondition
      {
         get { return this.afterCondition; }
         set { this.afterCondition = value; }
      }

      /// <summary>
      /// Gets or sets the theta funcion statements 
      /// </summary>
      public List<ThetaStatement> ThetaStatements
      {
         get { return this.thetaStats; }
         set { this.thetaStats = value; }
      }

      /// <summary>
      /// The set of references used in each case body, including the default section.
      /// Used for SSA purposes.
      /// </summary>
       public IDictionary<Block, IList<SingleIdentifierExpression>> ReferencesUsedCases {
           get { return this.referencesUsedCases; }
       }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of SwitchStatement
      /// </summary>
      /// <param name="cond">Condition expression of Switch statement.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
       public SwitchStatement(Expression cond, List<SwitchSection> sections, Location location)
           : base(location) 
      {
         this.condition = cond;
         this.switchBlock = sections;
         this.afterCondition = new List<MoveStatement>();
         this.thetaStats = new List<ThetaStatement>();
      }

      #endregion

      #region LabelCount

      /// <summary>
      /// Gets the number of labels the switch statement has.
      /// </summary>
      /// <returns>Number of labels the switch statement has.</returns>
      public int LabelCount()
      {
         if (this.labels == -1)
         {
            this.labels = 0;
            for (int i = 0; i < this.switchBlock.Count; i++)
               this.labels += this.switchBlock[i].LabelCount;
         }
         return this.labels;
      }

      #endregion

      #region GetSwitchSectionElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public SwitchSection GetSwitchSectionElement(int index)
      {
         return this.switchBlock[index];
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
