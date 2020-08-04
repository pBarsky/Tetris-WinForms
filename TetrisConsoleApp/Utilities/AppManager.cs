using System;
using System.Collections.Generic;
using GameEngine.AbstractClasses;

namespace GameEngine.Utilities
{
    public class AppManager : ControllableMenu
    {
        private Game _game;
        private ScoreboardManager _scoreboardManager;
        public readonly List<Tuple<string, Action>> MenuActions = new List<Tuple<string, Action>>();
        private readonly string[] _helpStrings = {
            $"\n{"DownArrow",-10} -> scroll down",
            $"{"UpArrow",-10} -> scroll up",
            $"{"ESC",-10} -> EXIT",
            $"{"ENTER",-10} -> select"
        };
        public AppManager()
        {
            // _game = new Game();
            // _scoreboardManager = new ScoreboardManager();
            // MenuActions.Add(new Tuple<string, Action>("Play", _game.Play));
            // MenuActions.Add(new Tuple<string, Action>("Scoreboard", _scoreboardManager.PrepareThenRun));
            // MenuActions.Add(new Tuple<string, Action>("Exit", () => _running = false));
            _running = true;
            _refresh = true;
        }

        protected override void Show(int index)
        {
            Console.SetCursorPosition(0, 3);
            for (int i = 0; i < MenuActions.Count; i++)
            {
                if (i == index)
                    ConsoleUtilities.ColorWriteLine($"{MenuActions[i].Item1,16}", ConsoleColor.Black, ConsoleColor.White);
                else
                    Console.WriteLine($"{MenuActions[i].Item1,-16}" + new string(' ', 20));
            }
            foreach (string helpString in _helpStrings)
            {
                Console.WriteLine(helpString);
            }
        }

        protected override void HandleInput()
        {
            var key = KeyboardHandler.GetDirection();
            switch (key)
            {
                case KeyCommand.Down:
                    _refresh = true;
                    _offset = ++_offset % MenuActions.Count;
                    break;
                case KeyCommand.Up:
                    _refresh = true;
                    _offset = _offset == 0 ? MenuActions.Count - 1 : --_offset % MenuActions.Count;
                    break;
                case KeyCommand.Enter:
                    _refresh = true;
                    MenuActions[_offset].Item2();
                    Console.Clear();
                    break;
                case KeyCommand.Escape:
                    _running = false;
                    break;
            }
        }
    }
}
