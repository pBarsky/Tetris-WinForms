using GameEngine;
using GameEngine.Boards;
using GameEngine.Utilities;
using System;
using System.IO;
using System.Windows.Forms;
using TetrisGame.Dialogs;
using TetrisGame.Utilities;

namespace TetrisGame.Views
{
    public sealed partial class GameView : BasicForm
    {
        private readonly Game _game = new Game();
        private Board _board;
        private int _elapsedFrames;
        private bool _firstClosingGate;
        private bool _secondClosingGate;
        private KeyCommand _currentKey = KeyCommand.None;
        private readonly Drawer _drawer = new Drawer();

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
            HandleGameOver();
            var ff = IsAutoDropBrick();
            NextFrame(ff);
            RefreshView();
            UpdateScore();
        }

        private bool IsAutoDropBrick()
        {
            _elapsedFrames++;
            var ff = _elapsedFrames > 10;
            if (ff)
            {
                _elapsedFrames = 0;
            }

            return ff;
        }

        private void HandleGameOver()
        {
            if (_game.Alive) return;
            gameTimer.Enabled = false;
            GameOver();
        }

        private void NextFrame(bool ff)
        {
            _board = _game.Step(ff, _currentKey);
        }

        private void RefreshView()
        {
            if (!_game.HasChanged) return;
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void GameOver()
        {
            if (_firstClosingGate)
            {
                return;
            }

            ShowNameInputDialog();

            HandleRestartOrClose();
        }

        private void HandleRestartOrClose()
        {
            if (!_secondClosingGate && YesNoDialog.ShowDialog("Want to play again?") == DialogResult.Yes)
            {
                _game.RestartGame();
                gameTimer.Enabled = true;
            }
            else
            {
                _firstClosingGate = true;
                Close();
            }
        }

        private void ShowNameInputDialog()
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
        }

        private void SaveScore(string name, int score)
        {
            try
            {
                _game.ScoreWriter.SaveScore(name, score);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (var i = 0; i < _board.Width; i++)
            {
                for (var j = 0; j < _board.Height; j++)
                {
                    if (_board.Tab[j, i] != 0)
                    {
                        _drawer.DrawRectangle(e, i, j);
                    }

                    _drawer.DrawBox(e, i, j);
                }
            }
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
            var brickCount = 1;
            var y = 0;
            var offset = 0;
            foreach (var brick in _game.QueueBricks)
            {
                _drawer.DrawBrick(e, brickCount, y, brick, offset);

                brickCount++;
                y += 20 * (2 + brick.Height);
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
                helpStringsLabel.Text += $@"{s}{Environment.NewLine}";
            }
        }

        private void GameView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _secondClosingGate = true;
            GameOver();
        }
    }
}