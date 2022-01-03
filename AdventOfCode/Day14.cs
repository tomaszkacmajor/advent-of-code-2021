using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day14
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input14-1.txt");
            Dictionary<string, string> dict = new Dictionary<string, string>();

            string template = lines[0];

            for (int i = 2; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                dict[row[0]] = row[1];
            }

            string current = template;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < template.Length - 1; j++)
                {
                    var key = template[j].ToString() + template[j + 1].ToString();
                    var value = dict[key];

                    var index = j * 2 + 1;
                    current = current.Insert(index, value);
                }

                template = current;
            }

            string abc = "ABCDEFGHIJKLMNOPRSTUWXYZ";

            List<long> lst = new List<long>();
            foreach (var item in abc)
            {
                var b = Regex.Matches(current, item.ToString()).Count;
                if (b > 0)
                    lst.Add(b);
            }


            Console.WriteLine(lst.Max() - lst.Min());
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input14-1.txt");
            Dictionary<string, string> dict = new Dictionary<string, string>();

            string template = lines[0];

            for (int i = 2; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                dict[row[0]] = row[1];
            }


            Dictionary<string, long> occurences = new Dictionary<string, long>();

            for (int j = 0; j < template.Length - 1; j++)
            {
                string pair = template[j].ToString() + template[j + 1].ToString();
                if (occurences.ContainsKey(pair))
                    occurences[pair]++;
                else
                    occurences[pair] = 1;
            }

            Dictionary<string, long> temp = new Dictionary<string, long>();

            for (int i = 0; i < 40; i++)
            {
                foreach (KeyValuePair<string, long> entry in occurences)
                {
                    var letter = dict[entry.Key];
                    var newPair1 = entry.Key[0].ToString() + letter;
                    var newPair2 = letter + entry.Key[1].ToString();

                    if (temp.ContainsKey(newPair1))
                        temp[newPair1] += entry.Value;
                    else
                        temp[newPair1] = entry.Value;

                    if (temp.ContainsKey(newPair2))
                        temp[newPair2] += entry.Value;
                    else
                        temp[newPair2] = entry.Value;
                }

                occurences = temp;
                temp = new Dictionary<string, long>();
            }

            Dictionary<char, long> countDict = new Dictionary<char, long>();

            foreach (KeyValuePair<string, long> entry in occurences)
            {
                if (countDict.ContainsKey(entry.Key[0]))
                    countDict[entry.Key[0]] += entry.Value;
                else
                    countDict[entry.Key[0]] = entry.Value;

                if (countDict.ContainsKey(entry.Key[1]))
                    countDict[entry.Key[1]] += entry.Value;
                else
                    countDict[entry.Key[1]] = entry.Value;
            }

            List<long> lst = new List<long>();
            char firstChar = template[0];
            char lastChar = template[template.Length - 1];

            foreach (KeyValuePair<char, long> entry in countDict)
            {
                char c = entry.Key;
                if (c == firstChar || c == lastChar)
                {
                    lst.Add((entry.Value - 1) / 2 + 1);
                }
                else
                {
                    lst.Add(entry.Value / 2);
                }
            }

            Console.WriteLine(lst.Max() - lst.Min());
            Console.ReadKey();
        }
    }
}
