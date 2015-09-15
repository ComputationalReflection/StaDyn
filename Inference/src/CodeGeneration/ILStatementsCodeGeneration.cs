using System.IO;
using System.Text;
using TypeSystem;
using System;
namespace CodeGeneration {

    public class ILStatementsCodeGeneration {
        protected TextWriter output;
        protected StringBuilder debugMemoryLog;
        public ILStatementsCodeGeneration(TextWriter output) {
            this.output = output;
#if DEBUG
            debugMemoryLog = new StringBuilder();
#endif
        }

        #region WriteComment()

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="msg">Message to write.</param>

        public virtual void WriteComment(string msg) {
            this.WriteComment(0, msg);
        }

        public virtual void WriteComment(int indent, string msg) {
            this.WriteLine("{0}// {1}", this.Indentation(indent), msg);
        }
        #endregion

        #region  Write() & WriteLine() & indentation

        /// <summary>
        /// Writes a line terminator in the text stream
        /// </summary>
        public void WriteLine(int indent, string format, params Object[] obj) {
            this.WriteIndentation(indent); 
            this.WriteLine(format, obj);
        }

        public void WriteLine(string format, params Object[] obj) {
            this.output.WriteLine(format, obj);
#if DEBUG
            debugMemoryLog.AppendFormat(format, obj);
            debugMemoryLog.AppendLine();
#endif

        }
 
 public void Write(string format, params Object[] obj) {
            this.output.Write(format, obj);
#if DEBUG
            debugMemoryLog.AppendFormat(format, obj);
#endif

 }

        public void Write(int indent, string format, params Object[] obj) {
            this.WriteIndentation(indent);
            this.Write(format, obj);
}

public void Write(int indent, string str) {
            this.WriteIndentation(indent);
            this.Write(str);

        }

public void Write(string str) {
    this.output.Write(str);
#if DEBUG
    debugMemoryLog.Append(str);
#endif
}

public void WriteLine(int indent, string str) {
    this.Write(indent, str);
    this.WriteLine();
}

public void WriteLine(string str) {
            this.Write(str);
            this.WriteLine();
    }

public void WriteLine() {
            this.output.WriteLine();
#if DEBUG
            debugMemoryLog.AppendLine();

#endif
        }

        public void WriteIndentation(int indent) {
            this.Write(this.Indentation(indent));
        }

        private string Indentation(int indent) {
            return indent > 0 ? " ".PadLeft(indent * 3) : "";
        }
#endregion

        public void WriteLNAssemblyDirective(string fileName) {
            this.WriteLine(".assembly extern mscorlib {}");
            this.WriteLine(".assembly {0} {{}}", fileName);
        }

        #region WriteClassVisibility

        public virtual void WriteClassVisibility(int indent, string name, ClassType type) {

            Modifier mask = type.ModifierMask;

            this.Write(indent, ".class ");

            if ( ( mask & Modifier.Public ) != 0 )
                this.Write("public ");

            if ( ( mask & Modifier.Internal ) != 0 )
                this.Write("private ");

            if ( ( mask & Modifier.Abstract ) != 0 )
                this.Write("abstract ");
            else if ( ( mask & Modifier.Static ) != 0 )
                this.Write("abstract sealed ");
        }

        #endregion

        public void WriteModule(string fileName) {
            this.WriteLine(".module {0}.exe", fileName);
        }

        public void WriteEndOfClass(string className) {
            this.WriteCloseBrace();
            this.WriteComment("End of class " + className);
        }


        #region WriteLNInterfaceHeader()

        // .class interface <flags> <name> implements <class_ref>[, class_ref*]
        // <flags> := public (public)
        //          | private (internal)
        //
        // Interfaz modifiers: new, public, protected, internal, private.
        // Namespace elements cannot be declared as private, protected or protected internal.
        //
        // Nested class currently does not apply because it is commented in grammar file.
        // For this reason, protected internal, protected and private do not use it.

        /// <summary>
        /// Writes the interface header
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Interface name.</param>
        /// <param name="type">Interface type expression.</param>
        public virtual void WriteLNInterfaceHeader(int indent, string name, InterfaceType type) {
            Modifier mask = type.ModifierMask;

            this.Write(indent, ".class interface ");

            if ( ( mask & Modifier.Public ) != 0 )
                this.Write("public abstract ");

            if ( ( mask & Modifier.Internal ) != 0 )
                this.Write("private abstract ");

            this.Write("auto ansi {0} ", name);

            if ( type.InterfaceList.Count != 0 ) {
                this.Write("implements ");
                for ( int i = 0; i < type.InterfaceList.Count - 1; i++ ) {

                    if ( type.InterfaceList[i] is BCLInterfaceType )
                        this.Write("[mscorlib]");
                    this.Write("{0}, ", type.InterfaceList[i].FullName);
                }
                if ( type.InterfaceList[type.InterfaceList.Count - 1] is BCLInterfaceType )
                    this.Write("[mscorlib]");

                this.Write("{0}", type.InterfaceList[type.InterfaceList.Count - 1].FullName);
            } //if

            this.WriteLine();
        }

        #endregion

 
 
        #region WriteField()

        // .field <flags> <type> <name>
        // .field <flags> <type> <name> = <const_type>[(<value>)]
        // <flags> := public (public)
        //          | private (private)
        //          | assembly (internal)
        //          | family (protected)
        //          | famorassem (protected internal)
        //          | static (static)
        //          | static literal (const)
        //
        // Field modifiers: new, public, protected, internal, private, static, readonly, volatile
        // Field cannot be both readonly and volatile
        // A constant field cannot be static
        //
        // Readonly and volatile modifier currently does not apply because it is commented in grammar file.

        /// <summary>
        /// Writes the field header.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Field name.</param>
        /// <param name="type">Field type expression.</param>
        /// <param name="constantField">True if the field is a constant. Otherwise, false.</param>
        internal void WriteField(int indent, string name, FieldType type, bool constantField) {
            Modifier mask = type.MemberInfo.ModifierMask;
            
            this.Write(indent, ".field ");

            if ( ( mask & Modifier.Public ) != 0 )
                this.Write("public ");

            if ( ( mask & Modifier.Private ) != 0 )
                this.Write("private ");

            if ( ( mask & ( Modifier.Protected | Modifier.Internal ) ) == ( Modifier.Protected | Modifier.Internal ) )
                this.Write("famorassem ");
            else {
                if ( ( mask & Modifier.Internal ) != 0 )
                    this.Write("assembly ");
                if ( ( mask & Modifier.Protected ) != 0 )
                    this.Write("family ");
            }
            if ( ( mask & Modifier.Static ) != 0 )
                this.Write("static ");
            else if ( constantField )
                this.Write("static literal ");

            if (type.FieldTypeExpression is TypeVariable) //if the field is TypeVariable, always is represented by an object in IL Code.
                this.WriteType(TypeVariable.NewTypeVariable);
            else
                this.WriteType(type.FieldTypeExpression);
            this.Write(" {0}", name);

        }
        /// <summary>
        /// Writes the field inicialization.
        /// </summary>
        /// <param name="type">WriteType Expression of the field inicialization.</param>
        internal void WriteFieldInitialization(TypeExpression type) {
            this.Write(" = {0}(", type.ILType());
        }
        #endregion

        #region WriteType()

        /// <summary>
        /// Writes the current type information
        /// </summary>
        /// <param name="type">WriteType expression with the type information.</param>
        internal void WriteType(TypeExpression type) {
            this.Write(type.ILType());
        }

        #endregion

        #region WriteNamespace()
        // .namespace <name>

        /// <summary>
        /// Writes the namespace header.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Namespace name.</param>
        public void WriteNamespace(string name) {
            this.Write(".namespace {0}", name);
        }

        #endregion

        #region OpenBrace & CloseBrace()

        /// <summary>
        /// Writes the termination token.
        /// Requires the output text would be formatted, with  tabs for example.
        /// </summary>
        public void WriteCloseBrace(int indent) {
            this.Write(indent, "}");
        }

        public void WriteCloseBrace() {
            this.WriteCloseBrace(0);
        }
        public void WriteLineCloseBrace(int indent) {
            this.WriteCloseBrace(indent);
            this.WriteLine();

        }
        public void WriteOpenBrace(int indent) {
            this.Write(indent, "{");
        }

        public void WriteOpenBrace() {
            this.WriteOpenBrace(0);
        }
        public void WriteLineOpenBrace(int indent) {
            this.WriteOpenBrace(indent);
            this.WriteLine();

        }
        #endregion

        #region WriteEntryPoint()

        public virtual void WriteEntryPoint() {
            this.Write(".entrypoint");
        }

        #endregion

        #region WriteAuxiliarLocalVariable()

        /// <summary>
        /// Writes the information of local variables.
        /// </summary>
        /// <param name="id">Auxiliar local variable identifier.</param>
        /// <param name="type">Auxiliar local variable type expression.</param>
        public virtual void WriteAuxiliarLocalVariable(string id, string type) {
            this.Write(".locals init({0} {1})", type, id);
        }

        #endregion


    }
}
