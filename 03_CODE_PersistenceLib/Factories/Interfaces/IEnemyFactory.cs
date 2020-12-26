using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_PersistenceLib.Factories.Interfaces
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(string type, int x, int y, int minX, int minY, int maxX, int maxY);
    }
}