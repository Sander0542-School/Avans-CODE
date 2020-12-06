using CODE_GameLib.Rooms;
using System;
using System.Collections.Generic;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public List<RoomBase> Rooms;
        public bool Quit { get; set; } = false;
        public ConsoleKey KeyPressed { get; private set; }

        public void Move(Direction direction)
        {
            Console.WriteLine(direction.ToString());

            //KeyPressed = Console.ReadKey().Key;
            //Quit = KeyPressed == ConsoleKey.Escape;

            //while (!Quit)
            //{
            //    Updated?.Invoke(this, this);

            //    KeyPressed = Console.ReadKey().Key;
            //    Quit = KeyPressed == ConsoleKey.Escape;
            //}

            //Updated?.Invoke(this, this);
        }
    }
}
