using System.Drawing;
using System.Windows.Forms;
using TetrisGame.Views;

namespace TetrisGame.Dialogs
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
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(MousePosition.X - form.Size.Width / 2, MousePosition.Y - form.Size.Height / 2);

                var yesButton = CreateYesButton(form);
                var messageTextBox = CreaMessageTextBox(promptText);
                var noButton = CreateNoButton(form);

                form.Load += (s, e) => yesButton.Select();

                form.Controls.Add(messageTextBox);
                form.Controls.Add(yesButton);
                form.Controls.Add(noButton);
                form.ShowDialog();

                return _result;
            }
        }

        private static Button CreateNoButton(BasicForm form)
        {
            var noButton = new Button
            {
                Font = new Font("Courier New", 19.8F, FontStyle.Bold, GraphicsUnit.Point, 238),
                Location = new Point(84, 156),
                Name = "button2",
                Size = new Size(200, 52),
                TabIndex = 2,
                Text = @"No",
                UseVisualStyleBackColor = true
            };
            noButton.Click += (s, e) =>
            {
                _result = DialogResult.No;
                form.Close();
            };
            return noButton;
        }

        private static TextBox CreaMessageTextBox(string promptText)
        {
            var messageTextBox = new TextBox
            {
                BackColor = Color.Black,
                Font = new Font("Courier New", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 238),
                ForeColor = Color.White,
                Location = new Point(26, 30),
                Name = "textBox1",
                Text = promptText,
                ReadOnly = true,
                HideSelection = true,
                TextAlign = HorizontalAlignment.Center,
                BorderStyle = BorderStyle.None,
                Size = new Size(322, 38)
            };
            return messageTextBox;
        }

        private static Button CreateYesButton(BasicForm form)
        {
            var yesButton = new Button
            {
                Font = new Font("Courier New", 19.8F, FontStyle.Bold,
                    GraphicsUnit.Point, 238),
                Location = new Point(84, 89),
                Name = "button1",
                Size = new Size(200, 52),
                TabIndex = 0,
                Text = @"Yes",
                UseVisualStyleBackColor = true
            };
            yesButton.Click += (sender, e) =>
            {
                _result = DialogResult.Yes;
                form.Close();
            };
            return yesButton;
        }
    }
}