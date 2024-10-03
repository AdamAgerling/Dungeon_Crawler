
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
        ColorPicker = ConsoleColor.Yellow;
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
    public void HandlePlayerAttack(Player player, Enemy enemy, List<LevelElement> levelElements)
    {
        var playerAttack = player.PlayerAttack.Throw();
        var playerDefence = player.PlayerDefence.Throw();
        var enemyAttack = enemy.AttackDice.Throw();
        var enemyDefence = enemy.DefenceDice.Throw();

        var playerDamageTaken = Math.Max(0, enemyAttack - playerDefence);
        var enemyDamageTaken = Math.Max(0, playerAttack - enemyDefence);
        GameLoop.ClearCurrentConsoleLine(1);
        GameLoop.ClearCurrentConsoleLine(2);

        enemy.Health -= enemyDamageTaken;
        Console.WriteLine($"\n{player.Name} attacks {enemy.Name} for {enemyDamageTaken} damage!");

        player.PlayerHealth -= playerDamageTaken;
        Console.WriteLine($"{enemy.Name} attacks {player.Name} for {playerDamageTaken} damage!");
        if (enemy.Health <= 0)
        {
            levelElements.Remove(enemy);
            Console.WriteLine($"{enemy.Name} died.");
        }
        if (player.PlayerHealth <= 0)
        {
            Console.WriteLine($"{player.Name} died. Game over..");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}