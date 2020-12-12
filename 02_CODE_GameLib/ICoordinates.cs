namespace CODE_GameLib
{
    public interface ICoordinates
    {
        int X { get; }
        int Y { get; }

        public bool HasCoordinates(int x, int y)
        {
            return x == X && y == Y;
        }
    }
}