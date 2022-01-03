using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day21
    {
        public void Solution1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input21-1.txt");
            int result = 0;

            List<Player> players = new List<Player>();

            foreach (var line in lines)
            {
                string[] row = line.Split(new[] { "Player ", " starting position: " }, StringSplitOptions.RemoveEmptyEntries);

                players.Add(new Player()
                {
                    Index = int.Parse(row[0]),
                    Position = int.Parse(row[1])
                });
            }

            int rolledCnt = 0;
            int noRolls = 3;

            while (!players.Exists(p => p.Score >= 1000))
            {
                foreach (var player in players)
                {
                    for (int i = 0; i < noRolls; i++)
                    {
                        player.Position += getDiceScore();
                        player.Position = ((player.Position - 1) % 10) + 1;

                        rolledCnt++;
                    }

                    player.Score += player.Position;
                    if (player.Score >= 1000)
                    {
                        break;
                    }
                }
            }

            var losingPlayer = players.First(p => p.Score < 1000);

            result = rolledCnt * losingPlayer.Score;

            Console.WriteLine(result);
            Console.ReadKey();
        }

        static int diceNum = 0;
        int getDiceScore()
        {
            diceNum++;
            diceNum = ((diceNum - 1) % 100) + 1;
            return diceNum;
        }


        public void Solution2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\inputs\input21-1.txt");

            List<Player> initialPlayers = new List<Player>();

            foreach (var line in lines)
            {
                string[] row = line.Split(new[] { "Player ", " starting position: " }, StringSplitOptions.RemoveEmptyEntries);

                initialPlayers.Add(new Player()
                {
                    Index = int.Parse(row[0]),
                    Position = int.Parse(row[1])
                });
            }

            List<GameState> states = new List<GameState>();
            states.Add(new GameState()
            {
                CurPlayerInd = 1,
                ThrowNumber = 0,
                Players = initialPlayers
            });

            Dictionary<int, int> diceFreqs = new Dictionary<int, int>();
            diceFreqs[3] = 1;
            diceFreqs[4] = 3;
            diceFreqs[5] = 6;
            diceFreqs[6] = 7;
            diceFreqs[7] = 6;
            diceFreqs[8] = 3;
            diceFreqs[9] = 1;


            while (states.Count() > 0)
            {
                var state = states[states.Count() - 1];
                state.ChangePlayer();

                var player = state.Players[state.CurPlayerInd];

                for (int diceScore = 3; diceScore <= 9; diceScore++)
                {
                    int freq = diceFreqs[diceScore];

                    int tempPos = player.Position;
                    int tempScore = player.Score;

                    player.Position += diceScore;
                    player.Position = ((player.Position - 1) % 10) + 1;
                    player.Score += player.Position;

                    if (player.Score >= 21)
                    {
                        if (player.Index == 1)
                            WinsCnt1+= freq * state.NumSameStates;
                        else
                            WinsCnt2+= freq;
                    }
                    else
                    {
                        List<Player> newPlayers = new List<Player>();
                        newPlayers.Add(new Player()
                        {
                            Position = state.Players[0].Position,
                            Score = state.Players[0].Score,
                            Index = 1,
                        });
                        newPlayers.Add(new Player()
                        {
                            Position = state.Players[1].Position,
                            Score = state.Players[1].Score,
                            Index = 2,
                        });

                        states.Add(new GameState()
                        {
                            CurPlayerInd = state.CurPlayerInd,
                            ThrowNumber = state.ThrowNumber,
                            Players = newPlayers,
                            NumSameStates = freq * state.NumSameStates
                        });

                    }

                    player.Score = tempScore;
                    player.Position = tempPos;

                }

                states.Remove(state);

            }


            long minWins = WinsCnt1 > WinsCnt2 ? WinsCnt1 : WinsCnt2;

            Console.WriteLine(minWins);
            Console.ReadKey();
        }

        public long WinsCnt1;
        public long WinsCnt2;

    }

    public class GameState
    {
        public int CurPlayerInd = 1;
        public int ThrowNumber;
        public List<Player> Players = new List<Player>();
        public long NumSameStates = 1;

        internal void ChangePlayer()
        {
            CurPlayerInd = CurPlayerInd == 0 ? 1 : 0;
        }

        public void IncreaseThrowNo()
        {
            ThrowNumber++;
            ThrowNumber = ThrowNumber % 3;
        }
    }


    public class Player
    {
        public int Score;
        public int Position;
        public int Index;
        public int ThrowNo;
    }

}

