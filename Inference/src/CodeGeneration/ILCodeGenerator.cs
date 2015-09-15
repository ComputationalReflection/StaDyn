using TypeSystem;
using System.IO;
using CodeGeneration.ExceptionManagement;
using System;
using AST;
using CodeGeneration.Operations;
namespace CodeGeneration {
    abstract public class ILCodeGenerator : CodeGenerator {

        #region Fields
        protected ILStatementsCodeGeneration ilStamentsCodeGeneration;

        #endregion

        #region NewLabel
        //TODO: Revisar si se mueve a ilStamentsCodeGeneration
        public override string NewLabel {
            get { return "IL_" + this.currentLabel++; }
        }
        #endregion

        #region Index of Local variables
        
        private static int localVariableIndex = 0;

        #endregion
        
        #region Properties

        public int LocalVariableIndex {
            get { return localVariableIndex; }
            set { localVariableIndex = value; }
        }
        
        #endregion
        
        #region Constructors

        public ILCodeGenerator(TextWriter output)
            : base(output) {
            // EL único que accede al fichero
            this.ilStamentsCodeGeneration = new ILStatementsCodeGeneration(output);
        }

        #endregion

        #region InitialComent
        /// <summary>
        /// Displays de comment to show in the first line of il file.
        /// </summary>
        public override void InitialComment() {
            this.ilStamentsCodeGeneration.WriteLine();
            this.Comment("=============== CLASS MEMBERS DECLARATION ===================");
        }
        #endregion

        #region WriteConstructorHeader()

        internal override void WriteConstructorHeader(int indent) {
                this.ilStamentsCodeGeneration.WriteLine(indent, ".method public specialname rtspecialname instance void .ctor()");
        }

        #endregion

        #region WriteStaticConstructorHeader()

        internal override void WriteStaticConstructorHeader(int indent) {
            this.ilStamentsCodeGeneration.WriteLine(indent, ".method public specialname rtspecialname static void .cctor()");
        }

        #endregion

        #region WriteComment
        /// <summary>
        /// Writes the specified message as a comment in il language
        /// </summary>
        /// <param name="msg">Message to write.</param>
        public override void Comment(string msg) {
            this.ilStamentsCodeGeneration.WriteComment(msg);
        }
        
        /// Writes the specified message stored in msg in a comment wrapped in 
        /// the proper format of the target langage.
        /// </summary>
        /// <param name="msg">comment to write.</param>
        /// <param name="indent">indentation to use </param>
        public override void Comment(int indent, string msg) {
            this.ilStamentsCodeGeneration.WriteComment(indent, msg);
        }
        #endregion

        #region WriteLabel
        /// <summary>
        /// Writes a label in il. Format  --indentation-- LABEL:\n
        /// </summary>
        /// <param name="label">text of the label</param>
        public override void WriteLabel(int indentation, string label) {
            this.ilStamentsCodeGeneration.WriteLine(indentation, label + ": nop");
        }

        #endregion

        #region WriteType()

        /// <summary>
        /// Writes the current type information
        /// </summary>
        /// <param name="type">WriteType expression with the type information.</param>
        public override void WriteType(TypeExpression type) {
            this.ilStamentsCodeGeneration.WriteType(type);
        }

        #endregion

        #region WriteHeader()

        /// <summary>
        /// Writes the header of the il code file.
        /// </summary>
        /// <param name="fileName">Name of the module.</param>
        public override void WriteHeader(string fileName) {
            this.ilStamentsCodeGeneration.WriteLNAssemblyDirective(fileName);
            this.ilStamentsCodeGeneration.WriteModule(fileName);
        }

        #endregion

        #region WriteNamespaceHeader()
        // .namespace <name>
        /// <summary>
        /// Writes the namespace header of an IL file.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Namespace name.</param>
        public override void WriteNamespaceHeader(int indent, string name) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteNamespace(name);
            this.ilStamentsCodeGeneration.WriteLine();
            this.ilStamentsCodeGeneration.WriteOpenBrace(indent);
        }

        #endregion

        #region WriteClassName

        public virtual void WriteClassName(string name) {
            this.ilStamentsCodeGeneration.Write("auto ansi {0} ", name);
        }

        #endregion

        #region WriteClassExtends

        public virtual void WriteClassExtends(ClassType type) {
            this.ilStamentsCodeGeneration.Write("extends ");
            if (type.BaseClass == null)
                this.ilStamentsCodeGeneration.Write("[mscorlib]System.Object");
            else
                this.ilStamentsCodeGeneration.Write(TypeMapping.Instance.GetBCLName(type.BaseClass.FullName, true).ToString());
        }
        #endregion

        #region WriteClassImplements

        public virtual void WriteClassImplements(ClassType type) {
            if (type.InterfaceList.Count == 0) // {P} type.InterfaceList.Count != 0)
                return;

            this.ilStamentsCodeGeneration.Write(" implements ");
            for (int i = 0; i < type.InterfaceList.Count - 1; i++) {
                if (type.InterfaceList[i] is BCLInterfaceType)
                    this.ilStamentsCodeGeneration.Write("[mscorlib]");
                this.ilStamentsCodeGeneration.Write("{0}, ", type.InterfaceList[i].FullName);
            }
            if (type.InterfaceList[type.InterfaceList.Count - 1] is BCLInterfaceType)
                this.ilStamentsCodeGeneration.Write("[mscorlib]");
            this.ilStamentsCodeGeneration.Write("{0}", type.InterfaceList[type.InterfaceList.Count - 1].FullName);
        }

        #endregion

        #region WriteLNClassHeader()

        /// <summary>
        /// Writes the class header in IL code
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Class name.</param>
        /// <param name="type">Class type expression.</param>
        public override void WriteLNClassHeader(int indent, string name, ClassType type) {
            this.ilStamentsCodeGeneration.WriteClassVisibility(indent, name, type);
            this.WriteClassName(name);
            this.WriteClassExtends(type);
            this.WriteClassImplements(type);
            this.ilStamentsCodeGeneration.WriteLine();
        }

        #endregion

        #region WriteEndOfClass()

        /// <summary>
        /// Writes the class termination acoording to IL code
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Name of the class to terminate.</param>       
        public override void WriteEndOfClass(int indent, string name) {
            //this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteCloseBrace(indent);
            this.ilStamentsCodeGeneration.WriteComment(name);
        }

        #endregion

        #region WriteLNInterfaceHeader()

        public override void WriteInterfaceHeader(int indent, string name, InterfaceType type) {
            this.ilStamentsCodeGeneration.WriteLNInterfaceHeader(indent, name, type);
        }
        #endregion

        #region WriteLNEndOfInterface()

        /// <summary>
        /// Writes the interface termination
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Name of the interface to terminate.</param>
        public override void WriteEndOfInterface(int indent, string name) {
            //this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteCloseBrace(indent);
            this.ilStamentsCodeGeneration.WriteComment("End of interface " + name);
        }
        #endregion

        #region ProcessField()

        virtual public void ProcessField(int indent, FieldDeclaration node, Object obj, bool constantField) {
            this.ilStamentsCodeGeneration.WriteLine(); //*
            this.WriteField(indent, ((FieldType)node.TypeExpr).MemberInfo.MemberIdentifier, (FieldType)node.TypeExpr, constantField);
            this.ilStamentsCodeGeneration.WriteLine(); //*
        }
        #endregion

        #region WriteField()

        public override void WriteField(int indent, string name, FieldType type, bool constantField) {
            this.ilStamentsCodeGeneration.WriteField(indent, name, type, constantField);
        }

        #endregion

        #region WriteLNFieldInitialization()

        public override void WriteLNFieldInitialization(TypeExpression type) {
            this.ilStamentsCodeGeneration.WriteFieldInitialization(type);
        }

        #endregion

        #region WriteEndOfField()

        /// <summary>
        /// Writes the field termination
        /// </summary>
        public override void WriteEndOfField() {
            this.ilStamentsCodeGeneration.Write(")");
        }

        #endregion

        #region WriteStartBlock()

        /// <summary>
        /// Writes the block inicialization
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public override void WriteStartBlock(int indent) {
            this.ilStamentsCodeGeneration.WriteOpenBrace(indent);
        }

        #endregion

        #region WriteLNEndOfBlock()

        /// <summary>
        /// Writes the termination token
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public override void WriteEndOfBlock(int indent) {
            this.ilStamentsCodeGeneration.WriteLineCloseBrace(indent); ;
        }

        #endregion

        #region WriteLNMethodHeader

        // .method <flags> <call_conv> <ret_type> <name>(<arg_list>) <impl> { <method_body> }
        // <flags> := public (public)
        //          | private (private)
        //          | assembly (internal)
        //          | family (protected)
        //          | famorassem (protected internal)
        //          | static (static)
        //          | newslot virtual (virtual)
        //          | virtual (override)
        //          | newslot abstract virtual (abstract)
        //
        // Method modifiers: new, public, protected, internal, private, static, virtual, sealed, 
        //                   override, abstract, extern
        // Method cannot be both virtual and static
        //
        // Sealed and extern modifier currently does not apply because it is commented in grammar file.

        /// <summary>
        /// Writes the method header.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Method name.</param>
        /// <param name="type">Method type expression.</param>
        public override void WriteLNMethodHeader(int indent, string name, MethodType type) {
            WriteMethodModifiers(indent, type);
            WriteMethodTypeOfAccess(name, type);
            WriteMethodParameters(type);
            WriteLNMethodEndOfHeader();
        }
        #endregion

        #region WriteLNMethodEndOfHeader
        protected virtual void WriteLNMethodEndOfHeader() {
            this.ilStamentsCodeGeneration.WriteLine(") cil managed");
        }
        #endregion
        // Este métodoo y el siguiente se dejan aquí por si puede aprovecharse para Rotor y CLR de no poder se delegaria
        // en ilStamentesCodeGeneration
        #region WriteMethodModifiers
        protected virtual void WriteMethodModifiers(int indent, MethodType type) {
            Modifier mask = type.MemberInfo.ModifierMask;

            this.ilStamentsCodeGeneration.Write(indent, ".method ");

            if (type.MemberInfo.Class is InterfaceType) {
                this.ilStamentsCodeGeneration.Write("public hidebysig ");
                return;
            }

            if ((mask & Modifier.Public) != 0)
                this.ilStamentsCodeGeneration.Write("public ");
            if ((mask & Modifier.Private) != 0)
                this.ilStamentsCodeGeneration.Write("private ");
            if ((mask & (Modifier.Protected | Modifier.Internal)) == (Modifier.Protected | Modifier.Internal))
                this.ilStamentsCodeGeneration.Write("famorassem ");
            else {
                if ((mask & Modifier.Internal) != 0)
                    this.ilStamentsCodeGeneration.Write("assembly ");
                if ((mask & Modifier.Protected) != 0)
                    this.ilStamentsCodeGeneration.Write("family ");
            }
            this.ilStamentsCodeGeneration.Write("hidebysig ");
        }
        #endregion

        #region WriteMethodTypeOfAccess

        public virtual void WriteMethodTypeOfAccess(string name, MethodType type) {
            Modifier mask = type.MemberInfo.ModifierMask;
            bool constructorFound = type.MemberInfo.Class.Name.Equals(name);
            if ((mask & Modifier.Virtual) != 0)
                this.ilStamentsCodeGeneration.Write("newslot virtual ");
            if ((mask & Modifier.Override) != 0)
                this.ilStamentsCodeGeneration.Write("virtual ");
            if ((mask & Modifier.Abstract) != 0 || type.MemberInfo.Class is InterfaceType)
                this.ilStamentsCodeGeneration.Write("newslot abstract virtual ");

            if (constructorFound)
                this.ilStamentsCodeGeneration.Write("specialname rtspecialname ");
            
            if ((mask & Modifier.Static) == 0 && !constructorFound && 
                type.MemberInfo.Class.InterfaceList.Count > 0) {
                this.ilStamentsCodeGeneration.Write("virtual final ");
            }

            if ((mask & Modifier.Static) != 0)
                this.ilStamentsCodeGeneration.Write("static ");
            else
                this.ilStamentsCodeGeneration.Write("instance ");

            if (constructorFound) {
                this.ilStamentsCodeGeneration.Write("void");
                if ((mask & Modifier.Static) != 0)
                    this.ilStamentsCodeGeneration.Write(" .cctor(");
                else
                    this.ilStamentsCodeGeneration.Write(" .ctor(");
            } else {
                if (type.Return is FieldType && ((FieldType)type.Return).FieldTypeExpression is TypeVariable)
                    this.WriteType(TypeVariable.NewTypeVariable);
                else
                    this.WriteType(type.Return);
                this.ilStamentsCodeGeneration.Write(" {0}(", name);
            }
        }

        #endregion

        #region WriteMethodParameters
        public virtual void WriteMethodParameters(MethodType type) {
            if (type.ParameterListCount == 0) // {P} type.ParameterListCount>0
                return;
            this.WriteType(type.GetParameter(0));
            this.ilStamentsCodeGeneration.Write(" {0}", type.ASTNode.ParametersInfo[0].ILName);
            for (int i = 1; i < type.ParameterListCount; i++)
                this.ilStamentsCodeGeneration.Write(", {0} {1}", type.GetParameter(i).ILType(), type.ASTNode.ParametersInfo[i].ILName);
        }
        #endregion

        #region WriteEndOfMethod()

        /// <summary>
        /// Writes the method termination. It writes also en end of line
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="name">Name of the method to terminate.</param>
        public override void WriteEndOfMethod(int indent, string name) {
            this.ilStamentsCodeGeneration.WriteCloseBrace(indent);
            this.ilStamentsCodeGeneration.WriteComment("End of method " + name);
        }

        #endregion

        #region WriteParams()
        /// Writes the parameters of a method
        /// </summary>
        /// <param name="memberType">The type of the method</param>s
        /// <param name="arguments">Actual arguments</param>
        public override void WriteParams(MethodType memberType, AST.CompoundExpression arguments) {
            this.ilStamentsCodeGeneration.Write("("); // start of signature
            if (memberType != null)
                // * When a method is known (not free type variable) we use the formal parameters
                if (memberType.ParameterListCount != 0) {
                    this.ilStamentsCodeGeneration.Write(memberType.GetParameter(0).ILType());
                    for (int i = 1; i < memberType.ParameterListCount; i++)
                        this.ilStamentsCodeGeneration.Write(", {0}", memberType.GetParameter(i).ILType());
                } else if (arguments != null && arguments.ExpressionCount != 0) { // * When a method is not known (free type variable) we use the actual parameters (arguments)
                    this.ilStamentsCodeGeneration.Write(arguments.GetExpressionElement(0).ILTypeExpression.ILType());
                    for (int i = 1; i < arguments.ExpressionCount; i++)
                        this.ilStamentsCodeGeneration.Write(", {0}", arguments.GetExpressionElement(i).ILTypeExpression.ILType());
                }
            this.ilStamentsCodeGeneration.Write(")"); // end of signature
        }
        #endregion

        #region AddLocalVariable()
        /// <summary>
        /// Stores the information of the local variable.
        /// </summary>
        /// <param name="name">Local variable identifier.</param>
        /// <param name="type">Local variable type.</param>
        public override void AddLocalVariable(string name, TypeExpression type) {
            if (this.currentLocalVars.Length == 0)
                LocalVariableIndex = 0;
            else
                this.currentLocalVars.Append(",");
            this.currentLocalVars.AppendLine();
            this.currentLocalVars.AppendFormat("\t\t\t[{0}] {1} {2}", LocalVariableIndex++, type.ILType(), name);
        }

        #endregion

        #region WriteLocalVariable()
        /// <summary>
        /// Writes the information of local variables.
        /// </summary>
        /// <param name="indent">Current indentation.</param>
        public override void WriteLocalVariable(int indent) {
            if (this.currentLocalVars.Length == 0) // {p} this.currentLocalVars.Length != 0
                return;
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine(".locals init({0})", this.currentLocalVars);
            this.currentLocalVars.Remove(0, this.currentLocalVars.Length);
        }

        #endregion

        #region WriteAuxiliarLocalVariable()
        /// <summary>
        /// Writes the information of local variables.
        /// </summary>
        /// <param name="indent">Current indentation.</param>
        /// <param name="id">Auxiliar local variable identifier.</param>
        /// <param name="type">Auxiliar local variable type expression.</param>
        public override void WriteAuxiliarLocalVariable(int indent, string id, string type) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteAuxiliarLocalVariable(id, type);
            this.ilStamentsCodeGeneration.WriteLine();
        }

        #endregion

        #region WriteEntryPoint ()

        public override void WriteEntryPoint(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteEntryPoint();
            this.ilStamentsCodeGeneration.WriteLine();
        }

        #endregion

        #region ThrowDirective ()

        public override void WriteTryDirective(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine(".try");
        }
        public override void WriteOpenBraceTry(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("{");
        }
        public override void WriteCloseBraceTry(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("}  //  end .try");
        }
        public  void WriteRethrow(int indent) {
            this.WriteLNNoArgsCommand(indent, "rethrow");
        }
        
        #endregion

        #region Catch()

        public override void WriteCatch(int indent, String type, String var) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("catch " + (var == null ? String.Empty : var));
        }
        public override void WriteOpenBraceCatch(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("{");
            
        }
        public override void WriteCloseBraceCatch(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("}  //  end handler");
        }

        #endregion

        #region Finally()

        public override void WriteFinally(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("finally");
        }
        public override void WriteOpenBraceFinally(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("{");
        }
        public override void WriteCloseBraceFinally(int indent) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            this.ilStamentsCodeGeneration.WriteLine("}  //  end .try");
        }

        #endregion

        public virtual void WriteThrowMissingMethodException(int indent, string method) 
        {
            WriteAuxiliarLocalVariable(indent, "_temp_top_stack_str_", "string");
            WriteLNNoArgsCommand(indent, "callvirt   instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()");
            WriteLNNoArgsCommand(indent, "callvirt   instance string [mscorlib]System.Reflection.MemberInfo::get_Name()");
            Comment(indent, "//in the top of the stack there is the name of the class");

            // swappingt he top of the stack with the string inmediately bellow
            stloc(indent, "_temp_top_stack_str_");
            ldstr(indent, "Class {0} does not accept message {1}().");
            ldloc(indent, "_temp_top_stack_str_");
            ldstr(indent, method);

            WriteLNNoArgsCommand(indent, "call   string [mscorlib]System.String::Format(string,object, object)");
            WriteThrowException(indent, "[mscorlib]System.MissingMemberException", new string[] { "string" });
        }

        public virtual void WriteThrowNonSuitableObjectException(int indent, string klass, string method) {
            ldstr(indent, String.Format("Class {0} does not accept message {1}().", klass, method));
            WriteThrowException(indent, "[mscorlib]System.MissingMemberException", new string[] { "string" });
        }
        #region AddExceptionCode()

        /// <summary>
        /// Adds the information of specified exception to include in intermediate code file.
        /// </summary>
        /// <param name="typeException">Exception to include.</param>
        public void AddExceptionCode(DynamicExceptionManager typeException) {
            if (this.exceptions.ContainsKey(typeException.TypeName)) // {P} the exception isn't included yet
                return;

            this.exceptions.Add(typeException.TypeName, typeException);
            DynamicExceptionManager baseType = typeException.BaseException;
            while (baseType != null)
                if (!this.exceptions.ContainsKey(baseType.TypeName)) {
                    this.exceptions.Add(baseType.TypeName, baseType);
                    baseType = baseType.BaseException;
                } else
                    break; //baseType = null;
        }

        #endregion

        #region RuntimeUnionTypePromotion()

        /// <summary>
        /// Checks if the union type can to promote to the specified type.
        /// </summary>
        /// <param name="indent">Indentation to write.</param>
        /// <param name="union">Union type to check.</param>
        /// <param name="type">WriteType to promote.</param>
        ////private void RuntimeUnionTypePromotion(int indent, UnionType union, TypeExpression type) {
        ////    if (union.Count != 1) {
        ////        string notThisType = this.NewLabel;
        ////        string endLabel = this.NewLabel;

        ////        this.RuntimeIsInstruction(indent, type);
        ////        this.brfalse(indent, notThisType);
        ////        ////
        ////             type.AcceptOperation( new CGRuntimeFreshTEPromotionOperation(indent, this));
        ////        ///this.RuntimeFreshTypeExpressionPromotion(indent, type);
        ////            ///
        ////        this.br(indent, endLabel);
        ////        this.ilStamentsCodeGeneration.WriteLabel(notThisType);
        ////        // If the expected type is an String, we must call its ToString method
        ////        if (TypeExpression.Is<StringType>(type))
        ////            this.CallVirt(indent, "instance", "string", "[mscorlib]System.Object", "ToString", null);
        ////        else {// Throw an exception
        ////            this.WriteThrowException(indent, new WrongDynamicTypeExceptionManager());
        ////            this.AddExceptionCode(new WrongDynamicTypeExceptionManager());
        ////        }
        ////        this.WriteLabel(endLabel);
        ////    }
        ////}
        #endregion

#region Exit(int) 
        /// <summary>
        /// generates exit environment function of the OS
        /// </summary>
        /// <param name="indent"></param>
        /// <param name="res">Code to return to the operative system</param>
        public virtual void Exit(int indent, int res) {
            this.ldci4(indent, res);
            this.WriteLNNoArgsCommand(indent, "call   void [mscorlib]System.Environment::Exit(int32)");

        }
#endregion
        #region Promotion()

        /// <summary>
        /// Writes the il code to promote typeExp1 to typeExp2
        /// </summary>
        /// <param name="typeExp1">The type of the element that is in the stack</param>
        /// <param name="typeExp2">The type to promote to</param>
        public void Promotion(int indent, TypeExpression typeExp1, TypeExpression certainType1, TypeExpression typeExp2, TypeExpression certainType2, bool unidirectionalConversion, bool makeBoxing) {
            FieldType aux;
            bool isFieldType1 = false;
            bool isFieldType2 = false;
            UnionType union = null;

            if ((aux = TypeExpression.As<FieldType>(typeExp1)) != null) {
                typeExp1 = aux.FieldTypeExpression;
                isFieldType1 = true;
            }
      

            if ((aux = TypeExpression.As<FieldType>(typeExp2)) != null) {
                typeExp2 = aux.FieldTypeExpression;
                isFieldType2 = true;
            }
            if ((aux = TypeExpression.As<FieldType>(certainType1)) != null)
                certainType1 = aux.FieldTypeExpression;

            if ((aux = TypeExpression.As<FieldType>(certainType2)) != null)
                certainType2 = aux.FieldTypeExpression;


            if (!certainType1.IsFreshVariable() && !TypeExpression.Is<UnionType>(certainType1))
            {
                // * If both values are built-in, check promotion of int to double
                if (certainType1.IsValueType() && certainType2.IsValueType()) {
                    // Only converts integer to double. If certain type1==Double laconversión es necesaria
                    if (TypeExpression.Is<DoubleType>(certainType2) &&
                        (TypeExpression.Is<CharType>(certainType1) || TypeExpression.Is<IntType>(certainType1))) {
                        if (typeExp1 is TypeVariable && makeBoxing)
                            this.UnboxAny(indent, certainType1);
                        this.convToDouble(indent);
                    } else {
                        if (!(TypeExpression.Is<IntType>(certainType2) && TypeExpression.Is<CharType>(certainType1))) {
                            // if certainType1 and certainType2 are the same value type, and one of this is a field variable type, then make boxing.
                            if (certainType1 == certainType2 || certainType1.ILType().Equals(certainType2.ILType()))
                            {
                                if (isFieldType1) {
                                    if (typeExp1 is TypeVariable)
                                        this.UnboxAny(indent, certainType1);
                                } else {
                                    if (!isFieldType1 && isFieldType2) {
                                        if (typeExp2 is TypeVariable)
                                            this.Box(indent, certainType2);
                                    }
                                }
                            } else {
                                if (typeExp1 is TypeVariable && makeBoxing)
                                {
                                    if (!TypeExpression.Is<UnionType>(certainType2))
                                        this.UnboxAny(indent, certainType2);
                                }
                                else
                                {
                                    if (!TypeExpression.Is<UnionType>(certainType1) && TypeExpression.Is<UnionType>(certainType2))
                                    {
                                        if (unidirectionalConversion)
                                            this.Box(indent, certainType1);
                                    }

                                    if ((union = TypeExpression.As<UnionType>(certainType1)) != null && !TypeExpression.Is<UnionType>(certainType2))
                                    {
                                        if (!IsUnionOfProperties(union))
                                            certainType2.AcceptOperation(new CGRuntimeUnionTypePromotionOperation<ILCodeGenerator>(indent, this, union), null);
                                        else if(union.Count > 0)
                                        {
                                            PropertyType propertyType = TypeExpression.As<PropertyType>(union.TypeSet[0]);
                                            Promotion(indent, propertyType.PropertyTypeExpression, propertyType.PropertyTypeExpression, typeExp2, certainType2, unidirectionalConversion, makeBoxing);
                                        }
                                    }
                                        
                                }

                                // ValueType to TypeVariable
                                // - This code is only execute if the conversion is unidirectional (e.g. argument to parameter)
                                // - The conversion can be bidirectional (e.g a != b; type(a) to type(b) or type(b) to type(a) 
                                //   In this case, the correct conversion is to make an Unbox on TypeVariable and not to make a Box on ValueType.
                                //   Note: Relational expressions are different of the rest of binary expression.
                                //         e.g. a + b; 'a' or 'b' are going to promote to the type of arithmetical node. But, in relational expression, 
                                //         the type of this expression is always bool, but it is possible that 'a' has to promote to 'b' or viceversa.
                                if (unidirectionalConversion) {
                                    if (!(typeExp1 is TypeVariable) && typeExp2 is TypeVariable && makeBoxing)
                                        this.Box(indent, certainType2);
                                }
                            }
                        }
                    }
                    return;
                }

                // * If the built-in must promote to an object, boxing is required
                if (certainType1.IsValueType() && !certainType2.IsValueType()) {
                    if (unidirectionalConversion) {
                        if ((!(typeExp1 is TypeVariable)) || (TypeExpression.Is<UnionType>(certainType2)))
                        //if (!(typeExp1 is TypeVariable)) //If TypeExp1 is a TypeVariable boxing is no needed
                            this.Box(indent, certainType1);
                        else if(!makeBoxing && !(TypeExpression.Is<UnionType>(certainType1)) && certainType2.FullName.Equals(typeof(Object).FullName))
                            this.Box(indent, certainType1);
                        else if (certainType2 is TypeVariable && ((TypeVariable)certainType2).Substitution == null && !isFieldType1)                        
                            this.Box(indent, certainType1);
                        else if (certainType2 is TypeVariable && ((TypeVariable)certainType2).Substitution == null && ((TypeVariable)certainType2).EquivalenceClass != null)
                            this.Box(indent, certainType1);
                    }

                    // * If the expected type is an String, we must call its ToString method
                    if (TypeExpression.Is<StringType>(certainType2))
                        this.CallVirt(indent, "instance", "string", "[mscorlib]System.Object", "ToString", null);

                    return;
                }

                // If the object must promote to an built-in, unboxing is required
                if (!certainType1.IsValueType() && certainType2.IsValueType()) {
                    if (TypeExpression.Is<UnionType>(certainType1))
                        /////
                        /////this.RuntimeFreshTypeExpressionPromotion(indent, certainType2);
                        ////////////////////////
                        certainType2.AcceptOperation(new CGRuntimeFreshTEPromotionOperation<ILCodeGenerator>(indent, this), null);
                    return;
                }

                if ((union = TypeExpression.As<UnionType>(certainType1)) != null && !TypeExpression.Is<UnionType>(certainType2) && (certainType2.IsValueType() || TypeExpression.Is<StringType>(certainType2)))
                    certainType2.AcceptOperation(new CGRuntimeUnionTypePromotionOperation<ILCodeGenerator>(indent, this, union), null);
            }
            else if (certainType1.IsFreshVariable())
            {
                // * We require a runtime check to know if a promotion is needed
                /////
                /////this.RuntimeFreshTypeExpressionPromotion(indent, certainType2);
                ////////////////////////IL
                certainType2.AcceptOperation(new CGRuntimeFreshTEPromotionOperation<ILCodeGenerator>(indent, this), null);
            }
            else if (TypeExpression.Is<UnionType>(certainType1) && IsValueType(certainType2))
            {
                certainType2.AcceptOperation(new CGRuntimeFreshTEPromotionOperation<ILCodeGenerator>(indent, this), null);
            }
        }

        private bool IsUnionOfProperties(UnionType ut)
        {
            foreach (var typeExpression in ut.TypeSet)
                if (!(typeExpression is PropertyType))
                    return false;
            return true;
        }
        
        protected bool IsValueType(TypeExpression exp)
        {
            if (exp is BoolType)
                return true;
            if (exp is CharType)
                return true;
            if (exp is IntType)
                return true;
            if (exp is DoubleType)
                return true;
            return false;
        }

        #endregion

        #region WriteCodeOfExceptionsTemplateMethod
        /// <summary>
        ///  Implements Template Method
        /// </summary>
        /// <param name="d">An TypeSsystem Mananager to exception to use</param>
        protected override void WriteCodeOfExceptionsTemplateMethod(DynamicExceptionManager d) {
            this.ilStamentsCodeGeneration.WriteLine(d.WriteDynamicExceptionCode());

        }

        #endregion

        #region RuntimeFreshTypeExpressionPromotion()
        // <summary>
        /// Converts a fresh type variable on the stack (object) to another type
        /// <param name="indent">Indentation level</param>
        /// <param name="typeExpression">The type expression to promote to</param>
        /// </summary>
        //   public  override void RuntimeFreshTypeExpressionPromotion(int indent, TypeExpression typeExpression) {
        //// * Nothing to do if it is not a value type
        //if (!typeExpression.IsValueType())
        //    return;

        //// * It could be necessary to do a promotion...
        //if (TypeExpression.Is<IntType>(typeExpression)) {
        //    // * char to int
        //    // * If we must promote to an integer, a Char could be on the stack
        //    this.dup(indent);
        //    this.isinst(indent, CharType.Instance);
        //    string notACharLabel = this.NewLabel;
        //    this.brfalse(indent, notACharLabel);
        //    this.UnboxAny(indent, CharType.Instance);
        //    string endLabel = this.NewLabel;
        //    this.br(indent, endLabel);

        //    this.WriteLabel(notACharLabel);
        //    this.UnboxAny(indent, IntType.Instance);

        //    this.WriteLabel(endLabel);

        //    return;
        //}
        //if (TypeExpression.Is<DoubleType>(typeExpression)) {
        //    // * char to double or int to double
        //    // * If we must promote to an double, a Char or a Int32 could be on the stack
        //    this.dup(indent);
        //    this.isinst(indent, CharType.Instance);
        //    string notACharLabel = this.NewLabel;
        //    this.brfalse(indent, notACharLabel);
        //    this.UnboxAny(indent, CharType.Instance);
        //    this.convToDouble(indent);
        //    string endLabel = this.NewLabel;
        //    this.br(indent, endLabel);

        //    this.WriteLabel(notACharLabel);
        //    this.dup(indent);
        //    this.isinst(indent, IntType.Instance);
        //    string notAInt32Label = this.NewLabel;
        //    this.brfalse(indent, notAInt32Label);
        //    this.UnboxAny(indent, IntType.Instance);
        //    this.convToDouble(indent);
        //    this.br(indent, endLabel);

        //    this.WriteLabel(notAInt32Label);
        //    this.UnboxAny(indent, DoubleType.Instance);

        //    this.WriteLabel(endLabel);

        //    return;
        //}

        //// * If not promotion is needed, we simply Unbox
        //this.UnboxAny(indent, typeExpression);
        //return;
        // }

        #endregion

        //////#region RuntimeIsInstruction()

        ///////// <summary>
        ///////// Checks if the type variable on the stack is a specified type or can be to promotion.
        ///////// <param name="indent">Indentation level</param>
        ///////// <param name="typeExpression">The type expression to promote to</param>
        ///////// </summary>
        //////public override void RuntimeIsInstruction(int indent, TypeExpression typeExpression) {
        //////    // * The type could be to promote to IntType...
        //////    if (TypeExpression.Is<IntType>(typeExpression)) {
        //////        string notACharLabel = this.NewLabel;
        //////        string endLabel = this.NewLabel;

        //////        // * char to int
        //////        // * If we could promote to an integer, a Char could be on the stack
        //////        this.dup(indent);
        //////        this.isinst(indent, CharType.Instance);
        //////        this.dup(indent);
        //////        this.brfalse(indent, notACharLabel);
        //////        this.br(indent, endLabel);

        //////        this.WriteLabel(notACharLabel);
        //////        this.pop(indent);
        //////        this.dup(indent);
        //////        this.isinst(indent, IntType.Instance);
        //////        this.WriteLabel(endLabel);
        //////        return;
        //////    }

        //////    // * The type could be to promote to DoubleType...
        //////    if (TypeExpression.Is<DoubleType>(typeExpression)) {
        //////        string notACharLabel = this.NewLabel;
        //////        string notAInt32Label = this.NewLabel;
        //////        string endLabel = this.NewLabel;

        //////        // * char to double or int to double
        //////        // * If we could promote to an double, a Char or a Int32 could be on the stack
        //////        this.dup(indent);
        //////        this.isinst(indent, CharType.Instance);
        //////        this.dup(indent);
        //////        this.brfalse(indent, notACharLabel);
        //////        this.br(indent, endLabel);

        //////        this.WriteLabel(notACharLabel);
        //////        this.pop(indent);
        //////        this.dup(indent);
        //////        this.isinst(indent, IntType.Instance);
        //////        this.dup(indent);
        //////        this.brfalse(indent, notAInt32Label);
        //////        this.br(indent, endLabel);

        //////        this.WriteLabel(notAInt32Label);
        //////        this.pop(indent);
        //////        this.dup(indent);
        //////        this.isinst(indent, DoubleType.Instance);
        //////        this.WriteLabel(endLabel);

        //////        return;
        //////    }

        //////    // * If not promotion is needed, we simply check the stack
        //////    // * Nothing to do if it is not a value type
        //////    this.dup(indent);
        //////    this.isinst(indent, typeExpression);
        //////    return;
        //////}

        //////#endregion

        #region WriteThrowException

        /// <summary>
        /// Writes the generation code to throw a specified exception.
        /// </summary>
        /// <param name="indent">Identation to use.</param>
        /// <param name="dynException">Exception to throw.</param>
        public override void WriteThrowException(int indent, DynamicExceptionManager dynException) {
            this.newobj(indent, dynException.TypeName, new string[] { });
            this.WriteThrow(indent);
        }
        
        /// <summary>
        ///Writes the code to throw an exception derived from Exception without using DynamicExceptionManager . 
        ////// </summary>
        /// <param name="indent">indentation to use</param>
        /// <param name="ex">Name of the exception to throw</param>
        /// <param name="type args">an array with the type name of the arguments of the constructor exception. If the list is empty there won't be code generation about this param</param>
        public override void WriteThrowException(int indent, string ex, string[] msg) {
            this.newobj(indent, ex, msg);
            this.WriteThrow(indent);
        }

#endregion
        // IL Instructions

        #region Addressing Classes and Value Types

        #region ldobj

        // ldobj <token>
        // Load an instance value type specified by <token> on the stack. 

        /// <summary>
        /// Writes the ldobj instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="val">Value to load.</param>
        public void ldobj(int indent, TypeExpression val) {
            this.WriteLNUnaryCommand(indent, "ldobj", val.ILType());
        }

        #endregion

        #region ldstr

        // ldstr <token>
        // Load a string reference on the stack.

        /// <summary>
        /// Writes the ldstr instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="val">Value to load.</param>
        public void ldstr(int indent, string val) {
            if (val[0] != '\"' || val[val.Length - 1] != '\"')
                val = "\"" + val + "\"";
            this.WriteLNUnaryCommand(indent, "ldstr", val);
        }

        #endregion

        #region ldnull

        // ldnull
        // Load a null object reference on the stack.

        /// <summary>
        /// Writes the ldnull instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void ldnull(int indent) {
            this.WriteLNNoArgsCommand(indent, "ldnull");
        }

        #endregion

        #region newobj

        #region newobj

        // newobj
        // Allocate memory for a new instance of a class—not a value type—and call
        // the instance constructor method specified by <token>. 

        /// <summary>
        /// Writes the newobj instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        public void newobj(int indent, MethodType memberType, TypeExpression obj, string member) {
            this.ilStamentsCodeGeneration.Write(indent, "newobj    instance void {0}::{1}", obj.ILType(), member);
            this.WriteParams(memberType, null);
            this.ilStamentsCodeGeneration.WriteLine();
        }

        #endregion


        /// <summary>
        /// Writes the newobj instruction
        /// </summary>
        /// <param name="indent">Text Indentation</param>
        /// <param name="klass">Class type</param>
        /// <param name="args">Types of parameters</param>
        public void newobj(int indent, string klass, string[] args) {
            this.ilStamentsCodeGeneration.Write(indent, "newobj    instance void {0}::.ctor(", klass);

            if (args != null)
                this.ilStamentsCodeGeneration.Write("{0}", String.Join(", ", args));
            this.ilStamentsCodeGeneration.WriteLine(")");
        }

        #endregion

        #region castclass

        // castclass <token>
        // Cast a class instance to the class specified by <token>.

        /// <summary>
        /// Writes the castclass instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="token">Type of the new instance.</param>
        public void castclass(int indent, TypeExpression token) {
            this.WriteLNUnaryCommand(indent, "castclass", token.ILType());
        }

        #endregion

        #region throw

        // throw
        // Pop the object reference from the stack and throw it as a managed exception.
        /// <summary>
        /// Writes the throw instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void WriteThrow(int indent) {
            this.WriteLNNoArgsCommand(indent, "throw");
        }

        #endregion

        #endregion

        #region Argument instructions

        #region ldarg()
        /// <summary>
        /// ldarg <unsigned int16>
        /// Load the argument number <unsigned int16> on the stack. The argument 
        /// enumeration is zero-based, but it’s important to remember that 
        /// instance methods have an 'invisible' argument not specified in the 
        /// method signature: the class instance pointer, this, which is always 
        /// argument number 0. Because static methods don’t have such an 'invisible' 
        /// argument, for them argument number 0 is the first argument specified in
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="argNumber">Argument number to load.</param>
        public void ldarg(int indent, int argNumber) {
            this.ilStamentsCodeGeneration.WriteLine(indent, "ldarg.{0}", argNumber);
        }

        /// <summary>
        /// Writes the ldarg instruction. Unsing argument names instead of numbers. See ldar(int, int) above
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="argName">Argument name to load.</param>
        public void ldarg(int indent, string argName) {
            this.WriteLNUnaryCommand(indent, "ldarg", argName);
        }

        #endregion

        #region ldarga()
        /// <summary>
        /// Writes the ldarga instruction, that loads the direction of argument with name argName.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="argName">Argument name to load.</param>
        public void ldarga(int indent, string argName) {
            this.WriteLNUnaryCommand(indent, "ldarga", argName);
        }
       #endregion

        #region starg()

        /// <summary>
        /// starg <unsigned int16>
        /// Take a value from the stack and store it in argument slot 
        /// number <unsigned int16>. The value on the stack must be of the same 
        /// type as the argument slot or must be convertible to the type of the 
        /// argument slot. The convertibility rules and effects are the same as 
        /// those for conversion operations, discussed earlier in this chapter. 
        /// With vararg methods, the starg instruction cannot target the arguments
        /// of the variable part of the signature.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="argNumber">Argument number to store.</param>
        public void starg(int indent, int argNumber) {
            this.ilStamentsCodeGeneration.WriteLine(indent, "starg.{0}", argNumber);
        }

        /// <summary>
        /// Writes the starg instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="argNumber">Argument name to store.</param>
        public void starg(int indent, string argNumber) {
            this.WriteLNUnaryCommand(indent, "starg", argNumber);
        }

        #endregion

        #region arglist()

        // arglist
        // Get the argument list handle.

        /// <summary>
        /// Writes the arglist instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void arglist(int indent) {
            this.WriteLNNoArgsCommand(indent, "arglist");
        }

        #endregion

        #endregion

        #region Arithmetical Operations

        // All arithmetical operations except the negation operation take two 
        // operands from the stack and put the result on the stack.

        #region add

        // add
        // Addition.

        /// <summary>
        /// Writes the add instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void add(int indent) {
            this.WriteLNNoArgsCommand(indent, "add");
        }

        /// <summary>
        /// Writes the concat instruction for string types.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void concat(int indent) {
            this.WriteLNNoArgsCommand(indent, "call    string [mscorlib]System.String::Concat(object, object)");
        }

        #endregion

        #region mul

        // mul
        // Multiplication.

        /// <summary>
        /// Writes the mul instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void mul(int indent) {
            this.WriteLNNoArgsCommand(indent, "mul");
        }

        #endregion

        #region div

        // div
        // Division.

        /// <summary>
        /// Writes the div instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void div(int indent) {
            this.WriteLNNoArgsCommand(indent, "div");
        }

        #endregion

        #region neg

        // neg
        // Negate—that is, invert the sign.

        /// <summary>
        /// Writes the neg instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void neg(int indent) {
            this.WriteLNNoArgsCommand(indent, "neg");
        }
        #endregion

        #region rem

        // rem
        // Remainder, modulo.

        /// <summary>
        /// Writes the rem instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void rem(int indent) {
            this.WriteLNNoArgsCommand(indent, "rem");
        }


        #endregion

        #region sub

        // sub
        // Subtraction.

        /// <summary>
        /// Writes the sub instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void sub(int indent) {
            this.WriteLNNoArgsCommand(indent, "sub");
        }
        #endregion

        #endregion

        #region Bitwise Operations

        // Bitwise operations have no parameters and are defined for integer types only

        #region and

        // and
        // Bitwise AND (binary).

        /// <summary>
        /// Writes the and instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void and(int indent) {
            this.WriteLNNoArgsCommand(indent, "and");
        }

        #endregion

        #region or

        // or
        // Bitwise OR (binary).

        /// <summary>
        /// Writes the or instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void or(int indent) {
            this.WriteLNNoArgsCommand(indent, "or");
        }

        #endregion

        #region xor

        // xor
        // Bitwise exclusive OR (binary).

        /// <summary>
        /// Writes the xor instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void xor(int indent) {
            this.WriteLNNoArgsCommand(indent, "xor");
        }

        #endregion

        #region not

        /// <summary>
        /// Bitwise inversion (unary).
        /// This operation, rather than neg, is recommended for integer sign 
        /// inversion because neg has a problem with the maximum negative numbers:
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void not(int indent) {
            this.WriteLNNoArgsCommand(indent, "not");
        }

        #endregion

        #endregion

        #region Box and Unbox

        #region Box
        // Box <token>
        // Convert a value type instance to an object reference. <token> specifies the value type being converted and must be a valid TypeDef or TypeRef token. 
        // This instruction pops the value type instance from the stack, creates a new instance of the type as an object, and pushes the object reference to this instance on the stack.

        /// <summary>
        /// Writes the Box instruction
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Converts to this object reference.</param>
        public override void Box(int indent, TypeExpression type) {
            this.WriteLNUnaryCommand(indent, "box", type.ILType());
        }

        /// <summary>
        /// Makes sure to convert a type to an object
        /// </summary>
        /// <param name="indent">Indentation level</param>
        /// <param name="type">The type to be promoted</param>
        public override void BoxIfNeeded(int indent, TypeExpression type) {
            if (TypeMapping.Instance.RequireBoxing(type.ILType()))
                this.Box(indent, type);
        }

        #endregion

        #region Unbox

        /// <summary>
        /// Convert and OBject to a valuetype if is not an reference type        /// </summary>
        /// <param name="indent">Indentation level</param>
        /// <param name="type">The type to be promoted</param>
        public virtual void UnBoxIfNeeded(int indent, TypeExpression type) {
            if ( TypeMapping.Instance.RequireBoxing(type.ILType()) )
                this.Unbox(indent, type);
        }        // Unbox <token>
        // Revert a boxed value type instance from the object form to its value type form. <token> specifies the value type being converted and must be a valid TypeDef or TypeRef token. 
        // This instruction takes an object reference from the stack and puts a managed pointer to the value type instance on the stack.

        /// <summary>
        /// Writes the Unbox instruction
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Value type to revert.</param>
        public override void Unbox(int indent, TypeExpression type) {
            this.WriteLNUnaryCommand(indent, "unbox", type.ILType());
        }

        #endregion

        #endregion

        #region Calling Methods

        #region call <token>

        #region WriteCallArguments()
        // Esta seguramente no sé podrá mover pues puede que se use en el dlR
        /// <summary>
        /// Writes the 'token' of call instructions.
        /// </summary>
        /// <param name="memberType">Member type (null when the implicit object is a fresh type variable).</param>
        /// <param name="klass">WriteType of the object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        /// <param name="arguments">Actual arguments. This parameter is only necessary when the "memberType" parameter may be null.</param>
        virtual protected void WriteCallArguments(MethodType memberType, TypeExpression klass, string member, AST.CompoundExpression arguments) {
            if (IsInstance(memberType))
                this.ilStamentsCodeGeneration.Write("instance ");

            string returnILType, classILType;
            if (memberType != null) {
                if (memberType.Return is FieldType && ((FieldType)memberType.Return).FieldTypeExpression is TypeVariable)
                    returnILType = "object"; // * Fresh type variables
                else
                    returnILType = memberType.Return.ILType();
                classILType = klass.ILType();
            } else {
                returnILType = "object"; // * Fresh type variables
                classILType = "[mscorlib]System.Object";
            }
            this.ilStamentsCodeGeneration.Write("{0} {1}::{2}", returnILType, classILType, member);
            this.WriteParams(memberType, arguments);
        }

        /// <summary>
        /// Writes the 'token' of call instructions.
        /// </summary>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param> (null when the implicit object is a fresh type variable)
        /// <param name="member">Member to call.</param>
        /// <param name="setProperty">True if it is a set property. False if it is a get property.</param>
        private void WriteCallArguments(PropertyType memberType, TypeExpression obj, string member, bool setProperty) {
            if (IsInstance(memberType))
                this.ilStamentsCodeGeneration.Write("instance ");

            string memberILType = memberType == null ? "object" : memberType.PropertyTypeExpression.ILType();

            if (setProperty)
                this.ilStamentsCodeGeneration.WriteLine("void {0}::set_{1}({2})", obj.ILType(), member, memberILType);
            else
                this.ilStamentsCodeGeneration.WriteLine("{0} {1}::get_{2}()", memberILType, obj.ILType(), member);
        }
        #endregion


        // call <token>
        // Call a nonvirtual method.

        /// <summary>
        /// Writes the call instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        public  override void Call(int indent, MethodType memberType, TypeExpression obj, string member) {
            this.ilStamentsCodeGeneration.Write(indent, "call    ");
            this.WriteCallArguments(memberType, obj, member, null);
            this.ilStamentsCodeGeneration.WriteLine();
        }

        /// <summary>
        /// Writes the call instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        /// <param name="setProperty">True if it is a set property. False if it is a get property.</param>
        public override void Call(int indent, PropertyType memberType, TypeExpression obj, string member, bool setProperty) {
            this.ilStamentsCodeGeneration.Write(indent, "call    ");
            this.WriteCallArguments(memberType, obj, member, setProperty);
            this.ilStamentsCodeGeneration.WriteLine();
        }
        //OJO: igual está mal el orden de los parámetros de este call, sólo es llamado desde el VisitorCLRCOdeGeneration

        /// <summary>
        /// Writes the call instruction
        /// </summary>
        /// <param name="indent">Text Indentation</param>
        /// <param name="methodType">Qualifiers of the method</param>
        /// <param name="result">Return type</param>
        /// <param name="klass">Class type</param>
        /// <param name="memberName">Name of the member</param>
        /// <param name="args">Types of parameters</param>
        public override void Call(int indent, string methodType, string result, string klass, string memberName, string[] args) {
            this.ilStamentsCodeGeneration.Write(indent, "call    {0} {1} {2}::{3}(", methodType, result, klass, memberName);
            if (args != null) {
                this.ilStamentsCodeGeneration.Write(String.Join(", ", args));
            }
            this.ilStamentsCodeGeneration.WriteLine(")");
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
            this.ilStamentsCodeGeneration.Write(indent, "callvirt    ");
            this.WriteCall(memberType, klass, member, null);
            this.ilStamentsCodeGeneration.WriteLine();
        }

        /// <summary>
        /// Writes the CallVirt instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        /// <param name="setProperty">True if it is a set property. False if it is a get property.</param>
        public override void CallVirt(int indent, PropertyType memberType, TypeExpression obj, string member, bool setProperty) {
            this.ilStamentsCodeGeneration.Write(indent, "callvirt    ");
            this.WriteCall(memberType, obj, member, setProperty);
            this.ilStamentsCodeGeneration.WriteLine();
        }


        /// <summary>
        /// Writes the CallVirt instruction
        /// </summary>
        /// <param name="indent">Text Indentation</param>
        /// <param name="methodType">Qualifiers of the method</param>
        /// <param name="result">Return type</param>
        /// <param name="klass">Class type</param>
        /// <param name="memberName">Name of the member</param>
        /// <param name="args">Types of parameters</param>
        public override void CallVirt(int indent, string methodType, string result, string klass, string memberName, string[] args) {
            this.ilStamentsCodeGeneration.Write(indent, "callvirt    {0} {1} {2}::{3}(", methodType, result, klass, memberName);

            if (args != null)
                this.ilStamentsCodeGeneration.Write(String.Join(", ", args));
            this.ilStamentsCodeGeneration.WriteLine(")");
        }
        #endregion

        #region WriteCall()

        /// <summary>
        /// Writes the 'token' of call instructions.
        /// </summary>
        /// <param name="memberType">Member type (null when the implicit object is a fresh type variable).</param>
        /// <param name="klass">WriteType of the object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        /// <param name="arguments">Actual arguments. This parameter is only necessary when the "memberType" parameter may be null.</param>
        virtual protected void WriteCall(MethodType memberType, TypeExpression klass, string member, AST.CompoundExpression arguments) {
            if (IsInstance(memberType))
                this.ilStamentsCodeGeneration.Write("instance ");

            string returnILType, classILType;
            if (memberType != null) {
                if (memberType.Return is FieldType && ((FieldType)memberType.Return).FieldTypeExpression is TypeVariable)
                    returnILType = "object"; // * Fresh type variables
                else
                    returnILType = memberType.Return.ILType();
                classILType = klass.ILType();
            } else {
                returnILType = "object"; // * Fresh type variables
                classILType = "[mscorlib]System.Object";
            }
            this.ilStamentsCodeGeneration.Write("{0} {1}::{2}", returnILType, classILType, member);
            this.WriteParams(memberType, arguments);
        }
        /// <summary>
        /// Writes the 'token' of call instructions.
        /// </summary>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param> (null when the implicit object is a fresh type variable)
        /// <param name="member">Member to call.</param>
        /// <param name="setProperty">True if it is a set property. False if it is a get property.</param>
        private void WriteCall(PropertyType memberType, TypeExpression obj, string member, bool setProperty) {
            if (IsInstance(memberType))
                this.output.Write("instance ");

            string memberILType = memberType == null ? "object" : memberType.PropertyTypeExpression.ILType();
            string klass = TypeMapping.Instance.GetBCLName(obj.ILType(), true);
            if (setProperty)
                this.ilStamentsCodeGeneration.Write("void {0}::set_{1}({2})", klass, member, memberILType);
            else
                this.ilStamentsCodeGeneration.Write("{0} {1}::get_{2}()", memberILType, klass, member);
        }
        #endregion

        #endregion

        #region Comparative Branching Instructions

        #region beq

        // beq <int32>
        // Branch if <value1> is equal to <value2>. 

        /// <summary>
        /// Writes the beq instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void beq(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "beq", label);
        }

        #endregion

        #region bne.un

        // bne.un <int32>
        // Branch if the two values are not equal. Integer values are interpreted as unsigned.

        /// <summary>
        /// Writes the bne instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void bne(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "bne.un", label);
        }

        #endregion

        #region bge

        // bge <int32>
        // Branch if greater or equal.

        /// <summary>
        /// Writes the bge instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void bge(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "bge", label);
        }

        #endregion

        #region bgt

        // bgt <int32>
        // Branch if greater.

        /// <summary>
        /// Writes the bgt instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void bgt(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "bgt", label);
        }

        #endregion

        #region ble

        // ble <int32>
        // Branch if less or equal.

        /// <summary>
        /// Writes the ble instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void ble(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "ble", label);
        }

        #endregion

        #region blt

        // blt <int32>
        // Branch if less.

        /// <summary>
        /// Writes the blt instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void blt(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "blt", label);
        }

        #endregion

        #endregion

        #region Constant Loading

        #region ldc <int32>

        // ldc.i4 <int32>
        // Load <int32> on the stack.

        /// <summary>
        /// Writes the ldc.i4 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="val">Value to load.</param>
        public void ldci4(int indent, int val) {
            this.WriteLNUnaryCommand(indent, "ldc.i4", val.ToString()); ;
        }

        #endregion

        #region ldc <float64>

        // ldc.r8 <float64>
        // Load <float64> (double-precision) on the stack. ILAsm permits the use
        // of integer parameters in both the ldc.r4 and ldc.r8 instructions; in 
        // such cases, the integers are interpreted as binary images of the 
        // floating-point numbers.

        /// <summary>
        /// Writes the ldc.i8 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="val">Value to load.</param>
        public void ldcr8(int indent, string val) {
            this.WriteLNUnaryCommand(indent, "ldc.r8", val);
        }

        #endregion

        #region ldc <int32>
        // ldc.i4 <int32>
        // Load <int32> on the stack.

        /// <summary>
        /// Writes the ldc instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="val">Value to load if true ldc.</param>
        public void ldc(int indent, bool val) {
            this.WriteLNNoArgsCommand(indent, val ? "ldc.i4.1" : "ldc.i4.0");
        }

        #endregion

        #region ldtoken <type>
        // ldtoken <type>
        // Load <type> on the stack.

        /// <summary>
        /// Writes the ldtoken instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="ILType">Representation of the IL type.</param>
        public void ldtoken(int indent, string ILType) {
            this.WriteLNUnaryCommand(indent, "ldtoken", ILType);
        }

        #endregion

        #endregion


        #region constructorCall

        /// <summary>
        /// Writes the call instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="memberType">Member type.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        public void constructorCall(int indent, MethodType memberType, TypeExpression obj, string member) {
            this.ilStamentsCodeGeneration.Write(indent, "call    instance {0} {1}::{2}", VoidType.Instance.ILType(), obj.ILType(), member);
            this.WriteParams(memberType, null);
            this.ilStamentsCodeGeneration.WriteLine();
        }

        /// <summary>
        /// Writes the call instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="obj">Object to access to the member.</param>
        /// <param name="member">Member to call.</param>
        public void constructorCall(int indent, TypeExpression obj, string member) {
            this.ilStamentsCodeGeneration.WriteLine(indent, "call\tinstance {0} {1}::{2}()", VoidType.Instance.ILType(), obj.ILType(), member);
        }

        #endregion


        #region Conversion Operations

        #region convToInt

        // conv.i4
        // Convert the value to int32.

        /// <summary>
        /// Writes the conv.i4 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void convToInt(int indent) {
            this.WriteLNNoArgsCommand(indent, "conv.i4");
        }

        #endregion

        #region convToChar

        // conv.u2
        // Convert the value to char.

        /// <summary>
        /// Writes the conv.u2 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void convToChar(int indent)
        {
            this.WriteLNNoArgsCommand(indent, "conv.u2");
        }

        #endregion

        #region convToDouble

        // conv.r8
        // Convert the value to float64.

        /// <summary>
        /// Writes the conv.r8 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void convToDouble(int indent) {
            this.WriteLNNoArgsCommand(indent, "conv.r8");
        }

        #endregion

        #endregion

        #region Field instructions

        #region ldfld()

        // ldfld <token>
        // Pop the instance pointer from the stack and load the value of an 
        // instance field on the stack. <token> must be a valid FieldDef or 
        // MemberRef token, uncompressed and uncoded.

        /// <summary>
        /// Writes the ldfld instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Field type.</param>
        /// <param name="classId">Class identifier of the field.</param>
        /// <param name="fieldName">Field identifier.</param>
        /// <example> ldfld NS.Class::field </example>
        public void ldfld(int indent, TypeExpression type, string classId, string fieldName) {
            this.ilStamentsCodeGeneration.WriteIndentation(indent);
            FieldType t = type as FieldType;
            if (t != null)
                type = t.FieldTypeExpression;

            if (type is TypeVariable)
                this.ilStamentsCodeGeneration.WriteLine("ldfld    {0} {1}::{2}", "object", classId, fieldName);
            else if (type is ClassTypeProxy)
                this.ilStamentsCodeGeneration.WriteLine("ldfld    {0} {1}::{2}", ((ClassTypeProxy)type).RealType.ILType(), classId, fieldName);
            else
                this.ilStamentsCodeGeneration.WriteLine("ldfld    {0} {1}::{2}", type.ILType(), classId, fieldName);
        }

        #endregion

        #region ldsfld()

        // ldsfld <token>
        // Load the value of a static field on the stack.

        /// <summary>
        /// Writes the ldsfld instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Field type.</param>
        /// <param name="classId">Class identifier of the field.</param>
        /// <param name="fieldName">Field identifier.</param>
        public void ldsfld(int indent, TypeExpression type, string classId, string fieldName) {
            FieldType t = type as FieldType;
            if (t != null)
                type = t.FieldTypeExpression;
            if (type is TypeVariable)
                this.ilStamentsCodeGeneration.WriteLine(indent, "ldsfld    {0} {1}::{2}", "object", classId, fieldName);
            else if (type is ClassTypeProxy)
                this.ilStamentsCodeGeneration.WriteLine("ldfld    {0} {1}::{2}", ((ClassTypeProxy)type).RealType.ILType(), classId, fieldName);
            else
                this.ilStamentsCodeGeneration.WriteLine(indent, "ldsfld    {0} {1}::{2}", type.ILType(), classId, fieldName);
        }

        #endregion

        #region ldflda()

        /// <summary>
        /// Writes the ldflda instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Field type.</param>
        /// <param name="classId">Class identifier of the field.</param>
        /// <param name="fieldName">Field identifier.</param>
        public void ldflda(int indent, TypeExpression type, string classId, string fieldName) {
            this.ilStamentsCodeGeneration.WriteLine(indent, "ldflda    {0} {1}::{2}", type.ILType(), classId, fieldName);
        }

        /// <summary>
        /// Writes the ldflda instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="token">Full field specification</param>
        public void ldflda(int indent, string token) {
            this.WriteLNUnaryCommand(indent, "ldflda", token);
        }

        #endregion

        #region ldsflda()

        /// <summary>
        /// Writes the ldsflda instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Field type.</param>
        /// <param name="classId">Class identifier of the field.</param>
        /// <param name="fieldName">Field identifier.</param>
        public void ldsflda(int indent, TypeExpression type, string classId, string fieldName) {
            this.ilStamentsCodeGeneration.WriteLine(indent, "ldsflda    {0} {1}::{2}", type.ILType(), classId, fieldName);
        }

        /// <summary>
        /// Writes the ldsflda instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="token">Full field specification</param>
        public void ldsflda(int indent, string token) {
            this.WriteLNUnaryCommand(indent, "ldsflda", token);
        }

        #endregion

        #region stfld()

        // stfld <token>
        // Pop the value from the stack, pop the instance pointer from the stack, 
        // and store the value in the instance field.

        /// <summary>
        /// Writes the stfld instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Field type.</param>
        /// <param name="classId">Class identifier of the field.</param>
        /// <param name="fieldName">Field identifier.</param>
        public void stfld(int indent, TypeExpression type, string classId, string fieldName) {
            FieldType t = type as FieldType;
            if (t != null)
                type = t.FieldTypeExpression;
            if (type is TypeVariable)
                this.ilStamentsCodeGeneration.WriteLine(indent, "stfld    {0} {1}::{2}", "object", classId, fieldName);
            else if(type is ClassTypeProxy)
                this.ilStamentsCodeGeneration.WriteLine(indent, "stfld    {0} {1}::{2}", ((ClassTypeProxy)type).RealType.ILType(), classId, fieldName);
            else
                this.ilStamentsCodeGeneration.WriteLine(indent, "stfld    {0} {1}::{2}", type.ILType(), classId, fieldName);
        }

        #endregion

        #region stsfld()

        // stsfld <token>
        // Store the value from the stack in the static field.

        /// <summary>
        /// Writes the stsfld instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">Field type.</param>
        /// <param name="classId">Class identifier of the field.</param>
        /// <param name="fieldName">Field identifier.</param>
        public void stsfld(int indent, TypeExpression type, string classId, string fieldName) {
            FieldType t = type as FieldType;
            if (t != null)
                type = t.FieldTypeExpression;
            if (type is TypeVariable)
                this.ilStamentsCodeGeneration.WriteLine(indent, "stsfld    {0} {1}::{2}", "object", classId, fieldName);
            else if (type is ClassTypeProxy)
                this.ilStamentsCodeGeneration.WriteLine(indent, "stfld    {0} {1}::{2}", ((ClassTypeProxy)type).RealType.ILType(), classId, fieldName);
            else
                this.ilStamentsCodeGeneration.WriteLine(indent, "stsfld    {0} {1}::{2}", type.ILType(), classId, fieldName);
        }

        #endregion

        #endregion

        #region Flow Control Instructions

        #region ret()

        // ret
        // Return from a method.

        /// <summary>
        /// Writes the ret instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void ret(int indent) {
            this.WriteLNNoArgsCommand(indent, "ret");
        }

        #endregion

        #endregion

        #region Local variable instructions

        #region ldloc()

        // ldloc <unsigned int16>
        // Load the value of local variable number <unsigned int16> on the stack.
        // Local variable numbers can range from 0 to 0xFFFE.

        /// <summary>
        /// Writes the ldloc instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="locNumber">Local variable number to load.</param>
        public void ldloc(int indent, int locNumber) {
            this.WriteLNUnaryCommand(indent, "ldloc", locNumber.ToString());
        }

        /// <summary>
        /// Writes the ldloc instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="locNumber">Local variable to load.</param>
        public void ldloc(int indent, string locVar) {
            this.WriteLNUnaryCommand(indent, "ldloc", locVar);
        }

        #endregion

        #region ldloca()

        // ldloca <unsigned int16>
        // Load the address of local variable number <unsigned int16> on the 
        // stack. The local variable number can vary from 0 to 0xFFFE.

        /// <summary>
        /// Writes the ldloca instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="locNumber">Local variable number to load.</param>
        public void ldloca(int indent, int locNumber) {
            this.WriteLNUnaryCommand(indent, "ldloca", locNumber.ToString());
        }

        /// <summary>
        /// Writes the ldloca instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="locVar">Local variable to load.</param>
        public void ldloca(int indent, string locVar) {
            this.WriteLNUnaryCommand(indent, "ldloca", locVar);
        }
        
        /// <summary>
        ///  loads the content of variable tmpVar in the top of the stack
        /// </summary>
        /// <param name="tmpVar">var to be dumped in the top of the stack</param>
        public void ldloca_s(int indent, string tmpVar) {
            this.WriteLNUnaryCommand(indent, "ldloca.s", tmpVar);
        }
        #endregion
        #region stloc()

        // stloc <unsigned int16>
        // Pop the value from the stack and store it in local variable slot 
        // number <unsigned int16>. The value on the stack must be of the same 
        // type as the argument slot or must be convertible to the type of the 
        // argument slot. The convertibility rules and effects are the same as 
        // those for the conversion operations, discussed earlier in this chapter.

        /// <summary>
        /// Writes the stloc instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="locNumber">Local variable number to store.</param>
        public void stloc(int indent, int locNumber) {
            this.WriteLNNoArgsCommand(indent, "stloc." + locNumber.ToString());
        }

        /// <summary>
        /// Writes the stloc.s instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="variable">variable to store.</param>
        public void stloc_s(int indent, string variable) {
            this.WriteLNUnaryCommand(indent, "stloc.s", variable);
        }


        /// <summary>
        /// Writes the ldloc instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="locVar">Local variable to store.</param>
        public void stloc(int indent, string locVar) {
            this.WriteLNUnaryCommand(indent, "stloc", locVar);
        }

        #endregion

        #endregion

        
        #region Logical Condition Check Operations

        #region ceq

        // ceq
        // Check whether the two values on the stack are equal.

        /// <summary>
        /// Writes the ceq instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void ceq(int indent) {
            this.WriteLNNoArgsCommand(indent, "ceq");
        }

        #endregion

        #region cgt

        // cgt
        // Check whether the first value is greater than the second value. It’s
        // the stack we are working with, so the “second” value is the one on the
        // top of the stack.

        /// <summary>
        /// Writes the cgt instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void cgt(int indent) {
            this.WriteLNNoArgsCommand(indent, "cgt");
        }

        #endregion

        #region clt

        // clt
        // Check whether the first value is less than the second value.

        /// <summary>
        /// Writes the clt instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void clt(int indent) {
            this.WriteLNNoArgsCommand(indent, "clt");
        }

        #endregion

        #region isinst
        // isinst <token>
        // Check to see whether the object reference on the stack is an instance of the class specified by <token>. 

        /// <summary>
        /// Writes the isinst instruction.
        /// About the isinst Instruction.
        ///  It takes two arguments: an object reference and a metadata token representing the type 
        ///  of reference desired. 
        ///  It generates code that examines the object's type handle to determine whether or not the object's type 
        ///  is compatible with the requested type. 
        ///  If the test succeeds, it leaves the object reference on the evaluation stack. 
        ///  If the test fails, puts a null reference onto the evaluation stack if the test fails
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="token">Type of the new instance.</param>
        public void isinst(int indent, TypeExpression token) {
            this.WriteLNUnaryCommand(indent, "isinst", TypeMapping.Instance.GetBCLName(token.ILType(), true));
        }

        #endregion

        #endregion

        #region Shift Operations

        // Shift operations have no parameters and are defined for integer 
        // operands only. The shift operations are binary: they take from the 
        // stack the shift count and the value being shifted, in that order, and 
        // put the shifted value on the stack.

        #region shl

        // shl
        // Shift left.

        /// <summary>
        /// Writes the shl instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void shl(int indent) {
            this.WriteLNNoArgsCommand(indent, "shl");
        }

        #endregion

        #region shr

        // shr
        // Shift right.

        /// <summary>
        /// Writes the shr instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void shr(int indent) {
            this.WriteLNNoArgsCommand(indent, "shr");
        }

        #endregion

        #endregion

        #region Stack manipulation

        #region nop()

        // nop  
        // No operation; a placeholder only. The nop instruction is not exactly a 
        // stack manipulation instruction, since it does not touch the stack. 

        /// <summary>
        /// Writes a nop instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void nop(int indent) {
            this.WriteLNNoArgsCommand(indent, "nop");
        }

        #endregion

        #region dup()

        // dup  
        // Duplicate the value on the top of the stack. If the stack is empty, 
        // the JIT compiler fails because of the stack underflow.

        /// <summary>
        /// Writes a dup instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void dup(int indent) {
            this.WriteLNNoArgsCommand(indent, "dup");
        }

        #endregion

        #region pop()

        // pop  
        // Remove the value from the top of the stack. The value is lost. If the 
        // stack is empty, the JIT compiler fails.

        /// <summary>
        /// Writes a pop instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public void pop(int indent) {
            this.WriteLNNoArgsCommand(indent, "pop");
        }

        #endregion

        #endregion
        
        #region leave
        /// <summary>
        /// Write leave.s label
        /// </summary>
        /// <param name="indentation"></param>
        /// <param name="label">label to write</param>
        public void WriteLeave(int indentation, string label) {
            this.WriteLNUnaryCommand(indentation, "leave", label);
        }

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
        public abstract override void UnboxAny(int indent, TypeExpression type);

        #endregion

        #region Unconditional and Conditional Branching Instructions

        #region br

        // br <int32>

        /// <summary>
        /// Writes the br instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void br(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "br", label);
        }

        #endregion

        #region brfalse

        // brfalse (brnull, brzero) <int32>
        // Branch if <value> is 0.

        /// <summary>
        /// Writes the brfalse instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void brfalse(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "brfalse", label);
        }

        #endregion

        #region brtrue

        // brtrue <int32>
        // Branch if <value> is nonzero.

        /// <summary>
        /// Writes the brtrue instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="label">Label to jump.</param>
        public void brtrue(int indent, string label) {
            this.WriteLNUnaryCommand(indent, "brtrue", label);
        }

        #endregion

        #endregion

        #region VectorInstructions

        #region newarr

        // newarr <token>
        // Create a vector. <token> specifies the type of vector elements.

        /// <summary>
        /// Writes the newarr instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="token">WriteType of the vector elements.</param>
        public virtual void newarr(int indent, TypeExpression token) {
            if(token is TypeVariable && ((TypeVariable)token).Substitution != null && ((TypeVariable)token).Substitution.IsValueType() && !(((TypeVariable)token).Substitution is StringType))            
                this.WriteLNUnaryCommand(indent, "newarr", "object");
            else
                this.WriteLNUnaryCommand(indent, "newarr", token.ILType());
        }
        /// <summary>
        /// Writes the newarr instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// <param name="type">String representation of a type.</param>
        public virtual void newarr(int indent, string type) {
            this.WriteLNUnaryCommand(indent, "newarr", type);
        }

        #endregion

        #region ldlen

        // ldlen
        // Get the element count of a vector.

        /// <summary>
        /// Writes the ldlen instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        /// TODO: para qué sirve el parámetro token?
        public virtual void ldlen(int indent, TypeExpression token) {
            this.WriteLNNoArgsCommand(indent, "ldlen");
        }

        #endregion

        #region ldelem

        // ldelem.i4
        // Load a vector element of type int32.

        /// <summary>
        /// Writes the ldelem.i4 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public virtual void ldelemInt(int indent) {
            this.WriteLNNoArgsCommand(indent, "ldelem.i4");
        }

        /// <summary>
        /// Writes the ldelem.u2 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public virtual void ldelemChar(int indent)
        {
            this.WriteLNNoArgsCommand(indent, "ldelem.u2");
        }

        // ldelem.r8
        // Load a vector element of type float64.

        /// <summary>
        /// Writes the ldelem.r8 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public virtual void ldelemDouble(int indent) {
            this.WriteLNNoArgsCommand(indent, "ldelem.r8");
        }

        // ldelem.ref
        // Load a vector element of object reference type.

        /// <summary>
        /// Writes the ldelem.ref instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public virtual void ldelemRef(int indent) {
            this.WriteLNNoArgsCommand(indent, "ldelem.ref");
        }

        #endregion

        #region stelem

        // stelem.i4
        // Store a value in a vector element of type int32.

        /// <summary>
        /// Writes the stelem.i4 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public virtual void stelemInt(int indent) {
            this.WriteLNNoArgsCommand(indent, "stelem.i4");
        }

        // stelem.r8
        // Store a value in a vector element of type float64.

        /// <summary>
        /// Writes the stelem.r8 instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public virtual void stelemDouble(int indent) {
            this.WriteLNNoArgsCommand(indent, "stelem.r8");
        }

        // stelem.ref
        // Store a value in a vector element of object reference type.

        /// <summary>
        /// Writes the stelem.ref instruction.
        /// </summary>
        /// <param name="indent">Indentation to use.</param>
        public virtual void stelemRef(int indent) {
            this.WriteLNNoArgsCommand(indent, "stelem.ref");
        }

        #endregion

        #endregion

        //helpers

        internal void WriteLNNoArgsCommand(int indentation, string command) {
            ilStamentsCodeGeneration.WriteIndentation(indentation);
            this.WriteLNNoArgsCommand(command);
        }

        
        internal void WriteLNNoArgsCommand(string command) {
            this.ilStamentsCodeGeneration.WriteLine(command);
        }
        public void WriteLine(int indentation, string msg) {
            ilStamentsCodeGeneration.WriteIndentation(indentation);
            ilStamentsCodeGeneration.WriteLine(msg);
        }
        internal void WriteLNUnaryCommand(int indentation, string command, string argument) {
            ilStamentsCodeGeneration.WriteLine(indentation, "{0}\t{1}", command, argument);
        }

        private static bool IsInstance(PropertyType memberType) {
            return memberType == null || (memberType.MemberInfo.ModifierMask & Modifier.Static) == 0;
        }

        private static bool IsInstance(MethodType memberType) {
            return memberType == null || (memberType.MemberInfo.ModifierMask & Modifier.Static) == 0;
        }
        public void WriteLine() {
            this.ilStamentsCodeGeneration.WriteLine();
        }
    }
}
