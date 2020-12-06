using System.Collections.ObjectModel;

namespace CODE_GameLib
{
    public class Player
    {
        public int Lives { get; set; }
        
        public ObservableCollection<Item> Items { get; set; }
    }
}