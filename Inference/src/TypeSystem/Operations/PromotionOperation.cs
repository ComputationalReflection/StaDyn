using System.Collections.Generic;
using DynVarManagement;
using System;
using ErrorManagement;
namespace TypeSystem.Operations {
    /// <summary>
    /// This class implements factory method pattern ( virtual constructor).
    /// Role: Factory
    /// </summary>
    public enum KindOfPromotion { Simple, Verbose };

    public abstract class PromotionOperation : TypeSystemOperation {

        public static PromotionOperation Create(TypeExpression type, MethodType methodAnalyzed, Location location) {
            return new SimplePromotionOperation(type, methodAnalyzed, location);
        }

        public static PromotionOperation Create(TypeExpression type, Enum op, MethodType methodAnalyzed, Location location) {

            return new VerbosePromotionOperation(type, op, methodAnalyzed, location);
        }

        public abstract KindOfPromotion KindOfPromotion {
            get;
        }
    }
}
