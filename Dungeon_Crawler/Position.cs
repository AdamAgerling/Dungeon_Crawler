
public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    // This controls the view distance, but the implementation is used in LevelElements.
    public int ViewDistanceX(Position position) => Math.Abs(position.X - X);
    public int ViewDistanceY(Position position) => Math.Abs(position.Y - Y);

    public double ViewDistance(Position position)
    {
        var viewDistanceX = ViewDistanceX(position);
        var viewDistanceY = ViewDistanceY(position);
        var viewDistance = Math.Sqrt(Math.Pow(viewDistanceX, 2) + Math.Pow(viewDistanceY, 2));
        return viewDistance;
    }
}