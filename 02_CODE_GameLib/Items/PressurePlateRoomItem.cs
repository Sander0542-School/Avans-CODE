using System.Linq;
using CODE_GameLib.Doors;

namespace CODE_GameLib.Items
{
    public class PressurePlateRoomItem : IRoomItem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void OnUse(Player player)
        {
            foreach (var connection in player.Room.Connections.Values.Where(connection => connection.Door is ToggleDoor))
            {
                ((ToggleDoor) connection.Door).Toggle();
            }
        }
    }
}