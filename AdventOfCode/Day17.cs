using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day17
    {
        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input17-1.txt");

            string[] row = lines[0].Split(new[] { "target area: x=", "..", ", y=" }, StringSplitOptions.RemoveEmptyEntries);
            int x1 = int.Parse(row[0]);
            int x2 = int.Parse(row[1]);
            int y1 = int.Parse(row[2]);
            int y2 = int.Parse(row[3]);

            int xMin = x1 < x2 ? x1 : x2;
            int yMin = y1 < y2 ? y1 : y2;
            int xMax = x1 > x2 ? x1 : x2;
            int yMax = y1 > y2 ? y1 : y2;
            Point areaCorner1 = new Point(xMin, yMax);
            Point areaCorner2 = new Point(xMax, yMin);

            List<Point> points = new List<Point>();

            for (int i = -5000; i < 5000; i++)
            {
                for (int j = -5000; j < 5000; j++)
                {
                    var vel = new Point(i, j);
                    var res = ReachesTarget(new Point(0, 0), vel, areaCorner1, areaCorner2);
                    if (res.Item1)
                    {
                        if (!points.Contains(vel))
                        {
                            points.Add(vel);
                        }
                    }
                }
            }

            Console.WriteLine(points.Count());
            Console.ReadKey();
        }

        Point GetNewPos(Point curPos, ref Point curVel)
        {
            var newPoint = new Point(curPos.X + curVel.X, curPos.Y + curVel.Y);

            if (curVel.X > 0)
                curVel.X--;
            else if (curVel.X < 0)
                curVel.X++;

            curVel.Y--;

            return newPoint;
        }

        (bool, int) ReachesTarget(Point curPos, Point vel, Point areaCorner1, Point areaCorner2)
        {
            bool result = false;
            int maxHeight = int.MinValue;

            while ((vel.X >= 0 && curPos.X < areaCorner2.X && curPos.Y > areaCorner2.Y)
                || (vel.X <= 0 && curPos.X > areaCorner1.X && curPos.Y > areaCorner2.Y))
            {
                curPos = GetNewPos(curPos, ref vel);

                if (curPos.Y > maxHeight)
                {
                    maxHeight = curPos.Y;
                }

                if (curPos.X >= areaCorner1.X
                    && curPos.X <= areaCorner2.X
                    && curPos.Y <= areaCorner1.Y
                    && curPos.Y >= areaCorner2.Y)
                {
                    return (true, maxHeight);
                }
            }

            return (result, maxHeight);
        }
    }

    class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
