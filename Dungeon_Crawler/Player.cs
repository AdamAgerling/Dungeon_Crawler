
internal class Player : LevelElement
{
    //public int Experience { get; set; } // Might not use.

    public string Name { get; set; }
    public Dice PlayerAttack { get; set; }
    public Dice PlayerDefence { get; set; }
    public int PlayerHealth { get; set; }


    public Player(Position position)
    {
        Name = "Hero";
        ColorPicker = ConsoleColor.White;
        MapElement = '@';
        PlayerHealth = 100;
        Position = position;
        PlayerAttack = new Dice(2, 6, 2);
        PlayerDefence = new Dice(2, 6, 0);
    }


    public Position GetNewPlayerPosition(ConsoleKeyInfo cki)
    {
        Position newPlayerPosition = new Position(Position.X, Position.Y);

        switch (cki.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                newPlayerPosition.Y -= 1;
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                newPlayerPosition.Y += 1;
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                newPlayerPosition.X -= 1;
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                newPlayerPosition.X += 1;
                break;
        }

        return newPlayerPosition;
    }

}

