class Rat : Enemy
{
    public Rat(Position position)
    {
        Position = position;
        ColorPicker = ConsoleColor.Red;
        Health = 15;
        MapElement = 'r';
        Name = "Ratman";
        AttackDice = new Dice(1, 6, 3);
        DefenceDice = new Dice(1, 6, 1);
    }
    // Rat movement pattern with each of my button presses, they're supposed to walk around randomly.
    public override void UpdateEnemies(List<LevelElement> levelElements, Player player)
    {
        var random = new Random();

        foreach (var element in levelElements)
        {
            if (element is Rat rat && rat.Health >= 0)
            {
                var ratMoveDirection = random.Next(1, 5);

                Position? oldRatPosition = rat.Position;
                Console.SetCursorPosition(oldRatPosition.Value.X, oldRatPosition.Value.Y + 4);
                Console.Write(' ');

                Position updatedRatPosition = oldRatPosition.Value;
                switch (ratMoveDirection)
                {
                    case 1:
                        updatedRatPosition.Y += 1;
                        break;
                    case 2:
                        updatedRatPosition.Y -= 1;
                        break;
                    case 3:
                        updatedRatPosition.X += 1;
                        break;
                    case 4:
                        updatedRatPosition.X -= 1;
                        break;
                }
                var collisionElement = levelElements
               .FirstOrDefault(e => e.Position.X == updatedRatPosition.X && e.Position.Y == updatedRatPosition.Y);

                if (collisionElement == null || collisionElement is Player)
                {
                    if (updatedRatPosition.X == player.Position.X && updatedRatPosition.Y == player.Position.Y)
                    {
                        HandleEnemyAttack(rat, player, levelElements);
                        return;
                    }
                    rat.Position = updatedRatPosition;
                }
            }
        }
    }

    // This Method exists because the print when the rats walk in to the player is supposed to be different from when the player walks in to the rat.
    // Like if they engage first, that should be written first in the combat log. Its a clunky implementation, but it works for the purpose of this application.
    private void HandleEnemyAttack(Enemy enemy, Player player, List<LevelElement> levelElements)
    {
        var playerAttack = player.PlayerAttack.Throw();
        var playerDefence = player.PlayerDefence.Throw();
        var enemyAttack = enemy.AttackDice.Throw();
        var enemyDefence = enemy.DefenceDice.Throw();

        var playerDamageTaken = Math.Max(0, enemyAttack - playerDefence);
        var enemyDamageTaken = Math.Max(0, playerAttack - enemyDefence);


        player.PlayerHealth -= playerDamageTaken;
        enemy.Health -= enemyDamageTaken;

        if (player.PlayerHealth <= 0)
        {
            GameLoop.ClearHorizontalConsoleRow(3);
            Console.WriteLine($"{player.Name} died. Game over..");
            Console.ReadKey();
            Environment.Exit(0);
        }

        switch (playerDamageTaken)
        {
            case 0:
                GameLoop.ClearHorizontalConsoleRow(1);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                    $"The attack missed you.");
                Console.ResetColor();
                break;
            case 1:
            case 2:
            case 3:
                GameLoop.ClearHorizontalConsoleRow(1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                    $"The attack grazed you.");
                Console.ResetColor();
                break;
            case 4:
            case 5:
            case 6:
                GameLoop.ClearHorizontalConsoleRow(1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                    $"The attack somewhat hurt you");
                Console.ResetColor();
                break;
            default:
                {
                    GameLoop.ClearHorizontalConsoleRow(1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttack}) attacked you (DEF: {player.PlayerDefence} => {playerDefence}), " +
                        $"The attack really hurt you.");
                    Console.ResetColor();
                    break;
                }
        }
        switch (enemyDamageTaken)
        {
            case 0:
                GameLoop.ClearHorizontalConsoleRow(2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                            $"Your attack missed it");
                Console.ResetColor();
                break;
            case 1:
            case 2:
            case 3:
                GameLoop.ClearHorizontalConsoleRow(2);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                    $"Your attack grazed it.");
                Console.ResetColor();
                break;
            case 4:
            case 5:
            case 6:
                GameLoop.ClearHorizontalConsoleRow(2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                    $"Your attack somewhat hurt it");
                Console.ResetColor();
                break;
            default:
                {
                    GameLoop.ClearHorizontalConsoleRow(2);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"You (ATK: {player.PlayerAttack} => {playerAttack}) {enemy.Name} (DEF: {enemy.AttackDice} => {enemyDefence}), " +
                        $"Your attack really hurt it.");
                    Console.ResetColor();
                    break;
                }
        }
        if (enemy.Health <= 0)
        {
            levelElements.Remove(enemy);
            Console.WriteLine($"{enemy.Name} died.");
        }
    }
}
