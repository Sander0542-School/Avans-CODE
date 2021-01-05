using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Connections;
using CODE_GameLib.Floors;
using CODE_GameLib.Items;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Rooms
{
    public abstract class RoomBase
    {
        public RoomBase(int id, int height, int width)
        {
            Id = id;
            Width = width;
            Height = height;

            Connections = new Dictionary<Direction, Connection>();
            Portals = new Dictionary<Tuple<int, int>, Portal>();
            Items = new List<IItem>();
        }

        public int Id { get; }

        public int Width { get; }
        public int Height { get; }

        public Dictionary<Direction, Connection> Connections { get; }

        public Dictionary<Tuple<int, int>, Portal> Portals { get; }
        public List<IItem> Items { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<IFloor> Floors { get; set; }

        /// <summary>
        ///     Shoot all the enemies in each direction from the given x and y value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public int ShootEnemies(int x, int y)
        {
            Enemies.FirstOrDefault(enemy => enemy.CurrentXLocation == x && enemy.CurrentYLocation == y - 1)?.GetHurt(1); // NORTH
            Enemies.FirstOrDefault(enemy => enemy.CurrentXLocation == x + 1 && enemy.CurrentYLocation == y)?.GetHurt(1); // EAST
            Enemies.FirstOrDefault(enemy => enemy.CurrentXLocation == x && enemy.CurrentYLocation == y + 1)?.GetHurt(1); // SOUTH
            Enemies.FirstOrDefault(enemy => enemy.CurrentXLocation == x - 1 && enemy.CurrentYLocation == y)?.GetHurt(1); // WEST

            return Enemies.RemoveAll(enemy => enemy.NumberOfLives < 1);
        }

        /// <summary>
        ///     Move all the enemies to their next location
        /// </summary>
        public void MoveEnemies()
        {
            foreach (var enemy in Enemies)
            {
                enemy.Move();

                Floors.FirstOrDefault(floor1 => floor1.X == enemy.CurrentXLocation && floor1.Y == enemy.CurrentYLocation)?.OnEnter(enemy);
            }
        }

        /// <summary>
        ///     Checks if your new position is a bordertile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsBorderTile(int x, int y)
        {
            if (HasConnection(x, y)) return false;

            return x == Width - 1 || x == 0 || y == Height - 1 || y == 0;
        }

        /// <summary>
        ///     checks if there is a connection
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool HasConnection(int x, int y)
        {
            return HasConnection(x, y, out _, out _);
        }

        /// <summary>
        ///     Returns connection
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool HasConnection(int x, int y, out Direction direction)
        {
            return HasConnection(x, y, out direction, out _);
        }

        /// <summary>
        ///     checks if there is a connection and returns direction and connection it if so
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public bool HasConnection(int x, int y, out Direction direction, out Connection connection)
        {
            direction = Direction.NORTH;
            connection = null;

            if (x == Width / 2)
            {
                if (y == 0 && Connections.TryGetValue(Direction.NORTH, out connection))
                {
                    direction = Direction.NORTH;
                    return true;
                }

                if (y == Height - 1 && Connections.TryGetValue(Direction.SOUTH, out connection))
                {
                    direction = Direction.SOUTH;
                    return true;
                }
            }
            else if (y == Height / 2)
            {
                if (x == 0 && Connections.TryGetValue(Direction.WEST, out connection))
                {
                    direction = Direction.WEST;
                    return true;
                }

                if (x == Width - 1 && Connections.TryGetValue(Direction.EAST, out connection))
                {
                    direction = Direction.EAST;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Checks if there is an portal on the given x and y value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool HasPortal(int x, int y)
        {
            return HasPortal(x, y, out _);
        }

        /// <summary>
        ///     Checks if there is an portal on the given x and y value and returns the portal
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="portal"></param>
        /// <returns></returns>
        public bool HasPortal(int x, int y, out Portal portal)
        {
            portal = Portals.FirstOrDefault(pair => pair.Key.Item1 == x && pair.Key.Item2 == y).Value;

            return portal != null;
        }

        /// <summary>
        ///     Checks if there is an special floor tile on the given x and y value and return the floor tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public bool IsFloorTile(int x, int y, out IFloor floor)
        {
            floor = Floors.FirstOrDefault(floor1 => floor1.X == x && floor1.Y == y);

            return floor != null;
        }
    }
}