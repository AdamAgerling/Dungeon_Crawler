
internal class Wall : LevelElement
{


    public Wall(Position position)
    {
        Position = position;
        ColorPicker = ConsoleColor.Gray;
        MapElement = '#';
    }
}