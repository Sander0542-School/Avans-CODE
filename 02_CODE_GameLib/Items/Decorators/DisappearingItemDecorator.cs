namespace CODE_GameLib.Items.Decorators
{
    public class DisappearingItemDecorator : BaseItemDecorator
    {
        public DisappearingItemDecorator(IItem decoratee) : base(decoratee)
        {
        }

        public override void OnUse(Player player)
        {
            base.OnUse(player);

            Visible = false;
        }
    }
}