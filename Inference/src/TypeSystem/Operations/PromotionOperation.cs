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
        protected bool NotifyError { set; get; }

        protected PromotionOperation(bool notifyError)
        {
            this.NotifyError = notifyError;
        }

        public static PromotionOperation Create(TypeExpression type, MethodType methodAnalyzed, Location location, bool notifyError = true) {
            return new SimplePromotionOperation(type, methodAnalyzed, location, notifyError);
        }

        public static PromotionOperation Create(TypeExpression type, Enum op, MethodType methodAnalyzed, Location location, bool notifyError = true) {
            return new VerbosePromotionOperation(type, op, methodAnalyzed, location, notifyError);
        }

        public abstract KindOfPromotion KindOfPromotion {
            get;
        }
    }
}
