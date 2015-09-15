using AST;
using ErrorManagement;
using System;


namespace AST.Operations {
    /// <summary>
    /// This class represent the entry wnen sending a message to an operation object. derived from AstNode
    /// </summary>
    public abstract class AstOperation {
        public virtual object Exec(AstNode a, object arg)                    { return ReportErrorSafe("AstNode", a); }
        public virtual object Exec(BaseCallExpression b, object arg)         { return Exec((Expression)b, arg); }
        public virtual object Exec(BaseExpression b, object arg)             { return Exec((Expression)b, arg); }
        public virtual object Exec(Declaration d, object arg)                { return Exec((Statement)d, arg); }
        public virtual object Exec(Definition d, object arg)                 { return Exec((IdDeclaration)d, arg); }
        public virtual object Exec(Expression e, object arg)                 { return Exec((Statement)e, arg); }
        public virtual object Exec(FieldAccessExpression f, object arg)      { return Exec((Expression)f, arg); }
        public virtual object Exec(IdentifierExpression i, object arg)       { return Exec((Expression)i, arg); }
        public virtual object Exec(NewExpression n, object arg)              { return Exec((BaseCallExpression)n, arg); }
        public virtual object Exec(SingleIdentifierExpression s, object arg) { return Exec((IdentifierExpression)s, arg); }
        public virtual object Exec(Statement s, object arg)                  { return Exec((AstNode)s, arg); }
        public virtual object Exec(InvocationExpression i, object arg)       { return Exec((BaseCallExpression)i, arg); }
        
        public virtual object ReportErrorSafe(String operation, AstNode astNode) {
            if (astNode == null)
                return ReportError(operation, new Location());
            return ReportError(operation, astNode.getText(), astNode.Location != null ? astNode.Location : new Location());
        }

        public virtual object ReportError(String operation, String element, Location location) {
            ErrorManager.Instance.NotifyError(new InternalOperationInterfaceError(operation, location));
            return null;
        }
        public virtual object ReportError(String operation, Location location) {
            ErrorManager.Instance.NotifyError(new InternalOperationInterfaceError(operation, location));
            return null;
        }

 
    }
}