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
    private void HandleEnemyAttack(Rat rat, Player player, List<LevelElement> levelElements)
    {
        var playerAttack = player.PlayerAttack.Throw();
        var playerDefence = player.PlayerDefence.Throw();
        var enemyAttack = rat.AttackDice.Throw();
        var enemyDefence = rat.DefenceDice.Throw();

        var playerDamageTaken = Math.Max(0, enemyAttack - playerDefence);
        var enemyDamageTaken = Math.Max(0, playerAttack - enemyDefence);
        Console.SetCursorPosition(0, 21);
        Console.WriteLine();
        player.PlayerHealth -= playerDamageTaken;
        Console.WriteLine($"\n{rat.Name} attacked {player.Name} for {playerDamageTaken} damage!");
        rat.Health -= enemyDamageTaken;

        Console.WriteLine($"{player.Name} attacks {rat.Name} for {enemyDamageTaken} damage!");

        Game.ClearCurrentConsoleLine(22);
        Game.ClearCurrentConsoleLine(23);

        if (player.PlayerHealth <= 0)
        {
            Console.WriteLine($"{player.Name} died. Game over..");
            Environment.Exit(0);
        }
        if (rat.Health <= 0)
        {
            levelElements.Remove(rat);
            Console.WriteLine($"{rat.Name} died.");
        }
    }
}