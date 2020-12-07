namespace CODE_GameLib.Items
{
    public class DisappearingBoobietrapRoomItem : BoobietrapRoomItem
    {
        private bool _exploded = false;

        public DisappearingBoobietrapRoomItem(int damage) : base(damage)
        {
        }

        public override void OnUse(Player player)
        {
            base.OnUse(player);
            player.Room.Items.Remove(this);
        }
    }
}