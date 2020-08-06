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
    public partial class MainMenu : BasicForm
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            var frm = new GameView { Location = this.Location, StartPosition = FormStartPosition.Manual };
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Want to exit?", "Exit prompt", MessageBoxButtons.YesNo) == DialogResult.Yes)
            if (YesNoDialog.ShowDialog("Want to exit?") == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void scoreboardButton_Click(object sender, EventArgs e)
        {

        }
    }
}
