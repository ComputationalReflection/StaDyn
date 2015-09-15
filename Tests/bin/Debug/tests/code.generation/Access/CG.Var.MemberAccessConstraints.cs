using System;

namespace CG.Var.MemberAccessConstraints {

    class Class {
        var attribute;

        Class(var param) {
            attribute = param;
        }

        // (string)->bool   |    Var(5):=Var(0) GreaterThan int x Var(5) <= bool
        bool simpleMethod(string b) { return attribute > b.Length; }
    }

    class Test {         
        // * Generates a constraint:
        //   var3 -> var4   |   var4:=var3.attribute
        static var fieldAccess(var obj) { 
            return obj.attribute; 
        }

        public static void testFieldAccessConstraint() {
            Class obj = new Class(3);
            int n = fieldAccess(obj);
        }

        // * Generates a constraint:
        //  (Var(3) x Var(4))->Var(8)  |  Var(7):=Var(3).simpleMethod x Var(8):=Var(7)(Var(3),Var(4)) x Var(8) <= Var(2)
        static var methodInvocation(var obj, var param) {
            return obj.simpleMethod(param);
        }

        public static void testMethodCallConstraint() {
            Class obj = new Class(3);
            bool n = methodInvocation(obj,"obj");
        }


        public static void Main() {
        }

    }

}

