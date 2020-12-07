namespace CODE_GameLib.Items
{
    public class DisappearingBoobietrapRoomItem : BoobietrapRoomItem
    {
        private bool _exploded = false;

        public DisappearingBoobietrapRoomItem(int x, int y, int damage) : base(x, y, damage)
        {
        }

        public override void OnUse(Player player)
        {
            base.OnUse(player);
            player.Room.Items.Remove(this);
        }
    }
}