using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
