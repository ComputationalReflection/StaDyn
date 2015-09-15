using System.Collections.Generic;
using Tools;
namespace TypeSystem.Operations {
    public class UnifyOperation : TypeSystemOperation {
    //TypeExpression te;
    //SortOfUnification unification;
    //IList<Pair<TypeExpression, TypeExpression>> previouslyUnified;
    
    //public UnifyOperation(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {
    //    this.te = te;
    //    this.unification = unification;
    //    this.previouslyUnified = previouslyUnified;
    //}
    //    //WriteType varial
    //public override object AcceptOperation(TypeVariable t0) {
    //    if (this.te == null)
    //        return false;
    //    bool success = t0.addToMyEquivalenceClass(this.te, this.unification, this.previouslyUnified);
    //    // * Clears the type expression cache
    //    t0.ValidTypeExpression = this.te.ValidTypeExpression = false;
    //    return success;
    //}
    //public override objectn Unify(MethodType m0) {
    //    MethodType mt = this.te as MethodType;
    //    if (mt != null) {
    //        if (mt.ParameterListCount != m0.ParameterListCount)
    //            return false;
    //        bool success = true;
    //        for (int i = 0; i < m0.ParameterListCount; i++)
    //            if (!(bool)m0.GetParameter[i].AcceptOperation(new UnifyOperation(mt.GetParameter(i), this.unification, this.previouslyUnified)) {
    //                success = false;
    //                break;
    //            }
    //        if (success)
    //            success = (bool)m0.Return.AcceptOperation(new UnifyOperation(mt.Return, this.unification, this.previouslyUnified));
    //        // * Clears the type expression cache
    //        m0.ValidTypeExpression = false;
    //        this.te.ValidTypeExpression = false;
    //        return success;
    //    }
    //    if (this.te is TypeVariable && this.unification != SortOfUnification.Incremental)
    //        // * No incremental unification is commutative
    //        return this.te.AcceptOperation(UnifyOperation(m0, this.unification, this.previouslyUnified);
    //    return false;
    //}

    public override object ReportError(TypeExpression firstOperand) {
        throw new System.NotImplementedException();
    }
    }
}