using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program {
    static string[] signList;
    static string[] answers;
    static string[][] combinationList;
    static int[][] numberCombinationList;
    // RNP notation combinations
    static int[][] rnpList;
    static int[] numberList;

    static int combinations;
    static int numberCombinations;
    static string answer;

    //Methods
    static void WriteCombinations (int a, int b, int c, int d) {
        numberList = new int[4];

        numberList[0] = a;
        numberList[1] = b;
        numberList[2] = c;
        numberList[3] = d;
        signList = new string[4];
        signList[0] = "+";
        signList[1] = "-";
        signList[2] = "*";
        signList[3] = "/";
        // 0 is a number and 1 is an operator
        rnpList = new int[5][];
        rnpList[0] = new int[7] { 0, 0, 1, 0, 1, 0, 1 };
        rnpList[1] = new int[7] { 0, 0, 1, 0, 0, 1, 1 };
        rnpList[2] = new int[7] { 0, 0, 0, 1, 1, 0, 1 };
        rnpList[3] = new int[7] { 0, 0, 0, 1, 0, 1, 1 };
        rnpList[4] = new int[7] { 0, 0, 0, 0, 1, 1, 1 };
        //CALCULATE ALL THE COMBINATIONS OF THE OPERATORS AND STORE THEM IN A LIST
         SetSignCombinations ();
         SetNumberCombinations ();
        //CALCULATE THE NUMBERS TO CHECK IF THEY EQUAL 24
         CalculateNumbers (a, b, c, d);

    }

    static void SetSignCombinations () {
        
        combinations = (4 * 3 * 2) * 3 - 8;
        combinationList = new string[combinations][];
        for (int i = 0; i < combinations; i++) {
            combinationList[i] = new string[] { "1", "2", "3" };
        }
        int count = 0;

        // set each unit of the combination - 3 units
        for (int j = 0; j < 4; j++) {

            for (int k = 0; k < 4; k++) {

                for (int a = 0; a < 4; a++) {
                    if (count < combinationList.Length) {

                        combinationList[count][0] = signList[j];
                        combinationList[count][1] = signList[k];
                        combinationList[count][2] = signList[a];
                    }

                    count++;
                }

            }

        }

    }
    static void SetNumberCombinations () {
        int[, ] permutations = { { 1, 2, 3, 4, },
            { 1, 2, 4, 3, },
            { 1, 3, 2, 4, },
            { 1, 3, 4, 2, },
            { 1, 4, 2, 3, },
            { 1, 4, 3, 2, },
            { 2, 1, 3, 4, },
            { 2, 1, 4, 3, },
            { 2, 3, 1, 4, },
            { 2, 3, 4, 1, },
            { 2, 4, 1, 3, },
            { 2, 4, 3, 1, },
            { 3, 1, 2, 4, },
            { 3, 1, 4, 2, },
            { 3, 2, 1, 4, },
            { 3, 2, 4, 1, },
            { 3, 4, 1, 2, },
            { 3, 4, 2, 1, },
            { 4, 1, 2, 3, },
            { 4, 1, 3, 2, },
            { 4, 2, 1, 3, },
            { 4, 2, 3, 1, },
            { 4, 3, 1, 2, },
            { 4, 3, 2, 1, },
        };
        numberCombinations = 4 * 3 * 2 * 1;
        numberCombinationList = new int[numberCombinations][];
        for (int i = 0; i < numberCombinations; i++) {
            numberCombinationList[i] = new int[] { 1, 2, 3, 4 };
        }

        for (int j = 0; j < numberCombinations; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                switch (permutations[j,i])
                {
                    case 1:
                    permutations[j,i] = numberList[0];
                        break;
                    case 2:
                     permutations[j,i] = numberList[1];
                        break;
                    case 3:
                     permutations[j,i] = numberList[2];
                        break;
                    case 4:
                     permutations[j,i] = numberList[3];
                        break;
                    default:
                        break;
                }
                numberCombinationList[j][i] = permutations[j,i];
                
            }
            // Console.WriteLine($"{permutations[j,0]} {permutations[j,1]} {permutations[j,2]} {permutations[j,3]} ");
        }
        

    }

    static void CalculateNumbers (int num1, int num2, int num3, int num4) {

        Stack<String> answerStack = new Stack<string> ();
            for (int n = 0; n < numberCombinationList.Length; n++)
            {
                 for (int i = 0; i < combinations; i++) {

            for (int j = 0; j < rnpList.Length; j++) {
                Stack<int> myStack = new Stack<int> ();
                Stack<MathNode> myFinalStack = new Stack<MathNode> ();

                int count = 0;
                int operationCount = 0;
                string[] rnpString = new string[7];
                string firstbracket = null;
                string secondbracket = null;
                string thirdbracket = null;
                // string finalOutput = null;
                int finalrnp = 0;
                for (int k = 0; k < 7; k++) {
                    if (rnpList[j][k] == 0) {
                        //put in stack
                        //here's my numbers from the combination list                                                           
                        myStack.Push (numberCombinationList[n][count]);
                        var numberNode = new MathNode (numberCombinationList[n][count]);
                        myFinalStack.Push (numberNode);
                        // finalOutput += $" {numberList[count].ToString()} " ;
                        count++;

                    } else {
                        //take out of stack, calculate, put in stack
                        var operatorNode = new MathNode (combinationList[i][operationCount]);
                        operatorNode.child2 = myFinalStack.Pop ();
                        operatorNode.child1 = myFinalStack.Pop ();
                        myFinalStack.Push (operatorNode);
                        myStack.Push (DoTheMath (combinationList[i][operationCount], myStack.Pop (), myStack.Pop ()));
                        //  rnpString[k] = combinationList[i][operationCount].ToString();
                        // finalOutput += $" {combinationList[i][operationCount].ToString()} " ;
                        operationCount++;

                    }

                }
                var finalOutput = myFinalStack.Pop ();
                finalrnp = myStack.Pop ();
                if (finalrnp == 24) {
                //    Console.WriteLine ($"{Visit(finalOutput, finalOutput.precedence)}={finalrnp}");

                    if (!answerStack.Contains (Visit (finalOutput, finalOutput.precedence))) {

                        answerStack.Push (Visit (finalOutput, finalOutput.precedence));
                    }

                }
            }
        }
            }
           
        
        
        Console.WriteLine ($"The answer is:");
        if (answerStack.Count < 0) {
             Console.WriteLine ("No combinations exist");
        } else {

            for (int i = 0; i < 1; i++) {
                 Console.WriteLine ($"{answerStack.Pop()}");
                if (answerStack.Count > 0) {
                    i--;
                }
            }

        }
    }
    //This piece of code comes from the article https://boyet.com/blog/postfix-to-infix-converting-rpn-to-algebraic-expressions/
    //They gave an example pseudo code and I just modified it
    static string Visit (MathNode node, int priorPrecedence) {
        if (node.operation == null) {
            return node.value.ToString ();
        } else {

            string result = null;

            result = Visit (node.child1, node.precedence) + node.operation + Visit (node.child2, node.precedence);

            if (node.precedence < priorPrecedence) {
                result = "(" + result + ")";
            }

            return result;

        }

    }

    static int DoTheMath (string operation, int num2, int num1) {
        switch (operation) {
            case "+":
                return num1 + num2;
            case "-":
                return num1 - num2;
            case "*":
                return num1 * num2;
            case "/":
                //have to stop divide by zero
                if (num2 == 0) {
                    return num1 / 1 / 100000;
                } {

                    return num1 / num2;
                }
        }

        return int.MaxValue;
    }

    static void Main (string[] args) {

        Console.WriteLine ("Input 4 numbers in the following way: ");
        Console.WriteLine ("1 2 3 4");
        Console.WriteLine ("Hit enter to calculate");

        foreach (var inputString in args) {
            Console.WriteLine (inputString);
        }

        int a = 0, b = 0, c = 0, d = 0;
        //test numbers
        //a = 8;
        //b = 6;
        //c = 2;
        //d = 2;

        //input
        string input = Console.ReadLine ();
        string number = null;
        int numberAmount = 0;
        for (int i = 0; i < input.Length; i++) {

            if (input.ElementAt (i) == ' ') {
                numberAmount++;
                switch (numberAmount) {
                    case 1:
                        a = int.Parse (number);
                        break;
                    case 2:
                        b = int.Parse (number);
                        break;
                    case 3:
                        c = int.Parse (number);
                        break;
                    case 4:
                        d = int.Parse (number);
                        break;

                }
                number = null;
            } else {
                number += input.ElementAt (i).ToString ();

            }

        }
        if (numberAmount == 3) {

            d = int.Parse (number);
        }
        // a = int.Parse(args[0]);
        // b = int.Parse(args[1]);
        // c = int.Parse(args[2]);
        // d = int.Parse(args[3]);

        //Execute
        WriteCombinations (a, b, c, d);

    }

}