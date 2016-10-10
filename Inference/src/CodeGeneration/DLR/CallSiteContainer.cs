using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public class CallSiteContainer
    {

        public string ReferenceClass { get; private set; }
        public string MemberName { get; private set; }

        /// <summary>
        /// Collection of callsites of the callsite container
        /// </summary>
        public IList<ICallSite> CallSiteReferences { get; private set; }


        /// <summary>
        /// Constructor of the CallSiteContainer
        /// </summary>
        /// <param name="referenceClass">Full Name of the class where callsite is called</param>
        /// <param name="memberName">Name of the member where the callsite is called</param>
        public CallSiteContainer(String referenceClass, String memberName)
        {
            ReferenceClass = referenceClass;
            MemberName = memberName;
            this.CallSiteReferences = new List<ICallSite>();
        }
        
        /// <summary>
        /// Name of the CallSiteContainer class
        /// </summary>
        public string Name { 
            get
            {
                return "CallSiteContainer_" + ReferenceClass.Replace(".", "_") + "_" + MemberName;
            }
        }

        /// <summary>
        /// Full Name of the CallSiteContainer class
        /// </summary>
        public string FullName
        {
            get { return "STADYN_SERVER." + Name; }
        }

        public GetMemberCallSite AddGetMemberCallSite(String fieldName)
        {
            GetMemberCallSite csr = new GetMemberCallSite(CallSiteReferences.Count, fieldName ,this); 
            CallSiteReferences.Add(csr);
            return csr;
        }

        public SetMemberCallSite AddSetMemberCallSite(String fieldName)
        {
            SetMemberCallSite csr = new SetMemberCallSite(CallSiteReferences.Count, fieldName, this );
            CallSiteReferences.Add(csr);
            return csr;
        }

        public InvokeMemberCallSite AddInvokeMemberCallSite(String memberName,IList<String> parameters)
        {
            InvokeMemberCallSite csr = new InvokeMemberCallSite(CallSiteReferences.Count, memberName,parameters, this);
            CallSiteReferences.Add(csr);
            return csr;
        }
    }
}
