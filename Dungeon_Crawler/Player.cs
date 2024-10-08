
internal class Player : LevelElement
{
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

    // Player Movement controls
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
    // Player Attack method
    public void HandlePlayerAttack(Player player, Enemy enemy, List<LevelElement> levelElements)
    {
        var playerAttack = player.PlayerAttack.Throw();
        var playerDefence = player.PlayerDefence.Throw();
        var enemyAttack = enemy.AttackDice.Throw();
        var enemyDefence = enemy.DefenceDice.Throw();

        var playerDamageTaken = Math.Max(0, enemyAttack - playerDefence);
        var enemyDamageTaken = Math.Max(0, playerAttack - enemyDefence);


        enemy.Health -= enemyDamageTaken;
        player.PlayerHealth -= playerDamageTaken;

        if (enemy.Health <= 0)
        {
            Console.SetCursorPosition(0, 3);
            levelElements.Remove(enemy);
            Console.WriteLine($"{enemy.Name} died.");
        }

        switch (enemyDamageTaken)
        {
            case 0:
                GameLoop.ClearHorizontalConsoleRow(1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                     $"Your attack missed it");
                Console.ResetColor();
                break;
            case 1:
            case 2:
            case 3:
                GameLoop.ClearHorizontalConsoleRow(1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                    $"Your attack grazed it.");
                Console.ResetColor();
                break;
            case 4:
            case 5:
            case 6:
                GameLoop.ClearHorizontalConsoleRow(1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                    $"Your attack somewhat hurt it");
                Console.ResetColor();
                break;
            default:
                {
                    GameLoop.ClearHorizontalConsoleRow(1);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                     $"Your attack really hurt it.");
                    Console.ResetColor();
                    break;
                }
        }
        switch (playerDamageTaken)
        {
            case 0:
                GameLoop.ClearHorizontalConsoleRow(2);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                    $"The attack missed you.");
                Console.ResetColor();
                break;
            case 1:
            case 2:
            case 3:
                GameLoop.ClearHorizontalConsoleRow(2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                    $"The attack grazed you.");
                Console.ResetColor();
                break;
            case 4:
            case 5:
            case 6:
                GameLoop.ClearHorizontalConsoleRow(2);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                    $"The attack somewhat hurt you");
                Console.ResetColor();
                break;
            default:
                {
                    GameLoop.ClearHorizontalConsoleRow(2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                        $"The attack really hurt you.");
                    Console.ResetColor();
                    break;
                }
        }
        if (player.PlayerHealth <= 0)
        {
            Console.WriteLine($"{player.Name} died. Game over..");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}