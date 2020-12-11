using CODE_GameLib.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Items;

namespace CODE_GameLib
{
    public class Game
    {
        private bool _quit = false;

        public event EventHandler<Game> Updated;

        public Player Player { get; private set; }
        public List<RoomBase> Rooms { get; private set; }

        public bool Quit
        {
            get => _quit;
            set
            {
                _quit = value;
                Updated?.Invoke(this, this);
            }
        }

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

                //When all the SankaraStones are picked up, the game ends.
                if (!Rooms.Any(room1 => room1.Items.Any(item => item.Visible && item.GetItem() is SankaraStoneItem)))
                {
                    Quit = true;
                }

                //When the Lives are over the game ends
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

            if (HasConnection(room, nextX, nextY, out var doorDirection, out var connection) && direction == doorDirection)
            {
                if (!(connection.Door?.IsOpen(player) ?? true))
                {
                    return false;
                }

                room = connection.TargetRoom;

                //Ensures that the player arrives at the correct coordinates in the new room
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

                return true;
            }

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

            //Player cannot run into the border
            if (IsBorderTile(player.Room, nextX, nextY))
            {
                return false;
            }

            if (HasConnection(room, nextX, nextY, out doorDirection, out connection))
            {
                if (connection.Door != null && !connection.Door.IsOpen(player))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsBorderTile(RoomBase room, int x, int y)
        {
            if (HasConnection(room, x, y))
            {
                return false;
            }

            return x == room.Width - 1 || x == 0 || y == room.Height - 1 || y == 0;
        }

        //Looks if there is a connection and returns direction and connection it if so
        public bool HasConnection(RoomBase room, int x, int y) => HasConnection(room, x, y, out _, out _);

        public bool HasConnection(RoomBase room, int x, int y, out Direction direction) => HasConnection(room, x, y, out direction, out _);

        public bool HasConnection(RoomBase room, int x, int y, out Direction direction, out Connection connection)
        {
            direction = Direction.NORTH;
            connection = null;

            if (x == room.Width / 2)
            {
                if (y == 0 && room.Connections.TryGetValue(Direction.NORTH, out connection))
                {
                    direction = Direction.NORTH;
                    return true;
                }

                if (y == room.Height - 1 && room.Connections.TryGetValue(Direction.SOUTH, out connection))
                {
                    direction = Direction.SOUTH;
                    return true;
                }
            }
            else if (y == room.Height / 2)
            {
                if (x == 0 && room.Connections.TryGetValue(Direction.WEST, out connection))
                {
                    direction = Direction.WEST;
                    return true;
                }

                if (x == room.Width - 1 && room.Connections.TryGetValue(Direction.EAST, out connection))
                {
                    direction = Direction.EAST;
                    return true;
                }
            }

            return false;
        }
    }
}