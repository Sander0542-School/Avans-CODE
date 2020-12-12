using CODE_GameLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CODE_Frontend.Tiles;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;

namespace CODE_Frontend
{
    public class GameView
    {
        public GameView()
        {
            Console.WriteLine("Please hit any keys or hit escape to exit...");
        }

        /// <summary>
        /// Generates the display
        /// </summary>
        /// <param name="game"></param>
        public void Draw(Game game)
        {
            Console.Clear();

            Console.WriteLine("+-------------------------------------------------");

            Console.WriteLine("| Welcome to the Temple of Doom!");
            Console.WriteLine($"| Current level: {game.Level}");

            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine("|");

            var room = game.Player.Room;

            //Generates the room by height and width
            for (var y = 0; y < room.Height; y++)
            {
                Console.Write("|   ");
                for (var x = 0; x < room.Width; x++)
                {
                    ITileView tileView = new EmptyTileView();
                    
                    if (game.Player.X == x && game.Player.Y == y)
                    {
                        tileView = new PlayerTileView();
                    }
                    else if (game.HasConnection(room, x, y, out var direction, out var connection))
                    {
                        tileView = new DoorTileView(connection.Door, direction);
                    }
                    else if (game.IsBorderTile(room, x, y))
                    {
                        tileView = new BorderTileView();
                    }
                    else if (room.Items.Any(item => item.Visible && item.X == x && item.Y == y))
                    {
                        var item = room.Items.First(item1 => item1.X == x && item1.Y == y);

                        tileView = new ItemTileView(item, Direction.NORTH);
                    }
                    
                    tileView.BackgroundColor = GetBackgroundColor(x, y);
                    tileView.Draw();
                }

                Console.WriteLine();
            }

            Console.WriteLine("|");
            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine($"| Lives:  {game.Player.Lives}");
            Console.WriteLine($"| Stones: {game.Player.Items.Count(item => item is SankaraStoneItem)}");
            Console.WriteLine($"| Keys:   {string.Join(", ", game.Player.Items.Where(item => item.GetItem() is KeyItem).Select(item => ((KeyItem) item.GetItem()).Color))}");
            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine("| A game for the course Code Development (20/21) by Tommy den Reijer and Sander Jochems.");
            Console.WriteLine("+-------------------------------------------------");
        }

        /// <summary>
        /// Gets the colour from the box on the board
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private ConsoleColor GetBackgroundColor(int x, int y)
        {
            var xIsEven = x % 2 == 0;
            var yIsEven = y % 2 == 0;

            if (xIsEven && yIsEven)
            {
                return ConsoleColor.DarkGray;
            }

            if (!xIsEven && !yIsEven)
            {
                return ConsoleColor.DarkGray;
            }

            return ConsoleColor.Black;
        }
    }
}