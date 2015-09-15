////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: GetMembers.cs                                                         //
// Authors:  Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents an operation to return all the members in a type.            //
//    Inheritance: Type System Operation.                                    //
//    Implements Double Dispatch Pattern.                               //
// -------------------------------------------------------------------------- //
// Create date: 22-04-2010                                                    //
// Modification date: 22-04-2010                                              //
////////////////////////////////////////////////////////////////////////////////

using TypeSystem;
using AST;
using ErrorManagement;
using System;
using TypeSystem.Constraints;
using DynVarManagement;
using System.Collections.Generic;
using System.Reflection;

namespace TypeSystem.Operations {
    /// <summary>
    /// Represents an operation to return all the members in a type
    /// </summary>
    public class GetMembersOperation : TypeSystemOperation {
        
        #region Fields
        /// <summary>
        /// Singleton objects that are returned by specific methods
        /// </summary>
        static BCLClassType arrayBCLClassType = new BCLClassType("System.Array", Type.GetType("System.Array"));
        static AccessModifier[] emptyAccessModifier = new AccessModifier[]{};
        static BCLClassType stringBCLClassType = new BCLClassType("System.String", Type.GetType("System.String"));
        #endregion
        
        #region Constructor
        public GetMembersOperation() {
        }
        // * We simply check if the index is promotable to an Integer.
        #endregion


        #region TypeExpression
        /// <summary>
        /// Default implementation. Returns an empty array.
        /// </summary>
        /// <param name="typeExpression">The type expression</param>
        /// <param name="arg">Not used (part of the double dispatch design pattern)</param>
        /// <returns>AccessModifier[]</returns>
        public override object Exec(TypeExpression typeExpression, object arg) {
            return emptyAccessModifier;
        }
        #endregion

        #region ArrayType
        public override object Exec(ArrayType arrayType, object arg) {
            return arrayBCLClassType.AcceptOperation(this, null);
        }
        #endregion

        #region StringType
        public override object Exec(StringType stringType, object arg) {
            return stringBCLClassType.AcceptOperation(this, null);
        }
        #endregion

        #region TypeVariable
        public override object Exec(TypeVariable tv, object arg) { // this is a likely array
            TypeExpression subtitution = tv.Substitution;
            if (subtitution == null) 
                return emptyAccessModifier;
            return subtitution.AcceptOperation(this, null);
        }
        #endregion

        #region FieldType
        public override object Exec(FieldType g, object arg) {
            return g.FieldTypeExpression.AcceptOperation(this, null);
        }
        #endregion


        #region UnionType
        public override object Exec(UnionType unionType, object arg) {
            AccessModifier[] allMembers = emptyAccessModifier;
            foreach (TypeExpression type in unionType.TypeSet) {
                AccessModifier[] temp = (AccessModifier[])type.AcceptOperation(this, null);
                allMembers = union(allMembers, temp);
            }
            // * Dynamic => Union of all members
            if (unionType.IsDynamic)
                return allMembers;
            // * Static => Intersection of members
            AccessModifier[] members = allMembers ;
            foreach (TypeExpression type in unionType.TypeSet) {
                AccessModifier[] temp = (AccessModifier[])type.AcceptOperation(this, null);
                members = intersect(members, temp);
            }
            return members;
        }
        private static AccessModifier[] intersect(AccessModifier[] set1, AccessModifier[] set2) {
            List<AccessModifier> list = new List<AccessModifier>();
            foreach (AccessModifier accessModifier in set1)
                if (contains(set2, accessModifier, ContainsStrategy.OnlyName))
                    list.Add(accessModifier);
            AccessModifier[] result = new AccessModifier[list.Count];
            list.CopyTo(result);
            return result;
        }
        private static AccessModifier[] union(AccessModifier[] set1, AccessModifier[] set2) {
            List<AccessModifier> list = new List<AccessModifier>();
            list.AddRange(set1);
            foreach (AccessModifier accessModifier in set2)
                if (!contains(set1, accessModifier, ContainsStrategy.NameAndClass))
                    list.Add(accessModifier);
            AccessModifier[] result = new AccessModifier[list.Count];
            list.CopyTo(result);
            return result;
        }

        private enum ContainsStrategy { OnlyName, NameAndClass };
        private static bool contains(AccessModifier[] set, AccessModifier element, ContainsStrategy strategy) {
            foreach (AccessModifier accessModifier in set)
                if (accessModifier.MemberIdentifier.Equals(element.MemberIdentifier))
                    if (strategy == ContainsStrategy.OnlyName)
                        return true;
                    else // Class and Name
                        if (accessModifier.Class == null || element.Class == null)
                            return true;
                        else
                            // Circle::x is different to Rectangle::x
                            if (accessModifier.Class.Name.Equals(element.Class.Name))
                                return true;
            return false;
        }
        #endregion

        #region ClassType
        public override object Exec(ClassType classType, object arg) {
            AccessModifier[] inheritedMembers = emptyAccessModifier;
            if (classType.BaseClass != null)
                inheritedMembers = (AccessModifier[])classType.BaseClass.AcceptOperation(this, null);
            AccessModifier[] members = new AccessModifier[inheritedMembers.Length + classType.Members.Count];
            Array.Copy(inheritedMembers, 0, members, 0, inheritedMembers.Length);
            classType.Members.Values.CopyTo(members, inheritedMembers.Length);
            return members;
        }
        #endregion

        #region ClassType
        public override object Exec(InterfaceType interfaceType, object arg) {
            AccessModifier[] members = new AccessModifier[interfaceType.Members.Count];
            interfaceType.Members.Values.CopyTo(members, 0);
            foreach (InterfaceType baseType in interfaceType.InterfaceList) {
                AccessModifier[] tempMembers = (AccessModifier[])baseType.AcceptOperation(this, null);
                int previousLength = members.Length;
                Array.Resize(ref members, members.Length + tempMembers.Length);
                Array.Copy(tempMembers, 0, members, previousLength, tempMembers.Length);
            }
            return members;
        }
        #endregion

        #region BCLClassType
        public override object Exec(BCLClassType bclClassType, object arg) {
            // * This operation has an important performance cost. Therefore, it is done locally because
            //   the BCLClassType only returns the type of an specific member (not all of them)
            return this.getAccessModifiers(bclClassType, false);
        }
        private AccessModifier[] getAccessModifiers(IBCLUserType bclUserType, bool isInterface) {
            List<AccessModifier> list = new List<AccessModifier>();
            Type type = Type.GetType(bclUserType.Name);
            MemberInfo[] members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public);
            foreach (MemberInfo member in members) {
                // * Duck typing :-)
                IMemberType memberType = (IMemberType)bclUserType.GetType().GetMethod("FindMember").Invoke(bclUserType, new object[] { member.Name });
                AccessModifier accessModifier = new AccessModifier(Introspection.getMethodModifierList(member),
                    member.Name, memberType, isInterface);
                list.Add(accessModifier);
            }
            AccessModifier[] result = new AccessModifier[list.Count];
            list.CopyTo(result);
            return result;
        }
        #endregion

        #region BCLInterfaceType
        public override object Exec(BCLInterfaceType bclInterfaceType, object arg) {
            // * This operation has an important performance cost. Therefore, it is done locally because
            //   the BCLClassType only returns the type of an specific member (not all of them)
            return this.getAccessModifiers(bclInterfaceType, true);
        }
        #endregion


        
   }

}