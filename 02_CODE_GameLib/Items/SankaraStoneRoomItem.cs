using System.Drawing;

namespace CODE_GameLib.Items
{
    public class SankaraStoneRoomItem : IRoomItem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void OnUse(Player player)
        {
            player.AddItem(this);
            player.Room.Items.Remove(this);
        }
    }
}