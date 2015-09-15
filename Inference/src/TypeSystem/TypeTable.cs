////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TypeTable.cs                                                         //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Implementation of a table of types.                                     //
//    Implements Singleton pattern.                                           //
// -------------------------------------------------------------------------- //
// Create date: 15-10-2006                                                    //
// Modification date: 06-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;

namespace TypeSystem
{
   /// <summary>
   /// Implementation of a table of types.
   /// </summary>
   /// <remarks>
   /// Implements Singleton pattern
   /// </remarks>
   public class TypeTable
   {
      #region Fields

      /// <summary>
      /// Unique instance of TypeTable.
      /// </summary>
      private static TypeTable instance = new TypeTable();

      /// <summary>
      /// Provides a mapping between a string name and its type expression.
      /// </summary>
      private Dictionary<string, TypeExpression> table;

      /// <summary>
      /// Provides a mapping between a string name and its type.
      /// </summary>
      private Dictionary<string, System.Type> map;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the unique instance of TypeTable
      /// </summary>
      public static TypeTable Instance
      {
         get { return instance; }
      }

      #endregion

      #region Constructors

      /// <summary>
      /// Private constructor of class TypeTable.
      /// </summary>
      private TypeTable()
      {
         this.table = new Dictionary<string, TypeExpression>();
         // Add predefined types
         this.table.Add(IntType.Instance.ToString(), IntType.Instance);
         this.table.Add(DoubleType.Instance.ToString(), DoubleType.Instance);
         this.table.Add(CharType.Instance.ToString(), CharType.Instance);
         this.table.Add(VoidType.Instance.ToString(), VoidType.Instance);
         this.table.Add(StringType.Instance.ToString(), StringType.Instance);
         this.table.Add(BoolType.Instance.ToString(), BoolType.Instance);
         // mapping with mscorlib.dll
         this.table.Add("System.Int32", IntType.Instance);
         this.table.Add("System.Double", DoubleType.Instance);
         this.table.Add("System.Char", CharType.Instance);
         this.table.Add("System.String", StringType.Instance);
         this.table.Add("System.Boolean", BoolType.Instance);
         this.table.Add("System.Void", VoidType.Instance);
         this.table.Add("object", new BCLClassType("System.Object", Type.GetType("System.Object")));

         // Auxiliar mapping
         this.map = new Dictionary<string, Type>(5);
         this.map.Add(IntType.Instance.ToString(), Type.GetType("System.Int32"));
         this.map.Add(DoubleType.Instance.ToString(), Type.GetType("System.Double"));
         this.map.Add(CharType.Instance.ToString(), Type.GetType("System.Char"));
         this.map.Add(StringType.Instance.ToString(), Type.GetType("System.String"));
         this.map.Add(BoolType.Instance.ToString(), Type.GetType("System.Boolean"));
         this.map.Add(VoidType.Instance.ToString(), Type.GetType("System.Void"));
     }
      #endregion

     #region Clear()
     /// <summary>
     ///  Clears the type table (debug purposes)
     /// </summary>
     public void Clear() {
         instance = new TypeTable();
     }
     #endregion

      #region GetType()

      /// <summary>
      /// Gets the type associated to the name specified in the argument.
      /// </summary>
      /// <param name="name">Name of the type.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      /// <returns>Concrete type linked to type expression.</returns>
      public TypeExpression GetType(string name, Location location)
      {
          if (name == null)
              return null;

         // If tmpName exists, return the type
         if (this.table.ContainsKey(name))
            return this.table[name];

         // Try to find the type (class or interface) in the BCL (mscorlib.dll)
         Type type = Type.GetType(name, false);
         if (type != null) {
             TypeExpression te = Introspection.createBCLUserType(name, type, location);
             AddType(name, te, location);
            return te;
         }
          
         return null;
      }

      #endregion

      #region TypeExpressionToType()

      /// <summary>
      /// Gets the type of the type expression.
      /// </summary>
      /// <param name="args">WriteType expression information.</param>
      /// <returns>Returns the type expressions </returns>
      public Type[] TypeExpressionToType(TypeExpression[] typ, Location location)
      {
         Type[] aux = new Type[typ.GetLength(0)];

         for (int i = 0; i < typ.GetLength(0); i++)
         {
            if (this.map.ContainsKey(typ[i].FullName))
               aux[i] = this.map[typ[i].FullName];
            else
            {
               if (typ[i] is IBCLUserType)
                   aux[i] = ((IBCLUserType)typ[i]).TypeInfo;
               else
                  ErrorManager.Instance.NotifyError(new UnknownTypeError(typ[i].FullName, location));
            }
         }
         return aux;
      }

      #endregion

      #region ContainsType()

      /// <summary>
      /// Returns if the type table contains the specified tmpName.
      /// </summary>
      /// <param name="tmpName">type identifier.</param>
      /// <returns>Returns TRUE if the type table contains the tmpName, returns FALSE in other case.</returns>
      public bool ContainsType(string key)
      {
         return this.table.ContainsKey(key);
      }

      #endregion

      #region AddType()

      /// <summary>
      /// Adds a new type into the type table
      /// </summary>
      /// <param name="name">string with the information of type expression.</param>
      /// <param name="type">type associated to the name.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>      
      public void AddType(string name, TypeExpression type, Location location)
      {
         if (!this.table.ContainsKey(name))
            this.table.Add(name, type);
         else
            ErrorManager.Instance.NotifyError(new DefinedTypeError(name, location));
      }

      /// <summary>
      /// Adds a new variable type
      /// </summary>
      /// <param name="type">Variable type to add.</param>
      public void AddVarType(TypeVariable type)
      {
         if (!this.table.ContainsKey(type.FullName))
            this.table.Add(type.FullName, type);
      }

      #endregion

      #region ObtainNewType()

      public static string ObtainNewType(string type)
      {
         if (type.StartsWith("Var("))
         {
            string aux = Convert.ToString(TypeSystem.TypeVariable.NewTypeVariable);
            int index = type.IndexOf(')', 3) + 1;
            type = type.Substring(index, type.Length - index);
            return aux + type;
         }
         else
            return type;
      }

      #endregion

      #region ToString()

      /// <summary>
      /// Dumps the information stores in the table of types.
      /// </summary>
      /// <returns>Returns a string with the information stores in the table.</returns>
      public override string ToString()
      {
         StringBuilder aux = new StringBuilder();
         Dictionary<string, TypeExpression>.KeyCollection keys = this.table.Keys;
         foreach (string key in keys)
         {
            aux.AppendFormat("{0}\t\t\t{1}", key, this.table[key].ToString());
            aux.AppendLine();
         }
         return aux.ToString();
      }

      #endregion
   }
}
