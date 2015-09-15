////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SSAElement.cs                                                        //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class stores the information to use in SSA map and allows to       //
// create a new declarations.                                                 //
// -------------------------------------------------------------------------- //
// Create date: 11-04-2007                                                    //
// Modification date: 30-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using TypeSystem;

namespace Semantic.SSAAlgorithm
{
   /// <summary>
   /// This class stores the information to use in SSA map and allows to
   /// create a new declarations.
   /// </summary>
   class SSAElement
   {
      #region Fields

      /// <summary>
      /// Represents the type of the declaration.
      /// </summary>
      private string type;

      /// <summary>
      ///Location: Encapsulates in one object the line, column and filename
      // the ocurrence of an intem in the text program
      /// </summary>
      protected Location location;

  

      /// <summary>
      /// Represents the current index of the declaration in SSA algorithm
      /// </summary>
      private int indexSSA;

      #endregion

      #region Properties

      public Location Location {
          get { return this.location; }
      }
      /// <summary>
      /// Gets the type identifier.
      /// </summary>
      public string TypeId
      {
         get { return type; }
      }

      /// <summary>
      /// Gets the file name;
      /// </summary>
     

      /// <summary>
      /// Gets or sets the current index of the declatation in SSA algorithm
      /// </summary>
      public int IndexSSA
      {
         get { return indexSSA; }
      }

      #endregion

      #region Constructors

      /// <summary>
      /// Constructor of SSAElement.
      /// </summary>
      /// <param name="type">WriteType of the declaration.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      public SSAElement(string type, Location location)
      {
         this.type = type;
         this.location = location;
         this.indexSSA = 0;
      }

      #endregion

      #region UpdateIndexSSA

      /// <summary>
      /// Updates the value of index SSA
      /// </summary>
      /// <param name="value">Value to update the index of SSA.</param>
      public void UpdateIndexSSA(int value)
      {
         this.indexSSA = value;
      }

      #endregion

      #region Clone()

      /// <summary>
      /// Clones the current SSA element.
      /// </summary>
      /// <returns>Returns the clone.</returns>
      public SSAElement Clone()
      {
         SSAElement aux = new SSAElement(this.type, location.Clone());
         aux.indexSSA = this.indexSSA;
         return aux;
      }

      #endregion
   }
}
