namespace CODE_GameLib.Items
{
    public class BoobietrapRoomItem : IRoomItem
    {
        public int X { get; set; }
        
        public int Y { get; set; }

        public int Damage { get; private set; }
        
        public BoobietrapRoomItem(int damage)
        {
            Damage = damage;
        }
        
        public virtual void OnUse(Player player)
        {
            player.Damage(Damage);
        }
    }
}