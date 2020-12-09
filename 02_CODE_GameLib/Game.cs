using CODE_GameLib.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Items;

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
            if (CanPlayerMove(Player, direction, out var room, out var nextX, out var nextY))
            {
                Player.Move(room, nextX, nextY);

                if (!Rooms.Any(room1 => room1.Items.Any(item => item is SankaraStoneRoomItem)))
                {
                    Quit = true;
                }

                if (Player.Lives <= 0)
                {
                    Quit = true;
                }

                Updated?.Invoke(this, this);
            }
        }

        private bool CanPlayerMove(Player player, Direction direction, out RoomBase room, out int nextX, out int nextY)
        {
            nextX = player.X;
            nextY = player.Y;
            room = player.Room;

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

            if (IsBorderTile(player.Room, nextX, nextY))
            {
                return false;
            }

            if (HasConnection(room, nextX, nextY, out var doorDirection))
            {
                var connection = room.Connections[doorDirection];

                if (connection.Door != null && !connection.Door.IsOpen(player))
                {
                    return false;
                }
            }

            if (!IsInRoom(room, nextX, nextY, out var exitDirection))
            {
                var connection = room.Connections[exitDirection];

                room = connection.TargetRoom;

                switch (connection.TargetDirection)
                {
                    case Direction.NORTH:
                        nextY = 0;
                        nextX = room.Width / 2;
                        break;
                    case Direction.EAST:
                        nextY = room.Height / 2;
                        nextX = room.Width - 1;
                        break;
                    case Direction.SOUTH:
                        nextY = room.Height - 1;
                        nextX = room.Width / 2;
                        break;
                    case Direction.WEST:
                        nextY = room.Height / 2;
                        nextX = 0;
                        break;
                    default:
                        throw new NotImplementedException("This entrance direction is not yet supported");
                }
                
                connection.Door?.AfterUse(player);
            }

            return true;
        }

        public bool IsInRoom(RoomBase room, int x, int y)
        {
            return IsInRoom(room, x, y, out var direction);
        }

        public bool IsInRoom(RoomBase room, int x, int y, out Direction direction)
        {
            direction = Direction.NORTH;

            if (x < 0)
            {
                direction = Direction.WEST;
            }
            else if (x >= room.Width)
            {
                direction = Direction.EAST;
            }
            else if (y < 0)
            {
                direction = Direction.NORTH;
            }
            else if (y >= room.Height)
            {
                direction = Direction.SOUTH;
            }
            else
            {
                return true;
            }

            return false;
        }

        public bool IsBorderTile(int x, int y) => IsBorderTile(Player.Room, x, y);

        public bool IsBorderTile(RoomBase room, int x, int y)
        {
            if (HasConnection(room, x, y, out var direction))
            {
                return false;
            }

            return x == room.Width - 1 || x == 0 || y == room.Height - 1 || y == 0;
        }

        public bool HasConnection(RoomBase room, int x, int y, out Direction direction)
        {
            direction = Direction.NORTH;

            if (x == room.Width / 2)
            {
                if (y == 0 && room.Connections.ContainsKey(Direction.NORTH))
                {
                    direction = Direction.NORTH;
                    return true;
                }

                if (y == room.Height - 1 && room.Connections.ContainsKey(Direction.SOUTH))
                {
                    direction = Direction.SOUTH;
                    return true;
                }
            }
            else if (y == room.Height / 2)
            {
                if (x == 0 && room.Connections.ContainsKey(Direction.WEST))
                {
                    direction = Direction.WEST;
                    return true;
                }

                if (x == room.Width - 1 && room.Connections.ContainsKey(Direction.EAST))
                {
                    direction = Direction.EAST;
                    return true;
                }
            }

            return false;
        }
    }
}