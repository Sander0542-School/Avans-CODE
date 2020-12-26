using System;
using System.Collections.Generic;
using CODE_GameLib;
using CODE_GameLib.Floors;
using CODE_PersistenceLib.Factories.Interfaces;

namespace CODE_PersistenceLib.Factories
{
    public class FloorFactory : IFloorFactory
    {
        public IFloor CreateFloor(string type, int x, int y, IDictionary<string, string> options)
        {
            IFloor floor;

            switch (type)
            {
                case "conveyor belt":
                    floor = new ConveyorBeltFloor(x, y, Enum.Parse<Direction>(options["direction"]));
                    break;
                default:
                    throw new NotImplementedException("This floor has not been implemented yet");
            }

            return floor;
        }
    }
}