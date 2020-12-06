using CODE_GameLib.Rooms;
using System;
using System.Collections.Generic;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public List<RoomBase> Rooms;

        public ConsoleKey KeyPressed { get; private set; }
        public bool Quit { get; private set; } = false;

        public void Run()
        {
            KeyPressed = Console.ReadKey().Key;
            Quit = KeyPressed == ConsoleKey.Escape;

            while (!Quit)
            {
                Updated?.Invoke(this, this);

                KeyPressed = Console.ReadKey().Key;
                Quit = KeyPressed == ConsoleKey.Escape;
            }

            Updated?.Invoke(this, this);
        }
    }
}
