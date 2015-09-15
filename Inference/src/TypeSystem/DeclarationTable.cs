////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DeclarationTable.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Implementation of a table of declarations.                              //
// -------------------------------------------------------------------------- //
// Create date: 31-01-2007                                                    //
// Modification date: 27-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ErrorManagement;
using TypeSystem.Operations;
namespace TypeSystem
{
   /// <summary>
   /// Implementation of a table of declarations.
   /// </summary>
   class DeclarationTable
   {
      #region Fields

      /// <summary>
      /// Stores the declarations in their scopes.
      /// </summary>
      private Dictionary<string, TypeExpression> table;

      #endregion

      #region DEBUG

      //private StreamWriter sw;
      
      ///// <summary>
      ///// Closes the file
      ///// </summary>
      //public void Close()
      //{
      //   sw.Close();
      //}

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of class DeclarationTable.
      /// </summary>
      public DeclarationTable()
      {
         this.table = new Dictionary<string, TypeExpression>();
         //sw = new StreamWriter("DeclarationTable"+name+".txt");
      }

      #endregion

    

      #region Reset()

      /// <summary>
      /// Removes the last information.
      /// </summary>
      public void Reset() //string id
      {
         this.table.Clear();
         //this.sw.WriteLine("Close Scope: {0}", id);
      }

      #endregion

      #region AddDeclaration()

      /// <summary>
      /// Adds a new declaration into their scope.
      /// </summary>
      /// <param name="name">string with the declaration name.</param>
      /// <param name="type">type associated to the declaration.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      public void AddDeclaration(string name, TypeExpression type, Location location)
      {
         if (!this.table.ContainsKey(name))
         {
            this.table.Add(name, type);
            //sw.WriteLine("\t{0}\t\t{1}:{2}", name, type.FullName, type.ToString());
         }
         else
            ErrorManager.Instance.NotifyError(new DeclarationFoundError(name, location));
      }

      #endregion

      #region SearchType()

      /// <summary>
      /// Gets the type associated to the declaration name.
      /// </summary>
      /// <param name="name">Declaration name.</param>
      /// <returns>WriteType expression associated to the declaration.</returns>
      public TypeExpression SearchType(string name)
      {
         if (this.table.ContainsKey(name))
            return this.table[name];
         return null;
      }

      #endregion
   }
}
