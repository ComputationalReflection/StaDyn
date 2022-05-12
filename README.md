# *StaDyn*

StaDyn is an object-oriented general-purpose programming language for the .NET platform that supports both static and dynamic typing in the same programming language. The StaDyn compiler gathers type information for the dynamically typed code. That type information is used to detect type errors at compilation time and to perform significant optimizations. For that purpose, it provides type reconstruction (inference), flow-sensitive types, union and intersection types, constraint-based typing,  alias analysis and method specialization.

Its first prototype appeared in 2007, as a modification of C# 3.0. Type inference was supported by including ```var``` as a new type, unlike C#, which only offers ```var``` to define initialized local variables. Flow-sensitive types of ```var``` references are inferred by the compiler, providing type safe duck typing [1]. When a more lenient approach is required by the programmer, the ```dynamic``` type could be used instead of ```var```. Although type inference is still performed, dynamic references behave closer to those in dynamic languages.

## Variables with different types

Just like dynamic languages, variables may hold different types in the same scope:

```C#
using System;
class Program {
    public static void Main() {
        Console.Write("Number: ");
        var age = Console.In.ReadLine();
        Console.WriteLine("Digits: " + age.Length);

        age = Convert.ToInt32(age);
        Console.WriteLine(age.GetType());
        age++;

        Console.WriteLine("Happy birthday, you are " + age +
                          " years old now.");
        int length = age.Length; // * Compiler error
    }
}               
```

The ```age``` variable is first inferred as string, so it is safe to get its ```Length``` property. Then, it holds an integer, so ```age++``` is a valid expression.  The compiler detects an error in the last line, since ```Length``` is no longer provided by ```age```.

The generated code does not use a single ```Object``` variable to represent age, but two different variables whose types are string and int. This is achieved with a modification of the algorithm to compute the [SSA form](https://en.wikipedia.org/wiki/Static_single_assignment_form). This makes the generated code to be more efficient, since runtime type conversions are not required.





----------------------------------------------


1. **Performance**. The exact knowledge of object structure at compile time makes possible the generation of optimum code. This information need not to be retrieved at runtime, causing a performance penalty.
2. **No early detection of type errors**. Dynamic type checking do not make possible to fix programming errors immediately rather than discovering them at runtime; when the programmer's efforts might be aimed at some other task, or even after the program has been deployed.
3. **Legibility loss**. Type declarations constitute a form of documentation, giving useful hints about behavior and structure. Unlike descriptions embedded in comments, this form of documentation cannot become outdated, since it is checked during every run of the compiler.

## Project Aim
The main objective of this project is combining the benefits of both dynamic and static languages in just one single programming language: *StaDyn*. *StaDyn* permits straightforward development of adaptable software and rapid prototyping, without sacrificing application robustness, performance and legibility of source code.

Static typing is centered on making sure that programs behave correctly at runtime. This is the reason why languages with static typing employ a pessimistic policy regarding to programs correctness. This pessimism causes compilation errors of programs that are dynamically correct. The following Java code shows this limitation:

```C#
        Object[] v=new Object[10]; 
        for (int i = 0; i < 10; i++) { 
            v[i] = "String " + i; 
            int length = v[i].length();  // Compilation error
        } 
```

Dynamic languages follow the opposite point of view. They do not perform type checking at compile time at all. Dynamic languages are very optimistic, compiling programs that might be identified as erroneous statically. The following code is a Python example:

```Python
        myObjet = object()
        length = myObjet.length()  # No compilation error
```                 
                    
## Language Benefits

1. *Improving the Performance, Robustness, Legibility and Scalability of dynamic languages*. Adding a flexible static type system to a dynamic language will produce the previous benefits. As an example, the previous Python code will show a compile-time error.
2. *Improving the adaptability and rapid prototyping features of "static" languages*. Using type inference and type reconstruction techniques, together with constraint based type systems, will make typeable a higher number of programs that are dynamically correct. These features imply a static duck typing system. It is not necessary to use the explicitly Java interface based programming style. Objects interface is inferred statically.
3. *Separation of the Dynamism Concern*. To make dynamic and static type systems converge, we follow the Separation of Concerns principle applied to our programming language. The programmer may specify those parts of the code that need a higher adaptability (dynamic) and those that that may guarantee a correct behavior at runtime (static). This separation permits to turn rapidly developed prototypes into a final robust and efficient application. It will also be possible to make parts of an application flexible, maintaining the robustness of the rest of the program.
4. *Direct Interoperation between Static and Dynamic Languages*. This is a side effect of the previous benefit, caused by sharing the same type system and platform in both sort of language. A set of rules to convert optimistic code into pessimistic are set to make static and dynamic code really interoperable.
5. *High Level of Legibility and Simplicity*. The absence of obligatory type reference specification implies a loss of legibility in source code. A constraint-based type system will be able to document restrictions using an attribute (annotation) based notation. It is worth noting that these annotations are automatically generated by the environment; not by the programmer. This way, the benefits obtained are twofold: first, the legibility offered by types is not missed; second, other languages would interoperate with *StaDyn*.
Opposite to our approach Java uses bounded polymorphism. In standard Java 1.6, the ```min``` method of the ```java.util.Collections``` class (returns the min value in a collection) is declared this way:

```Java
        public static  <T extends Object & Comparable<? super T>> T min(Collection<? extends T> coll)	
```

In our language, we simply write:

```C#
        public static var min(var coll)	
```

The type system infers the restrictions that the parameter must satisfy, analyzing its source code. These restrictions are automatically annotated by the compiler.

For more detailed information, please visit the [*StaDyn* website](http://www.reflection.uniovi.es/stadyn/).
