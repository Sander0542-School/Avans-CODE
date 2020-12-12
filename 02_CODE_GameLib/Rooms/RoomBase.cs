﻿using System.Collections.Generic;
using CODE_GameLib.Doors;
using CODE_GameLib.Items;

namespace CODE_GameLib.Rooms
{
    public abstract class RoomBase
    {
        public int Id { get; }

        public int Width { get; }
        public int Height { get; }

        public RoomBase(int id, int height, int width)
        {
            Id = id;
            Width = width;
            Height = height;

            Connections = new Dictionary<Direction, Connection>();
            Items = new List<IItem>();
        }

        public Dictionary<Direction, Connection> Connections { get; set; }
        public List<IItem> Items { get; set; }
    }
}