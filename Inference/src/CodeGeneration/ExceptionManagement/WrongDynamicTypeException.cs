////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: WrongDynamicTypeException.cs                                         //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class encapsulates the IL code to generate an exception produced   //
// when the type on the top of the stack is incorrect.                        //
//    Inheritance: DynamicException.                                          //
// -------------------------------------------------------------------------- //
// Create date: 31-10-2007                                                    //
// Modification date: 31-10-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System.Text;

namespace CodeGeneration.ExceptionManagement
{
   /// <summary>
   /// This class encapsulates the IL code to generate an exception produced 
   /// when the type on the top of the stack is incorrect.
   /// </summary>
   /// <remarks>
   /// Inheritance: DynamicException.
   /// </remarks>
   class WrongDynamicTypeExceptionManager : DynamicExceptionManager
   {
      #region Properties

      /// <summary>
      /// Gets the type information
      /// </summary>
      public override string TypeName 
      {
         get { return "ExceptionManagement.WrongDynamicTypeException"; }
      }

      /// <summary>
      /// Gets a instace of base exception
      /// </summary>
      public override DynamicExceptionManager BaseException
      {
         get { return new DynamicExceptionManager(); }
      }

      #endregion

      #region WriteDynamicExceptionCode()

      public override string WriteDynamicExceptionCode()
      {
         StringBuilder aux = new StringBuilder();

         aux.AppendLine("// =============== CLASS MEMBERS DECLARATION ===================");
         aux.AppendLine(".namespace ExceptionManagement");
         aux.AppendLine("{");
         aux.AppendLine("   .class private auto ansi beforefieldinit WrongDynamicTypeException extends ExceptionManagement.DynamicException");
         aux.AppendLine("   {");
         aux.AppendLine("      .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed");
         aux.AppendLine("      {");
         aux.AppendLine("         ldarg.0");
         aux.AppendLine("         call       instance void ExceptionManagement.DynamicException::.ctor()");
         aux.AppendLine("         ldarg.0");
         aux.AppendLine("         ldstr      \"Specified dynamic type is not valid.\"");
         aux.AppendLine("         stfld      string ExceptionManagement.DynamicException::msg");
         aux.AppendLine("         ret");
         aux.AppendLine("      } // End of method WrongDynamicTypeException");
         aux.AppendLine("   } // End of class WrongDynamicTypeException");
         aux.AppendLine("}");

         return aux.ToString();
      }

      #endregion
   }
}
