using System;
using TypeSystem.Operations;
using TypeSystem;
namespace CodeGeneration.Operations {
    /// <summary>
    ///   ///  Generates the store operation according with the type of the array
    ///  </summary>       
    internal class CGStoreArrayElementOperation<T> : TypeSystemOperation where T : ILCodeGenerator {

        /// <summary>
        /// stream to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;


        public CGStoreArrayElementOperation(T codeGenerator, int indent) {
            this.codeGenerator = codeGenerator;
            this.indent = indent;
        }
        public override object Exec(ClassType ct, object arg) {
            this.codeGenerator.stelemRef(this.indent);
            return null;
        }
        public override object Exec(StringType s, object arg) {
            this.codeGenerator.stelemRef(this.indent);
            return null;
        }
        public override object Exec(ArrayType a, object arg) {
            this.codeGenerator.stelemRef(this.indent); 
            return null;
        }
        public override object Exec(TypeVariable t, object arg)
        {
            if (t.Substitution != null && !t.Substitution.IsValueType())
                this.codeGenerator.stelemRef(this.indent);
            else
            {
                String tempIndex = this.codeGenerator.NewLabel + "_temp";
                String tempValue = this.codeGenerator.NewLabel + "_temp";
                this.codeGenerator.WriteAuxiliarLocalVariable(this.indent, tempValue, "object");
                this.codeGenerator.WriteAuxiliarLocalVariable(this.indent, tempIndex, "int32");
                this.codeGenerator.stloc(this.indent, tempValue);
                this.codeGenerator.stloc(this.indent, tempIndex);
                this.codeGenerator.ldloc(this.indent, tempValue);
                this.codeGenerator.ldloc(this.indent, tempIndex);
                this.codeGenerator.CallVirt(this.indent, "instance", "void", "class [mscorlib]System.Array", "SetValue",
                                            new string[] {"class [mscorlib]System.Object", "int32"});
            }
            return null;
        }
        public override object Exec(UnionType t, object arg) {
            this.codeGenerator.stelemRef(this.indent);
            return null;
        }
        public override object Exec(DoubleType d, object arg) {
            this.codeGenerator.stelemDouble(this.indent);
            return null;
        }
        public override object Exec(TypeExpression t, object arg) {
            this.codeGenerator.stelemInt(this.indent);
            return null;
        }
    }

}