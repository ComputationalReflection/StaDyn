            //////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: BCLClassType.cs                                                      
// Authors: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Represents a class that is part of the BCL. We obtain its behavior by
//       using an intropection object.
//    Inheritance: ClassType.                                            
//    Implements Composite pattern [Leaf].                               
//    Implements Adapter pattern [Adapter].                               
// -------------------------------------------------------------------------- 
// Create date: 07-04-2007                                                    
// Modification date: 05-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;
using TypeSystem.Operations;

namespace TypeSystem {
    public class BCLClassType : ClassType, IBCLUserType {
        #region Fields
        /// <summary>
        /// To delegate all the functionalities
        /// </summary>
        private Introspection introspection;

        /// <summary>
        /// Returns the real introspective type
        /// </summary>
        public Type TypeInfo {
            get { return this.introspection.TypeInfo; }
        }

        public TypeExpression FindConstructor(Location location) {
            return introspection.FindConstructor(location);
        }
        
        public TypeExpression FindMember(string memberName) {
            return this.introspection.FindMember(memberName);
        }

        /// <summary>
        /// To know the type equivalence between BCL types and builtin types
        /// </summary>
        public static IDictionary<string, TypeExpression> BCLtoTypeSystemMapping = new Dictionary<string, TypeExpression>();

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that creates the inheritance tree by means of introspection
        /// </summary>
        /// <param name="name">The name of the class</param>
        /// <param name="introspectiveType">The real introspective type</param>
        public BCLClassType(string name, Type introspectiveType)
            :
            base(name) {
            introspection = new Introspection(this, introspectiveType);
            introspection.createBaseClassAndInterfacesTree();
        }

        static BCLClassType() {
            BCLtoTypeSystemMapping["System.Boolean"] = BoolType.Instance;
            BCLtoTypeSystemMapping["System.Char"] = CharType.Instance;
            BCLtoTypeSystemMapping["System.Int32"] = IntType.Instance;
            BCLtoTypeSystemMapping["System.Double"] = DoubleType.Instance;
            BCLtoTypeSystemMapping["System.String"] = StringType.Instance;
            BCLtoTypeSystemMapping["TypeSystem.BoolType"] = BoolType.Instance;
            BCLtoTypeSystemMapping["TypeSystem.CharType"] = CharType.Instance;
            BCLtoTypeSystemMapping["TypeSystem.IntType"] = IntType.Instance;
            BCLtoTypeSystemMapping["TypeSystem.DoubleType"] = DoubleType.Instance;
            BCLtoTypeSystemMapping["TypeSystem.StringType"] = StringType.Instance;
        }
        #endregion

        #region UpdateConstructors()

        /// <summary>
        /// Makes sure the constructors has been loaded
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        /// </summary>
        public void UpdateConstructors(Location location) {
            // * Load the constructors
            if (this.Constructors == null)
                this.introspection.FindConstructor(location);
          }

        #endregion

          // WriteType Inference
        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion
       
        // Code Generation

        #region ILType()

        /// <summary>
        /// Gets the string type to use in IL code.
        /// </summary>
        /// <returns>Returns the string type to use in IL code.</returns>
        public override string ILType()
        {

            if (this.TypeInfo.IsArray && this.TypeInfo.FullName.Contains("Char")) //This hack enables System.String::ToCharArray()
                return this.TypeInfo.Name.ToLower(); // "class System.Char[]" is changed by "char[]"
           StringBuilder aux = new StringBuilder();
           if (!this.TypeInfo.IsValueType)
              aux.Append("class ");
           else
              aux.Append("valuetype ");

           aux.AppendFormat("[mscorlib]{0}", this.fullName);
           return aux.ToString();
        }

        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType()
        {
           return this.TypeInfo.IsValueType;
           //return this.introspection.GetType().IsValueType;
        }

        #endregion

    }
}
