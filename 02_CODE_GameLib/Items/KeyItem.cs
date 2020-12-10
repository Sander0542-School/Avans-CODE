namespace CODE_GameLib.Items
{
    public class KeyItem : IItem
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public string Color { get; private set; }

        public KeyItem(int y, int x, string color)
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