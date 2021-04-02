using System;
using GameEngine.Utilities;

namespace TetrisGame.Views
{
    public partial class ScoreboardView : BasicForm
    {
        private readonly ScoreboardManager _scoreboardManager = new ScoreboardManager();

        public ScoreboardView()
        {
            InitializeComponent();
        }

        private void Scoreboard_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _scoreboardManager.Records;
            dataGridView1.Columns[0].HeaderText = @"Player name";
            dataGridView1.Columns[1].HeaderText = @"Score";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}