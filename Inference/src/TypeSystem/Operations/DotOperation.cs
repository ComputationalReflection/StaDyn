using System.Collections.Generic;
namespace TypeSystem.Operations {

    public enum DotKind { Constrained, Unconstrained };
/// <summary>
/// Implements a factory method pattern. Virtual constructor.
/// Role: factory
/// Implements a double dispatcher pattern
/// Role:
/// </summary>
    public abstract class DotOperation : TypeSystemOperation {


        public abstract DotKind kindOfDot {
            get;
        }

        #region Factory Methods
        
        public static DotOperation Create(string memberName, IList<TypeExpression> previousDot) {
            return new UnconstrainedDotOperation(memberName, previousDot);
        }

        public static DotOperation Create(string member, MethodType methodAnalyzed, IList<TypeExpression> previousDot, ErrorManagement.Location loc) {
            return new ConstrainedDotOperation(member, methodAnalyzed, previousDot, loc);
        }
        
        #endregion
    }     
}