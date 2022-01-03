using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day12
    {
        Dictionary<string, List<string>> nodes = new Dictionary<string, List<string>>();

        List<List<string>> results = new List<List<string>>();

       public void Solution2()
       {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input12-1.txt");

            foreach (var line in lines)
            {
                var nodesStr = line.Split('-');
                string a = nodesStr[0];
                string b = nodesStr[1];
                if (!nodes.ContainsKey(a))
                {
                    List<string> list = new List<string>();
                    list.Add(b);
                    nodes[a] = list;
                } else
                {
                    nodes[a].Add(b);
                }

                if (!nodes.ContainsKey(b))
                {
                    List<string> list = new List<string>();
                    list.Add(a);
                    nodes[b] = list;
                }
                else
                {
                    nodes[b].Add(a);
                }
            }

            Search("start", new List<string>(), true);

            

            Console.WriteLine(results.Count());
            Console.ReadKey();
        }

        void Search(string node, List<string> path, bool smallCaveTwicePermission)
        {
            var localPath = new List<string>(path);
            localPath.Add(node);
            if (localPath.Count > 1 && node == "start")
            {
                return;
            }

            if (localPath.Count > 1 && node == "end")
            {
                results.Add(path);
                return;
            }

            foreach (var neighbor in nodes[node])
            {
                if (neighbor == "start")
                    continue;

                var permissionToPass = smallCaveTwicePermission;

                if (!IsAllUpper(neighbor) && localPath.Contains(neighbor))
                {
                    if (!smallCaveTwicePermission)
                    {
                        continue;
                    }
                    else
                    {
                        permissionToPass = false;
                    }
                   
                }

                Search(neighbor, localPath, permissionToPass);
            }
        }

        bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }
    }
}
