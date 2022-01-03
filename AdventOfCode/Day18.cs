using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day18
    {
        public static int Mag = 0;

        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input18-1.txt");

            Pair curPair = new Pair(0, null);

            int curInd = 1;
            curPair.Parse(lines[0], ref curInd);
            curPair.AssignParents();

            for (int i = 1; i < lines.Length; i++)
            {
                Pair newPair = new Pair(0, null);

                curInd = 1;
                newPair.Parse(lines[i], ref curInd);
                newPair.AssignParents();


                curPair = AddPairs(curPair, newPair);

                Reduct(curPair);

                List<(Pair, Pair, int)> stack = new List<(Pair, Pair, int)>();
                curPair.PutOnStack(stack, null, 0);
            }

            Console.WriteLine(curPair.CalcMagnitude());
            Console.ReadKey();
        }


        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input18-1.txt");

            int maxMag = int.MinValue;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (i == j)
                        continue;

                    Pair pair1 = new Pair(0, null);

                    int curInd = 1;
                    pair1.Parse(lines[i], ref curInd);
                    pair1.AssignParents();

                    Pair pair2 = new Pair(0, null);

                    curInd = 1;
                    pair2.Parse(lines[j], ref curInd);
                    pair2.AssignParents();


                    Pair pairSum = AddPairs(pair1, pair2);

                    Reduct(pairSum);
                    int mag = pairSum.CalcMagnitude();
                    if (mag > maxMag)
                        maxMag = mag;

                    if (i == 0 && j == 1)
                    {
                        List<(Pair, Pair, int)> stack = new List<(Pair, Pair, int)>();
                        pairSum.PutOnStack(stack, null, 0);
                    }

                }
            }

            Console.WriteLine(maxMag);
            Console.ReadKey();

        }

        private void Reduct(Pair curPair)
        {
            while (ExplodePossible(curPair) || SplitPossible(curPair))
            {
                if (ExplodePossible(curPair))
                {
                    Explode(ref curPair);
                    curPair.AssignParents();
                    continue;
                }

                if (SplitPossible(curPair))
                {
                    Split(curPair);
                    curPair.AssignParents();
                    continue;
                }
            }
        }

        private void Split(Pair curPair)
        {
            List<(Pair, Pair, int)> stack = new List<(Pair, Pair, int)>();
            curPair.PutOnStack(stack, null, 0);

            for (int i = 0; i < stack.Count; i++)
            {
                var pair = stack[i].Item1;
                if (pair.LeftValType == Type.VALUE && pair.LeftVal >= 10)
                {
                    pair.LeftValType = Type.PAIR;
                    Pair newPair = new Pair(pair.Level + 1, pair);
                    pair.LeftPair = newPair;
                    newPair.LeftValType = Type.VALUE;
                    newPair.RightValType = Type.VALUE;
                    newPair.LeftVal = (int)Math.Floor((double)pair.LeftVal / 2);
                    newPair.RightVal = (int)Math.Ceiling((double)pair.LeftVal / 2);

                    break;
                }

                if (pair.RightValType == Type.VALUE && pair.RightVal >= 10)
                {
                    pair.RightValType = Type.PAIR;
                    Pair newPair = new Pair(pair.Level + 1, pair);
                    pair.RightPair = newPair;
                    newPair.LeftValType = Type.VALUE;
                    newPair.RightValType = Type.VALUE;
                    newPair.LeftVal = (int)Math.Floor((double)pair.RightVal / 2);
                    newPair.RightVal = (int)Math.Ceiling((double)pair.RightVal / 2);

                    break;
                }
            }

        }

        private static void Explode(ref Pair curPair)
        {
            List<(Pair, Pair, int)> stack = new List<(Pair, Pair, int)>();
            curPair.PutOnStack(stack, null, 0);

            for (int i = 0; i < stack.Count; i++)
            {
                var pair = stack[i].Item1;
                if (pair.Level >= 4)
                {
                    if (i > 0)
                    {
                        var leftPair = stack[i - 1].Item1;
                        if (leftPair.RightValType == Type.VALUE)
                        {
                            leftPair.RightVal += pair.LeftVal;
                        }
                        else if (leftPair.LeftValType == Type.VALUE)
                        {
                            leftPair.LeftVal += pair.LeftVal;
                        }
                    }

                    if (i < stack.Count - 1)
                    {
                        var rightPair = stack[i + 1].Item1;
                        if (rightPair.LeftValType == Type.VALUE)
                        {
                            rightPair.LeftVal += pair.RightVal;
                        }
                        else if (rightPair.RightValType == Type.VALUE)
                        {
                            rightPair.RightVal += pair.RightVal;
                        }
                    }


                    var parent = pair.Parent;
                    if (stack[i].Item3 == 0)
                    {
                        parent.LeftValType = Type.VALUE;
                        parent.LeftVal = 0;
                    }
                    else
                    {
                        parent.RightValType = Type.VALUE;
                        parent.RightVal = 0;
                    }

                    break;
                }
            }
        }

        private static bool ExplodePossible(Pair curPair)
        {
            List<(Pair, Pair, int)> stack = new List<(Pair, Pair, int)>();
            curPair.PutOnStack(stack, null, 0);

            return stack.Count(p => p.Item1.Level >= 4) > 0;
        }

        private static bool SplitPossible(Pair curPair)
        {
            List<(Pair, Pair, int)> stack = new List<(Pair, Pair, int)>();
            curPair.PutOnStack(stack, null, 0);

            return stack.Count(p =>
            (p.Item1.LeftValType == Type.VALUE && p.Item1.LeftVal >= 10)
            || (p.Item1.RightValType == Type.VALUE && p.Item1.RightVal >= 10)) > 0;
        }


        private Pair AddPairs(Pair pair1, Pair pair2)
        {
            Pair newPair = new Pair(0, null);
            newPair.LeftValType = Type.PAIR;
            newPair.RightValType = Type.PAIR;
            pair1.IncreaseLevel();
            pair2.IncreaseLevel();
            pair1.Parent = newPair;
            pair2.Parent = newPair;
            newPair.LeftPair = pair1;
            newPair.RightPair = pair2;

            return newPair;
        }
    }

    public class Pair
    {
        public Type LeftValType;
        public Type RightValType;

        public int LeftVal;
        public int RightVal;

        public Pair LeftPair;
        public Pair RightPair;

        public int Level = 0;
        public Pair Parent;

        bool leftParsed = false;


        public Pair(int level, Pair parent)
        {
            Parent = parent;
            Level = level;
        }

        public void Parse(string line, ref int ind)
        {
            while (true)
            {
                char c = line[ind];
                if (c == '[')
                {
                    if (!leftParsed)
                    {
                        LeftValType = Type.PAIR;
                        LeftPair = new Pair(Level + 1, null);
                        ind++;
                        LeftPair.Parse(line, ref ind);
                    }
                    else
                    {
                        RightValType = Type.PAIR;
                        RightPair = new Pair(Level + 1, null);
                        ind++;
                        RightPair.Parse(line, ref ind);
                    }

                }
                else if (c == ',')
                {
                    leftParsed = true;
                    ind++;
                    continue;
                }
                else if (c == ']')
                {
                    ind++;
                    return;
                }
                else
                {
                    if (!leftParsed)
                    {
                        LeftValType = Type.VALUE;
                        LeftVal = int.Parse(c.ToString());
                        ind++;
                    }
                    else
                    {
                        RightValType = Type.VALUE;
                        RightVal = int.Parse(c.ToString());
                        ind++;
                    }
                }
            }

        }

        public void AssignParents()
        {
            if (LeftValType == Type.PAIR)
            {
                LeftPair.Parent = this;
                LeftPair.AssignParents();
            }

            if (RightValType == Type.PAIR)
            {
                RightPair.Parent = this;
                RightPair.AssignParents();
            }
        }

        internal void IncreaseLevel()
        {
            Level++;
            if (LeftValType == Type.PAIR)
                LeftPair.IncreaseLevel();
            if (RightValType == Type.PAIR)
                RightPair.IncreaseLevel();
        }

        internal void PutOnStack(List<(Pair, Pair, int)> stack, Pair parent, int side)
        {
            bool added = false;

            if (LeftValType == Type.VALUE)
            {
                stack.Add((this, parent, side));
                added = true;
            }
            else if (LeftValType == Type.PAIR)
            {
                LeftPair.PutOnStack(stack, this, 0);
            }

            if (RightValType == Type.VALUE && !added)
            {
                stack.Add((this, parent, side));
            }
            else if (RightValType == Type.PAIR)
            {
                RightPair.PutOnStack(stack, this, 1);
            }
        }

        public int CurMag = 0;

        internal int CalcMagnitude()
        {
            int mag = 0;

            if (LeftValType == Type.VALUE)
            {
                mag += LeftVal * 3;
            }

            if (RightValType == Type.VALUE)
            {
                mag += RightVal * 2;
            }

            if (LeftValType == Type.PAIR)
            {
                mag += 3 * LeftPair.CalcMagnitude();
            }

            if (RightValType == Type.PAIR)
            {
                mag += 2 * RightPair.CalcMagnitude();
            }

            return mag;



        }
    }

    public enum Type
    {
        VALUE,
        PAIR
    }
}
