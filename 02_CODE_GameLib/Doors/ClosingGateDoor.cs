namespace CODE_GameLib.Doors
{
    public class ClosingGateDoor : IDoor
    {
        private bool _closed;
        public bool PortalMode { get; set; }

        public bool IsOpen(Player player)
        {
            return !_closed || PortalMode;
        }

        public void AfterUse(Player player)
        {
            _closed = true;
        }
    }
}