using System.Collections.ObjectModel;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Player : ICoordinates
    {
        public int Lives { get; private set; }

        public RoomBase Room { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ObservableCollection<IRoomItem> Items { get; set; }

        public void Move(int x, int y)
        {
            Move(Room, x, y);
        }

        public void Move(RoomBase room, int x, int y)
        {
            Room = room;
            X = x;
            Y = y;
        }

        public void Damage(int damage)
        {
            Lives -= damage;
        }

        public void AddItem(IRoomItem item)
        {
            Items.Add(item);
        }
    }
}