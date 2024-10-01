//En game loop är en loop som körs om och om igen medan spelet är igång, och i vårat fall kommer ett varv i loopen motsvaras av en omgång i spelet. 
//För varje varv i loopen inväntar vi att användaren trycker in en knapp;
//sedan utför vi spelarens drag, följt av datorns drag (uppdatera alla fiender), innan vi loopar igen.
//Möjligtvis kan man ha en knapp (Esc) för att avsluta loopen/spelet.

//När spelaren/fiender flyttar på sig behöver vi beräkna deras nya position och leta igenom alla vår LevelElements för att
//se om det finns något annat objekt på den platsen man försöker flytta till. Om det finns en vägg eller annat objekt (fiende/spelaren)
//på platsen måste förflyttningen avbrytas och den tidigare positionen gälla. Notera dock att om spelaren flyttar sig till en plats där
//det står en fiende så attackerar han denna (mer om detta längre ner). 
//Detsamma gäller om en fiende flyttar sig till platsen där spelaren står. Fiender kan dock inte attackera varandra i spelet.

//Spelaren förflyttar sig 1 steg upp, ner, höger eller vänster varje omgång, alternativt står still, beroende på vilken knapp användaren tryckt på.
//Rat förflyttar sig 1 steg i slumpmässig vald riktning (upp, ner, höger eller vänster) varje omgång.
//Snake står still om spelaren är mer än 2 rutor bort, annars förflyttar den sig bort från spelaren.
//Varken spelare, rats eller snakes kan gå igenom väggar eller varandra.
class Game
{
    public void Play()
    {
        var levelData = new LevelData();
        var mapLoadStartPosition = levelData.Load("Level1.txt");

        var player = new Player(mapLoadStartPosition);
        var rat = new Rat(mapLoadStartPosition);
        var snake = new Snake(mapLoadStartPosition);

        var initialState = levelData.Elements;
        var updatedState = GetUpdatedMapState(initialState, player);

        while (true)
        {
            PrintMap(updatedState);
            PlayerStats(player);

            var keyPress = Console.ReadKey();
            if (keyPress.Key == ConsoleKey.Escape)
            {
                break;
            }
            var playerCurrentPosition = player.GetNewPlayerPosition(keyPress);
            updatedState = TryMoveHere(updatedState, player, playerCurrentPosition, keyPress);


            ClearMap();
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

        var playerAttack = player.PlayerAttack.Throw();
        var playerDefence = player.PlayerDefence.Throw();
        var playerHealth = player.PlayerHealth;

        if (moveTo != null)
        {
            if (moveTo is Wall)
            {
                return levelElements;
            }
            else if (moveTo is Snake snake)
            {
                HandleEncounter(player, snake, levelElements);
            }
            else if (moveTo is Rat rat)
            {
                HandleEncounter(player, rat, levelElements);
            }
            if (playerHealth <= 0)
            {
                Console.WriteLine($"{player.Name} died. Game over..");
                Environment.Exit(0);
            }
        }
        else
        {
            player.Position = playerNewPosition;
        }

        return GetUpdatedMapState(levelElements, player);
    }

    private void PrintMap(List<LevelElement> levelElements)
    {

        foreach (var element in levelElements)
        {
            Console.SetCursorPosition(element.Position.X, element.Position.Y);
            element.Draw();
        }
    }

    private void HandleEncounter(Player player, Enemy enemy, List<LevelElement> levelElements)
    {
        var playerAttack = player.PlayerAttack.Throw();
        var playerDefence = player.PlayerDefence.Throw();
        var enemyAttack = enemy.AttackDice.Throw();
        var enemyDefence = enemy.DefenceDice.Throw();
        var playerDamageTaken = Math.Max(0, enemyAttack - playerDefence);
        var enemyDamageTaken = Math.Max(0, playerAttack - enemyDefence);

        enemy.Health -= enemyDamageTaken;
        Console.WriteLine($"{player.Name} attacks {enemy.Name} for {enemyDamageTaken} damage!");

        player.PlayerHealth -= playerDamageTaken;
        Console.WriteLine($"{enemy.Name} attacks {player.Name} for {playerDamageTaken} damage!");

        if (enemy.Health <= 0)
        {
            Console.WriteLine($"{enemy.Name} died.");
            levelElements.Remove(enemy);
        }
    }

    private void UpdateEnemies(List<LevelElement> levelElements, Player player)
    {
        foreach (var element in levelElements)
        {
            if (element is Rat rat)
            {
                // NYI.
            }
            else if (element is Snake snake)
            {
                // NYI.
            }

        }
    }

    private void PlayerStats(Player player)
    {
        Console.WriteLine($"{player.Name}:{player.PlayerHealth}HP");

    }
    private void ClearMap()
    {
        Console.Clear();
    }
}