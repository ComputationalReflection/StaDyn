//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: CLRCodeGenerator.cs                                                     
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    This class encapsulates the IL CLR code generator IL.   
//    Inheritance: CodeGenerator
//    Implements Factory method  [Abstract Product].       
// -------------------------------------------------------------------------- 
// Create date: 21-08-2007                                                    
// Modification date: 21-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TypeSystem;

namespace CodeGeneration {
    /// <summary>
    /// This class encapsulates the IL CLR code generator IL.
    /// </summary>
  public class CLRCodeGenerator : ILCodeGenerator {         
        #region Constructor
        /// <summary>
        /// Constructor of CodeGenerator.
        /// </summary>
        /// <param name="writer">Writer to write the intermediate code.</param>
        public CLRCodeGenerator(TextWriter writer)
            : base(writer) {

        }
        #endregion

        #region UnboxAny
        // Unbox <token>
        // Revert a boxed value type instance from the object form to its value type form. <token> specifies the value type being converted and must be a valid TypeDef or TypeRef token. 
        // This instruction takes an object reference from the stack and puts a managed pointer to the value type instance on the stack.

        /// <summary>
        /// Writes the Unbox and ldobj instruction (Unbox.any = Unbox + ldobj)
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Value type to revert.</param>
        public override void UnboxAny(int indent, TypeExpression type) {
            if(type is UnionType)
                this.UnboxAny(indent, type as UnionType);
            else if (!(type is TypeVariable))
                this.WriteLNUnaryCommand(indent, "unbox.any", type.ILType());
            else if(((TypeVariable)type).Substitution != null)
                this.WriteLNUnaryCommand(indent, "unbox.any", type.ILType());
        }

        private void UnboxAny(int indent, UnionType type)
        {
            String finalLabel = this.NewLabel;
            String nextLabel = "";
            for (int i = 0; i < type.TypeSet.Count; i++)
            {
                if (!String.IsNullOrEmpty(nextLabel))
                    this.WriteLabel(indent, nextLabel);
                if (i != type.TypeSet.Count - 1)
                {
                    nextLabel = this.NewLabel;
                    this.dup(indent);
                    this.isinst(indent, type.TypeSet[i]);
                    this.brfalse(indent, nextLabel);
                }
                this.WriteLNUnaryCommand(indent, "unbox.any", type.TypeSet[i].ILType());
                if (i != type.TypeSet.Count - 1)
                    this.br(indent, finalLabel);
            }
            this.WriteLabel(indent, finalLabel);    
        }
      #endregion

        #region CallVirt <token>
        // CallVirt <token>
        // Call the virtual method specified by <token>. 

        /// <summary>
        /// Writes the CallVirt instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="klass">Class to access to the member.</param>
        /// <param name="member">Member to call.</param>
        /// <param name="arguments">Actual arguments</param>
        public override void CallVirt(int indent, MethodType memberType, TypeExpression klass, string member, AST.CompoundExpression arguments) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.Write("callvirt\t");
            this.WriteCall(memberType, klass, member, null);
            this.ilStamentsCodeGeneration.WriteLine();
        }
        #endregion
    }
}