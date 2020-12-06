using System.Collections.ObjectModel;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Player : ICoordinates
    {
        public int Lives { get; set; }
        
        public RoomBase Room { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        
        public ObservableCollection<IRoomItem> Items { get; set; }
    }
}