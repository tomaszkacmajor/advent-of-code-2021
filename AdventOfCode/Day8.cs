using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day8
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input8-1.txt");
            List<string[]> digits = new List<string[]>();
            List<string[]> outputs = new List<string[]>();

            foreach (var line in lines)
            {
                string[] s = line.Split(new[] { "", " ", "|" }, StringSplitOptions.RemoveEmptyEntries);
                string[] nums = new string[10];
                string[] nums2 = new string[4];
                for (int i = 0; i < 10; i++)
                {
                    nums[i] = s[i];
                }
                digits.Add(nums);
                for (int i = 0; i < 4; i++)
                {
                    nums2[i] = s[i + 10];
                }
                outputs.Add(nums2);
            }

            int cnt = 0;
            foreach (var output in outputs)
            {
                foreach (var s in output)
                {
                    int l = s.Length;
                    if (l == 2 || l == 3 || l == 4 || l == 7)
                        cnt++;
                }
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input8-2.txt");
            int result = 0;

            List<string[]> digits = new List<string[]>();
            List<string[]> outputs = new List<string[]>();


            foreach (var line in lines)
            {
                string[] s = line.Split(new[] { "", " ", "|" }, StringSplitOptions.RemoveEmptyEntries);
                string[] nums = new string[10];
                string[] nums2 = new string[4];
                for (int i = 0; i < 10; i++)
                {
                    nums[i] = s[i];
                }
                digits.Add(nums);
                for (int i = 0; i < 4; i++)
                {
                    nums2[i] = s[i + 10];
                }
                outputs.Add(nums2);
            }

            for (int i = 0; i < digits.Count; i++)
            {
                char[] p = new char[2];
                char[] r = new char[1];
                char[] s = new char[2];
                char[] t = new char[2];

                Dictionary<string, int> mapping = new Dictionary<string, int>();

                foreach (var numeral in digits[i])
                {
                    if (numeral.Length == 2)
                    {
                        p = numeral.ToCharArray();

                        var numeralChar = numeral.ToCharArray();
                        Array.Sort(numeralChar, StringComparer.InvariantCulture);
                        mapping[new string(numeralChar)] = 1;
                    }

                }

                foreach (var numeral in digits[i])
                {
                    if (numeral.Length == 3)
                    {
                        foreach (var ch in numeral)
                        {
                            if (!p.Contains(ch))
                            {
                                r[0] = ch;
                            }
                        }

                        var numeralChar = numeral.ToCharArray();
                        Array.Sort(numeralChar, StringComparer.InvariantCulture);
                        mapping[new string(numeralChar)] = 7;
                    }
                }

                foreach (var numeral in digits[i])
                {
                    if (numeral.Length == 4)
                    {
                        int cnt = 0;
                        foreach (var ch in numeral)
                        {
                            if (!p.Contains(ch))
                            {
                                s[cnt] = ch;
                                cnt++;
                            }
                        }

                        var numeralChar = numeral.ToCharArray();
                        Array.Sort(numeralChar, StringComparer.InvariantCulture);
                        mapping[new string(numeralChar)] = 4;
                    }

                }

                foreach (var numeral in digits[i])
                {
                    if (numeral.Length == 7)
                    {
                        int cnt = 0;
                        foreach (var ch in numeral)
                        {
                            if (!p.Contains(ch) && !r.Contains(ch) && !s.Contains(ch))
                            {
                                t[cnt] = ch;
                                cnt++;
                            }
                        }

                        var numeralChar = numeral.ToCharArray();
                        Array.Sort(numeralChar, StringComparer.InvariantCulture);
                        mapping[new string(numeralChar)] = 8;
                    }

                }


                foreach (var numeral in digits[i])
                {
                    int l = numeral.Length;
                    if (!(l == 1 || l == 3 || l == 4 || l == 7))
                    {
                        bool includesP = Includes(p, numeral);
                        bool includesR = Includes(r, numeral);
                        bool includesS = Includes(s, numeral);
                        bool includesT = Includes(t, numeral);

                        var numeralChar = numeral.ToCharArray();
                        Array.Sort(numeralChar, StringComparer.InvariantCulture);

                        if (!includesP && includesR && !includesS && includesT)
                            mapping[new string(numeralChar)] = 2;

                        if (includesP && includesR && !includesS && !includesT)
                            mapping[new string(numeralChar)] = 3;

                        if (!includesP && includesR && includesS && !includesT)
                            mapping[new string(numeralChar)] = 5;

                        if (!includesP && includesR && includesS && includesT)
                            mapping[new string(numeralChar)] = 6;

                        if (includesP && includesR && includesS && !includesT)
                            mapping[new string(numeralChar)] = 9;

                        if (includesP && includesR && !includesS && includesT)
                            mapping[new string(numeralChar)] = 0;

                    }
                }


                var output = outputs[i];

                int res = 0;
                for (int j = 0; j < output.Length; j++)
                {
                    var numeralChar = output[j].ToCharArray();
                    Array.Sort(numeralChar, StringComparer.InvariantCulture);
                    int liczba = mapping[new string(numeralChar)];
                    res += liczba * (int)(Math.Pow(10, (3 - j)));
                }

                result += res;

            }

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static bool Includes(char[] p, string numeral)
        {
            bool includesP = true;
            foreach (var ch in p)
            {
                if (!numeral.Contains(ch))
                {
                    includesP = false;
                }
            }

            return includesP;
        }
    }
}
