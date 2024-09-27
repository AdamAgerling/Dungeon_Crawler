
internal class Player : LevelElement
    {
    //public int Experience { get; set; } // Might not use.
    public char MapElement { get; set; }
    public ConsoleColor ColorPicker { get; set; }
    public Position Position { get; set; }
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
        }


    public void PlayerUpdate(LevelData levelData, ConsoleKeyInfo cki)
        {
        Position currentPosition = Position;
        }


    }

