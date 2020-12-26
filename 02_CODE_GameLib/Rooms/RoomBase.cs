﻿using System.Collections.Generic;
using CODE_GameLib.Items;

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
            Items = new List<IItem>();
        }

        public int Id { get; }

        public int Width { get; }
        public int Height { get; }

        public Dictionary<Direction, Connection> Connections { get; set; }
        public List<IItem> Items { get; set; }

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
    }
}