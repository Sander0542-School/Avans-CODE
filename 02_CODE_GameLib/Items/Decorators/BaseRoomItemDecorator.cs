using System.Drawing;

namespace CODE_GameLib.Items.Decorators
{
    public abstract class BaseRoomItemDecorator : IRoomItem
    {
        private IRoomItem _decoratee { get; }
        
        public int X
        {
            get => _decoratee.X;
            set => _decoratee.X = value;
        }
        
        public int Y
        {
            get => _decoratee.Y;
            set => _decoratee.Y = value;
        }

        public BaseRoomItemDecorator(IRoomItem decoratee)
        {
            _decoratee = decoratee;
        }

        public virtual Color GetColor() => _decoratee.GetColor();
    }
}