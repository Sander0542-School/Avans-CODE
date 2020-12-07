namespace CODE_GameLib.Items
{
    public class BoobietrapRoomItem : IRoomItem
    {
        public int X { get; private set; }
        
        public int Y { get; private set; }

        public int Damage { get; private set; }
        
        public BoobietrapRoomItem(int x, int y, int damage)
        {
            X = x;
            Y = y;
            
            Damage = damage;
        }
        
        public virtual void OnUse(Player player)
        {
            player.Damage(Damage);
        }
    }
}