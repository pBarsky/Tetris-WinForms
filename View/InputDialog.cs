using System;
using System.Windows.Forms;

namespace View
{
    public partial class InputDialog : BasicForm
    {
        public string ReturnValue { get; private set; }
        public InputDialog(int score)
        {
            InitializeComponent();
            congratulationsLabel.Text = $@"You've scored {score} points!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReturnValue = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
