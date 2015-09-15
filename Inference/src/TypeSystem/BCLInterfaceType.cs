//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: BCLInterfaceType.cs                                                      
// Authors: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Represents an interface that is part of the BCL. We obtain its 
//       behavior by using an intropection object.
//    Inheritance: ClassType.                                            
//    Implements Composite pattern [Leaf].                               
//    Implements Adapter pattern [Adapter].                               
// -------------------------------------------------------------------------- 
// Create date: 07-04-2007                                                    
// Modification date: 07-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using TypeSystem.Operations;
//vISTO
namespace TypeSystem
{
   public class BCLInterfaceType : InterfaceType, IBCLUserType
   {
      #region Fields
      /// <summary>
      /// To delegate all the functionalities
      /// </summary>
      private Introspection introspection;

      #endregion


      #region Constructors
      /// <summary>
      /// Constructor that creates the inheritance tree by means of introspection
      /// </summary>
      /// <param name="name">The name of the class</param>
      /// <param name="introspectiveType">The real introspective type</param>
      public BCLInterfaceType(string name, Type introspectiveType)
         :
          base(name)
      {
         introspection = new Introspection(this, introspectiveType);
         introspection.createBaseClassAndInterfacesTree();
      }
      #endregion

      #region Properties
      /// <summary>
      /// Returns the real introspective type
      /// </summary>
      public Type TypeInfo
      {
         get { return this.introspection.TypeInfo; }
      }

      #endregion


      public TypeExpression FindMember(string memberName) {
          return this.introspection.FindMember(memberName);
      }
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
         StringBuilder aux = new StringBuilder();
         aux.AppendFormat("class [mscorlib]{0}", this.fullName);
         return aux.ToString();
      }

      #endregion


   }
}
