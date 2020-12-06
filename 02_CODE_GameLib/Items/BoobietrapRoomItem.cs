namespace CODE_GameLib.Items
{
    public class BoobietrapRoomItem : IRoomItem
    {
        public BoobietrapRoomItem(int damage)
        {
            Damage = damage;
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        public int Damage { get; set; }
    }
}