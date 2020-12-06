using System.Drawing;

namespace CODE_GameLib.Items
{
    public interface IRoomItem : ICoordinates
    {
        Color GetColor()
        {
            return Color.White;
        }
    }
}