using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine.Utilities;

namespace View
{
    public partial class ScoreboardForm : BasicForm
    {
        private ScoreboardManager _scoreboardManager = new ScoreboardManager();
        public ScoreboardForm()
        {
            InitializeComponent();
        }

        private void Scoreboard_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _scoreboardManager.Records;
            dataGridView1.Columns[0].HeaderText = "Player name";
            dataGridView1.Columns[1].HeaderText = "Score";
        }
    }
}
