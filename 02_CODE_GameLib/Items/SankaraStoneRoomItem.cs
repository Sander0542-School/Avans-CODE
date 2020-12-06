using System.Drawing;

namespace CODE_GameLib.Items
{
    public class SankaraStoneRoomItem : IRoomItem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Color GetColor()
        {
            return Color.Orange;
        }
    }
}