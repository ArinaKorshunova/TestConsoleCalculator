using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите пример: ");

                var example = Console.ReadLine().Replace(" ", "");

                var tt = Regex.Match(example, @"^(?:([0-9]*[.,]?[0-9])+([*+-]|/(?!0)))+([0-9]*[.,]?[0-9])+$");

                if (tt.Success)
                {
                    Console.WriteLine("Результат: " + Calculate(example));
                }
                else
                {
                    Console.WriteLine("Результат не может быть вычислен, строка имеет некорректный формат.");
                }

                Console.ReadLine();
            }
        }

        private static decimal Sum(string s1, string s2)
        {
            return Calculate(s1) + Calculate(s2);
        }

        private static decimal Diff(string s1, string s2)
        {
            return Calculate(s1) - Calculate(s2);
        }

        private static decimal Multiplication(string s1, string s2)
        {
            return Calculate(s1) * Calculate(s2);
        }

        private static decimal Division(string s1, string s2)
        {
            return Calculate(s1) / Calculate(s2);
        }

        private static decimal Calculate(string str)
        {
            decimal result;
            if (Decimal.TryParse(str.Replace(".", ","),  out result))
            {
                return result;
            }

            bool ifNotCalculate = true;

            var sums = str.Split('+');
            if (sums.Length > 1 && ifNotCalculate)
            {
                decimal prevSum = Calculate(sums[0]);
                for (int i = 1; i < sums.Length; i++)
                {
                    prevSum = prevSum + Calculate(sums[i]);
                }

                result = prevSum;
                ifNotCalculate = false;
            }

            var diffs = str.Split('-');
            if (diffs.Length > 1 && ifNotCalculate)
            {
                decimal prevDiff = Calculate(diffs[0]);
                for (int i = 1; i < diffs.Length; i++)
                {
                    prevDiff = prevDiff - Calculate(diffs[i]);
                }

                result = prevDiff;
                ifNotCalculate = false;
            }

            var multys = str.Split('*');
            if (multys.Length > 1 && ifNotCalculate)
            {
                decimal prevMulty = Calculate(multys[0]);
                for (int i = 1; i < multys.Length; i++)
                {
                    prevMulty = prevMulty * Calculate(multys[i]);
                }

                result = prevMulty;
                ifNotCalculate = false;
            }

            var divisions = str.Split('/');
            if (divisions.Length > 1 && ifNotCalculate)
            {
                decimal prevDivision = Calculate(divisions[0]);
                for (int i = 1; i < divisions.Length; i++)
                {
                    prevDivision = prevDivision / Calculate(divisions[i]);
                }

                result = prevDivision;
                ifNotCalculate = false;
            }

            return result;
        }
    }
}
