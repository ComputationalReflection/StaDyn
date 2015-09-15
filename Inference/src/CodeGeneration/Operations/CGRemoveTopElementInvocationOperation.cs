using TypeSystem.Operations;
using TypeSystem;
namespace CodeGeneration.Operations {
    /// <summary>
    ///  It checks if it is neccesary to perform a pop operation after an invocation
    ///  </summary>       
    internal class CGRemoveTopElementInvocationOperation : TypeSystemOperation {

        public CGRemoveTopElementInvocationOperation() { }
        /// <summary>
        /// The invocation has no return type so no pop is needed
        /// </summary>
        /// <param name="v"></param>
        /// <returns>false</returns>
        public override object Exec(VoidType v, object arg) {
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ut"></param>
        /// <returns>true or false depending whether the typeset of the UnionType returns void</returns>
        public override object Exec(UnionType ut, object arg) {
            return !(ut.TypeSet[0] is VoidType);
        }

        public override object Exec(TypeVariable t, object arg) {
            if ( t.IsFreshVariable() )
                return true; //It is an introspective method call
            return t.Substitution.AcceptOperation(this, arg);
        }
         /// <summary>
        /// Returns true because it is neccesary to perform a pop
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override object Exec(TypeExpression t, object arg) {
            return true;
        }
    }
}