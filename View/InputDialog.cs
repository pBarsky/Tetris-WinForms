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
        public InputDialog()
        {
            InitializeComponent();
            AcceptButton = button1;
        }

        public static string ShowDialog(int score)
        {
            InputDialog form = new InputDialog();
            form.Show();
            return form.DialogResult == DialogResult.OK ? form.textBox1.Text : string.Empty;
        }
    }
}
