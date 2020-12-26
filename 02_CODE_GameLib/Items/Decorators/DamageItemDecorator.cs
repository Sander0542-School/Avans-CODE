namespace CODE_GameLib.Items.Decorators
{
    public class DamageItemDecorator : BaseItemDecorator
    {
        private readonly int _damage;

        public DamageItemDecorator(IItem decoratee, int damage) : base(decoratee)
        {
            _damage = damage;
        }

        public override void OnUse(Player player)
        {
            base.OnUse(player);
            player.Damage(_damage);
        }
    }
}