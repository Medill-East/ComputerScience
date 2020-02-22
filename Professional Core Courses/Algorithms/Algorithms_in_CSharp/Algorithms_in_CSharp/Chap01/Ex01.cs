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
            Console.WriteLine("The numbers you input are: " + str);


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

            if (flag_equal == true)
            {
                Console.WriteLine("These three numbers are equal.");
            }
            else if (flag_equal == false)
            {
                Console.WriteLine("These three numbers are not equal.");
            }
        }

        public static void Ansto1_1_5()
        {
            Console.WriteLine("Answer to the ques 1.1.5:\n");
            double x = 0.00000000000000001;
            double y = 0.999999999;
            bool areXandYbetween0and1 = false;
            if (0.0 < x && x < 1.0 && 0.0 < y && y < 1.0)
            {
                areXandYbetween0and1 = true;
            }
            Console.WriteLine($"Both x and y are between 0 and 1: "+ areXandYbetween0and1);
        }

        public static void Ansto1_1_7()
        {
            double t = 9.0;
            while (Math.Abs(t-9.0/t)>.001)
            {
                t = (9.0 / t + t) / 2.0;
            }

            int sum = 0;
            for (int i = 1; i < 1000; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    sum++;
                }
            }

            int sumc = 0;
            for (int i = 1; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    sum++;
                }
            }
            Console.WriteLine("Answer to the ques 1.1.7:\n");
            Console.WriteLine($"t= {t}, sum= {sum}, sumc = {sumc}");
        }

        public static void Ansto1_1_8()
        {
            Console.WriteLine('b');
            Console.WriteLine('b' + 'c' + " = ASCALL ('b' + 'c') = 98 + 99");
            Console.WriteLine((char)('a' + '4')) ;
        }

    }
}
