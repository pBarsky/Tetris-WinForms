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
            scoreTextbox.Text = $@"{score}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReturnValue = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void InputDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}