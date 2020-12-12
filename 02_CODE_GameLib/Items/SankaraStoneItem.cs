namespace CODE_GameLib.Items
{
    public class SankaraStoneItem : IItem
    {
        public SankaraStoneItem(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public int Damage { get; set; }

        public bool Visible { get; set; } = true;

        public void OnUse(Player player)
        {
        }
    }
}