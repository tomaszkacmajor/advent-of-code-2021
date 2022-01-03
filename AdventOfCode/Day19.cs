using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day19
    {
        List<Neighbor>[] scannersCommonPointsTab = new List<Neighbor>[100];
        List<Scanner> scanners = new List<Scanner>();

        public void Solution1()
        {
            List<Point> allPoints = getAllPoints();
            Console.WriteLine(allPoints.Count());
            Console.ReadKey();
        }

        private List<Point> getAllPoints()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input19-1.txt");

            Scanner scanner = new Scanner();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("---"))
                {
                    scanner = new Scanner();
                    scanners.Add(scanner);
                }
                else
                {
                    var pointsStrTab = line.Split(',');
                    var x = int.Parse(pointsStrTab[0]);
                    var y = int.Parse(pointsStrTab[1]);
                    var z = int.Parse(pointsStrTab[2]);
                    scanner.AddPoint(new Point(x, y, z));
                }
            }

            foreach (var s in scanners)
            {
                s.CreateRelDistances();
            }

            for (int i = 0; i < scanners.Count(); i++)
            {
                scannersCommonPointsTab[i] = new List<Neighbor>();
            }

            for (int i = 0; i < scanners.Count(); i++)
            {
                for (int j = i + 1; j < scanners.Count(); j++)
                {
                    var scanner1 = scanners[i];
                    var scanner2 = scanners[j];

                    var combScanner1 = scanner1.RelDistancesCombinations[0];

                    for (int s2CombIndex = 0; s2CombIndex < scanner2.RelDistancesCombinations.Count(); s2CombIndex++)
                    {
                        var combScanner2 = scanner2.RelDistancesCombinations[s2CombIndex];

                        var intersected = combScanner1.Intersect(combScanner2).ToList();
                        var intersectCount = intersected.Count();
                        if (intersectCount >= 132)
                        {
                            var exemplaryRelativeDist = intersected[0];
                            var diff = exemplaryRelativeDist.Diff;
                            var rd1 = combScanner1.First(p => p.Diff.X == diff.X && p.Diff.Y == diff.Y && p.Diff.Z == diff.Z);
                            var rd2 = combScanner2.First(p => p.Diff.X == diff.X && p.Diff.Y == diff.Y && p.Diff.Z == diff.Z);

                            var diffPoint = new Point(rd2.Point1.X - rd1.Point1.X,
                                rd2.Point1.Y - rd1.Point1.Y,
                                rd2.Point1.Z - rd1.Point1.Z);

                            scannersCommonPointsTab[i].Add(new Neighbor(j, s2CombIndex, true, diffPoint));
                            scannersCommonPointsTab[j].Add(new Neighbor(i, s2CombIndex, false, diffPoint));

                            break;
                        }
                    }

                }
            }


            var allPoints = JoinPoints(0);

            allPoints = allPoints.OrderBy(p => p.X).ToList();
            return allPoints;
        }

        public void Solution2()
        {
            getAllPoints();

            var list = scanners[0].KnownScannersPositions;
            int max = int.MinValue;
            for (int i = 0; i < list.Count(); i++)
            {
                for (int j = 0; j < list.Count(); j++)
                {

                    var dist = calcDist(list[i], list[j]);
                    if (dist > max)
                        max = dist;
                }
            }

            int calcDist(Point p1, Point p2)
            {
                return Math.Abs(p1.X - p2.X) +
                    Math.Abs(p1.Y - p2.Y) +
                    Math.Abs(p1.Z - p2.Z);
            }

            Console.WriteLine(max);
            Console.ReadKey();
        }

        List<int> visited = new List<int>();

        List<Point> JoinPoints(int groupInd)
        {
            List<Point> groupPoints = new List<Point>();
            var scanner = scanners[groupInd];
            groupPoints = scanner.PointsCombinations[0];

            var neighbors = scannersCommonPointsTab[groupInd];

            visited.Add(groupInd);


            for (int i = 0; i < neighbors.Count(); i++)
            {
                var neighbor = neighbors[i];
                var neighborId = neighbor.Index;

                if (visited.Contains(neighborId))
                    continue;

                visited.Add(neighborId);
                var neighborScanner = scanners[neighborId];

                List<Point> neighborPoints = new List<Point>();
                var neighborNeighbors = scannersCommonPointsTab[neighborId];

                if (neighborNeighbors.Where(p => !visited.Contains(p.Index)).Count() >= 1)
                {
                    neighborScanner.PointsCombinations[0] = JoinPoints(neighborId);
                }

                var shift = neighbor.Diff;

                if (neighbor.ForwardDir)
                {
                    foreach (var p in neighborScanner.PointsCombinations[0])
                    {
                        var transformedP = Transform(p, neighbor.TransformInd, true);
                        var newPoint = new Point(transformedP.X - shift.X, transformedP.Y - shift.Y, transformedP.Z - shift.Z);
                        neighborPoints.Add(newPoint);
                    }

                    foreach (var p in neighborScanner.KnownScannersPositions)
                    {
                        var transformedP = Transform(p, neighbor.TransformInd, true);
                        var newPoint = new Point(transformedP.X - shift.X, transformedP.Y - shift.Y, transformedP.Z - shift.Z);
                        scanner.KnownScannersPositions.Add(newPoint);
                    }
                }
                else
                {
                    foreach (var p in neighborScanner.PointsCombinations[0])
                    {
                        var newPoint = new Point(p.X + shift.X, p.Y + shift.Y, p.Z + shift.Z);
                        newPoint = Transform(newPoint, neighbor.TransformInd, false);
                        neighborPoints.Add(newPoint);
                    }

                    foreach (var p in neighborScanner.KnownScannersPositions)
                    {
                        var newPoint = new Point(p.X + shift.X, p.Y + shift.Y, p.Z + shift.Z);
                        newPoint = Transform(newPoint, neighbor.TransformInd, false);
                        scanner.KnownScannersPositions.Add(newPoint);
                    }
                }

                var intersected = groupPoints.Intersect(neighborPoints).ToList();
                groupPoints = groupPoints.Union(neighborPoints).ToList();
            }

            return groupPoints;
        }

        class Neighbor
        {
            public int Index;
            public int TransformInd;
            public bool ForwardDir;
            public Point Diff;

            public Neighbor(int index, int transformId, bool forwardDir, Point diff)
            {
                Index = index;
                TransformInd = transformId;
                ForwardDir = forwardDir;
                Diff = diff;
            }
        }


        public class Point
        {
            public int X;
            public int Y;
            public int Z;

            public Point(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public override int GetHashCode()
            {
                return this.X * 101 + Y * 99 + Z * 107;
            }

            public override bool Equals(object other)
            {
                if (other is Point)
                    return ((Point)other).X == this.X
                        && ((Point)other).Y == this.Y
                        && ((Point)other).Z == this.Z;
                return false;
            }
        }

        class TwoPointsRelation
        {
            public Point Point1;
            public Point Point2;
            public Point Diff;

            public TwoPointsRelation(Point p1, Point p2)
            {
                Point1 = p1;
                Point2 = p2;
                Diff = new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
            }

            public override int GetHashCode()
            {
                return Diff.X * 1001 + Diff.Y * 27 + Diff.Z * 89;
            }

            public override bool Equals(object other)
            {
                if (other is TwoPointsRelation)
                    return ((TwoPointsRelation)other).Diff.X == this.Diff.X
                        && ((TwoPointsRelation)other).Diff.Y == this.Diff.Y
                        && ((TwoPointsRelation)other).Diff.Z == this.Diff.Z;
                return false;
            }
        }


        class Scanner
        {
            public List<List<Point>> PointsCombinations = new List<List<Point>>();
            public List<List<TwoPointsRelation>> RelDistancesCombinations = new List<List<TwoPointsRelation>>();
            public List<Point> KnownScannersPositions = new List<Point>();

            public Scanner()
            {
                for (int i = 0; i < 24; i++)
                {
                    PointsCombinations.Add(new List<Point>());
                }

                KnownScannersPositions.Add(new Point(0, 0, 0));
            }

            public void AddPoint(Point point)
            {
                var combs = getAllCombinations(point);
                int iter = 0;
                foreach (var combination in PointsCombinations)
                {
                    combination.Add(combs[iter]);
                    iter++;
                }
            }

            public void CreateRelDistances()
            {
                foreach (var comb in PointsCombinations)
                {
                    List<TwoPointsRelation> RelDistances = new List<TwoPointsRelation>();

                    for (int i = 0; i < comb.Count(); i++)
                    {
                        for (int j = 0; j < comb.Count(); j++)
                        {
                            if (i == j)
                                continue;

                            var p1 = comb[i];
                            var p2 = comb[j];
                            RelDistances.Add(new TwoPointsRelation(p1, p2));
                        }
                    }

                    RelDistancesCombinations.Add(RelDistances);
                }
            }



            List<Point> getAllCombinations(Point point)
            {
                List<Point> comb = new List<Point>();

                for (int i = 0; i < 24; i++)
                {
                    comb.Add(Transform(point, i, true));
                }

                return comb;
            }
        }


        public static Point Transform(Point point, int index, bool forward)
        {
            int x = point.X;
            int y = point.Y;
            int z = point.Z;

            switch (index)
            {
                case 0:
                    return point;
                case 1:
                    {
                        if (forward)
                            return new Point(x, -y, -z);
                        else
                            return new Point(x, -y, -z);
                    }
                case 2:
                    {
                        if (forward)
                            return new Point(x, z, -y);
                        else
                            return new Point(x, -z, y);
                    }
                case 3:
                    {
                        if (forward)
                            return new Point(x, -z, y);
                        else
                            return new Point(x, z, -y);
                    }
                case 4:
                    {
                        if (forward)
                            return new Point(-y, x, z);
                        else
                            return new Point(y, -x, z);
                    }
                case 5:
                    {
                        if (forward)
                            return new Point(y, x, -z);
                        else
                            return new Point(y, x, -z);
                    }
                case 6:
                    {
                        if (forward)
                            return new Point(z, x, y);
                        else
                            return new Point(y, z, x);
                    }
                case 7:
                    {
                        if (forward)
                            return new Point(-z, x, -y);
                        else
                            return new Point(y, -z, -x);
                    }
                case 8:
                    {
                        if (forward)
                            return new Point(-x, y, -z);
                        else
                            return new Point(-x, y, -z);
                    }
                case 9:
                    {
                        if (forward)
                            return new Point(-x, -y, z);
                        else
                            return new Point(-x, -y, z);
                    }
                case 10:
                    {
                        if (forward)
                            return new Point(-x, -z, -y);
                        else
                            return new Point(-x, -z, -y);
                    }
                case 11:
                    {
                        if (forward)
                            return new Point(-x, z, y);
                        else
                            return new Point(-x, z, y);
                    }
                case 12:
                    {
                        if (forward)
                            return new Point(y, -x, z);
                        else
                            return new Point(-y, x, z);
                    }
                case 13:
                    {
                        if (forward)
                            return new Point(-y, -x, -z);
                        else
                            return new Point(-y, -x, -z);
                    }
                case 14:
                    {
                        if (forward)
                            return new Point(-z, -x, y);
                        else
                            return new Point(-y, z, -x);
                    }
                case 15:
                    {
                        if (forward)
                            return new Point(z, -x, -y);
                        else
                            return new Point(-y, -z, x);
                    }
                case 16:
                    {
                        if (forward)
                            return new Point(-z, y, x);
                        else
                            return new Point(z, y, -x);
                    }
                case 17:
                    {
                        if (forward)
                            return new Point(z, -y, x);
                        else
                            return new Point(z, -y, x);
                    }
                case 18:
                    {
                        if (forward)
                            return new Point(-y, -z, x);
                        else
                            return new Point(z, -x, -y);
                    }
                case 19:
                    {
                        if (forward)
                            return new Point(y, z, x);
                        else
                            return new Point(z, x, y);
                    }
                case 20:
                    {
                        if (forward)
                            return new Point(y, -z, -x);
                        else
                            return new Point(-z, x, -y);
                    }
                case 21:
                    {
                        if (forward)
                            return new Point(-y, z, -x);
                        else
                            return new Point(-z, -x, y);
                    }
                case 22:
                    {
                        if (forward)
                            return new Point(-z, -y, -x);
                        else
                            return new Point(-z, -y, -x);
                    }
                case 23:
                    {
                        if (forward)
                            return new Point(z, y, -x);
                        else
                            return new Point(-z, y, x);
                    }
                default:
                    return point;

            }
        }
    }


}
