using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day22
    {
       public void Solution1()
       {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input22-1.txt");
            List<Range> ranges = new List<Range>();
            HashSet<Point3D> set = new HashSet<Point3D>(); 

            foreach (var line in lines)
            {
                var range = new Range();
                string[] row = line.Split(new[] { " x=", "..", ",y=", ",z=" }, StringSplitOptions.RemoveEmptyEntries);
                range.Parse(row);
                ranges.Add(range);
            }


            foreach (var range in ranges)
            {
                for (int x = range.X1; x <= range.X2; x++)
                {
                    for (int y = range.Y1; y <= range.Y2; y++)
                    {
                        for (int z = range.Z1; z <= range.Z2; z++)
                        {
                            if (x < -50 || x > 50 || y < -50 || y > 50 || z < -50 || z > 50)
                                continue;

                            var point = new Point3D() { X = x, Y = y, Z = z };
                           
                            if (range.On)
                            {
                                if (!set.Contains(point))
                                    set.Add(point);
                            }
                            else
                            {
                                if (set.Contains(point))
                                    set.Remove(point);
                            }
                        }
                    }
                }
            }


            Console.WriteLine(set.Count());
            Console.ReadKey();
        }
    }

    internal class Point3D
    {
        public int X;
        public int Y;
        public int Z;

        public override int GetHashCode()
        {
            return this.X * 101 + Y * 99 + Z * 107;
        }

        public override bool Equals(object other)
        {
            if (other is Point3D)
                return ((Point3D)other).X == this.X
                    && ((Point3D)other).Y == this.Y
                    && ((Point3D)other).Z == this.Z;
            return false;
        }
    }

    public class Range
    {
        public bool On;
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;
        public int Z1;
        public int Z2;

        public void Parse(string[] row)
        {
            On = row[0] == "on" ? true : false;
            X1 = int.Parse(row[1]);
            X2 = int.Parse(row[2]);
            Y1 = int.Parse(row[3]);
            Y2 = int.Parse(row[4]);
            Z1 = int.Parse(row[5]);
            Z2 = int.Parse(row[6]);

            if (X1 < -50)
                X1 = -50;
            if (X2 > 50)
                X2 = 50;
            if (Y1 < -50)
                Y1 = -50;
            if (Y2 > 50)
                Y2 = 50;
            if (Z1 < -50)
                Z1 = -50;
            if (Z2 > 50)
                Z2 = 50;
        }
    }
}
