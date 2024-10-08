abstract class LevelElement
{
    public ConsoleColor ColorPicker { get; set; }
    public Position Position { get; set; }
    public char MapElement { get; set; }

    public virtual void Draw(Player player)
    {
        if (player.Position.ViewDistance(Position) < 4)
        {
            Console.ForegroundColor = ColorPicker;
            Console.Write(MapElement);
            Console.ResetColor();
        }
    }
}