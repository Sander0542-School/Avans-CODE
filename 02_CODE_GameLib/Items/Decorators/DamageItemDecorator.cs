namespace CODE_GameLib.Items.Decorators
{
    public class DamageItemDecorator : BaseItemDecorator
    {
        public DamageItemDecorator(IItem decoratee, int damage) : base(decoratee)
        {
            Damage = damage;
        }

        public override void OnUse(Player player)
        {
            base.OnUse(player);
            player.Damage(Damage);
        }
    }
}