using System;

namespace AdventOfCode
{
    public class Day1
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input1-1.txt");

            int prevNum = int.Parse(lines[0]);
            int cnt = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                int num = int.Parse(lines[i]);
                cnt += num > prevNum ? 1 : 0;
                prevNum = num;
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input1-2.txt");

            int prevSum = int.Parse(lines[0]) + int.Parse(lines[1]) + int.Parse(lines[2]);
            int cnt = 0;

            for (int i = 3; i < lines.Length; i++)
            {
                int num = int.Parse(lines[i]);
                int sum = prevSum + num - int.Parse(lines[i - 3]);
                cnt += sum > prevSum ? 1 : 0;
                prevSum = sum;
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }
    }
}
