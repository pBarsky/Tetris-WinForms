using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine;
using GameEngine.Boards;
using GameEngine.Utilities;

namespace View
{
    public partial class GameView : BasicForm
    {
        private Game _game = new Game();
        private Board _board = null;
        private int elapsedFrames = 0;
        private SolidBrush _brush = new SolidBrush(Color.White);
        private Pen _pen = new Pen(new SolidBrush(Color.Red), 2);
        private int _boxMarginVertical = 10;
        private int _boxMarginHorizontal = 10;
        private int _boxWidth = 10;
        private int _boxHeight = 10;
        private int _scaleFactor = 2;
        public GameView()
        {
            InitializeComponent();
            _game.Prepare();
            timer1_Tick(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            elapsedFrames++;
            var ff = elapsedFrames > 10;
            _board = _game.Step(ff);
            pictureBox1.Refresh();
        }

        private void Draw(Graphics g)
        {
            for (int i = 0; i < _board.Width; i++)
            {
                for (int j = 0; j < _board.Height; j++)
                {
                    if (_board.Tab[j, i] != 0)
                        g.FillRectangle(_brush, (i + 1) * _boxMarginHorizontal * _scaleFactor, (j + 1) * _boxMarginVertical * _scaleFactor, _boxWidth * _scaleFactor, _boxHeight * _scaleFactor);
                    g.DrawRectangle(_pen, (i + 1) * _boxMarginHorizontal * _scaleFactor, (j + 1) * _boxMarginVertical * _scaleFactor, _boxWidth * _scaleFactor, _boxHeight * _scaleFactor);
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
    }
}
