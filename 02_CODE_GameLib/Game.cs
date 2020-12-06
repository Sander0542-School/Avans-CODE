using CODE_GameLib.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        
        public Player Player { get; private set; }
        public List<RoomBase> Rooms { get; private set; }
        public bool Quit { get; set; } = false;
        public string Level { get; set; }

        public Game(string level, List<RoomBase> rooms, Player player)
        {
            Level = level;
            
            Rooms = rooms;
            Player = player;
        }

        public void Move(Direction direction)
        {
            if (CanPlayerMove(Player, direction, out var nextX, out var nextY))
            {
                Player.X = nextX;
                Player.Y = nextY;
            }

            Updated?.Invoke(this, this);
        }

        private bool CanPlayerMove(Player player, Direction direction, out int nextX, out int nextY)
        {
            nextX = player.X;
            nextY = player.Y;
            
            switch (direction)
            {
                case Direction.NORTH:
                    nextY--;
                    break;
                case Direction.EAST:
                    nextX++;
                    break;
                case Direction.SOUTH:
                    nextY++;
                    break;
                case Direction.WEST:
                    nextX--;
                    break;
                default:
                    throw new NotImplementedException("This direction has not been implemented yet");
            }
            
            return !IsBorderTile(player.Room, nextX, nextY);
        }

        public bool IsBorderTile(int x, int y) => IsBorderTile(Player.Room, x, y);
        public bool IsBorderTile(RoomBase room, int x, int y)
        {
            return x == room.Width - 1 || x == 0 || y == room.Height - 1 || y == 0;
        }
    }
}
