using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using AST;
using DynVarManagement;
using ErrorManagement;
using Semantic;
using Tools;
using TypeSystem;

namespace CodeGeneration
{
    class VisitorSpelializer: VisitorAdapter
    {
        private VisitorTypeInference visitorTypeInference;        
        private Dictionary<String, MethodDefinition> specilizedMethods;
        private IList<Pair<TypeExpression, TypeExpression>> previouslyUnified;
        private bool isCurrentMethodDynamic;

        public VisitorSpelializer(VisitorTypeInference visitorTypeInference)
        {
            this.visitorTypeInference = visitorTypeInference;            
            specilizedMethods = new Dictionary<string, MethodDefinition>();
            previouslyUnified = new List<Pair<TypeExpression, TypeExpression>>();
        }

        public override Object Visit(ClassDefinition node, Object obj)
        {
            int originalMemberCount = node.MemberCount;
            for (int i = 0; i < originalMemberCount ; i++)
                node.GetMemberElement(i).Accept(this, obj);
            return null;
        }
        
        public override Object Visit(InvocationExpression node, Object obj)
        {            
            MethodType originalMemberTypeExpression = node.ActualMethodCalled as MethodType;
            if (node.Arguments.ExpressionCount == 0 || originalMemberTypeExpression == null || !originalMemberTypeExpression.HasTypeVariables())
                return null;

            MethodDefinition originalMethodDefinition = originalMemberTypeExpression.ASTNode as MethodDefinition;
            if (originalMethodDefinition == null)
                return null;
            TypeExpression[] originalParamsType = new TypeExpression[originalMemberTypeExpression.ParameterListCount];
            for (int i = 0 ; i < originalMemberTypeExpression.ParameterListCount ; i++)
                originalParamsType[i] = originalMemberTypeExpression.GetParameter(i);
            var originalMethodIndentificator = MethodIndentificator(originalMethodDefinition.FullName, originalParamsType,true);
            if (specilizedMethods.ContainsKey(originalMethodIndentificator))
                return specilizedMethods[originalMethodIndentificator];
            TypeExpression[] args = this.compoundExpressionToArray(node.Arguments);
            var methodIndentificator = MethodIndentificator(originalMethodDefinition.FullName, args);

            if (methodIndentificator.Equals(originalMethodIndentificator)) //Method does not need to be specialized
            {
                specilizedMethods[originalMethodIndentificator] = originalMethodDefinition;
                return false;
            }

            bool specializeOrCreate = !HasUnionTypes(originalMethodDefinition.FullName, methodIndentificator);
            MethodDefinition method =  specializeOrCreate ? SpecilizeMethod(methodIndentificator, originalMethodDefinition, args) : CreateMethod(methodIndentificator, originalMethodDefinition, args,node);
            node.ActualMethodCalled = method.TypeExpr;
            method.IdentifierExp.ExpressionType = method.TypeExpr;
            TypeExpression returnTypeExpresion = ((MethodType) method.TypeExpr).Return;
                
            if (!specializeOrCreate && node.ExpressionType is TypeVariable) //TODO: FieldAcces, UnionType, etc.
                ((TypeVariable) node.ExpressionType).EquivalenceClass.add(returnTypeExpresion,SortOfUnification.Equivalent, previouslyUnified);
            
            node.ExpressionType = returnTypeExpresion;
            
            if (node.Identifier is FieldAccessExpression)
            {
                FieldAccessExpression fae = new FieldAccessExpression(((FieldAccessExpression) node.Identifier).Expression,method.IdentifierExp, node.Location);
                fae.FieldName.IndexOfSSA = -1;
                fae.ExpressionType = method.TypeExpr;
                node.Identifier = fae;
            }
            else
                node.Identifier = method.IdentifierExp;
            return null;            
        }

        #region compoundExpressionToArray()        
        public TypeExpression[] compoundExpressionToArray(CompoundExpression args) {
            TypeExpression[] aux = new TypeExpression[args.ExpressionCount];
            TypeExpression te;

            for (int i = 0; i < args.ExpressionCount; i++) 
            {
                if ((te = args.GetExpressionElement(i).ILTypeExpression) != null)
                {
                    if (te is FieldType)
                    {
                        te = ((FieldType) te).FieldTypeExpression;
                    }
                    aux[i] = te;
                }
                else
                    return null;
            }
            return aux;
        }
        #endregion
        
        private MethodDefinition CreateMethod(string fullMethodIndentificator, MethodDefinition originalMethodDefinition, TypeExpression[] args, InvocationExpression node)
        {
            if (!specilizedMethods.ContainsKey(fullMethodIndentificator))
            {
                String methodIndentificator = fullMethodIndentificator.Replace(originalMethodDefinition.FullName, originalMethodDefinition.Identifier);
                List<MethodDefinition> methods = new List<MethodDefinition>();                
                foreach (TypeExpression[] listOfArgs in GetTypes(args))
                {
                    methods.Add(SpecilizeMethod(MethodIndentificator(originalMethodDefinition.FullName, listOfArgs),originalMethodDefinition, listOfArgs));
                }

                MethodType originalMethodType = (MethodType) originalMethodDefinition.TypeExpr;
                isCurrentMethodDynamic = IsCurrentMethodDynamic(originalMethodType);
                MethodType newMethodType = new MethodType(originalMethodType.Return);
                Location location = originalMethodDefinition.IdentifierExp.Location;                
                SingleIdentifierExpression newSingleIdentifierExpression = new SingleIdentifierExpression(methodIndentificator, location);
                Block newBlock = new Block(location);

                IList<InvocationExpression> invocations = new List<InvocationExpression>();
                IList<CastExpression> castExpressions = new List<CastExpression>();
                int methodsCount = 0;       
                foreach (var methodDefinition in methods)
                {                   
                    MethodType methodType = (MethodType)methodDefinition.TypeExpr;
                    IList<IsExpression> isExpressions = new List<IsExpression>();
                    CompoundExpression compoundExpression = new CompoundExpression(location);

                    for (int i = 0 ; i < methodDefinition.ParametersInfo.Count ; i++)
                    {
                        SingleIdentifierExpression identifier = new SingleIdentifierExpression(methodDefinition.ParametersInfo[i].Identifier, location);
                        identifier.IndexOfSSA = 0;
                        if (args[i] is UnionType || (args[i] is TypeVariable && ((TypeVariable)args[i]).Substitution is UnionType))
                        {
                            IsExpression isExpression = new IsExpression(identifier,
                                methodType.GetParameter(i).Freeze().ILType(), location);
                            isExpression.TypeExpr = methodType.GetParameter(i).Freeze();
                            isExpressions.Add(isExpression);

                            CastExpression castExpression = new CastExpression(isExpression.TypeExpr.ILType(),
                                identifier, location);
                            castExpressions.Add(castExpression);
                            castExpression.CastType = isExpression.TypeExpr;
                            compoundExpression.AddExpression(castExpression);
                        }
                        else
                        {
                            compoundExpression.AddExpression(identifier);
                        }
                    }

                    Expression condition = isExpressions[0];
                    if (isExpressions.Count > 1)
                        for (int i = 1 ; i < isExpressions.Count; i++)
                            condition = new LogicalExpression(condition, isExpressions[i], LogicalOperator.And, location);                   

                    InvocationExpression invocationExpression = new InvocationExpression(methodDefinition.IdentifierExp,compoundExpression,location);
                    invocations.Add(invocationExpression);
                    ReturnStatement returnStatement = new ReturnStatement(invocationExpression,location);
                    if (++methodsCount < methods.Count || isCurrentMethodDynamic)
                    {
                        IfElseStatement ifElseStatement = new IfElseStatement(condition, returnStatement, location);
                        newBlock.AddStatement(ifElseStatement);
                    }
                    else if (!isCurrentMethodDynamic)
                    {
                        newBlock.AddStatement(returnStatement);
                    }                   
                }
                //If there is any dynamic union type then it is necessary invoke the original method
                if (isCurrentMethodDynamic)
                {
                    CompoundExpression compoundExpression = new CompoundExpression(location);
                    foreach (var parameter in originalMethodDefinition.ParametersInfo)
                    {
                        SingleIdentifierExpression identifier = new SingleIdentifierExpression(parameter.Identifier, location);
                        identifier.IndexOfSSA = 0;
                        compoundExpression.AddExpression(identifier);
                    }                 
                    newBlock.AddStatement(new ReturnStatement(new InvocationExpression(new SingleIdentifierExpression(originalMethodDefinition.Identifier, location), compoundExpression, location), location));
                }
               
                MethodDefinition newMethodDefinition = new MethodDefinition(newSingleIdentifierExpression, newBlock,
                    originalMethodDefinition.ReturnTypeInfo, originalMethodDefinition.ParametersInfo,
                    originalMethodDefinition.ModifiersInfo, location);

                newMethodDefinition.FullName = fullMethodIndentificator;
                newMethodType.MemberInfo = new AccessModifier(originalMethodType.MemberInfo.Modifiers,newSingleIdentifierExpression.Identifier, newMethodType, false);
                newMethodType.MemberInfo.Class = originalMethodType.MemberInfo.Class;
                newMethodType.MemberInfo.TypeDefinition = originalMethodType.MemberInfo.TypeDefinition;

                for (int i = 0; i < originalMethodType.ParameterListCount; i++)                    
                    newMethodType.AddParameter(args[i]);
                
                newMethodType.ASTNode = newMethodDefinition;
                newMethodDefinition.TypeExpr = newMethodType;

                TypeDefinition originalTypeDefinition = newMethodType.MemberInfo.TypeDefinition;
                originalTypeDefinition.AddMethod(newMethodDefinition);
                UserType originalClass = newMethodType.MemberInfo.Class;

                originalClass.AddMember(methodIndentificator, newMethodType.MemberInfo);
                newMethodDefinition.Accept(new VisitorSymbolIdentification(null), null);
                bool previousDynamism = DynVarOptions.Instance.EverythingDynamic;
                DynVarOptions.Instance.EverythingDynamic = false;                
                newMethodDefinition.Accept(visitorTypeInference, null);
                DynVarOptions.Instance.EverythingDynamic = previousDynamism;
                foreach (var invocation in invocations)
                {
                    if (invocation.ActualMethodCalled is UnionType)
                        invocation.ActualMethodCalled = ((UnionType) invocation.ActualMethodCalled).TypeSet[1];
                }                
                foreach (var castExpression in castExpressions)
                {
                    ((SingleIdentifierExpression)castExpression.Expression).FrozenTypeExpression = new ClassType("System.Object");
                    castExpression.Expression.ExpressionType = new ClassType("System.Object");                    
                }


                specilizedMethods.Add(fullMethodIndentificator,newMethodDefinition);
                return newMethodDefinition;
            }
            return specilizedMethods[fullMethodIndentificator];            
        }

        private bool IsCurrentMethodDynamic(MethodType originalMethodType)
        {
            for( int i = 0 ; i < originalMethodType.ParameterListCount; i++)
                if (originalMethodType.GetParameter(i).IsDynamic) return true;
            return false;
        }

        private IList<TypeExpression[]> GetTypes(TypeExpression[] args)
        {
            IList<IList<TypeExpression>> lists = new List<IList<TypeExpression>>();
            foreach (var typeExpression in args)
            {
                List<TypeExpression> list = new List<TypeExpression>();
                if (typeExpression is UnionType)
                {
                    foreach (var expression in ((UnionType)typeExpression).TypeSet)
                    {
                        list.Add(expression);
                    }
                }
                else if (typeExpression is TypeVariable && ((TypeVariable)typeExpression).Substitution is UnionType)
                {                    
                    foreach (var expression in ((UnionType) ((TypeVariable) typeExpression).Substitution).TypeSet)
                    {
                        list.Add(expression);
                    }
                }               
                else
                {
                    list.Add(typeExpression);
                }
                lists.Add(list);
            }

            IList<TypeExpression[]> result = new List<TypeExpression[]>();
           
            int listCount = lists.Count;
            List<int> indexes = new List<int>();
            for (int i = 0; i < listCount; i++)
                indexes.Add(0);

            while (true)
            {
                // construct values
                TypeExpression[] values = new TypeExpression[listCount];
                for (int i = 0; i < listCount; i++)
                    values[i] = lists[i][indexes[i]];

                result.Add(values);

                // increment indexes
                int incrementIndex = listCount - 1;
                while (incrementIndex >= 0 && ++indexes[incrementIndex] >= lists[incrementIndex].Count)
                {
                    indexes[incrementIndex] = 0;
                    incrementIndex--;
                }

                // break condition
                if (incrementIndex < 0)
                    break;
            }
            return result;
        }

        private bool HasUnionTypes(String originalIndentificator, String methodIndentificator)
        {
            return methodIndentificator.Replace(originalIndentificator, "").Contains("_or_");
        }

        private MethodDefinition SpecilizeMethod(String methodIndentificator, MethodDefinition originalMethodDefinition,TypeExpression[] args)
        {
            MethodDefinition clonedMethodDefinition;
            if (!specilizedMethods.ContainsKey(methodIndentificator))
            {                
                clonedMethodDefinition = (MethodDefinition) originalMethodDefinition.Accept(new VisitorASTCloner(this), args);
                specilizedMethods.Add(methodIndentificator, clonedMethodDefinition);
                clonedMethodDefinition.Accept(visitorTypeInference, null);                
            }
            else
            {
                clonedMethodDefinition = specilizedMethods[methodIndentificator];
            }
            return clonedMethodDefinition;
        }

        private static string MethodIndentificator(String methodIndentificator, TypeExpression[] args, bool original = false)
        {
            methodIndentificator += "_";
            int i = 1;
            foreach (var typeExpression in args)
            {
                methodIndentificator += "_" + i++ + "_" + TypeExpressionRepresentation(typeExpression, original);
            }            
            return methodIndentificator;
        }

        private static String TypeExpressionRepresentation(TypeExpression typeExpression, bool original = false)
        {
            if (typeExpression.ILType().Contains("class"))
                return typeExpression.ILType().Replace("class ", "").Replace(".", "_");

            UnionType ut = null;
            if (typeExpression is UnionType)
            {
                ut = (UnionType)typeExpression;                
            }
            else if (typeExpression is TypeVariable && ((TypeVariable)typeExpression).Substitution is UnionType)
            {
                ut = (UnionType)((TypeVariable)typeExpression).Substitution;                               
            }
            if (ut != null)
            {
                List<String> result = new List<string>();
                foreach (var expression in ut.TypeSet)
                {
                    result.Add(TypeExpressionRepresentation(expression,original));
                }
                result.Sort();                
                return String.Join("_or_",result.ToArray());
            }
            if (original && typeExpression is TypeVariable)
                return TypeVariable.NewTypeVariable.ILType();
            return typeExpression.ILType();
        }

        public override Object Visit(BaseCallExpression node, Object obj)
        {
            return obj;
        }

        public override Object Visit(NewExpression node, Object obj)
        {
            return obj;
        }
    }
}
