using System.Collections.Generic;
using CODE_GameLib.Items;

namespace CODE_FileSystem.Factories.Interfaces
{
    public interface IItemFactory
    {
        IItem CreateItem(string type, int x, int y, IDictionary<string, string> options);
    }
}