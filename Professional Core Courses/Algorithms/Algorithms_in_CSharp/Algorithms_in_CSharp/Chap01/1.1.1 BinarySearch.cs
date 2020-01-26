using System;
namespace Algorithms_in_CSharp.Chap01
{
	public class BinarySearch
	{
		public BinarySearch()
		{
		}

        public static object BinarySearchDisplay(int[] inputList, int target)
        {
            int minNum = 0;
            int maxNum = inputList.Length - 1;

            while(minNum<=maxNum)
            {
                int mid = (minNum + maxNum) / 2;    //define the middle index
                if(target == inputList[mid])        //if the target is equall to arr[mid]
                {
                    return mid;
                }
                else if (target > inputList[mid])
                {
                    minNum = mid + 1;
                }
                else
                {
                    maxNum = mid - 1;
                }
            }
            return "None";
        }

        public static void CallBinarySearchDisplay()
        {
            int[] inputList = { 7, 9, 5, 2, 3 };
            Array.Sort(inputList);

            Console.WriteLine("The results of binary search are:");
            Console.WriteLine(BinarySearchDisplay(inputList, 3));
            Console.WriteLine(BinarySearchDisplay(inputList, 2));
            Console.WriteLine(BinarySearchDisplay(inputList, 9));
            Console.WriteLine(BinarySearchDisplay(inputList, 0));




        }
    }
}
