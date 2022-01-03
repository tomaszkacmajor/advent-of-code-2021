using System;

namespace AdventOfCode
{
    public class Day20
    {
        string algoLine;
        int noSteps = 50;

        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input20-1.txt");
            int margin = 3 * noSteps;

            algoLine = lines[0];
            char[,] inputImg = new char[lines.Length - 2 + margin * 2, lines[2].Length + margin * 2];
            for (int i = 0; i < inputImg.GetLength(0); i++)
            {
                for (int j = 0; j < inputImg.GetLength(1); j++)
                {
                    inputImg[i, j] = '.';
                }
            }

            for (int i = 2; i < lines.Length; i++)
            {
                var line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    inputImg[i + margin - 2, j + margin] = line[j];
                }
            }

            var outputImg = Transform(inputImg);
            for (int i = 0; i < noSteps - 1; i++)
            {
                outputImg = Transform(outputImg);
            }


            int cnt = 0;
            for (int i = noSteps; i < outputImg.GetLength(0) - noSteps; i++)
            {
                for (int j = noSteps; j < outputImg.GetLength(1) - noSteps; j++)
                {
                    //Console.Write(outputImg[i, j]);
                    if (outputImg[i, j] == '#')
                        cnt++;
                }
                //Console.WriteLine();
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        private char[,] Transform(char[,] inputImg)
        {
            char[,] output = new char[inputImg.GetLength(0), inputImg.GetLength(1)];

            for (int i = 1; i < inputImg.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < inputImg.GetLength(1) - 1; j++)
                {
                    string chars =
                        inputImg[i - 1, j - 1].ToString() +
                        inputImg[i - 1, j].ToString() +
                        inputImg[i - 1, j + 1].ToString() +
                        inputImg[i, j - 1].ToString() +
                        inputImg[i, j].ToString() +
                        inputImg[i, j + 1].ToString() +
                        inputImg[i + 1, j - 1].ToString() +
                        inputImg[i + 1, j].ToString() +
                        inputImg[i + 1, j + 1].ToString();

                    string bin_strng = "";
                    foreach (var item in chars)
                    {
                        char num = item == '#' ? '1' : '0';
                        bin_strng += num.ToString();
                    }

                    int number = Convert.ToInt32(bin_strng, 2);

                    output[i, j] = algoLine[number];
                }
            }

            return output;
        }
    }
}
