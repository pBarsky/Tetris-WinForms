using System;

namespace GameEngine.Utilities
{
    public static class RandomRecordGenerator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        private static readonly string[] Names =
        {
            "Sachi", "Derwood", "Zug", "Casey", "Goggin", "Vanni", "Gould", "Centeno", "Janette", "Eliseo", "Nevlin",
            "Cogswell", "Nemhauser", "Belmonte", "Carmelia", "Madai", "Sewell", "Samantha", "Linker", "Burnett",
            "Cherian", "Philips", "Barger", "Mitch", "Franni", "Martelli", "Alisa", "Romona", "Aniela", "Hayton",
            "Piselli", "Guillermo", "Shamus", "Britt", "Wickham", "Hepsiba", "Boucher", "Reis", "Gilead", "Cerellia",
            "Coulter", "Martyn", "Dominick", "Kathye", "Schulz", "Carolynn", "Grand", "Kirstyn", "Bury", "Placido",
            "Gilpin", "Gunner", "Bonina", "Yul", "Brigida", "Gracie", "Langham", "Jurgen", "Alli", "Leslie",
            "Geraldina", "Fauver", "Karissa", "Drue", "Guerin", "Service", "Warchaw", "Saval", "Sirkin", "Irby",
            "Clementius", "Cowan", "Lira", "Natascha", "Cherise", "Landry", "Naldo", "Mohun", "Craggy", "Dream",
            "Tolmann", "Albur", "Julianna", "Rastus", "Manlove", "Iona", "Francoise", "Tristan", "Henricks", "Blaise",
            "Domela", "Bryn", "Demetrius", "Prent", "Luhey", "Dorise", "Anselm", "Afton", "Sutphin", "Irina", "Laurita",
            "Latimore", "Xanthe", "Lala", "Pengelly", "Magdaia", "Nicolau", "Tali", "Solitta", "Jeavons", "Morgun",
            "Elkin", "Ressler", "Jim", "Jacey", "Edward", "Maura", "LeRoy", "Vargas", "Tullius", "Moazami", "Pleasant",
            "Selemas", "Mullen", "Daniele", "Lat", "Winton", "Mirabelle", "Meill", "Hera", "Pammie", "Adur", "Danforth",
            "Orban", "Yorgen", "Yoshiko", "Sabra", "Magee", "Skantze", "Silden", "Rocray", "Casabonne", "Khosrow",
            "Ludwog", "Dielu", "Prowel", "Kimberly", "Jakob", "Mast", "Trula", "Agnola", "Pleasant", "Sokul", "Robins",
            "Korwin", "Trillby", "Fallon", "Oralee", "Meijer", "Ori", "Iny", "Torruella", "Oswell", "Klinges", "JoAnn",
            "Chuah", "Donaldson", "Barrie", "Pessa", "Harvey", "Bernadette", "Nguyen", "Zilvia", "Aun", "O'Neill",
            "Brocky", "Fisk", "Berard", "Tedder", "Alexandr", "Stich", "Hazeghi", "Tarrel", "Riorsson", "Ferriter",
            "Welby", "Lodmilla", "Petuu", "Scheider", "Rhea", "Artamas", "Leggat", "Glenda", "Duleba", "Lorollas",
            "Katzir", "Cyndia", "Greff", "Orion", "Almeria"
        };

        public static Tuple<string, int> GenerateRandomRecord()
        {
            var name = Names[Random.Next(Names.Length)];
            var playerNo = Random.Next(100);
            var score = Random.Next(2000);
            return new Tuple<string, int>($"{name}{playerNo}", score);
        }
    }
}