namespace CODE_GameLib.Doors
{
    public interface IDoor
    {
        bool IsOpen(Player player);

        void AfterUse(Player player);
    }
}