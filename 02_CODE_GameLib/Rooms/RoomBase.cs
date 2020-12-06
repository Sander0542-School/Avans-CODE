using System.Collections.Generic;
using CODE_GameLib.Doors;
using CODE_GameLib.Items;

namespace CODE_GameLib.Rooms
{
    public abstract class RoomBase
    {
        public int Id { get; set; }
        
        public int Width { get; set; }
        public int Height { get; set; }
        
        public Dictionary<Direction, Connection> Connections { get; set; }
        public List<RoomItem> Items { get; set; }
    }
}