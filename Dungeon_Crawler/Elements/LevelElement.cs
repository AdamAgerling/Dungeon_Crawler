
abstract class LevelElement
{
    public ConsoleColor ColorPicker { get; set; }
    public Position Position { get; set; }
    public char MapElement { get; set; }

    public void Draw()
    {
        Console.ForegroundColor = ColorPicker;
        Console.Write(MapElement);
        Console.ResetColor();
    }
}

