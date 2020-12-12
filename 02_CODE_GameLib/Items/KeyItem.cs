namespace CODE_GameLib.Items
{
    public class KeyItem : IItem
    {
        public KeyItem(int y, int x, string color)
        {
            Y = y;
            X = x;

            Color = color;
        }

        public string Color { get; }
        public int X { get; }

        public int Y { get; }

        public int Damage { get; set; }

        public bool Visible { get; set; } = true;

        public void OnUse(Player player)
        {
        }
    }
}