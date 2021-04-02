namespace GameEngine.Utilities
{
    public class InputHandler
    {
        private readonly Game _game;

        public InputHandler(Game game)
        {
            _game = game;
        }

        public void HandleUpPress()
        {
            _game.CurrentBrick.DoRotate(false);
            if (_game.Board.IsColliding(_game.CurrentBrick, 0, 0))
            {
                _game.CurrentBrick.DoRotate();
                return;
            }

            _game.HasChanged = true;
            _game.Board.InsertBrick(_game.CurrentBrick);
        }

        public void HandleRightPress()
        {
            if (_game.Board.IsColliding(_game.CurrentBrick, 1, 0)) return;
            _game.HasChanged = true;
            _game.CurrentBrick.MoveRight();
            _game.Board.InsertBrick(_game.CurrentBrick);
        }

        public void HandleLeftPress()
        {
            if (_game.Board.IsColliding(_game.CurrentBrick, -1, 0)) return;
            _game.HasChanged = true;
            _game.CurrentBrick.MoveLeft();
            _game.Board.InsertBrick(_game.CurrentBrick);
        }

        public void HandleDownPress(bool fastForward)
        {
            if (_game.Board.IsColliding(_game.CurrentBrick, 0, 1))
            {
                _game.Board.FreezeBrick(_game.CurrentBrick);
                _game.Score += _game.Board.Gravitate(_game.Board.Width);
                _game.NextBrick();
                return;
            }

            _game.HasChanged = true;
            _game.CurrentBrick.MoveDown();
            if (fastForward)
            {
                _game.Score += 1;
            }

            _game.Board.InsertBrick(_game.CurrentBrick);
        }
    }
}