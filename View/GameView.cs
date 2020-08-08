using GameEngine;
using GameEngine.AbstractClasses;
using GameEngine.Boards;
using GameEngine.Utilities;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace View
{
    public partial class GameView : BasicForm
    {
        private readonly Game _game = new Game();
        private Board _board = null;
        private int _elapsedFrames = 0;
        private readonly SolidBrush _brush = new SolidBrush(Color.White);
        private readonly Pen _pen = new Pen(new SolidBrush(Color.Red), 2);
        private const int BoxMarginVertical = 10;
        private const int BoxMarginHorizontal = 10;
        private const int BoxWidth = 10;
        private const int BoxHeight = 10;
        private readonly int _scaleFactor = 2;
        private KeyCommand _currentKey = KeyCommand.None;
        private readonly Font _myFont = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold);
        public GameView()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _game.Prepare();
            timer1_Tick(null, null);
            PrepareHelpStrings();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_game.Alive)
            {
                gameTimer.Enabled = false;
                GameOver();
            }

            _elapsedFrames++;
            var ff = _elapsedFrames > 10;
            if (ff)
            {
                _elapsedFrames = 0;
            }

            _board = _game.Step(ff, _currentKey);
            if (_game.HasChanged)
            {
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }

            UpdateScore();
        }

        private void GameOver()
        {
            using (var form = new InputDialog(_game.Score))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var userName = form.ReturnValue;
                    SaveScore(userName, _game.Score);
                }
            }

            if (YesNoDialog.ShowDialog("Want to play again?") == DialogResult.Yes)
            {
                _game.RestartGame();
                gameTimer.Enabled = true;
            }
            else
            {
                Close();
            }
        }

        private void SaveScore(string name, int score)
        {
            try
            {
                var sw = new ScoreWriter();
                sw.SaveScore(name, score);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < _board.Width; i++)
            {
                for (int j = 0; j < _board.Height; j++)
                {
                    if (_board.Tab[j, i] != 0)
                    {
                        DrawBrick(e, i, j);
                    }

                    DrawBox(e, i, j);
                }
            }
        }
        private void DrawBrick(PaintEventArgs e, int x, int y)
        {
            e.Graphics.FillRectangle(_brush, (x + 1) * BoxMarginHorizontal * _scaleFactor, (y + 1) * BoxMarginVertical * _scaleFactor, BoxWidth * _scaleFactor, BoxHeight * _scaleFactor);
        }
        private void DrawBox(PaintEventArgs e, int x, int y)
        {
            e.Graphics.DrawRectangle(_pen, (x + 1) * BoxMarginHorizontal * _scaleFactor, (y + 1) * BoxMarginVertical * _scaleFactor, BoxWidth * _scaleFactor, BoxHeight * _scaleFactor);
        }
        private void DrawString(string text, PaintEventArgs e, int x, int y)
        {
            e.Graphics.DrawString(text, _myFont, _brush, x, y);
        }
        private void DrawString(string text, PaintEventArgs e, int x, int y, StringFormat format)
        {
            e.Graphics.DrawString(text, _myFont, _brush, x, y, format);
        }
        private void GameView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _currentKey = KeyCommand.Left;
                    break;
                case Keys.Right:
                    _currentKey = KeyCommand.Right;
                    break;
                case Keys.Up:
                    _currentKey = KeyCommand.Up;
                    break;
                case Keys.Down:
                    _currentKey = KeyCommand.Down;
                    break;
                case Keys.Enter:
                    _currentKey = KeyCommand.Enter;
                    break;
                case Keys.Escape:
                    _currentKey = KeyCommand.Escape;
                    break;
            }
        }
        private void GameView_KeyUp(object sender, KeyEventArgs e)
        {
            _currentKey = KeyCommand.None;
        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            int brickCount = 0;
            int y = 0;
            int offset = 0;
            int brickDisplayCount = 0;
            foreach (Brick brick in _game.QueueBricks)
            {
                DrawString($"Brick {++brickCount}:", e, 20, y + 20);
                y += 20;
                for (int i = 0; i < brick.Width; i++)
                {
                    for (int j = 0; j < brick.Height; j++)
                    {
                        if (brick.Shape[j, i] != 0)
                        {
                            DrawBrick(e, i, j + offset + brickCount + brickDisplayCount);
                        }

                        DrawBox(e, i, j + offset + brickCount + brickDisplayCount);
                    }
                }

                brickDisplayCount++;
                y += 20 * (1 + brick.Height);
                offset += brick.Height;
            }
        }
        private void UpdateScore()
        {
            scoreLabel.Text = $@"{_game.Score}";
        }
        private void PrepareHelpStrings()
        {
            helpStringsLabel.Text = string.Empty;
            foreach (var s in _game.HelpStrings)
            {
                helpStringsLabel.Text += $"{s}{Environment.NewLine}";
            }
        }
    }
}
