namespace CODE_GameLib.Items.Decorators
{
    public abstract class BaseItemDecorator : IItem
    {
        public BaseItemDecorator(IItem decoratee)
        {
            Decoratee = decoratee;
        }

        public IItem Decoratee { get; }

        public virtual int X => Decoratee.X;

        public virtual int Y => Decoratee.Y;

        public bool Visible
        {
            get => Decoratee.Visible;
            set => Decoratee.Visible = value;
        }

        public int Damage
        {
            get => Decoratee.Damage;
            set => Decoratee.Damage = value;
        }

        /// <summary>
        ///     Trigger when the player use the item
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnUse(Player player)
        {
            Decoratee.OnUse(player);
        }

        /// <summary>
        ///     Returns the item
        /// </summary>
        /// <returns></returns>
        public IItem GetItem()
        {
            return Decoratee.GetItem();
        }
    }
}