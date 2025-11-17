using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MathNode
{
    public int value;
    public string operation;
    public int precedence;
    public MathNode child1, child2;

    public MathNode(int myValue)
    {
        value = myValue;
    }

    public MathNode(string myOperation)
    {
        value = 0;
        if (myOperation == "*" || myOperation == "/")
        {
           
                precedence =2;
            
        }else{
            precedence = 1;
        }
        operation = myOperation;
    }

    // public override string ToString()
    // {
    //     if (string.IsNullOrEmpty(operation)) return value.ToString();

    //     return $"({child1} {operation} {child2})";
    // }

    // public int Evaluate()
    // {
    //     if (string.IsNullOrEmpty(operation)) return value;

    //     switch (operation)
    //     {
    //         case "+":
    //             return child1.Evaluate() + child2.Evaluate();
    //         case "-":
    //             return child1.Evaluate() - child2.Evaluate();
    //         case "*":
    //             return child1.Evaluate() * child2.Evaluate();
    //         case "/":
    //         //have to stop divide by zero
    //             if (child2.Evaluate() == 0)
    //             {
    //                 return child1.Evaluate() / 1/100000;
    //             }{

    //             return child1.Evaluate() / child2.Evaluate();
    //             }
    //     }

    //     return int.MaxValue;
    // }
}
