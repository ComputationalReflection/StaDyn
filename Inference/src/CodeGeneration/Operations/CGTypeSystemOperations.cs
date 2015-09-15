using System;
using System.Collections.Generic;
using ErrorManagement;
using TypeSystem;
using TypeSystem.Operations;

namespace CodeGeneration.Operations {
    /// <summary>
    /// This class represent the entry wnen sending a message to an operation object derived from TypeExpression.
    /// </summary>
    public class CGTypeSystemOperation : TypeSystemOperation {
        public virtual void  ReportError(String msg) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError(msg));
        }
        public virtual void ReportError(TypeExpression t1, TypeExpression t2, Enum opera) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError(String.Format("Cannot perform operation {0} {1} {2}", t1, GetOperator(opera), t2)));
        }
        /// <summary>
        ///  Dictionary to translate between a operator code to a string representation
        /// </summary>

        private static Dictionary<Enum , string > conversor;
        protected static Dictionary<Enum , string> Instance {
            get{
                if (conversor== null)
                    init();
                return conversor;
            }
    }
        
        public static string GetOperator(Enum op) {
            string s = Instance[op];
            if (String.IsNullOrEmpty(s))
                return "??";
            return s;
        }
        protected static void init() {
            conversor = new Dictionary<Enum, string>();
            conversor[AST.ArithmeticOperator.Minus] = "-";
            conversor[AST.ArithmeticOperator.Plus] = "+";
            conversor[AST.ArithmeticOperator.Mult] = "*";
            conversor[AST.ArithmeticOperator.Div] = "/";
            conversor[AST.ArithmeticOperator.Mod] = "MOD";
            }
        }

    }
