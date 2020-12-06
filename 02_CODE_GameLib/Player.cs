using System.Collections.ObjectModel;
using CODE_GameLib.Items;

namespace CODE_GameLib
{
    public class Player
    {
        public int Lives { get; set; }
        
        public ObservableCollection<IRoomItem> Items { get; set; }
    }
}