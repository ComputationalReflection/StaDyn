////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: AccessModifier.cs                                                    //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Association Class between ClassType and MethodType (or Fields).         //
//    Represents the access modifier infomation about a concrete attribute and   //
// its class.                                                                 //
// -------------------------------------------------------------------------- //
// Create date: 22-10-2006                                                    //
// Modification date: 18-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text; 

using AST;
using TypeSystem.Operations;
//viSto0
namespace TypeSystem
{
   #region Modifier

   /// <summary>
   /// Indicates differents modifiers to use in class (only public, internal or static), fields or methods.
   /// </summary>
   public enum Modifier
   {
      Public = 2,
      Protected = 4,
      Internal = 8,
      Private = 16,
      Static = 32,
      Abstract = 64,
      New = 128,
      Override = 256,
      Virtual = 512,
      AccessLevel = Public | Protected | Private | Internal,
      CanRead = 1024,
      CanWrite = 2048,
   }

   #endregion

   /// <summary>
   /// Association Class between ClassType and MethodType (or Fields).
   /// Represents the access modifier information about a concrete attribute and
   /// its class.
   /// </summary>
   /// //VISTO
   public class AccessModifier : ICloneable
   {
      #region Fields

      /// <summary>
      /// The method belongs to this class type.
      /// </summary>
      private UserType classType;

      /// <summary>
      /// Represents the type of the attribute.
      /// </summary>
      private IMemberType type;

      /// <summary>
      /// Represents the attribute modifiers.
      /// </summary>
      private List<Modifier> modifierList;

      /// <summary>
      /// Stores a mask with the modifier information
      /// </summary>
      private Modifier modifierMask;

      /// <summary>
      /// Name of the attribute
      /// </summary>
      private string id;

      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets the attribute type expression
      /// </summary>
      public TypeExpression Type
      {
         get
         {
            System.Diagnostics.Debug.Assert(type==null || type is IMemberType, "The type must be a TypeExpression");
            return (TypeExpression)type;
         }
         set
         {
            System.Diagnostics.Debug.Assert(value is IMemberType, "The type must implement IMemberType");
            type = (IMemberType)value;
         }
      }

      /// <summary>
      /// Gets or sets the class type reference
      /// </summary>
      public UserType Class
      {
         get { return this.classType; }
         set { this.classType = value; }
      }

      /// <summary>
      /// Gets the attribute name
      /// </summary>
      public string MemberIdentifier
      {
         get { return this.id; }
      }

      /// <summary>
      /// Gets the modifiers of the element
      /// </summary>
      public List<Modifier> Modifiers
      {
         get { return this.modifierList; }
      }

      /// <summary>
      /// Gets the modifier information
      /// </summary>
      public Modifier ModifierMask
      {
         get { return this.modifierMask; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of AccessModifier
      /// </summary>
      /// <param name="mods">List of modifiers.</param>
      /// <param name="idMember">Name of the attribute.</param>
      /// <param name="memberType">WriteType expression associated to the attribute.</param>
      /// <param name="interfaceMember">True if attribute belongs to an interfaz.</param>
      public AccessModifier(List<Modifier> mods, string idMember, IMemberType memberType, bool interfaceMember)
      {
         for (int i = 0; i < mods.Count; i++)
         {
            this.modifierMask |= mods[i];
         }

         if (!interfaceMember)
         {
            if ((this.modifierMask & Modifier.AccessLevel) == 0)
            {
               mods.Add(Modifier.Private);
               this.modifierMask |= Modifier.Private;
            }
         }

         this.modifierList = mods;
         this.type = memberType;
         this.id = idMember;
      }
      #endregion
      
      #region Dispatcher
      //public override object AcceptOperation(TypeSystemOperation op) { return op.AcceptOperation(this); }
      #endregion

      #region hasModifier()
      /// <summary>
      /// To know if a modifier is supported
      /// </summary>
      /// <param name="mod">The modifir</param>
      /// <returns>True if it is implemented</returns>
      public bool hasModifier(Modifier mod)
      {
         if ((this.modifierMask & mod) != 0)
            return true;

         for (int i = 0; i < this.modifierList.Count; i++)
            if ((this.modifierList[i] & mod) != 0)
               return true;
         return false;
      }
      #endregion

      #region Clone()
      public Object Clone()
      {
         return this.MemberwiseClone();
      }
      #endregion
   }
}
