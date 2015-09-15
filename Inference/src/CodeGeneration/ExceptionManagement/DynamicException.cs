////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DynamicException.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class encapsulates the IL code to generate a dynamic exception.   //
// -------------------------------------------------------------------------- //
// Create date: 31-10-2007                                                    //
// Modification date: 31-10-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System.Text;

namespace CodeGeneration.ExceptionManagement
{
   /// <summary>
   /// This class encapsulates the IL code to generate a dynamic exception.
   /// </summary>
   public class DynamicExceptionManager
   {
      #region Properties

      /// <summary>
      /// Gets the type information
      /// </summary>
      public virtual string TypeName
      {
         get { return "ExceptionManagement.DynamicException"; }
      }

      /// <summary>
      /// Gets a instace of base exception
      /// </summary>
      public virtual DynamicExceptionManager BaseException
      {
         get { return null; }
      }


      #endregion

      #region WriteDynamicExceptionCode()

      public virtual string WriteDynamicExceptionCode()
      {
         StringBuilder aux = new StringBuilder();

         aux.AppendLine("// =============== CLASS MEMBERS DECLARATION ===================");
         aux.AppendLine(".namespace ExceptionManagement");
         aux.AppendLine("{");
         aux.AppendLine("   .class private auto ansi DynamicException extends [mscorlib]System.Exception");
         aux.AppendLine("   {");
         aux.AppendLine("      .field family string msg");
         aux.AppendLine("      .method public hidebysig specialname virtual instance string get_Message() cil managed");
         aux.AppendLine("      {");
         aux.AppendLine("         ldarg.0");
         aux.AppendLine("         ldfld      string ExceptionManagement.DynamicException::msg");
         aux.AppendLine("         ret");
         aux.AppendLine("      } // End of method get_Message");
         aux.AppendLine("      .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed");
         aux.AppendLine("      {");
         aux.AppendLine("         ldarg.0");
         aux.AppendLine("         call       instance void [mscorlib]System.Exception::.ctor()");
         aux.AppendLine("         ret");
         aux.AppendLine("      } // End of method DynamicException");
         aux.AppendLine("      .property instance string Message()");
         aux.AppendLine("      {");
         aux.AppendLine("         .get instance string ExceptionManagement.DynamicException::get_Message()");
         aux.AppendLine("      } // End of property Message");
         aux.AppendLine("   } // End of class DynamicException");
         aux.AppendLine("}");

         return aux.ToString();
      }

      #endregion
   }
}
