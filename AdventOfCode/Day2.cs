using System;

namespace AdventOfCode
{
    public class Day2
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input2-1.txt");
            int x = 0;
            int d = 0;

            foreach (var line in lines)
            {
                string command = line.Split(' ')[0];
                int num = int.Parse(line.Split(' ')[1]);
                if (command == "forward")
                {
                    x += num;
                }
                else if (command == "down")
                {
                    d += num;
                }
                else if (command == "up")
                {
                    d -= num;
                }
            }

            int result = x * d;

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input2-2.txt");
            int aim = 0;
            int d = 0;
            int x = 0;

            foreach (var line in lines)
            {
                string command = line.Split(' ')[0];
                int num = int.Parse(line.Split(' ')[1]);
                if (command == "forward")
                {
                    x += num;
                    d += aim * num;
                }
                else if (command == "down")
                {
                    aim += num;
                }
                else if (command == "up")
                {
                    aim -= num;
                }
            }

            int result = x * d;

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
