﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = Game.Instance;
            game.Play();
            Console.ReadLine();
        }
    }
}
