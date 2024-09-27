
internal class Wall : LevelElement
    {
    public int X { get; set; }
    public int Y { get; set; }

    public Wall(Position position)
        {
        Position = position;
        ColorPicker = ConsoleColor.Gray;
        MapElement = '#';
        }
    }

