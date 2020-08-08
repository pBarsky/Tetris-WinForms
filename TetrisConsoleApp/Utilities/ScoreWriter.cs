using System;
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
                SanitizeInput(ref name);
                streamWriter.WriteLine($"{name}: {score}");
            }
        }
        private void SanitizeInput(ref string input)
        {
            // Removing only ':' (as for now), because i split my records using it.
            input = input.Trim();
            input = input.Replace(":", "");
            if (input.Length > 16)
                input = input.Substring(0, 16);
        }
    }
}
