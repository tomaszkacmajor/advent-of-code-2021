using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day10
    {
        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input10-1.txt");

            Dictionary<char, int> dict = new Dictionary<char, int>();
            dict[')'] = 3;
            dict[']'] = 57;
            dict['}'] = 1197;
            dict['>'] = 25137;

            Dictionary<char, int> dict2 = new Dictionary<char, int>();
            dict2[')'] = 1;
            dict2[']'] = 2;
            dict2['}'] = 3;
            dict2['>'] = 4;

            List<char> heap = new List<char>();
            List<char> leftChars = new List<char>();
            List<char> rightChars = new List<char>();

            leftChars.Add('(');
            leftChars.Add('[');
            leftChars.Add('{');
            leftChars.Add('<');

            rightChars.Add('}');
            rightChars.Add(']');
            rightChars.Add('}');
            rightChars.Add('>');

            Dictionary<char, char> leftToRight = new Dictionary<char, char>();
            leftToRight['('] = ')';
            leftToRight['{'] = '}';
            leftToRight['['] = ']';
            leftToRight['<'] = '>';

            List<long> scores = new List<long>();

            foreach (var line in lines)
            {
                heap = new List<char>();
                bool corrupted = false;

                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    if (leftChars.Contains(c))
                    {
                        heap.Add(c);
                    }
                    else
                    {
                        if (leftToRight[heap[heap.Count - 1]] == c)
                        {
                            heap.RemoveAt(heap.Count - 1);
                        }
                        else
                        {
                            corrupted = true;
                            break;
                        }
                    }
                }

                if (!corrupted)
                {
                    heap.Reverse();
                    long score = 0;
                    foreach (var c in heap)
                    {
                        score = score * 5 + dict2[leftToRight[c]];
                    }
                    scores.Add(score);
                }
            }

            scores.Sort();
            int ind = (scores.Count - 1) / 2;


            Console.WriteLine(scores[ind]);
            Console.ReadKey();
        }
    }
}
