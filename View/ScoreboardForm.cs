using GameEngine.Utilities;
using System;

namespace View
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
    }
}
