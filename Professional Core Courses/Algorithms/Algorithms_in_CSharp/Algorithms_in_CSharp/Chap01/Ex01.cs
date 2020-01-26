using System;
namespace Algorithms_in_CSharp.Chap01
{
    public class Ex01
    {
        public Ex01()
        {
        }

        public static void Ansto1_1_1()
        {
            float a = (0 + 15) / 2;
            double b = 2.0e-6 * 100000000.1;
            bool c = true && false || true & true;
            Console.WriteLine($"The answers to ques 1.1.1 are:\n a: {a}\n b: {b}\n c: {c}\n");
        }

        public static void Ansto1_1_2()
        {
            var a = (1 + 2.236) / 2;
            var b = 1 + 2 + 3 + 4.0;
            var c = 4.1 >= 4;
            var d = 1 + 2 + "3";
            Console.WriteLine($"The type and value of ques 1.1.2 are:\n a: {a.GetType()} {a}\n" +
                $" b: {b.GetType()} {b}\n c: {c.GetType()} {c}\n d: {d.GetType()} {d}");
        }

        public static void Ansto1_1_3()
        {
            Console.WriteLine("Answer to the ques 1.1.3:\n");

            Console.WriteLine("Please input three numbers for the comparision, " +
                "split by space and end by entrence:");
            //string str = Console.ReadLine();
            //string str = "1 2 3";
            string str = "1 1 2";

            string[] compListStr = str.Split(' ');
            string comparator = compListStr[0];
            bool flag_equal = true;

            foreach (var numStr in compListStr)
            {
                if (numStr != comparator)
                {
                    flag_equal = false;
                }
            }

            //for (int i = 0; i < compListStr.Length-1; i++)
            //{
            //    if (compListStr[i] == compListStr[i + 1])
            //    {
            //        flag_equal = true;
            //    }
            //    else
            //    {
            //        flag_equal = false;
            //    }
            //}

            if (flag_equal == true)
            {
                Console.WriteLine("These three numbers are equal.");
            }
            else if (flag_equal == false)
            {
                Console.WriteLine("These three numbers are not equal.");
            }
        }
    }
}
