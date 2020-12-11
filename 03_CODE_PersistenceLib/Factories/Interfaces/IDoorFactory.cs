using System.Collections.Generic;
using CODE_GameLib.Doors;

namespace CODE_FileSystem.Factories.Interfaces
{
    public interface IDoorFactory
    {
        IDoor CreateDoor(string type, IDictionary<string, string> options);
    }
}