//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: TypeMapping.cs                                                     
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Offers the mappings between different representations of types in IL.
//    Implements Singleton [Singleton].       
// -------------------------------------------------------------------------- 
// Create date: 21-08-2007                                                    
// Modification date: 21-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;


namespace CodeGeneration {
    /// <summary>
    /// Offers the mappings between different representations of types in IL.
    /// </summary>
    class TypeMapping {

        #region SingletonDesingPattern
        private static TypeMapping instance = new TypeMapping();

        public static TypeMapping Instance {
            get { return instance; }
        }

        private TypeMapping() {
            // * Types that require boxing
            this.requireBoxing = new List<string>();
            this.requireBoxing.Add("bool");
            this.requireBoxing.Add("char");
            this.requireBoxing.Add("int32");
            this.requireBoxing.Add("float64");
            // * Mapping of types
            this.mappings = new Dictionary<string, string>();
            this.mappings["bool"] = "System.Boolean";
            this.mappings["char"] = "System.Char";
            this.mappings["int32"] = "System.Int32";
            this.mappings["float64"] = "System.Double";
            this.mappings["string"] = "System.String";
            this.mappings["object"] = "System.Object";
        }
        #endregion

        #region Fields
        /// <summary>
        /// Types that require boxing to convert into an object.
        /// These types are the ILType method in TypeExpression.
        /// </summary>
        private IList<string> requireBoxing;

        /// <summary>
        /// Key = Built-in type representation
        /// Value = BCL type representation
        /// </summary>
        private IDictionary<string, string> mappings;
        #endregion


        #region RequiresBoxing
        /// <summary>
        /// Tells if an IL type requires boxing to convert into an object
        /// </summary>
        /// <param name="ilType">The name of the IL type</param>
        /// <returns>If it requires boxing</returns>
        public bool RequireBoxing(string ilType) {
            return this.requireBoxing.Contains(ilType);
        }
        #endregion


        /// <summary>
        /// Obtains the BCL representation of an IL type
        /// </summary>
        /// <param name="ilType">The name of the IL type</param>
        /// <param name="includeLibrary">If we want a prefix with the library (mscorlib)</param>
        /// <returns>The returned representation</returns>
        public string GetBCLName(string ilType, bool includeLibrary) {
            if (this.mappings.ContainsKey(ilType))
                // * It is a built-in type
                return includeLibrary ? "[mscorlib]"+this.mappings[ilType] : this.mappings[ilType];
            if (this.mappings.Values.Contains(ilType))
                // * It is a BCL type
                return includeLibrary ? "[mscorlib]" + ilType : ilType;
            // * Another type
            return ilType;
        }

    }
}