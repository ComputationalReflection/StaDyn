using System;
using System.Linq;
using System.Collections.Generic;
using AST;
using ErrorManagement;
using TypeSystem;

namespace CodeGeneration {

    class VisitorDLRCodeGeneration<T> : VisitorCLRCodeGeneration<T> where T : DLRCodeGenerator {
        private IDictionary<String, CallSiteContainer> callSiteContainers;
        
        public VisitorDLRCodeGeneration(string moduleName, T codeGenerator): base(moduleName, codeGenerator)
        {
            callSiteContainers = new Dictionary<string, CallSiteContainer>();
        }
        
        private CallSiteContainer GetCallSiteContainer(MethodDefinition method)
        {
            if (!callSiteContainers.ContainsKey(method.FullName))
                callSiteContainers.Add(method.FullName, new CallSiteContainer(method.FullName.Replace("." + method.Identifier,""), method.Identifier));
            return callSiteContainers[method.FullName];
        }

        public override Object Visit(SourceFile node, Object obj)
        {
            object result = base.Visit(node, obj);
            this.codeGenerator.WriteLine(0, ".namespace STADYN_SERVER");
            this.codeGenerator.WriteLine(0, "{");
            foreach (var callSiteContainer in callSiteContainers.Values)
                this.codeGenerator.WriteCallSiteContainer(callSiteContainer);
            this.codeGenerator.WriteLine(0, "}");
            return result;
        }

        protected override void InstrospectiveFieldInvocation(Expression node, string memberName, Object obj)
        {
            InheritedAttributes ia = (InheritedAttributes)obj;            
            GetMemberCallSite callSite = GetCallSiteContainer(ia.CurrentMethod).AddGetMemberCallSite(memberName);
            this.codeGenerator.pop(indent);
            this.codeGenerator.WriteLine(indent, "ldsfld class " + callSite.CallSiteType + " " + callSite.FullName);
            this.codeGenerator.WriteLine(indent, "ldfld !0 class " + callSite.CallSiteType + "::Target");
            this.codeGenerator.WriteLine(indent, "ldsfld class " + callSite.CallSiteType + " " + callSite.FullName);
            node.Accept(this, obj);             
            this.codeGenerator.WriteLine(indent, "callvirt instance !2 class "+ callSite.CallSiteSubType +"::Invoke(!0,!1)");            
        }

        protected override void InstrospectiveFieldAssignation(Expression node, string memberName, Object obj)
        {
            InheritedAttributes ia = (InheritedAttributes)obj;
            SetMemberCallSite callSite = GetCallSiteContainer(ia.CurrentMethod).AddSetMemberCallSite(memberName);
            string id = GetAuxFielVar() + memberName;
            this.codeGenerator.WriteAuxiliarLocalVariable(this.indent, id, "object");
            this.codeGenerator.stloc(this.indent, id);
            this.codeGenerator.pop(this.indent);
            this.codeGenerator.WriteLine(indent, "ldsfld class " + callSite.CallSiteType + " class " + callSite.FullName);
            this.codeGenerator.WriteLine(indent, "ldfld !0 class " + callSite.CallSiteType + "::Target");
            this.codeGenerator.WriteLine(indent, "ldsfld class " + callSite.CallSiteType + " class " + callSite.FullName);
            node.Accept(this, obj);
            this.codeGenerator.ldloc(this.indent, id);
            this.codeGenerator.WriteLine(indent, "callvirt instance void class " + callSite.CallSiteSubType + "::Invoke(!0, !1, !2)");            
        }

        internal override void IntrospectiveInvocation(InvocationExpression node, Object obj, Object inheritedAttributes, string memberName)
        {
            InheritedAttributes ia = (InheritedAttributes)obj;
            IList<String> parameters = new List<string>();
            for (int i = 0; i < node.Arguments.ExpressionCount; i++)
                parameters.Add(node.Arguments.GetExpressionElement(i).ILTypeExpression.ILType());
            InvokeMemberCallSite callSite = GetCallSiteContainer(ia.CurrentMethod).AddInvokeMemberCallSite(memberName,parameters);
            this.codeGenerator.WriteLine(indent, "ldsfld class " + callSite.CallSiteType + " class " + callSite.FullName);
            this.codeGenerator.WriteLine(indent, "ldfld !0 class " + callSite.CallSiteType + "::Target");
            this.codeGenerator.WriteLine(indent, "ldsfld class " + callSite.CallSiteType + " class " + callSite.FullName);
            node.Identifier.Accept(this, ia);
            for (int i = 0; i < node.Arguments.ExpressionCount; i++)
                node.Arguments.GetExpressionElement(i).Accept(this, inheritedAttributes);
            this.codeGenerator.WriteLine(indent, "callvirt instance !" + (parameters.Count + 2) + " class " + callSite.CallSiteSubType + "::Invoke(" + String.Join(",", Enumerable.Range(0, node.Arguments.ExpressionCount + 2).Select((t, i) => "!" + i)) + ")");                       
        }
    }
}
