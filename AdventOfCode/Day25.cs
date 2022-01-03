using System;

namespace AdventOfCode
{
    public class Day25
    {
       public void Solution1()
       {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input25-1.txt");

            var height = lines.Length;
            var width = lines[0].Length;
            char[,] tab = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tab[i, j] = lines[i][j];
                }
            }

            bool movePossible = true;
            int cnt = 0;
            char[,] newTab = new char[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    newTab[i, j] = tab[i, j];
                }
            }

            while (movePossible)
            {
                movePossible = false;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (tab[i,j] == '>')
                        {
                            var newJPos = (j + 1) % width;
                            if (tab[i,newJPos] == '.')
                            {
                                newTab[i, j] = '.';
                                newTab[i, newJPos] = '>';
                                movePossible = true;
                            }
                        }
                    }
                }

                tab = CopyTab(newTab);

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (tab[i, j] == 'v')
                        {
                            var newIPos = (i + 1) % height;
                            if (tab[newIPos, j] == '.')
                            {
                                newTab[i, j] = '.';
                                newTab[newIPos, j] = 'v';
                                movePossible = true;
                            }
                        }
                    }
                }

                tab = CopyTab(newTab);

                cnt++;
            }

            PrintTab(tab);

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        char[,] CopyTab(char[,] tab)
        {
            char[,] copiedTab = new char[tab.GetLength(0), tab.GetLength(1)];

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    copiedTab[i, j] = tab[i, j];
                }
            }
            return copiedTab;

        }

        void PrintTab(char[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    //Console.Write(tab[i,j]);
                }
                //Console.WriteLine();
            }
        }
    }
}
