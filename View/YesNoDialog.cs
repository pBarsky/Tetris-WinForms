using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace View
{
    public partial class YesNoDialog : BasicForm
    {
        private static DialogResult _result;
        public YesNoDialog()
        {
            InitializeComponent();
        }

        public static DialogResult ShowDialog(string promptText)
        {
            using (var form = new BasicForm())
            {
                form.Size = new Size(400, 300);
                var button1 = new Button()
                {
                    Font = new System.Drawing.Font("Courier New", 19.8F, System.Drawing.FontStyle.Bold,
                         System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                    Location = new System.Drawing.Point(84, 89),
                    Name = "button1",
                    Size = new Size(200, 52),
                    TabIndex = 0,
                    Text = "Yes",
                    UseVisualStyleBackColor = true
                };
                button1.Click += (sender, e) =>
                {
                    _result = DialogResult.Yes;
                    form.Close();
                };

                var textBox1 = new TextBox()
                {
                    BackColor = System.Drawing.Color.Black,
                    Font = new System.Drawing.Font("Courier New", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                    ForeColor = System.Drawing.Color.White,
                    Location = new System.Drawing.Point(26, 30),
                    Name = "textBox1",
                    Text = promptText,
                    ReadOnly = true,
                    HideSelection = true,
                    TextAlign = HorizontalAlignment.Center,
                    BorderStyle = BorderStyle.None,
                    Size = new System.Drawing.Size(322, 38),
                };
                var button2 = new Button()
                {
                    Font = new System.Drawing.Font("Courier New", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                    Location = new System.Drawing.Point(84, 156),
                    Name = "button2",
                    Size = new System.Drawing.Size(200, 52),
                    TabIndex = 2,
                    Text = "No",
                    UseVisualStyleBackColor = true
                };
                button2.Click += (s, e) =>
                {
                    _result = DialogResult.No;
                    form.Close();
                };
                form.Load += (s, e) => button1.Select();
                form.Controls.Add(textBox1);
                form.Controls.Add(button1);
                form.Controls.Add(button2);
                form.ShowDialog();
                return _result;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
