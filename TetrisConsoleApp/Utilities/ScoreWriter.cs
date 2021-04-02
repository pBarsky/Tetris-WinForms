using System;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Utilities
{
    public class ScoreWriter
    {
        private const string FilePath = @".\scores.txt";

        public void SaveScore(string name, int score)
        {
            using (var streamWriter = new StreamWriter(FilePath, true))
            {
                streamWriter.WriteLine($"{SanitizeInput(name)}: {score}");
            }
        }

        public static void SaveScore(ICollection<Record> records)
        {
            using (var streamWriter = new StreamWriter(FilePath, true))
            {
                foreach (var (name, score) in records)
                {
                    streamWriter.WriteLine($"{SanitizeInput(name)}: {score}");
                }
            }
        }

        private static string SanitizeInput(string input)
        {
            // Removing only ':' (as for now), because i split my records using it.
            input = input.Trim();
            input = input.Replace(":", "");
            return input.Length > 16 ? input.Substring(0, 16) : input;
        }
    }
}