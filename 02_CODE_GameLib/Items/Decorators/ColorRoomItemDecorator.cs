using System.Drawing;

namespace CODE_GameLib.Items.Decorators
{
    public class ColorRoomItemDecorator : BaseRoomItemDecorator
    {
        private Color _color { get; }

        public ColorRoomItemDecorator(IRoomItem decoratee, Color color) : base(decoratee)
        {
            _color = color;
        }

        public override Color GetColor()
        {
            return _color;
        }
    }
}