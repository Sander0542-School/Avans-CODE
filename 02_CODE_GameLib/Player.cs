using System.Collections.ObjectModel;
using System.Linq;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Player : ICoordinates
    {
        public int Lives { get; private set; }

        public RoomBase Room { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public ObservableCollection<IItem> Items { get; set; }

        public Player(int lives, RoomBase room, int x, int y)
        {
            Lives = lives;
            Room = room;
            X = x;
            Y = y;
            
            Items = new ObservableCollection<IItem>();
        }

        public void Move(RoomBase room, int x, int y)
        {
            Room = room;
            X = x;
            Y = y;

            room.Items.FirstOrDefault(item => item.Visible && item.X == x && item.Y == y)?.OnUse(this);
        }

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