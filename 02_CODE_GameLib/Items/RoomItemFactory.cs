using System;
using System.Collections.Generic;

namespace CODE_GameLib.Items
{
    public class RoomItemFactory
    {
        public IRoomItem CreateRoomItem(string type, int x, int y, IDictionary<string, string> options)
        {
            IRoomItem roomItem;

            switch (type)
            {
                case "key":
                    roomItem = new KeyRoomItem(options["color"]);
                    break;
                case "disappearing boobietrap":
                    roomItem = new DisappearingBoobietrapRoomItem(int.Parse(options["damage"]));
                    break;
                case "boobietrap":
                    roomItem = new BoobietrapRoomItem(int.Parse(options["damage"]));
                    break;
                case "sankara stone":
                    roomItem = new SankaraStoneRoomItem();
                    break;
                case "pressure plate":
                    roomItem = new PressurePlateRoomItem();
                    break;
                default:
                    throw new NotImplementedException("This item has not been implemented yet");
            }

            roomItem.X = x;
            roomItem.Y = y;

            return roomItem;
        }
    }
}