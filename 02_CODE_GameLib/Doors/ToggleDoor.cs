namespace CODE_GameLib.Doors
{
    public class ToggleDoor : IDoor
    {
        private bool _open;

        public bool IsOpen(Player player)
        {
            return _open;
        }

        public void AfterUse(Player player)
        {
        }

        public void Toggle()
        {
            _open = !_open;
        }
    }
}