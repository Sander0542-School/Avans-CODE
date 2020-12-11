namespace CODE_GameLib.Items.Decorators
{
    public class PickupableItemDecorator : BaseItemDecorator
    {
        public PickupableItemDecorator(IItem decoratee) : base(decoratee)
        {
        }

        public override void OnUse(Player player)
        {
            base.OnUse(player);
            
            player.Items.Add(GetItem());
        }
    }
}