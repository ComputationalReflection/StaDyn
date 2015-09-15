////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Instrospection.cs                                                    //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a type obtained using introspection.                         //
//    Implements Composite pattern [Composite].                               //
//    Implements Adapter pattern [Adaptee].                               
// -------------------------------------------------------------------------- //
// Create date: 31-01-2007                                                    //
// Modification date: 06-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using AST;
using ErrorManagement;

namespace TypeSystem {
    /// <summary>
    /// Represents a type obtained using introspection.
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    class Introspection {
        #region Fields

        /// <summary>
        /// Stores the type of introspectioned class
        /// </summary>
        private Type reflectionType;

        /// <summary>
        /// The user type of the type system
        /// </summary>
        private IBCLUserType userType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type
        /// </summary>
        public Type TypeInfo {
            get { return this.reflectionType; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of Introspection.
        /// </summary>
        /// <param name="userType">The user type of the type system</param>
        /// <param name="type">WriteType name to introspection.</param>
        public Introspection(IBCLUserType userType, Type type) {
            this.reflectionType = type;
            this.userType = userType;
        }

        #endregion

        /// <summary>
        /// Method that encapsulates the creation of a BCL class or interface
        /// </summary>
        /// <param name="name">The name of the user type (class or interface)</param>
        /// <param name="type"></param>
        /// <returns></returns>
        #region createBCLUserType()
        public static TypeExpression createBCLUserType(string name, System.Type type, Location location) {
            if ((type.IsClass) || (type.IsValueType))
                return new BCLClassType(name, type);
            if (type.IsInterface)
                return new BCLInterfaceType(name, type);
            throw new ArgumentException(String.Format("The type '{0}' is not a class, nor an interface.", type));
        }
        #endregion

        #region createMethod
        /// <summary>
        /// Creates a MethodType using the method information.
        /// </summary>
        /// <param name="methods">Methods information.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        private void createMethods(MethodInfo[] methods, Location location) {
            AccessModifier accessModifierInfo;
            MethodType mt;
            foreach (MethodInfo method in methods) {
                mt = new MethodType(TypeTable.Instance.GetType(method.ReturnType.FullName, location));
                ParameterInfo[] parameters = method.GetParameters();
                for (int j = 0; j < parameters.GetLength(0); j++) 
                    if (parameters[j].ParameterType.FullName!=null)
                        mt.AddParameter(TypeTable.Instance.GetType(parameters[j].ParameterType.FullName, location));               

                List<Modifier> mods = getMethodModifierList(method);

                accessModifierInfo = new AccessModifier(mods, method.Name, mt, false);
                mt.MemberInfo = accessModifierInfo;
                accessModifierInfo.Class = (UserType)userType;

                if (userType.Members.ContainsKey(method.Name)) {
                    if (!(userType.Members[method.Name].Type is IntersectionType)) // * It is not an intersection
                        // * An intersection type must be created
                        userType.Members[method.Name].Type = new IntersectionMemberType(userType.Members[method.Name].Type);
                    // * The type is added
                    ((IntersectionType)userType.Members[method.Name].Type).AddType(mt);
                } 
                else
                    userType.AddMember(method.Name, accessModifierInfo);
                accessModifierInfo.Type.BuildTypeExpressionString(TypeExpression.MAX_DEPTH_LEVEL_TYPE_EXPRESSION);
            }
        }

        #endregion

        #region createField()

        /// <summary>
        /// Creates a FieldType using the field information.
        /// </summary>
        /// <param name="field">Field information.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        /// <returns>Returns a FieldType associated to the field information.</returns>
        private FieldType createField(FieldInfo field, Location location) {
            AccessModifier accessModifierInfo;
            FieldType ft = new FieldType(TypeTable.Instance.GetType(field.FieldType.FullName, location));

            List<Modifier> mods = getFieldModifierList(field);

            accessModifierInfo = new AccessModifier(mods, field.Name, ft, false);
            ft.MemberInfo = accessModifierInfo;
            accessModifierInfo.Class = (UserType)userType;

            userType.AddMember(field.Name, accessModifierInfo);

            accessModifierInfo.Type.BuildTypeExpressionString(TypeExpression.MAX_DEPTH_LEVEL_TYPE_EXPRESSION);

            return ft;
        }

        #endregion

        #region createProperty()

        /// <summary>
        /// Creates a PropertyType using the property information.
        /// </summary>
        /// <param name="userType">The type of the implicit object</param>
        /// <param name="prop">Property information.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        /// <returns>Returns a PropertyType associated to the property information.</returns>
        private PropertyType createProperty(PropertyInfo prop, Location location) {
            AccessModifier accessModifierInfo;
            PropertyType pt = new PropertyType(TypeTable.Instance.GetType(prop.PropertyType.FullName, location), prop.CanRead, prop.CanWrite);

            // * The modifiers of a X property are the ones of the get_X method
            MethodInfo methodInfo;
            if (prop.CanRead)
                methodInfo = userType.TypeInfo.GetMethod("get_" + prop.Name);
            else
                methodInfo = userType.TypeInfo.GetMethod("set_" + prop.Name);
            List<Modifier> mods = getMethodModifierList(methodInfo);
            mods = getPropertyModifierList(prop, mods);

            accessModifierInfo = new AccessModifier(mods, prop.Name, pt, false);
            pt.MemberInfo = accessModifierInfo;
            accessModifierInfo.Class = (UserType)userType;

            userType.Members[prop.Name] = accessModifierInfo;
            accessModifierInfo.Type.BuildTypeExpressionString(TypeExpression.MAX_DEPTH_LEVEL_TYPE_EXPRESSION);

            return pt;
        }

        #endregion

        // WriteType inference

        #region FindMember()
        /// <summary>
        /// Finds a attribute of a BCL user type, using introspection.
        /// Inheritance is not taken into account.
        /// In the attribute is not found, an error is generated.
        /// </summary>
        /// <param name="memberName">The name of the attribute.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        /// <returns>WriteType obtained with the operation.</returns>
        public TypeExpression FindMember(string memberName, Location location) {
            TypeExpression member = this.FindMember(memberName);
            if (member == null)
                ErrorManager.Instance.NotifyError(new UnknownMemberError(memberName, location));
            return member;
        }
        /// <summary>
        /// Finds a attribute of a BCL user type, using introspection.
        /// Inheritance is not taken into account.
        /// In the attribute is not found, an error not is generated.
        /// </summary>
        /// <param name="memberName">The name of the attribute.</param>
        /// <returns>WriteType obtained with the operation.</returns>
        public TypeExpression FindMember(string memberName) {
            FieldInfo fieldInfo;
            MethodInfo[] methodsInfo;
            PropertyInfo propertyInfo;
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            // * Has been previously found?
            if (userType.Members.ContainsKey(memberName))
                return userType.Members[memberName].Type;

            // * Retrieve from the BCL by means of introspection
            if ((fieldInfo = reflectionType.GetField(memberName, flags)) != null)
                return createField(fieldInfo, new Location());

            if ((propertyInfo = reflectionType.GetProperty(memberName, flags)) != null)
                return createProperty(propertyInfo, new Location());

            if ((methodsInfo = reflectionType.GetMethods(flags)) != null) { 
                createMethods(methodsInfo, new Location());
                if (!userType.Members.ContainsKey(memberName))
                    // * Not found
                    return null;
                return userType.Members[memberName].Type;
            }

            return null;
        }
        #endregion

        #region FindConstructor
        /// <summary>
        /// Finds and adds the list of constructor to a BCL type
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        /// <returns>WriteType obtained</returns>
        public TypeExpression FindConstructor(Location location) {
            ConstructorInfo[] constructors = userType.TypeInfo.GetConstructors();
            AccessModifier accessModifierInfo;
            MethodType mt;
            foreach (ConstructorInfo constructor in constructors) {
                mt = new MethodType((UserType)userType); // The constructor returns its own class
                ParameterInfo[] parameters = constructor.GetParameters();
                for (int j = 0; j < parameters.GetLength(0); j++)
                    mt.AddParameter(TypeTable.Instance.GetType(parameters[j].ParameterType.FullName, location));

                List<Modifier> mods = getMethodModifierList(constructor);

                accessModifierInfo = new AccessModifier(mods, constructor.Name, mt, false);
                mt.MemberInfo = accessModifierInfo;
                accessModifierInfo.Class = (UserType)userType;

                if (userType.Constructors != null) {
                    if (!(userType.Constructors.Type is IntersectionType)) // * It is not an intersection
                        // * An intersection type must be created
                        userType.Members[userType.Name].Type = new IntersectionMemberType(userType.Members[userType.Name].Type);
                    // * The type is added
                    ((IntersectionType)userType.Members[userType.Name].Type).AddType(mt);
                }
                else
                    userType.AddMember(userType.Name, accessModifierInfo);
                accessModifierInfo.Type.BuildTypeExpressionString(TypeExpression.MAX_DEPTH_LEVEL_TYPE_EXPRESSION);
            }
            if (userType.Members.ContainsKey(userType.Name))
                return userType.Members[userType.Name].Type;
            return null;
        }

        #endregion


        // Helper Methods

        #region GetMethodModifierList()
        /// <summary>
        /// Gets the modifier list from a method info
        /// </summary>
        /// <param name="method">The method info</param>
        /// <returns>The modifiers list</returns>
        public static List<Modifier> getMethodModifierList(MethodBase method) {
            List<Modifier> mods = new List<Modifier>();
            if (method.IsAbstract)
                mods.Add(Modifier.Abstract);
            if (method.IsPrivate)
                mods.Add(Modifier.Private);
            if (method.IsPublic)
                mods.Add(Modifier.Public);
            if (method.IsFamily)
                mods.Add(Modifier.Protected);
            if (method.IsStatic)
                mods.Add(Modifier.Static);
            if (method.IsVirtual)
                mods.Add(Modifier.Virtual);
            return mods;
        }
        public static List<Modifier> getMethodModifierList(MemberInfo member) {
            List<Modifier> mods = new List<Modifier>();
            if (member.DeclaringType.IsAbstract)
                mods.Add(Modifier.Abstract);
            if (member.DeclaringType.IsNotPublic)
                mods.Add(Modifier.Private);
            if (member.DeclaringType.IsPublic)
                mods.Add(Modifier.Public);
            return mods;
        }
        #endregion

        #region GetFieldModifierList()
        /// <summary>
        /// Gets the modifier list forma a fieldinfo
        /// </summary>
        /// <param name="field">The fieldinfo</param>
        /// <returns>The modifier list</returns>
        private static List<Modifier> getFieldModifierList(FieldInfo field) {
            List<Modifier> mods = new List<Modifier>();
            if (field.IsPrivate)
                mods.Add(Modifier.Private);
            if (field.IsPublic)
                mods.Add(Modifier.Public);
            if (field.IsFamily)
                mods.Add(Modifier.Protected);
            if (field.IsStatic)
                mods.Add(Modifier.Static);
            return mods;
        }
        #endregion

        #region GetPropertyModifierList()
        /// <summary>
        /// Gets the modifier list from a fieldinfo
        /// </summary>
        /// <param name="property">The property info</param>
        /// <returns>The modifier list</returns>
        private static List<Modifier> getPropertyModifierList(PropertyInfo property, List<Modifier> mods) {
            if (property.CanRead)
                mods.Add(Modifier.CanRead);
            if (property.CanWrite)
                mods.Add(Modifier.CanWrite);
            return mods;
        }

        #endregion


        #region createBaseClassAndInterfacesTree()
        /// <summary>
        /// Creates, based on introspection, the base class of the user type and the list
        /// of interfaces implemented
        /// </summary>
        public void createBaseClassAndInterfacesTree() {
            // * Creates the base class tree
            System.Type baseClass = this.reflectionType.BaseType;
            if (baseClass != null) {
                BCLClassType BCLBaseClass = new BCLClassType(baseClass.FullName, baseClass);
                userType.AddBaseClass(BCLBaseClass, new Location(null, 0, 0));
            }
            // * Creates the intefaces tree
            Type[] interfaces = this.reflectionType.GetInterfaces();
            foreach (Type interfaze in interfaces) {
                BCLInterfaceType baseInterface = new BCLInterfaceType(interfaze.FullName, interfaze);
                userType.AddBaseInterface(baseInterface, new Location(null, 0, 0));
            }
        }

        #endregion
    }
}
