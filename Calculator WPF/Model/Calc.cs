using System;

namespace Calculator_WPF.Model
{
    public static class Calc
    {
        public static double Calculate(double firstArgument, double secondArgument, string operation)
        {

            switch (operation)
            {
                case "+":
                    return firstArgument + secondArgument;
                case "-":
                    return firstArgument - secondArgument;
                case "×":
                    return firstArgument * secondArgument;
                case "÷":
                    return firstArgument / secondArgument;
                default:
                    throw new ArgumentException("Invalid argument", "operation");
            }
        }
    }
}
