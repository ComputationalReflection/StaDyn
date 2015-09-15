//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: SSAInfo.cs                                                          
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                   
// Description:                                                              
//    This helper class offers static methods to save and update types 
//       of references.              
// -------------------------------------------------------------------------- 
// Create date: 08-06-2007                                                    
// Modification date: 08-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using AST;
using TypeSystem;
using Tools;
using TypeSystem.Constraints;
using ErrorManagement;
using TypeSystem.Operations;

namespace Semantic.SSAAlgorithm {
    /// <summary>
    /// This class stores the information to use in SSA algorithm
    /// </summary>
    static class SSAHelper {

        #region SimplifyReferencesUsedInBodies()
        /// <summary>
        /// Simplifies the set of used references in a body
        /// <param name="references">The set of references to be simplied</param>
        /// </summary>
        public static void SimplifyReferences(IList<SingleIdentifierExpression> references) {
            for (int i = references.Count - 1; i >= 0; i--) {
                // * If it is a class or namespace, we can erase it
                if (references[i].ExpressionType is ClassType || references[i].ExpressionType is NameSpaceType) {
                    references.RemoveAt(i);
                    continue;
                }
                TypeExpression symbolType;
                if (references[i].IdSymbol!=null && (symbolType = references[i].IdSymbol.SymbolType) != null)
                    // * We can erase it if...
                    if (!symbolType.HasTypeVariables() || // it doesn't have free variables
                          symbolType is FieldType || // it is a field
                          symbolType is MethodType || // it is a method
                          symbolType is ArrayType || // it is an array
                          symbolType is PropertyType  // it is a property
                        )
                        references.RemoveAt(i);
            }
        }
        #endregion

        #region CloneTypesOfReferences()
        /// <summary>
        /// To clone the types of all the used IDs detected in the SSA algorithm
        /// </summary>
        /// <param name="references">The references used in the block.</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="typeExpressions">The cloned type expressions</param>
        public static void CloneTypesOfReferences(IList<SingleIdentifierExpression> references, MethodType methodAnalyzed, IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressions) {
            IDictionary<int, TypeVariable> clonedTypeVariables = new Dictionary<int, TypeVariable>();
            IList<EquivalenceClass> equivalenceClasses = new List<EquivalenceClass>();

            // * Clones each type
            foreach (SingleIdentifierExpression singleId in references)
                if (!typeExpressions.ContainsKey(singleId)) // * Previously cloned
                    if (singleId.IdSymbol == null || singleId.IdSymbol.SymbolType == null)
                        typeExpressions[singleId] = null;
                    else
                        typeExpressions[singleId] = singleId.IdSymbol.SymbolType.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);

            // * Clones and updates the equivalence classes
            CloneEquivalenceClasses(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
        }
        public static void CloneTypesOfReferences(IDictionary<SingleIdentifierExpression, TypeExpression> references, MethodType methodAnalyzed, IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressions) {
            IList<SingleIdentifierExpression> list = new List<SingleIdentifierExpression>();
            foreach (KeyValuePair<SingleIdentifierExpression, TypeExpression> pair in references) {
                pair.Key.IdSymbol.SymbolType = pair.Value;
                list.Add(pair.Key);
            }
            CloneTypesOfReferences(list, methodAnalyzed, typeExpressions);
        }
        #endregion

        #region CloneEquivalenceClasses()
        /// <summary>
        /// To clone the equivalence classes of a previously cloned type variables
        /// </summary>
        /// <param name="references">The references used in the block.</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        private static void CloneEquivalenceClasses(IDictionary<int, TypeVariable> clonedTypeVariables, IList<EquivalenceClass> equivalenceClasses, MethodType methodAnalyzed) {
            // * Clones and updates the equivalence classes
            foreach (EquivalenceClass oldEquivalenceClass in equivalenceClasses) {
                EquivalenceClass newEquivalenceClass = new EquivalenceClass();
                // * Clones the substitution
                TypeExpression newSubstitution = null;
                if (oldEquivalenceClass.Substitution != null)
                    newSubstitution = oldEquivalenceClass.Substitution.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
                // * Updates the substitution to the cloned one
                newEquivalenceClass.add(newSubstitution, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>());
                foreach (int typeVariableNumber in oldEquivalenceClass.TypeVariables.Keys) {
                    // * Adds the cloned type variable to the new equivalence class
                    TypeVariable newTypeVariable = clonedTypeVariables[typeVariableNumber];
                    newEquivalenceClass.TypeVariables[newTypeVariable.Variable] = newTypeVariable;
                    // * Sets the class equivalence of the cloned type variable
                    newTypeVariable.EquivalenceClass = newEquivalenceClass;
                    clonedTypeVariables[typeVariableNumber].ValidTypeExpression = false;
                }
            }
        }

        #endregion
        #region CloneType()
        /// <summary>
        /// To clone one simple clone type in the SSA algorithm
        /// </summary>
        /// <param name="typeExpression">The type to be cloned</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <returns>The cloned type</returns>
        public static TypeExpression CloneType(TypeExpression typeExpression, MethodType methodAnalyzed) {
            IDictionary<int, TypeVariable> clonedTypeVariables = new Dictionary<int, TypeVariable>();
            IList<EquivalenceClass> equivalenceClasses = new List<EquivalenceClass>();
            TypeExpression newTypeExpression = typeExpression.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
            CloneEquivalenceClasses(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
            return newTypeExpression;
        }
        #endregion

        #region GetTypesOfReferences()
        /// <summary>
        /// Gets the type expressions of the references obtained in the SSA algorithm
        /// </summary>
        /// <param name="references">The references used in a block.</param>
        /// <returns>A mapping container of tmpName=symbol value=type</returns>
        public static IDictionary<SingleIdentifierExpression, TypeExpression> GetTypesOfReferences(IList<SingleIdentifierExpression> references) {
            IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressions = new Dictionary<SingleIdentifierExpression, TypeExpression>();
            foreach (SingleIdentifierExpression singleId in references)
                if (singleId.IdSymbol!=null)
                    typeExpressions[singleId] = singleId.IdSymbol.SymbolType;
            return typeExpressions;
        }
        #endregion


        /// <summary>
        /// Calculates the intersection of two sets.
        /// Used to know which references are used in the else branch body, but not in the if branch body
        /// </summary>
        /// <param name="setA">One set</param>
        /// <param name="setB">Another set</param>
        /// <returns>The intersection set</returns>
        public static IDictionary<SingleIdentifierExpression, TypeExpression> Intersection(IDictionary<SingleIdentifierExpression, TypeExpression> setA, IList<SingleIdentifierExpression> setB) {
            IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionIntersectionSet = new Dictionary<SingleIdentifierExpression, TypeExpression>();
            foreach (KeyValuePair<SingleIdentifierExpression, TypeExpression> pair in setA)
                if (setB.Contains(pair.Key))
                    typeExpressionIntersectionSet[pair.Key] = pair.Value;
            return typeExpressionIntersectionSet;
        }


        #region SetTypesOfReferences()
        /// <summary>
        /// Updates the type of every symbol in the referencesUsedInTrueBranch with the mapped type expression hold by the references parameter
        /// </summary>
        /// <param name="source">The references used to be copy.</param>
        /// <param name="destination">The sybols wher the referecence types are to be cloned.</param>
        /// <param name="references">Maps a symbol with the new type we want it to have</param>
        public static void SetTypesOfReferences(IDictionary<SingleIdentifierExpression, TypeExpression> source, IList<SingleIdentifierExpression> destination) {
            foreach (SingleIdentifierExpression singleId in destination)
                if (source.ContainsKey(singleId))
                    singleId.IdSymbol.SymbolType = source[singleId];
        }
        #endregion

        #region SetUnionTypesOfReferences()
        /// <summary>
        /// Updates the type of every symbol, following the following algorithm used in the SSA proccess of the if else statement:
        /// 1.- If the same symbol exists in both branches, its type is the union of both "after" parameters
        /// 2.- If the same symbol exists in only one branch, its type is the union of "after" and "before" types
        /// </summary>
        /// <param name="referencesBeforeIfElse">Cloned types of references in the both branches body</param>
        /// <param name="referencesAfterIf">Final types of the references in the true branch body</param>
        /// <param name="referencesAfterElse">Final types of the references in the true branch body</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="actualImplicitObject">The actual implicit object</param>
        public static void SetUnionTypesOfReferences(IDictionary<SingleIdentifierExpression, TypeExpression> referencesBeforeIfElse,
                                                     IDictionary<SingleIdentifierExpression, TypeExpression> referencesAfterIf,
                                                     IDictionary<SingleIdentifierExpression, TypeExpression> referencesAfterElse,
                                                     MethodType methodAnalyzed, TypeExpression actualImplicitObject) {
            IList<SingleIdentifierExpression> alreadyVisited = new List<SingleIdentifierExpression>();
            foreach (KeyValuePair<SingleIdentifierExpression, TypeExpression> pair in referencesAfterIf) {
                alreadyVisited.Add(pair.Key);
                if (referencesAfterElse.ContainsKey(pair.Key))
                    // * Option 1
                    pair.Key.IdSymbol.SymbolType = UnionType.collect(pair.Value, referencesAfterElse[pair.Key]);
                //pair.Key.IdSymbol.SymbolType.Assignment(UnionType.collect(pair.Value, referencesAfterElse[pair.Key]), AssignmentOperator.Assign, null, SortOfUnification.Override, null, "", 0, 0);
                // * Option 2
                pair.Key.IdSymbol.SymbolType = UnionType.collect(pair.Value, referencesBeforeIfElse[pair.Key]);
                //pair.Key.IdSymbol.SymbolType.Assignment(UnionType.collect(pair.Value, referencesBeforeIfElse[pair.Key]), AssignmentOperator.Assign, null, SortOfUnification.Override, null, "", 0, 0);
            }
            foreach (KeyValuePair<SingleIdentifierExpression, TypeExpression> pair in referencesAfterElse) {
                if (alreadyVisited.Contains(pair.Key))
                    continue;
                alreadyVisited.Add(pair.Key);
                // * Option 2
                pair.Key.IdSymbol.SymbolType = UnionType.collect(pair.Value, referencesBeforeIfElse[pair.Key]);
                //pair.Key.IdSymbol.SymbolType.Assignment(UnionType.collect(pair.Value, referencesBeforeIfElse[pair.Key]), AssignmentOperator.Assign, null, SortOfUnification.Override, null, "", 0, 0);
            }
        }
        #endregion


        /// <summary>
        /// Updates the type of every symbol, following the following algorithm used in the SSA proccess of the switch statement:
        /// The new type of each symbols is a union of its types in the typeExpressionsAfterCases container.
        /// Additionaly, if the symbol does not exist in the intersection set, the type in typeExpressionsBeforeSwitch is also added to the union.
        /// </summary>
        /// <param name="typeExpressionsBeforeSwitch">Types of all the references before the switch</param>
        /// <param name="typeExpressionsAfterCases">Types of the references after each switch</param>
        /// <param name="typeExpressionIntersectionSet">References that are used in all the cases (including default)</param>
        /// <param name="methodAnalyzed">The method being analyzed</param>
        /// <param name="actualImplicitObject">The actual implicit object</param>
        public static void SetUnionTypesOfReferences(IDictionary<Block, IDictionary<SingleIdentifierExpression, TypeExpression>> typeExpressionsBeforeSwitch,
                                                     IDictionary<Block, IDictionary<SingleIdentifierExpression, TypeExpression>> typeExpressionsAfterCases,
                                                     IDictionary<SingleIdentifierExpression, TypeExpression> typeExpressionIntersectionSet,
                                                     MethodType methodAnalyzed, TypeExpression actualImplicitObject) {
            // * We set all types to null
            foreach (KeyValuePair<Block, IDictionary<SingleIdentifierExpression, TypeExpression>> pair1 in typeExpressionsBeforeSwitch)
                foreach (KeyValuePair<SingleIdentifierExpression, TypeExpression> pair2 in pair1.Value)
                    pair2.Key.IdSymbol.SymbolType = null;

            // * We set the union types of all the references in a case body
            foreach (KeyValuePair<Block, IDictionary<SingleIdentifierExpression, TypeExpression>> pair1 in typeExpressionsAfterCases)
                foreach (KeyValuePair<SingleIdentifierExpression, TypeExpression> pair2 in pair1.Value)
                    pair2.Key.IdSymbol.SymbolType = UnionType.collect(pair2.Key.IdSymbol.SymbolType, pair2.Value);

            // * We add the initial type to all those symbols that were not used in all the cases
            foreach (KeyValuePair<Block, IDictionary<SingleIdentifierExpression, TypeExpression>> pair1 in typeExpressionsBeforeSwitch)
                foreach (KeyValuePair<SingleIdentifierExpression, TypeExpression> pair2 in pair1.Value)
                    if (typeExpressionIntersectionSet == null || !typeExpressionIntersectionSet.ContainsKey(pair2.Key))
                        pair2.Key.IdSymbol.SymbolType = UnionType.collect(pair2.Key.IdSymbol.SymbolType, pair2.Value);
        }

        #region AssignAttributes()
        /// Assigns all the attributes in many this references, generating the appropriate constraints.
        /// </summary>
        /// <param name="classType">The type of this</param>
        /// <param name="typesOfThisAfterCases">The types of this after each case</param>
        /// <param name="methodAnalyzed">Method being analyzed</param>
        /// <param name="unification">WriteType of unification</param>
        /// <param name="actualImplicitObject">The actual implicit object</param>
        public static void AssignAttributes(UserType classType, IList<TypeExpression> typesOfThisAfterCases,
                        MethodType methodAnalyzed, SortOfUnification unification, TypeExpression actualImplicitObject,
                        Location location) {
            if (methodAnalyzed == null)
                return;
            foreach (KeyValuePair<string, AccessModifier> pair in classType.Fields) {
                TypeExpression fieldType = pair.Value.Type;
                TypeExpression unionType = null;
                foreach (TypeExpression typeOfThis in typesOfThisAfterCases) {
                    TypeExpression eachFieldType = getFieldType(typeOfThis, pair.Key);
                    unionType = UnionType.collect(unionType, eachFieldType);
                }
                fieldType.AcceptOperation(new AssignmentOperation(unionType, AssignmentOperator.Assign, methodAnalyzed, unification, actualImplicitObject, location), null);
            }
        }
        /// <summary>
        /// Assigns all the attributes in two this references, generating the appropriate constraints.
        /// </summary>
        /// <param name="classType">The type of this</param>
        /// <param name="typeOfThis1">The first value of this' type</param>
        /// <param name="typeOfThis2">The second value of this' type</param>
        /// <param name="methodAnalyzed">Method being analyzed</param>
        /// <param name="unification">WriteType of unification</param>
        /// <param name="actualImplicitObject">The actual implicit object</param>
        /// <summary>
        public static void AssignAttributes(UserType classType, TypeExpression typeOfThis1, TypeExpression typeOfThis2,
                        MethodType methodAnalyzed, SortOfUnification unification, TypeExpression actualImplicitObject,
                        Location location) {
            if (methodAnalyzed == null)
                return;
            foreach (KeyValuePair<string, AccessModifier> pair in classType.Fields) {
                TypeExpression fieldType = pair.Value.Type,
                    fieldType1 = getFieldType(typeOfThis1, pair.Key),
                    fieldType2 = getFieldType(typeOfThis2, pair.Key);
                TypeExpression unionType = UnionType.collect(fieldType1, fieldType2);
                fieldType.AcceptOperation(new AssignmentOperation(unionType, AssignmentOperator.Assign, methodAnalyzed, unification, actualImplicitObject, location), null);
            }
        }
        private static TypeExpression getFieldType(TypeExpression type, string fieldName) {
            ClassType classType = TypeExpression.As<ClassType>(type);
            if (classType != null) {
                if (!classType.Fields.ContainsKey(fieldName))
                    return null;
                TypeExpression field= classType.Fields[fieldName].Type;
                FieldType fieldType = TypeExpression.As<FieldType>(field);
                if (fieldType == null)
                    return field;
                return fieldType.FieldTypeExpression;
            }
            UnionType unionType = TypeExpression.As<UnionType>(type);
            if (unionType != null) {
                UnionType newUnionType = new UnionType();
                foreach (TypeExpression typeInUnion in unionType.TypeSet) {
                    TypeExpression newType = SSAHelper.getFieldType(typeInUnion, fieldName);
                    newUnionType.AddType(newType);
                }
                return newUnionType;
            }
            return null;
        }

        #endregion
    }
}
