using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day3
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input3-1.txt");

            int listLen = lines.Length;
            int len = lines[0].Length;
            int[] cntTab = new int[len];
            foreach (var line in lines)
            {
                for (int i = 0; i < len; i++)
                {
                    if (line[i] == '1')
                    {
                        cntTab[i] += 1;
                    }
                }
            }

            string res = "";
            string res2 = "";
            for (int i = 0; i < len; i++)
            {
                if (cntTab[i] > listLen / 2)
                {
                    res += '1';
                    res2 += '0';

                }
                else
                {
                    res += '0';
                    res2 += '1';
                }
            }

            int output = Convert.ToInt32(res, 2);
            int output2 = Convert.ToInt32(res2, 2);

            Console.WriteLine(output * output2);
            Console.ReadKey();
        }


        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input3-2.txt");

            List<string> remain = new List<string>();
            foreach (var line in lines)
            {
                remain.Add(line);
            }

            int output1 = GetResult2(remain, (cnt1, cnt0) => (cnt1 >= cnt0));
            int output2 = GetResult2(remain, (cnt1, cnt0) => (cnt1 < cnt0));

            Console.WriteLine(output1 * output2);
            Console.ReadKey();
        }

        private static int GetResult2(List<string> remain, Func<int, int, bool> comp)
        {
            for (int i = 0; i < remain[0].Length; i++)
            {
                if (comp(remain.Count(p => p[i] == '1'), remain.Count(p => p[i] == '0')))
                    remain = remain.Where(p => p[i] == '1').ToList();
                else
                    remain = remain.Where(p => p[i] == '0').ToList();

                if (remain.Count == 1)
                    break;
            }

            return Convert.ToInt32(remain[0], 2);
        }
    }
}
