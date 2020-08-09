using System;
using GameEngine.Utilities;
using View;

namespace TetrisGame
{
    public partial class ScoreboardForm : BasicForm
    {
        private readonly ScoreboardManager _scoreboardManager = new ScoreboardManager();
        public ScoreboardForm()
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
