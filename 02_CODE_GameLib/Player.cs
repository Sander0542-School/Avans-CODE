using System;
using System.Collections.ObjectModel;
using System.Linq;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib
{
    public class Player
    {
        public Player(int lives, RoomBase room, int x, int y)
        {
            Lives = lives;
            Room = room;
            X = x;
            Y = y;

            Items = new ObservableCollection<IItem>();
        }

        public int Lives { get; private set; }

        public RoomBase Room { get; private set; }

        public ObservableCollection<IItem> Items { get; }
        public int X { get; private set; }
        public int Y { get; private set; }

        /// <summary>
        ///     Checks if player can move to the next location, so yes it returns the next y and x
        /// </summary>
        /// <param name="player"></param>
        /// <param name="direction"></param>
        /// <param name="room"></param>
        /// <param name="nextX"></param>
        /// <param name="nextY"></param>
        /// <returns></returns>
        public bool CanMove(Direction direction, out RoomBase room, out int nextX, out int nextY)
        {
            nextX = X;
            nextY = Y;
            room = Room;

            if (room.HasConnection(nextX, nextY, out var doorDirection, out var connection) && direction == doorDirection)
            {
                if (!(connection.Door?.IsOpen(this) ?? true)) return false;

                connection.GetExitLocation(out room, out nextX, out nextY);

                connection.Door?.AfterUse(this);

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

            var beforeFloorX = nextX;
            var beforeFloorY = nextY;

            if (room.IsFloorTile(nextX, nextY, out var floor))
            {
                floor.OnEnter(this, out nextX, out nextY);

                var floorSuccess = !room.IsBorderTile(nextX, nextY);

                if (room.HasConnection(nextX, nextY, out doorDirection, out connection))
                    if (connection.Door != null && !connection.Door.IsOpen(this))
                        floorSuccess = false;

                if (floorSuccess) return true;
            }

            nextX = beforeFloorX;
            nextY = beforeFloorY;

            //Player cannot run into the border
            if (room.IsBorderTile(nextX, nextY)) return false;

            if (room.HasConnection(nextX, nextY, out doorDirection, out connection))
                if (connection.Door != null && !connection.Door.IsOpen(this))
                    return false;

            if (room.HasPortal(nextX, nextY, out var portal))
                portal.GetExitLocation(out room, out nextX, out nextY);

            return true;
        }

        /// <summary>
        ///     Updates the user position for moving
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Move(int x, int y)
        {
            Move(Room, x, y);
        }

        /// <summary>
        ///     Updates the user position for moving
        /// </summary>
        /// <param name="room"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Move(RoomBase room, int x, int y)
        {
            Room = room;
            X = x;
            Y = y;

            room.Items.FirstOrDefault(item => item.Visible && item.X == x && item.Y == y)?.OnUse(this);
        }

        /// <summary>
        ///     Reduce Players lives
        /// </summary>
        /// <param name="damage"></param>
        public void Damage(int damage)
        {
            Lives -= damage;
        }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }
    }
}