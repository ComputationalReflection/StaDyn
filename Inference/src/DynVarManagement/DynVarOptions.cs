//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: DynVarOptions.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Offers the global options of the dynamic behaviour configuration.                                  
//    Implements Singleton pattern [Singleton]. 
// -------------------------------------------------------------------------- 
// Create date: 22-06-2007                                                    
// Modification date: 22-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using TypeSystem;

namespace DynVarManagement {
    /// <summary>
    /// Offers the global options of the dynamic behaviour configuration.
    /// </summary>
    public class DynVarOptions {
        #region Fields
        /// <summary>
        /// When true, ignores all the dyn files, setting all the references to dynamic.
        /// </summary>
        private bool everythingDynamic;

        /// <summary>
        /// When true, ignores all the dyn files, setting all the references to dynamic.
        /// </summary>
        private bool everythingStatic;
        #endregion

        #region Properties
        /// <summary>
        /// To ignore all the dyn files, setting all the references to dynamic.
        /// </summary>
        public bool EverythingDynamic {
            get { return this.everythingDynamic; }
            set { this.everythingDynamic = value; }
        }

        /// <summary>
        /// To ignore all the dyn files, setting all the references to dynamic.
        /// </summary>
        public bool EverythingStatic {
            get { return this.everythingStatic; }
            set { this.everythingStatic = value; }
        }
        #endregion

        #region SingletonDesignPattern
        /// <summary>
        /// Private constructor (Singleton)
        /// </summary>
        private DynVarOptions() {
        }

        /// <summary>
        /// Private static instance (Singleton)
        /// </summary>
        private static DynVarOptions instance;

        /// <summary>
        /// Unique class instance (Singleton)
        /// </summary>
        public static DynVarOptions Instance {
            get {
                if (instance == null)
                    instance = new DynVarOptions();
                return instance;
            }
        }
        #endregion

        #region SetDynamic
        /// <summary>
        /// To assign the IsDynamicProperty
        /// </summary>
        /// <param name="typeExpression1">First type expression</param>
        /// <param name="typeExpression2">Second type expression</param>
        public void AssignDynamism(TypeExpression typeExpression1, TypeExpression typeExpression2)
        {
           if (this.EverythingDynamic)
           {
              typeExpression1.IsDynamic = typeExpression2.IsDynamic = true;
              return;
           }
           if (this.EverythingStatic)
           {
              typeExpression1.IsDynamic = typeExpression2.IsDynamic = false;
              return;
           }
           typeExpression1.IsDynamic = typeExpression2.IsDynamic = typeExpression1.IsDynamic | typeExpression2.IsDynamic;
        }
        public void AssignDynamism(TypeExpression typeExpression, bool isDynamic)
        {
           if (this.EverythingDynamic)
           {
              typeExpression.IsDynamic = true;
              return;
           }
           if (this.EverythingStatic)
           {
              typeExpression.IsDynamic = false;
              return;
           }
           typeExpression.IsDynamic = isDynamic;
        }
        
        #endregion
    }
}
