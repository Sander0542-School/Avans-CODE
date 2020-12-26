using System.Collections.Generic;
using CODE_GameLib.Floors;

namespace CODE_PersistenceLib.Factories.Interfaces
{
    public interface IFloorFactory
    {
        IFloor CreateFloor(string type, int x, int y, IDictionary<string, string> options);
    }
}