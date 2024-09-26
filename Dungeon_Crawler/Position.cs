
public struct Position
    {
    public int X;
    public int Y;

    public Position(Position position) : this(position.X, position.Y) { }

    public Position(int x, int y)
        {
        this.X = x;
        this.Y = y;
        }

    }


