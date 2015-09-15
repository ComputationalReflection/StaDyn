////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ThetaStatement.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Theta function to use in SSA algorithm.                  //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 06-04-2007                                                    //
// Modification date: 10-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Theta function to use in SSA algorithm.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ThetaStatement : Statement
   {
      #region Fields

      /// <summary>
      /// This list of expressions represents a union of its expression to create a type union. 
      /// </summary>
      private List<SingleIdentifierExpression> thetaList;

      /// <summary>
      /// Function identifier.
      /// </summary>
      private SingleIdentifierExpression thetaId;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the parameter of theta funcion
      /// </summary>
      public List<SingleIdentifierExpression> ThetaList
      {
         get { return this.thetaList; }
      }

      /// <summary>
      /// Gets the expression 
      /// </summary>
      public SingleIdentifierExpression ThetaId
      {
         get { return this.thetaId; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ThetaStatement
      /// </summary>
      /// <param name="id">Function identifier.</param>
      /// <param name="list">Represents a union of expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      public ThetaStatement(SingleIdentifierExpression id, List<SingleIdentifierExpression> list, Location location)
         : base(location)
      {
         this.thetaId = id;
         this.thetaList = new List<SingleIdentifierExpression>();
         if (list.Count != 0)
            insertElements(list);
      }

      #endregion

      #region containsValue

      /// <summary>
      /// Checks if ThetaStatement has a element with this identifier and index
      /// </summary>
      /// <param name="id">Identifier of the element.</param>
      /// <param name="index">Index of the element.</param>
      /// <returns>Returns true if the element exists. Otherwise, false.</returns>
      private bool containsValue(string id, int index)
      {
         for (int i = 0; i < this.thetaList.Count; i++)
         {
            if (thetaList[i].Identifier.Equals(id))
               if (thetaList[i].IndexOfSSA == index)
                  return true;
         }
         return false;
      }

      #endregion

      #region insertElements

      /// <summary>
      /// Inserts the elementos of the argument in the ThetaStatement.
      /// </summary>
      /// <param name="list">Elements to add.</param>
      private void insertElements(List<SingleIdentifierExpression> list)
      {
         for (int i = 0; i < list.Count; i++)
         {
            if (!(containsValue(list[i].Identifier, list[i].IndexOfSSA)))
               this.thetaList.Add(list[i]);
         }
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
