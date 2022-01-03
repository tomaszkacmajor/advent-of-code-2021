using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day16
    {
        public void Solution2()
        {
            string line = System.IO.File.ReadAllLines(@"..\..\inputs\input16-1.txt")[0];

            string bits = "";
            foreach (var item in line)
            {
                string hex = item.ToString();
                int val = Convert.ToInt32(hex, 16);
                string binRepresentation = Convert.ToString(val, 2).PadLeft(4, '0');
                bits += binRepresentation;
            }

            int i = 0;
            Packet root = new Packet();
            root.ParsePacket(bits, ref i);

            Console.WriteLine(root.GetResult());
            Console.ReadKey();
        }

    }

    class Packet
    {
        public int Version;
        public string Type = "";  //literal or operator
        public long Literal;
        public int TypeId;
        public List<Packet> Subpackets = new List<Packet>();

        public int getSumVersions()
        {
            int sum = Version;
            foreach (var item in Subpackets)
            {
                sum += item.getSumVersions();
            }
            return sum;
        }

        public void ParsePacket(string bits, ref int i)
        {
            Version = ParseBits(bits, ref i, 3);
            int typeId = ParseBits(bits, ref i, 3);
            TypeId = typeId;

            if (typeId == 4)
            {
                //literal
                Type = "literal";
                string literal = "";
                while (bits[i].ToString() == "1")
                {
                    i += 1;
                    for (int j = 0; j < 4; j++)
                    {
                        literal += bits[i + j];
                    }
                    i += 4;
                }

                i += 1;
                for (int j = 0; j < 4; j++)
                {
                    literal += bits[i + j];
                }
                i += 4;

                Literal = Convert.ToInt64(literal, 2);
            }
            else
            {
                //operator
                Type = "operator";
                string lengthType = bits[i].ToString();
                i++;
                if (lengthType == "0")
                {
                    int totalLength = ParseBits(bits, ref i, 15);
                    int expectedEnd = i + totalLength;

                    while (expectedEnd - i > 6)
                    {
                        Packet newPacket = new Packet();
                        newPacket.ParsePacket(bits, ref i);
                        Subpackets.Add(newPacket);
                    }

                }
                else
                {
                    int numSubpackets = ParseBits(bits, ref i, 11);
                    for (int s = 0; s < numSubpackets; s++)
                    {
                        Packet newPacket = new Packet();
                        newPacket.ParsePacket(bits, ref i);
                        Subpackets.Add(newPacket);
                    }
                }
            }
        }


        private static int ParseBits(string bits, ref int i, int len)
        {
            string str = "";
            for (int k = 0; k < len; k++)
            {
                str += bits[i + k].ToString();
            }

            i += len;

            return Convert.ToInt32(str, 2);
        }

        public long GetResult()
        {
            if (Type == "literal")
            {
                return Literal;
            }
            else
            {
                switch (TypeId)
                {
                    case 0:
                        {
                            long sum = 0;
                            foreach (var item in Subpackets)
                            {
                                sum += item.GetResult();
                            }
                            return sum;
                        }
                    case 1:
                        {
                            long mul = 1;
                            foreach (var item in Subpackets)
                            {
                                mul *= item.GetResult();
                            }
                            return mul;
                        }
                    case 2:
                        {
                            long min = long.MaxValue;
                            foreach (var item in Subpackets)
                            {
                                var res = item.GetResult();
                                if (res < min)
                                    min = res;
                            }
                            return min;
                        }
                    case 3:
                        {
                            long max = long.MinValue;
                            foreach (var item in Subpackets)
                            {
                                var res = item.GetResult();
                                if (res > max)
                                    max = res;
                            }
                            return max;
                        }
                    case 5:
                        {
                            var pak1 = Subpackets[0];
                            var pak2 = Subpackets[1];
                            if (pak1.GetResult() > pak2.GetResult())
                                return 1;
                            else
                                return 0;
                        }
                    case 6:
                        {
                            var pak1 = Subpackets[0];
                            var pak2 = Subpackets[1];
                            if (pak1.GetResult() < pak2.GetResult())
                                return 1;
                            else
                                return 0;
                        }
                    case 7:
                        {
                            var pak1 = Subpackets[0];
                            var pak2 = Subpackets[1];
                            if (pak1.GetResult() == pak2.GetResult())
                                return 1;
                            else
                                return 0;
                        }
                    default: return 0;
                }
            }
        }
    }
}
