using System;
using System.Collections.Generic;
using System.Drawing;
using CODE_GameLib.Items.Decorators;

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
                    roomItem = new KeyRoomItem();
                    break;
                case "disappearing boobietrap":
                    roomItem = new DisappearingBoobietrapRoomItem(int.Parse(options["damage"]));
                    options.Remove("damage");
                    break;
                case "boobietrap":
                    roomItem = new BoobietrapRoomItem(int.Parse(options["damage"]));
                    options.Remove("damage");
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

            roomItem = ApplyDecorators(roomItem, options);

            roomItem.X = x;
            roomItem.Y = y;

            return roomItem;
        }

        private IRoomItem ApplyDecorators(IRoomItem roomItem, IDictionary<string, string> options)
        {
            foreach (var pair in options)
            {
                switch (pair.Key)
                {
                    case "color":
                        roomItem = new ColorRoomItemDecorator(roomItem, Color.FromName(pair.Value));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return roomItem;
        }
    }
}