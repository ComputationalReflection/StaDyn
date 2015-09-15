    ////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- // 
// File: CodeGenerator.cs                                                     //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
//          Daniel Zapico - daniel.zapico.rodriguez@gmail.com                 //
// Description:                                                               //
//    This class encapsulates the IL instruction used to generate the code.   
//   TODO: Maybe the comment above is wrong, the idea of this class is to add //
//functionality to the generated code not specifically IL code. In fact 
// children of this class are code generators of MSIL, CLR, rRotor and C#
//    Implements Factory method  [Abstract Product].       
// -------------------------------------------------------------------------- //
// Create date: 28-05-2007                                                    //
// Modification date: in progress                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AST;
using CodeGeneration.ExceptionManagement;
using TypeSystem;

namespace CodeGeneration {
    /// <summary>
    /// This class encapsulates the instruction used to generate the code.
    /// </summary>
   abstract public class CodeGenerator {
        

       #region Fields

        /// <summary>
        /// Writer to write the intermediate code.
        /// </summary>
        protected TextWriter output;

        /// <summary>
        /// Stores the current information about local variables.
        /// </summary>
        protected StringBuilder currentLocalVars;

        /// <summary>
        /// Represents the value of the current label.
        /// </summary>
        protected int currentLabel;

        /// <summary>
        /// List of exceptions that is necessary to include in intermediate code file.
        /// </summary>
        protected Dictionary<string, DynamicExceptionManager> exceptions;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the current label
        /// </summary>
        public abstract string NewLabel {
            get;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of CodeGenerator.
        /// </summary>
        /// <param name="writer">Writer to write the intermediate code.</param>
        public CodeGenerator(TextWriter writer) {
            this.output = writer;
            this.currentLocalVars = new StringBuilder();
            this.currentLabel = 0;
            this.exceptions = new Dictionary<string, DynamicExceptionManager>();
        }

        #endregion

        public abstract void InitialComment();
            
        #region WriteComment()

        /// <summary>
        /// Writes the specified message stored in msg in a comment wrapped in 
        /// the proper format of the target langage.
        /// </summary>
        /// <param name="msg">comment to write.</param>
        public abstract void Comment(string msg);
        /// <summary>
        /// Writes the specified message stored in msg in a comment wrapped in 
        /// the proper format of the target langage.
        /// </summary>
        /// <param name="msg">comment to write.</param>
        /// <param name="indent">indentation to use </param>
       public abstract void Comment(int indent, string msg);


        #endregion

        #region WriteLabel()

        /// <summary>
        /// Writes the specified label.
        /// </summary>
        /// <param name="label">Label to write.</param>
        public abstract void WriteLabel(int indent, string label);

        #endregion

        internal abstract void WriteConstructorHeader(int indent);
        
        internal abstract void WriteStaticConstructorHeader(int indent);
            

 /*       #region WriteLine()

        /// <summary>
        /// It inserts an eol in the file of the target code
        /// 
        /// </summary>
        public abstract void WriteLine();

        #endregion

        #region WriteIndentation()

        /// <summary>
        /// Writes indentation.
        /// </summary>
        /// <param name="lenght">Lenght of the indentation.</param>
        protected abstract void WriteIndentation(int lenght);

        #endregion

        #region Write ()
        //Helper writes the formatted mesage in the file. Using format and obj
        // to build the resulting string.
        public virtual void Write(string format, params Object[] obj) {
            this.output.Write(format, obj);
        }
        #endregion
        */
        #region WriteType()

        /// <summary>
        /// Writes the current type information
        /// </summary>
        /// <param name="type">WriteType expression with the type information.</param>
        public abstract void WriteType(TypeExpression type);
        #endregion

        #region WriteHeader()

        /// <summary>
        /// Writes the header of the object code file.
        /// </summary>
        /// <param name="fileName">Name of the module.</param>
        public abstract void WriteHeader(string fileName);

        #endregion

        #region WriteNamespaceHeader()
        /// <summary>
        /// Writes the namespace header.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Namespace name.</param><,
        public abstract void WriteNamespaceHeader(int indent, string name);

        #endregion

        #region WriteLNClassHeader()

        /// <summary>
        /// Writes the class header.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Class name.</param>
        /// <param name="type">Class type expression.</param>
        public abstract void WriteLNClassHeader(int indent, string name, ClassType type);
        #endregion

        #region WriteEndOfClass()

        /// <summary>
        /// Writes the class termination
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Name of the class to terminate.</param>
        public abstract void WriteEndOfClass(int indent, string name);

        #endregion

        #region WriteLNInterfaceHeader()
                
        /// <summary>
        /// Writes the interface header
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Interface name.</param>
        /// <param name="type">Interface type expression.</param>
        public abstract void WriteInterfaceHeader(int indent, string name, InterfaceType type);

        #endregion

        #region WriteLNEndOfInterface()

        /// <summary>
        /// Writes the interface termination
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Name of the interface to terminate.</param>
        public abstract void WriteEndOfInterface(int indent, string name);
        #endregion

        #region WriteLNMethodHeader()
        /// <summary>
        /// Writes the method header.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Method name.</param>
        /// <param name="type">Method type expression.</param>
        public abstract void WriteLNMethodHeader(int indent, string name, MethodType type);
        #endregion

        #region WriteEndOfMethod()

        /// <summary>
        /// Writes the method termination
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Name of the method to terminate.</param>
        public abstract void WriteEndOfMethod(int indent, string name);

        #endregion

        #region WriteParams()
        /// <summary>
        /// Writes the parameters of a method
        /// </summary>
        /// <param name="memberType">The type of the method</param>
        /// <param name="arguments">Actual arguments</param>
        public abstract void WriteParams(MethodType memberType, AST.CompoundExpression arguments);
        #endregion

        #region WriteField()

        /// <summary>
        /// Writes the field header.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Field name.</param>
        /// <param name="type">Field type expression.</param>
        /// <param name="constantField">True if the field is a constant. Otherwise, false.</param>
        public abstract void WriteField(int indent, string name, FieldType type, bool constantField);

        /// <summary>
        /// Writes the field inicialization.
        /// </summary>
        /// <param name="type">WriteType Expression of the field inicialization.</param>
        public abstract void WriteLNFieldInitialization(TypeExpression type);

        /// <summary>
        /// Writes the field inicialization expression.
        /// </summary>
        /// <param name="type">Expression of the field inicialization.</param>
        public virtual void WriteLNFieldInitialization(string init) {
            this.output.WriteLine(init);
        }

        #endregion

        #region WriteEndOfField()

        /// <summary>
        /// Writes the field termination
        /// </summary>
        public abstract void WriteEndOfField();
        #endregion

        #region WriteStartBlock()

        /// <summary>
        /// Writes the class inicialization
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public abstract void WriteStartBlock(int indent);

        #endregion

        #region WriteLNEndOfBlock()

        /// <summary>
        /// Writes the termination token
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public abstract void WriteEndOfBlock(int indent);

        #endregion

        #region AddLocalVariable();
        public abstract void AddLocalVariable(string name, TypeExpression type);

        #endregion

        #region WriteLocalVariable()

        /// <summary>
        /// Writes the information of local variables.
        /// </summary>
        /// <param name="indent">Current indentation.</param>
        public abstract void WriteLocalVariable(int indent);

        #endregion

        #region WriteAuxiliarLocalVariable()

        /// <summary>
        /// Writes the information of local variables.
        /// </summary>
        /// <param name="indent">Current indentation.</param>
        /// <param name="id">Auxiliar local variable identifier.</param>
        /// <param name="type">Auxiliar local variable type expression.</param>
        public abstract void WriteAuxiliarLocalVariable(int indent, string id, string type);

        #endregion

        #region WriteCodeOfExceptions()

        /// <summary>
        /// Implements Template Method Pattern
        /// Writes the intermediate code for each exceptions to include
        /// </summary>
        public virtual void WriteCodeOfExceptions() {
            foreach (string key in this.exceptions.Keys)
                WriteCodeOfExceptionsTemplateMethod(this.exceptions[key]);
        }
        /// <summary>
        /// Algorithm used in WriteCodeException
        /// </summary>
        /// <param name="d">The exception to write in the code</param>
        protected abstract void WriteCodeOfExceptionsTemplateMethod(DynamicExceptionManager d);

        #endregion

        #region WriteEntryPoint()

        /// <summary>
        /// Writes the entrypoint directive.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public abstract void WriteEntryPoint(int indent);


        #endregion


        #region WriteEntryPoint()

        public virtual void WriteEntryPoint() {
            this.WriteEntryPoint();
        }
        #endregion


        #region TryDirective ()

        public abstract void WriteTryDirective(int indent);

        public abstract void WriteOpenBraceTry(int indent);

        public abstract void WriteCloseBraceTry(int indent);

        #endregion

        #region Catch()

        public abstract void WriteCatch(int indent, String type, String var);

        public abstract void WriteOpenBraceCatch(int indent);

        public abstract void WriteCloseBraceCatch(int indent);

        #endregion

        #region Finally()

        public abstract void WriteFinally(int indent);

        public abstract void WriteOpenBraceFinally(int indent);

        public abstract void WriteCloseBraceFinally(int indent);
        #endregion
        //#region RuntimeFreshTypeExpressionPromotion()

        ///// <summary>
        ///// Converts a fresh type variable on the stack (object) to another type
        ///// <param name="indent">Indentation level</param>
        ///// <param name="typeExpression">The type expression to promote to</param>
        ///// </summary>
        //public  abstract void RuntimeFreshTypeExpressionPromotion(int indent, TypeExpression typeExpression);
        //#endregion

        #region RuntimeIsInstruction()

        /// <summary>
        /// Checks if the type variable on the stack is a specified type or can be to promotion.
        /// <param name="indent">Indentation level</param>
        /// <param name="typeExpression">The type expression to promote to</param>
        /// </summary>
        ///public abstract void RuntimeIsInstruction(int indent, TypeExpression typeExpression);

        #endregion

        #region WriteThrowException

        /// <summary>
        /// Writes the generation code to throw a specified exception.
        /// </summary>
        /// <param name="indent">Identation to use.</param>
        /// <param name="dynException">Exception to throw.</param>
        public abstract void WriteThrowException(int indent, DynamicExceptionManager dynException);

       /// <summary>
        ///Writes the code to throw an exception derived from Exception without using DynamicExceptionManager . 
        ////// </summary>
        /// <param name="indent">indentation to use</param>
        /// <param name="ex">Name of the exception to throw</param>
        /// <param name="args">arguments of the exception. If the list is empty there won't be code generation about this param</param>
        public abstract void WriteThrowException(int indent, string ex, string[] msg);

        #endregion

        #region Box and Unbox

        #region Box
       
        /// <summary>
        /// Writes the Box instruction
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Converts to this object reference.</param>
        public abstract void Box(int indent, TypeExpression type);
        /// <summary>
        /// Makes sure to convert a type to an object
        /// </summary>
        /// <param name="indent">Indentation level</param>
        /// <param name="type">The type to be promoted</param>
        public abstract void BoxIfNeeded(int indent, TypeExpression type);
        #endregion

        #region UnboxAny

        // Unbox <token>
        // Revert a boxed value type instance from the object form to its value type form. <token> specifies the value type being converted and must be a valid TypeDef or TypeRef token. 
        // This instruction takes an object reference from the stack and puts a managed pointer to the value type instance on the stack.

        /// <summary>
        /// Writes the Unbox instruction
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Value type to revert.</param>
        public abstract void UnboxAny(int indent, TypeExpression type);

        #endregion

        #region Unbox

        // Unbox <token>
        // Revert a boxed value type instance from the object form to its value type form. <token> specifies the value type being converted and must be a valid TypeDef or TypeRef token. 
        // This instruction takes an object reference from the stack and puts a managed pointer to the value type instance on the stack.

        /// <summary>
        /// Writes the Unbox instruction
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Value type to revert.</param>
        public abstract void Unbox(int indent, TypeExpression type);
        #endregion

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
        public abstract void CallVirt(int indent, MethodType memberType, TypeExpression klass, string member, AST.CompoundExpression arguments);

        /// <summary>
        /// Writes the CallVirt instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        /// <param name="setProperty">True if it is a set property. False if it is a get property.</param>
        public abstract void CallVirt(int indent, PropertyType memberType, TypeExpression obj, string member, bool setProperty);

        /// <summary>
        /// Writes the CallVirt instruction
        /// </summary>
        /// <param name="indent">Text Indentation</param>
        /// <param name="methodType">Qualifiers of the method</param>
        /// <param name="result">Return type</param>
        /// <param name="klass">Class type</param>
        /// <param name="memberName">Name of the member</param>
        /// <param name="args">Types of parameters</param>
        public abstract void CallVirt(int indent, string methodType, string result, string klass, string memberName, string[] args);

        #endregion

        #region call <token>
         // call <token>
        // Call a nonvirtual method.

        /// <summary>
        /// Writes the call instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        public abstract void Call(int indent, MethodType memberType, TypeExpression obj, string member);
        
       /// <summary>
        /// Writes the call instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        /// <param name="setProperty">True if it is a set property. False if it is a get property.</param>
        
       public abstract void Call(int indent, PropertyType memberType, TypeExpression obj, string member, bool setProperty);

        /// <summary>
        /// Writes the call instruction
        /// </summary>
        /// <param name="indent">Text Indentation</param>
        /// <param name="methodType">Qualifiers of the method</param>
        /// <param name="result">Return type</param>
        /// <param name="klass">Class type</param>
        /// <param name="memberName">Name of the member</param>
        /// <param name="args">Types of parameters</param>
        public abstract void Call(int indent, string methodType, string result, string klass, string memberName, string[] args);
        
       #endregion
        
       #region Close()

        /// <summary>
        /// Close the writer
        /// </summary>
        public void Close() {
            this.output.Close();
        }

        #endregion
        #region MakeCall()

        public virtual TypeExpression MakeCall(int indent, InvocationExpression node, Object o, MethodType actualMethodCalled, FieldAccessExpression fieldAccessExpression, object arg) {
            // shortcut evaluation. Null pointed error suprimed
            if (o is SynthesizedAttributes && ((SynthesizedAttributes)o).IdentifierExpressionMode == IdentifierMode.Instance)
                // * 1.1.1 The implicit parameter is an object (not a class)
                this.CallVirt(indent, actualMethodCalled, actualMethodCalled.MemberInfo.Class, fieldAccessExpression.FieldName.Identifier, node.Arguments);
            else if (actualMethodCalled.MemberInfo.Modifiers.Contains(Modifier.Abstract))
                this.CallVirt(indent, actualMethodCalled, actualMethodCalled.MemberInfo.Class, fieldAccessExpression.FieldName.Identifier, node.Arguments);
            else
                // * 1.1.2 The implicit parameter is a class (not an object)
                this.Call(indent, actualMethodCalled, actualMethodCalled.MemberInfo.Class, actualMethodCalled.MemberInfo.MemberIdentifier);

            return actualMethodCalled.Return;
        }

        #endregion

    }
}
