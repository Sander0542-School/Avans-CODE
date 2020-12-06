using System.Drawing;

namespace CODE_GameLib.Items
{
    public interface IRoomItem
    {
        int X { get; set; }
        int Y { get; set; }

        Color GetColor()
        {
            return Color.White;
        }
    }
}