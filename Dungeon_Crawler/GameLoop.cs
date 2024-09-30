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

            var keyPress = Console.ReadKey();
            if (keyPress.Key == ConsoleKey.Escape)
            {
                break;
            }

            var playerCurrentPosition = player.GetNewPlayerPosition(keyPress);

            updatedState = TryMoveHere(updatedState, player, playerCurrentPosition, keyPress, snake, rat);

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
    private List<LevelElement> TryMoveHere(List<LevelElement> levelElements, Player player, Position playerNewPosition, ConsoleKeyInfo keyPress, Snake snake, Rat rat)
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
            else if (moveTo is Snake)
            {
                var snakeDefence = snake.DefenceDice.Throw();
                var snakeDamageTaken = Math.Max(0, playerAttack - snakeDefence);
                var snakeAttack = snake.AttackDice.Throw();
                var damageTakenFromSnake = Math.Max(0, snakeAttack - playerDefence);
                snake.Health -= snakeDamageTaken;
                Console.WriteLine($"{player.Name} rolled a {playerAttack} in attack and {snake.Name} rolled a {snakeDefence} in defence. {snake.Name} takes: {snakeDamageTaken} damage.");

                playerHealth -= damageTakenFromSnake;
                Console.WriteLine($"{snake.Name} rolled a {snakeAttack} in attack and {player.Name} rolled a {playerDefence} in defence. {player.Name} takes: {damageTakenFromSnake} damage.");
                if (snake.Health <= 0)
                {
                    Console.WriteLine($"{snake.Name} died.");
                    levelElements.Remove(moveTo);
                }
            }
            else if (moveTo is Rat)
            {
                var ratDefence = rat.DefenceDice.Throw();
                var ratDamageTaken = Math.Max(0, playerAttack - ratDefence);
                var ratAttack = rat.AttackDice.Throw();
                var playerDamageTaken = Math.Max(0, ratAttack - playerDefence);
                rat.Health -= ratDamageTaken;
                Console.WriteLine($"{player.Name} rolled a {playerAttack} in attack and {rat.Name} rolled a {ratDefence} in defence. {rat.Name} takes: {ratDamageTaken} damage.");

                playerHealth -= playerDamageTaken;
                Console.WriteLine($"{rat.Name} rolled a {ratAttack} in attack and {player.Name} rolled a {playerDefence} in defence. {player.Name} takes: {playerDamageTaken} damage.");
                if (rat.Health <= 0)
                {
                    Console.WriteLine($"{rat.Name} died.");
                    levelElements.Remove(moveTo);
                }
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


    private void ClearMap()
    {
        Console.Clear();
    }
}