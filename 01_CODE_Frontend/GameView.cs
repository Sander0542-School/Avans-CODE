using System;
using System.Linq;
using CODE_Frontend.Tiles;
using CODE_GameLib;
using CODE_GameLib.Items;

namespace CODE_Frontend
{
    public class GameView
    {
        public GameView()
        {
            Console.WriteLine("Please hit any keys or hit escape to exit...");
        }

        /// <summary>
        ///     Generates the display
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

            var player = game.Player;
            var room = game.Player.Room;

            //Generates the room by height and width
            for (var y = 0; y < room.Height; y++)
            {
                Console.Write("|   ");
                for (var x = 0; x < room.Width; x++)
                {
                    ITileView tileView = new EmptyTileView();

                    if (player.X == x && player.Y == y)
                    {
                        tileView = new PlayerTileView();
                    }
                    else if (room.HasConnection(x, y, out var direction, out var connection))
                    {
                        tileView = new DoorTileView(connection.Door, direction);
                    }
                    else if (room.IsBorderTile(x, y))
                    {
                        tileView = new BorderTileView();
                    }
                    else if (room.Items.Any(item => item.Visible && item.HasCoordinates(x, y)))
                    {
                        var item = room.Items.First(item1 => item1.X == x && item1.Y == y);

                        tileView = new ItemTileView(item);
                    }

                    tileView.BackgroundColor = ConsoleColor.Black;
                    tileView.Draw();
                }

                Console.WriteLine();
            }

            Console.WriteLine("|");
            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine($"| Lives:  {player.Lives}");
            Console.WriteLine($"| Stones: {player.Items.Count(item => item.GetItem() is SankaraStoneItem)}");
            Console.WriteLine($"| Keys:   {string.Join(", ", player.Items.Where(item => item.GetItem() is KeyItem).Select(item => ((KeyItem) item.GetItem()).Color))}");
            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine("| A game for the course Code Development (20/21) by Sander Jochems.");
            Console.WriteLine("+-------------------------------------------------");
        }
    }
}