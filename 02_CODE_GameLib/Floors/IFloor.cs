using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Floors
{
    public interface IFloor
    {
        int X { get; }
        
        int Y { get; }
        
        void OnEnter(Player player, out int nextX, out int nextY);
        
        void OnEnter(Enemy enemy);
    }
}