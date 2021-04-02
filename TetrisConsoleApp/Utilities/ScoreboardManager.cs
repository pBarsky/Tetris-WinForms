using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEngine.Utilities
{
    public struct Record
    {
        public string Name;
        public int Score;

        public void Deconstruct(out string name, out int score)
        {
            name = Name;
            score = Score;
        }
    }

    public class ScoreboardManager
    {
        public List<Record> Records { get; private set; }
        private const string FilePath = @".\scores.txt";

        public ScoreboardManager(bool readData = true)
        {
            if (readData)
            {
                RefreshData();
            }
        }

        private static List<Record> ReadScores(bool sorted = true)
        {
            var scores = new List<Record>();
            try
            {
                ReadScoresFromFile(scores);
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

            return !sorted ? scores : scores.OrderByDescending(row => row.Score).ToList();
        }

        private static void ReadScoresFromFile(ICollection<Record> scores)
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parsedRecord = ParseLine(line);
                scores.Add(parsedRecord);
            }
        }

        private static Record ParseLine(string line)
        {
            var keyVal = line.Split(':');
            var parsedScore = ParseRawScore(keyVal[1]);
            return new Record { Name = keyVal[0], Score = parsedScore };
        }

        private static int ParseRawScore(string value)
        {
            return int.TryParse(value, out var rawScore) ? rawScore : 0;
        }

        private static void Seed(ICollection<Record> scores, int noOfRows)
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