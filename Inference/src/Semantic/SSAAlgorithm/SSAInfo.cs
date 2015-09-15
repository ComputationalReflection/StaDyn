////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SSAInfo.cs                                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class stores the information to use in SSA algorithm.              //
// -------------------------------------------------------------------------- //
// Create date: 09-04-2007                                                    //
// Modification date: 16-04-2007                                              //
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
   /// This class stores the information to use in SSA algorithm
   /// </summary>
   class SSAInfo
   {
      #region Fields

      private SSAMap firstOperandToMove;

      private SSAMap secondOperandToMove;

      private SSAMap firstOperandToUpdateId;

      private SSAMap secondOperandToUpdateId;

      #endregion

      #region Properties

      public SSAMap FirstOperandToMove
      {
         get { return this.firstOperandToMove; }
      }

      public SSAMap SecondOperandToMove
      {
         get { return this.secondOperandToMove; }
      }

      public SSAMap FirstOperandToUpdateId
      {
         get { return this.firstOperandToUpdateId; }
      }

      public SSAMap SecondOperandToUpdateId
      {
         get { return this.secondOperandToUpdateId; }
      }

      #endregion

      #region Constructors

      /// <summary>
      /// Constructor of SSAInfo.
      /// </summary>
      public SSAInfo(SSAMap firstOpToMove, SSAMap secondOpToMove, SSAMap firstOpToUpdate, SSAMap secondOpToUpdate)
      {
         this.firstOperandToMove = firstOpToMove;
         this.secondOperandToMove = secondOpToMove;
         this.firstOperandToUpdateId = firstOpToUpdate;
         this.secondOperandToUpdateId = secondOpToUpdate;
      }

      #endregion
   }
}
