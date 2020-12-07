using System;
using System.Collections.Generic;

namespace CODE_GameLib.Doors
{
    public class DoorFactory
    {
        public IDoor CreateDoor(string type, IDictionary<string, string> options)
        {
            IDoor door;

            switch (type)
            {
                case "colored":
                    return new ColoredDoor(options["color"]);
                case "toggle":
                    return new ToggleDoor();
                case "closing gate":
                    return new ClosingGateDoor();
                default:
                    throw new NotImplementedException("This door has not been implemented yet");
            }
        }
    }
}