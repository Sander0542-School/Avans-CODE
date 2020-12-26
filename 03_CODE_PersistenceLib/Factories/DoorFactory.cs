using System;
using System.Collections.Generic;
using CODE_GameLib.Doors;
using CODE_PersistenceLib.Factories.Interfaces;

namespace CODE_PersistenceLib.Factories
{
    public class DoorFactory : IDoorFactory
    {
        /// <summary>
        ///     Generates an Door according to the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="options"></param>
        /// <returns></returns>
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