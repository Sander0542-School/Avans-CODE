using System.Linq;
using CODE_GameLib.Items;

namespace CODE_GameLib.Doors
{
    public class ColoredDoor : IDoor
    {
        private string _color;

        public ColoredDoor(string color)
        {
            _color = color;
        }

        public bool IsOpen(Player player)
        {
            return player.Items.Any(item => item is KeyItem && ((KeyItem)item).Color == _color);
        }

        public void AfterUse(Player player)
        {

        }
    }
}