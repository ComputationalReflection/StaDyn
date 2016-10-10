using System;

namespace CodeGeneration
{
    public class SetMemberCallSite : AbstractCallSite
    {
        public SetMemberCallSite(int id, String memberName, CallSiteContainer callSiteContainer):base(id, memberName, callSiteContainer){}


        public override String CallSiteType
        {
            get
            {
                return "[System.Core]System.Runtime.CompilerServices.CallSite`1<class [mscorlib]System.Action`3<class [System.Core]System.Runtime.CompilerServices.CallSite, object, object>>";
            }
        }

        public override String CallSiteSubType
        {
            get
            {
                return "[mscorlib]System.Action`3<class [System.Core]System.Runtime.CompilerServices.CallSite, object, object>";
            }
        }
        
        public override void WriteInitialization(int indent, DLRCodeGenerator codeGenerator)
        {
            codeGenerator.ldci4(indent, 0);
            codeGenerator.ldstr(indent, MemberName);
            codeGenerator.ldtoken(indent, CallSiteContainer.ReferenceClass);
            codeGenerator.ldci4(indent, 2);
            codeGenerator.newarr(indent, "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo");
            codeGenerator.stloc(indent, "V" + Name);
            codeGenerator.ldloc(indent, "V" + Name);
            codeGenerator.ldci4(indent, 0);
            codeGenerator.ldci4(indent, 0);
            codeGenerator.ldnull(indent);
            codeGenerator.Call(indent, "class", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "Create", new string[] { "valuetype [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags", "string" });
            codeGenerator.stelemRef(indent);
            codeGenerator.ldloc(indent, "V" + Name);
            codeGenerator.ldci4(indent, 1);
            codeGenerator.ldci4(indent, 0);
            codeGenerator.ldnull(indent);
            codeGenerator.Call(indent, "class", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "Create", new string[] { "valuetype [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags", "string" });
            codeGenerator.stelemRef(indent);
            codeGenerator.ldloc(indent, "V" + Name);
            codeGenerator.Call(indent, "class", "[System.Core]System.Runtime.CompilerServices.CallSiteBinder", "[Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.Binder", "SetMember", new string[] { "valuetype [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags", "string", "class [mscorlib]System.Type", "class [mscorlib]System.Collections.Generic.IEnumerable`1<class [Microsoft.CSharp]Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo>" });
            codeGenerator.Call(indent, "class", "[System.Core]System.Runtime.CompilerServices.CallSite`1<!0>", "class " + CallSiteType, "Create", new string[] { "class [System.Core]System.Runtime.CompilerServices.CallSiteBinder" });
            codeGenerator.WriteLine(indent, "stsfld class " + CallSiteType + " class " + FullName);
            codeGenerator.WriteLine();
        }
    }
}
