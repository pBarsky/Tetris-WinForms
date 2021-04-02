using System;
using System.Windows.Forms;
using TetrisGame.Dialogs;

namespace TetrisGame.Views
{
    public partial class MainMenuView : BasicForm
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            ReplaceCurrentForm(new GameView());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Want to exit?", "Exit prompt", MessageBoxButtons.YesNo) == DialogResult.Yes)
            if (YesNoDialog.ShowDialog("Want to exit") == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void scoreboardButton_Click(object sender, EventArgs e)
        {
            ReplaceCurrentForm(new ScoreboardView());
        }

        private void ReplaceCurrentForm(Form form)
        {
            form.Location = Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { Show(); };
            form.Show();
            Hide();
        }
    }
}