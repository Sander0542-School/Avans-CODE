using System.Linq;

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
            return player.Keys.Any(key => key == Color);
        }

        public void AfterUse(Player player)
        {
        }
    }
}