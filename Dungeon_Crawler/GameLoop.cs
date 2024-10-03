class GameLoop
{
    private int turnCounter = 0;
    public void Play()
    {
        var levelData = new LevelData();
        var mapLoadStartPosition = levelData.Load("Level1.txt");
        var player = new Player(mapLoadStartPosition);
        Position? lastPlayerPosition = null;
        var rat = new Rat(mapLoadStartPosition);
        var snake = new Snake(mapLoadStartPosition);
        var initialState = levelData.Elements;
        var updatedState = GetUpdatedMapState(initialState, player);
        while (true)
        {
            turnCounter++;
            PlayerStats(player);
            PrintMap(updatedState, player, lastPlayerPosition);

            var keyPress = Console.ReadKey();
            if (keyPress.Key == ConsoleKey.Escape)
            {
                break;
            }
            lastPlayerPosition = player.Position;
            var playerNewPosition = player.GetNewPlayerPosition(keyPress);
            updatedState = TryMoveHere(updatedState, player, playerNewPosition, keyPress);
            rat.UpdateEnemies(updatedState, player);
            snake.UpdateEnemies(updatedState, player);
        }
    }

    private List<LevelElement> GetUpdatedMapState(List<LevelElement> levelElements, Player player)
    {

        if (!levelElements.Exists(x => x.GetType() == typeof(Player)))
        {
            levelElements.Add(player);
        }
        return levelElements.OrderBy(x => x.Position.Y).ThenBy(x => x.Position.X).ToList();
    }
    private List<LevelElement> TryMoveHere(List<LevelElement> levelElements, Player player, Position playerNewPosition, ConsoleKeyInfo keyPress)
    {
        var moveTo = levelElements
            .FirstOrDefault(element => element.Position.X == playerNewPosition.X && element.Position.Y == playerNewPosition.Y);

        if (moveTo != null)
        {
            if (moveTo is Wall)
            {
                return levelElements;
            }
            else if (moveTo is Enemy enemy)
            {
                HandlePlayerAttack(player, enemy, levelElements);
            }
        }
        else
        {
            player.Position = playerNewPosition;
        }

        return GetUpdatedMapState(levelElements, player);
    }

    private void PrintMap(List<LevelElement> levelElements, Player player, Position? lastPlayerPosition)
    {
        int mapOffset = 4;
        if (lastPlayerPosition != null)
        {
            Console.SetCursorPosition(lastPlayerPosition.Value.X, lastPlayerPosition.Value.Y + mapOffset);
            Console.Write(' ');
        }

        foreach (var element in levelElements)
        {
            Console.SetCursorPosition(element.Position.X, element.Position.Y + mapOffset);
            element.Draw();
        }
    }

    private void HandlePlayerAttack(Player player, Enemy enemy, List<LevelElement> levelElements)
    {
        var playerAttack = player.PlayerAttack.Throw();
        var playerDefence = player.PlayerDefence.Throw();
        var enemyAttack = enemy.AttackDice.Throw();
        var enemyDefence = enemy.DefenceDice.Throw();

        var playerDamageTaken = Math.Max(0, enemyAttack - playerDefence);
        var enemyDamageTaken = Math.Max(0, playerAttack - enemyDefence);

        Console.WriteLine();
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

    public void PlayerStats(Player player)
    {
        ClearCurrentConsoleLine(0);
        Console.WriteLine($"Name:{player.Name} - Health:{player.PlayerHealth}/100 - Turn: {turnCounter}");
    }
    public static void ClearCurrentConsoleLine(int row)
    {
        Console.SetCursorPosition(row, 0);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(row, 0);
    }
}