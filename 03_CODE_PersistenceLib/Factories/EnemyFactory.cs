using System;
using CODE_PersistenceLib.Factories.Interfaces;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_PersistenceLib.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        public Enemy CreateEnemy(string type, int x, int y, int minX, int minY, int maxX, int maxY)
        {
            switch (type)
            {
                case "horizontal":
                    return new HorizontallyMovingEnemy(1, x, y, minX, maxX);
                case "vertical":
                    return new VerticallyMovingEnemy(1, x, y, minY, maxY);
                default:
                    throw new NotImplementedException("This enemy has not been implemented yet");
            }
        }
    }
}