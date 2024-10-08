class GameLoop
{
    public int turnCounter = 0;

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
            PlayerStats(player);
            PrintMap(updatedState, player, lastPlayerPosition);

            Console.ForegroundColor = ConsoleColor.Black;
            var keyPress = Console.ReadKey();
            Console.ResetColor();
            if (keyPress != null)
            {
                ClearHorizontalConsoleRow(1);
                ClearHorizontalConsoleRow(2);
                ClearHorizontalConsoleRow(3);
                turnCounter++;
            }
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
        var collisionElement = levelElements
            .FirstOrDefault(element => element.Position.X == playerNewPosition.X && element.Position.Y == playerNewPosition.Y);

        if (collisionElement != null)
        {
            if (collisionElement is Wall)
            {
                return levelElements;
            }
            else if (collisionElement is Enemy enemy)
            {
                player.HandlePlayerAttack(player, enemy, levelElements, turnCounter);
                Console.SetCursorPosition(enemy.Position.X, enemy.Position.Y + 4);
                Console.Write(' ');
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
            element.Draw(player);
        }
    }

    public void PlayerStats(Player player)
    {
        ClearHorizontalConsoleRow(0);
        Console.WriteLine($"Name: {player.Name} - Health:{Math.Max(0, player.PlayerHealth)}/100 - Turn: {turnCounter}");
    }
    public static void ClearHorizontalConsoleRow(int row)
    {
        Console.SetCursorPosition(0, row);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, row);
    }

}