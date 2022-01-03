using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day7
    {
       public void Solution1()
       {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input7-1.txt");

            var tab = lines[0].Split(',');
            List<int> nums = new List<int>();

            foreach (var num in tab)
            {
                nums.Add(int.Parse(num));
            }

            int sum = 0;
            foreach (var num in nums)
            {
                sum += num;
            }

            int avg = (int)(sum / nums.Count);

            int min = int.MaxValue;
            int cur = avg;
            int summ = 0;
            foreach (var num in nums)
            {
                summ += Math.Abs(num - cur);
            }
            min = summ;

            for (int i = avg + 1; i < nums.Max(); i++)
            {
                summ = 0;
                foreach (var num in nums)
                {
                    summ += Math.Abs(num - i);
                }

                if (summ >= min)
                {
                    cur = i;
                    break;
                } else
                {
                    min = summ;
                }
            }

            for (int i = cur - 1; i >0; i--)
            {
                summ = 0;
                foreach (var num in nums)
                {
                    summ += Math.Abs(num - i);
                }

                if (summ > min)
                {

                    break;
                }
                else
                {
                    min = summ;
                }
            }

            Console.WriteLine(min);
            Console.ReadKey();
        }

        public static int getCost(int a, int b)
        {
            int c = Math.Abs(a - b);
            int rate = 1;
            int res = 0;
            for (int i = 0; i < c; i++)
            {
                res += rate;
                rate++;
            }
            return res;
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input7-2.txt");

            var tab = lines[0].Split(',');
            List<int> nums = new List<int>();

            foreach (var num in tab)
            {
                nums.Add(int.Parse(num));
            }

            int sum = 0;
            foreach (var num in nums)
            {
                sum += num;
            }

            int avg = (int)(sum / nums.Count);

            int min = int.MaxValue;
            int cur = avg;
            int summ = 0;
            foreach (var num in nums)
            {
                summ += getCost(num, cur);
            }
            min = summ;

            for (int i = avg + 1; i < nums.Max(); i++)
            {
                summ = 0;
                foreach (var num in nums)
                {
                    summ += getCost(num, i);
                }

                if (summ >= min)
                {
                    cur = i;
                    break;
                }
                else
                {
                    min = summ;
                }
            }

            for (int i = cur - 1; i > 0; i--)
            {
                summ = 0;
                foreach (var num in nums)
                {
                    summ += getCost(num, i);
                }

                if (summ > min)
                {

                    break;
                }
                else
                {
                    min = summ;
                }
            }

            Console.WriteLine(min);
            Console.ReadKey();
        }
    }
}
