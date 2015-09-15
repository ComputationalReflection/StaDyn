////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ClassTypeInfoError.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents a error produced when a MethodType has class information and //
// tries to assign other class information.                                   //
// -------------------------------------------------------------------------- //
// Create date: 21-11-2006                                                    //
// Modification date: 14-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement
{
   /// <summary>
   /// Represents a error produced when a MethodType has class information and
   /// tries to assign other class information.
   /// </summary>
   public class ClassTypeInfoError : ErrorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of ClassTypeInfoError
      /// </summary>
      /// <param name="type1"></param>
      /// <param name="type2"></param>
       public ClassTypeInfoError(string id, string class1, string class2)
      {
          this.Description = "Member " + id + " has " + class1 + " like class type. " + class2 + " class can't be assigned.";
      }

      #endregion
   }
}