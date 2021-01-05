using System;
using System.Linq;
using CODE_Frontend.Tiles;
using CODE_GameLib;
using CODE_GameLib.Connections;
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
            var player = game.Player;
            var room = game.Player.Room;
            
            Console.Clear();

            Console.WriteLine("+-------------------------------------------------");

            Console.WriteLine("| Welcome to the Temple of Doom!");
            Console.WriteLine($"| Current level: {game.Level}");
#if DEBUG
            Console.WriteLine($"| Room: {player.Room.Id} - Coords ({player.X}, {player.Y})");
#endif

            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine("|");

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
                    else if (room.Enemies.Any(enemy => enemy.CurrentXLocation == x && enemy.CurrentYLocation == y))
                    {
                        tileView = new EnemyTileView();
                    }
                    else if (room.HasPortal(x, y, out var portal))
                    {
                        tileView = new PortalTileView(portal.Door);
                    }
                    else if (room.HasConnection(x, y, out var direction, out var connection))
                    {
                        tileView = new DoorTileView(connection.Door, direction);
                    }
                    else if (room.IsBorderTile(x, y))
                    {
                        tileView = new BorderTileView();
                    }
                    else if (room.Items.Any(item => item.Visible && item.X == x && item.Y == y))
                    {
                        var item = room.Items.First(item1 => item1.Visible && item1.X == x && item1.Y == y);

                        tileView = new ItemTileView(item);
                    }
                    else if (room.Floors.Any(floor => floor.X == x && floor.Y == y))
                    {
                        var floor = room.Floors.First(floor1 => floor1.X == x && floor1.Y == y);

                        tileView = new FloorTileView(floor);
                    }

                    tileView.Draw();
                }

                Console.WriteLine();
            }

            Console.WriteLine("|");
            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine($"| Lives:  {player.Lives}");
            Console.WriteLine($"| Stones: {player.Stones}");
            Console.WriteLine($"| Keys:   {string.Join(", ", player.Keys)}");
            Console.WriteLine("+-------------------------------------------------");
            Console.WriteLine("| A game for the course Code Development (20/21) by Sander Jochems.");
            Console.WriteLine("+-------------------------------------------------");
        }
    }
}