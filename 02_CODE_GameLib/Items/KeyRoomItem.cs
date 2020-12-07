namespace CODE_GameLib.Items
{
    public class KeyRoomItem : IRoomItem
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public string Color { get; private set; }

        public KeyRoomItem(int y, int x, string color)
        {
            Y = y;
            X = x;
            
            Color = color;
        }
        
        public void OnUse(Player player)
        {
            player.AddItem(this);
            player.Room.Items.Remove(this);
        }
    }
}