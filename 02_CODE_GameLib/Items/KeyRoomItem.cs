namespace CODE_GameLib.Items
{
    public class KeyRoomItem : IRoomItem
    {
        public int X { get; set; }

        public int Y { get; set; }

        public string Color { get; private set; }

        public KeyRoomItem(string color)
        {
            Color = color;
        }
        
        public void OnUse(Player player)
        {
            player.AddItem(this);
            player.Room.Items.Remove(this);
        }
    }
}