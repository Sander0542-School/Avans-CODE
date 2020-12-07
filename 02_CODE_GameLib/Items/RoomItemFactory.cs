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
                    roomItem = new KeyRoomItem(x, y, options["color"]);
                    break;
                case "disappearing boobietrap":
                    roomItem = new DisappearingBoobietrapRoomItem(x, y, int.Parse(options["damage"]));
                    break;
                case "boobietrap":
                    roomItem = new BoobietrapRoomItem(x, y, int.Parse(options["damage"]));
                    break;
                case "sankara stone":
                    roomItem = new SankaraStoneRoomItem(x, y);
                    break;
                case "pressure plate":
                    roomItem = new PressurePlateRoomItem(x, y);
                    break;
                default:
                    throw new NotImplementedException("This item has not been implemented yet");
            }

            return roomItem;
        }
    }
}