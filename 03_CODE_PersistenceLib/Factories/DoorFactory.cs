using System;
using System.Collections.Generic;
using CODE_FileSystem.Factories.Interfaces;
using CODE_GameLib.Doors;

namespace CODE_FileSystem.Factories
{
    public class DoorFactory : IDoorFactory
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