using System.Linq;
using CODE_GameLib.Items;

namespace CODE_GameLib.Doors
{
    public class ColoredDoor : IDoor
    {
        public ColoredDoor(string color)
        {
            Color = color;
        }

        public string Color { get; }

        public bool IsOpen(Player player)
        {
            return player.Items.Any(item => item.GetItem() is KeyItem keyItem && keyItem.Color == Color);
        }

        public void AfterUse(Player player)
        {
        }
    }
}