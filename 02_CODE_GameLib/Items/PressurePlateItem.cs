using System.Linq;
using CODE_GameLib.Doors;

namespace CODE_GameLib.Items
{
    public class PressurePlateItem : IItem
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public PressurePlateItem(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void OnUse(Player player)
        {
            foreach (var connection in player.Room.Connections.Values.Where(connection => connection.Door is ToggleDoor))
            {
                ((ToggleDoor)connection.Door).Toggle();
            }
        }
    }
}