using CODE_GameLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

        //generates the display
        public void Draw(Game game)
        {
            Console.Clear();

            if (!game.Quit)
            {
                Console.WriteLine("+-------------------------------------------------");

                Console.WriteLine("| Welcome to the Temple of Doom!");
                Console.WriteLine($"| Current level: {game.Level}");

                Console.WriteLine("+-------------------------------------------------");
                Console.WriteLine("|");

                var room = game.Player.Room;

                //Generates the room by height and width
                for (var y = 0; y < room.Height; y++)
                {
                    Console.Write("| ");
                    for (var x = 0; x < room.Width; x++)
                    {
                        Console.BackgroundColor = GetBackgroundColor(x, y);
                        Console.Write(" ");
                        if (game.Player.X == x && game.Player.Y == y)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("P");
                        }
                        else if (game.HasConnection(room, x, y, out var direction))
                        {
                            var connection = room.Connections[direction];

                            Console.Write(connection.Door != null ? "D" : " ");
                        }
                        else if (game.IsBorderTile(room, x, y))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("#");
                        }
                        else if (room.Items.Any(item => item.X == x && item.Y == y))
                        {
                            var item = room.Items.First(item1 => item1.X == x && item1.Y == y);

                            // Console.ForegroundColor = FromColor(item.GetColor());
                            Console.Write("I");
                        }
                        else
                        {
                            Console.Write(" ");
                        }

                        Console.Write(" ");
                        Console.ResetColor();
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("|");
                Console.WriteLine("+-------------------------------------------------");
                Console.WriteLine($"| Lives:  {game.Player.Lives}");
                Console.WriteLine($"| Stones: {game.Player.Items.Count(item => item is SankaraStoneItem)}");
                Console.WriteLine($"| Keys:   {string.Join(", ", game.Player.Items.Where(item => item is KeyItem).Select(item => ((KeyItem)item).Color))}");
                Console.WriteLine("+-------------------------------------------------");
                Console.WriteLine("| A game for the course Code Development (20/21) by Tommy den Reijer and Sander Jochems.");
                Console.WriteLine("+-------------------------------------------------");
            }
            else
            {
                Console.WriteLine("Quitting game, goodbye!");
            }
        }

        public static ConsoleColor FromColor(Color color)
        {
            var index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0; // Bright bit
            index |= (color.R > 64) ? 4 : 0; // Red bit
            index |= (color.G > 64) ? 2 : 0; // Green bit
            index |= (color.B > 64) ? 1 : 0; // Blue bit
            return (ConsoleColor)index;
        }

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