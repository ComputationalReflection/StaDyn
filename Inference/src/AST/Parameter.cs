////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Parameter.cs                                                         //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a parameter declaration.                                   //
// -------------------------------------------------------------------------- //
// Create date: 01-02-2007                                                    //
// Modification date: 01-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;

namespace AST
{
   /// <summary>
   /// Encapsulates a parameter declaration.
   /// </summary>
   public struct Parameter
   {
      #region Fields

      /// <summary>
      /// Name associated to the parameter type
      /// </summary>
      private string paramType;

      /// <summary>
      /// Name of the parameter
      /// </summary>
      private string identifier;

      /// <summary>
      /// Line number
      /// </summary>
      private int line;

      /// <summary>
      /// Column number
      /// </summary>
      private int column;

      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets the name of the parameter type
      /// </summary>
      public string ParamType
      {
         get { return this.paramType; }
         set { this.paramType = value; }
      }

      /// <summary>
      /// Gets or sets parameter name
      /// </summary>
      public string Identifier
      {
         get { return this.identifier; }
         set { this.identifier = value; }
      }

      /// <summary>
      /// Gets the il parameter name.
      /// </summary>
      public string ILName
      {
         get 
         {
            if (!this.paramType.EndsWith("[]"))
               return this.identifier + "__0";
            else
               return this.identifier;
         }
      }

      /// <summary>
      /// Gets or sets the line number
      /// </summary>
      public int Line
      {
         get { return line; }
         set { line = value; }
      }

      /// <summary>
      /// Gets or sets the column number
      /// </summary>
      public int Column
      {
         get { return column; }
         set { column = value; }
      }

      #endregion
   }
}
