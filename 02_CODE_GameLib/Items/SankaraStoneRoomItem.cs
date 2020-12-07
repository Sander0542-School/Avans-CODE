using System.Drawing;

namespace CODE_GameLib.Items
{
    public class SankaraStoneRoomItem : IRoomItem
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public SankaraStoneRoomItem(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public void OnUse(Player player)
        {
            player.AddItem(this);
            player.Room.Items.Remove(this);
        }
    }
}