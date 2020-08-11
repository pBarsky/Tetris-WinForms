using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEngine.Utilities
{
    public class ScoreboardManager
    {
        public List<Tuple<string, int>> Records { get; private set; }
        private const string FilePath = @".\scores.txt";

        public ScoreboardManager(bool readData = true)
        {
            if (readData)
            {
                RefreshData();
            }
        }

        private List<Tuple<string, int>> ReadScores(bool sorted = true)
        {
            var scores = new List<Tuple<string, int>>();
            try
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var keyVal = line.Split(':');
                    scores.Add(new Tuple<string, int>(keyVal[0], int.TryParse(keyVal[1], out int score) ? score : 0));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("COULD NOT OPEN SCORES FILE.");
                Console.WriteLine(e.Message);
            }

            if (scores.Count < 1000)
            {
                Seed(scores, 1000 - scores.Count);
            }
            if (!sorted)
            {
                return scores;
            }

            var scoreSorting = scores.OrderByDescending(row => row.Item2);
            return scoreSorting.ToList();
        }

        private static void Seed(ICollection<Tuple<string, int>> scores, int noOfRows)
        {
            for (var i = 0; i < noOfRows; i++)
            {
                scores.Add(RandomRecordGenerator.GenerateRandomRecord());
            }
            ScoreWriter.SaveScore(scores);
        }

        private void RefreshData()
        {
            Records = ReadScores();
        }
    }
}