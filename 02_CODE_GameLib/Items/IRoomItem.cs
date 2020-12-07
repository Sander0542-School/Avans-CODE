using System.Drawing;

namespace CODE_GameLib.Items
{
    public interface IRoomItem : ICoordinates
    {
        void OnUse(Player player);
    }
}