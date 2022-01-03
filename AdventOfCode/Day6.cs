using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day6
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input6-1.txt");

            List<int> nums = new List<int>();

            foreach (var line in lines)
            {
                var tab = line.Split(',');
                foreach (var num in tab)
                {
                    nums.Add(int.Parse(num));
                }
            }

            for (int day = 0; day < 80; day++)
            {
                for (int i = 0; i < nums.Count; i++)
                {
                    if (nums[i] == 0)
                    {
                        nums[i] = 6;
                        nums.Add(9);
                    }
                    else
                    {
                        nums[i]--;
                    }
                }
            }

            Console.WriteLine(nums.Count);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input6-2.txt");
            Dictionary<int, long> nums = new Dictionary<int, long>();

            for (int i = 0; i < 9; i++)
            {
                nums[i] = 0;
            }

            foreach (var line in lines)
            {
                var tab = line.Split(',');
                foreach (var num in tab)
                {
                    nums[int.Parse(num)]++;
                }
            }

            for (int day = 0; day < 256; day++)
            {
                long temp = nums[0];
                for (int i = 0; i < 8; i++)
                {
                    nums[i] = nums[i + 1];
                }
                nums[6] += temp;
                nums[8] = temp;
            }

            long cnt = 0;
            for (int i = 0; i < 9; i++)
            {
                cnt += nums[i];
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }
    }
}
