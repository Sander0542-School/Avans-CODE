using System.Drawing;

namespace CODE_GameLib.Items
{
    public interface IItem : ICoordinates
    {
        void OnUse(Player player);
    }
}