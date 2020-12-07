namespace CODE_GameLib.Doors
{
    public class ClosingGateDoor : IDoor
    {
        private bool _open = true;

        public bool IsOpen(Player player)
        {
            return _open;
        }

        public void AfterUse(Player player)
        {
            _open = false;
        }
    }
}