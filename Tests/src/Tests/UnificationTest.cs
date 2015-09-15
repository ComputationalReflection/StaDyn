////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: UnificationTest.cs                                                   //
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                      //
// Description:                                                               //
//    Testing of the unification algorithm.                                   //
// -------------------------------------------------------------------------- //
// Create date: 04-04-2007                                                    //
// Modification date: 04-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using TypeSystem;
using Tools;

namespace Tests {

    /// <summary>
    /// Class to test the unification algorithm
    /// </summary>
    class UnificationTest {
        private  void Unify(TypeExpression type1, TypeExpression type2) {
            Console.WriteLine("To Unify\n\tType 1:\t{0}\n\tType 2:\t{1}", type1, type2);

            bool unification = type1.Unify(type2,SortOfUnification.Equivalent, new List<Pair<TypeExpression,TypeExpression>>());
            Console.WriteLine("The unification is {0}.", unification);

            if (unification)
                Console.WriteLine("And these are the results:\n\tType 1:\t{0}\n\tType 2:\t{1}", type1, type2);

            Console.WriteLine();
        }


        public  void testUnify1OK() {
            // * "Array(int)"
            TypeExpression type1=new ArrayType(IntType.Instance);
            // * "Array(Var(alpha))"

            TypeExpression type2 = new ArrayType(TypeVariable.NewTypeVariable); 
            
            Unify(type1, type2);
        }

        public  void testUnify2OK() {
           // * "Array(Var(alpha))->Var(alpha)"
            TypeExpression alpha = TypeVariable.NewTypeVariable;
            MethodType type1 = new MethodType(alpha);
            type1.AddParameter(new ArrayType(alpha));

            // * "Array(Array(int))->Var(beta)"
            TypeExpression beta = TypeVariable.NewTypeVariable;
            MethodType type2 = new MethodType(beta);
            type2.AddParameter(new ArrayType(new ArrayType(IntType.Instance)));
                
            Unify(type1, type2);
        }

        public  void testUnify3Wrong() {
            // * "Array(Var(alpha))->Var(alpha)"
            TypeExpression alpha =  TypeVariable.NewTypeVariable;
            MethodType type1 = new MethodType(alpha);
            type1.AddParameter(new ArrayType(alpha));

            // * "Array(Array(int))->double"
            MethodType type2 = new MethodType(DoubleType.Instance);
            type2.AddParameter(new ArrayType(new ArrayType(IntType.Instance)));

            Unify(type1, type2);
        }


        public  void testUnify4Wrong() {
            // * "Array(Var(alpha))->Var(alpha)"
            TypeExpression alpha = TypeVariable.NewTypeVariable;
            MethodType type1 = new MethodType(alpha);
            type1.AddParameter(new ArrayType(alpha));

            // * "(Array(Array(int))->Var(beta)) -> Var(gamma)"
            TypeExpression beta =  TypeVariable.NewTypeVariable;
            TypeExpression gamma = TypeVariable.NewTypeVariable;
            MethodType type2 = new MethodType(gamma);
            MethodType type3 = new MethodType(beta);
            type3.AddParameter(new ArrayType(new ArrayType(IntType.Instance)));
            type2.AddParameter(type3);

            Unify(type1, type2);
        }

        public  void testUnify5OK() {
            // <A(B(v),C(u,v)), A(B(w),C(w,D(x,))>
            // * "( Var(alpha)->Var(beta) ) -> Pointer(alpha)"
            TypeExpression alpha = TypeVariable.NewTypeVariable;
            TypeExpression beta = TypeVariable.NewTypeVariable;
            MethodType type1 = new MethodType(new ArrayType(alpha));
            MethodType typeAux = new MethodType(beta);
            typeAux.AddParameter(alpha);
            type1.AddParameter(typeAux);

            // * "( (double->double)->Var(gamma) ) -> Pointer(Var(gamma))"
            TypeExpression gamma = TypeVariable.NewTypeVariable;
            MethodType type2 = new MethodType(new ArrayType(gamma));
            MethodType typeAux1 = new MethodType(gamma);
            MethodType typeAux2 = new MethodType(DoubleType.Instance);
            typeAux2.AddParameter(DoubleType.Instance);
            typeAux1.AddParameter(typeAux2);
            type2.AddParameter(typeAux1);

            Unify(type1, type2);
        }

        public  void testUnify6OK() {
            // * "( Var(alpha) -> Array( int )"
            TypeExpression alpha = TypeVariable.NewTypeVariable;
            MethodType type1 = new MethodType(new ArrayType(IntType.Instance));
            type1.AddParameter(alpha);

            // * "( Var(beta) -> Array( Var(beta) )"
            TypeExpression beta = TypeVariable.NewTypeVariable;
            MethodType type2 = new MethodType(new ArrayType(beta));
            type2.AddParameter(beta);

            Unify(type1, type2);
        }

    }
}
