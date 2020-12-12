using System;

namespace CODE_GameLib.Items.Decorators
{
    public abstract class BaseItemDecorator : IItem
    {
        private IItem _decoratee { get; }

        public virtual int X => _decoratee.X;

        public virtual int Y => _decoratee.Y;

        public bool Visible
        {
            get => _decoratee.Visible;
            set => _decoratee.Visible = value;
        }
        
        public int Damage
        {
            get => _decoratee.Damage;
            set => _decoratee.Damage = value;
        }
        
        public BaseItemDecorator(IItem decoratee)
        {
            _decoratee = decoratee;
        }

        /// <summary>
        /// Trigger when the player use the item
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnUse(Player player) => _decoratee.OnUse(player);

        /// <summary>
        /// Returns the item
        /// </summary>
        /// <returns></returns>
        public IItem GetItem()
        {
            return _decoratee.GetItem();
        }
    }
}