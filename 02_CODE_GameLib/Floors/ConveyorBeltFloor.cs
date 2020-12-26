using System;
using CODE_GameLib.Rooms;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Floors
{
    public class ConveyorBeltFloor : IFloor
    {
        public ConveyorBeltFloor(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; }

        public int Y { get; }
        
        public Direction Direction { get; }

        public void OnEnter(Player player, out int nextX, out int nextY)
        {
            NextCoords(Direction, out nextX, out nextY);
        }

        public void OnEnter(Enemy enemy)
        {
            NextCoords(Direction, out var nextX, out var nextY);

            enemy.CurrentXLocation = nextX;
            enemy.CurrentYLocation = nextY;
        }

        private void NextCoords(Direction direction, out int nextX, out int nextY)
        {
            nextX = X;
            nextY = Y;

            switch (direction)
            {
                case Direction.NORTH:
                    nextY--;
                    break;
                case Direction.EAST:
                    nextX++;
                    break;
                case Direction.SOUTH:
                    nextY++;
                    break;
                case Direction.WEST:
                    nextX--;
                    break;
            }
        }
    }
}