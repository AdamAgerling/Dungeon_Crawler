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
        var playerStartPosition = levelData.Load("Level1.txt");

        var initialState = levelData.Elements;

        var player = new Player(playerStartPosition);
        var updatedState = GetUpdatedMapState(initialState, player);

        while (true)
        {

            PrintMap(updatedState);

            var keyPress = Console.ReadKey();
            if (keyPress.Key == ConsoleKey.Escape)
            {
                break;
            }

            var playernsPosition = player.Position;

            switch (keyPress.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    playernsPosition.Y -= 1;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    playernsPosition.Y += 1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    playernsPosition.X -= 1;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    playernsPosition.X += 1;
                    break;
            }

            updatedState = TryMoveHere(updatedState, player, playernsPosition, keyPress);

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
        var potentialEncounter = levelElements
            .FirstOrDefault(element => element.Position.X == playerNewPosition.X && element.Position.Y == playerNewPosition.Y);

        if (potentialEncounter != null)
        {
            if (potentialEncounter.GetType() == typeof(Wall))
            {

            }
            else
            {
                player.GetNewPlayerPosition(keyPress);
                levelElements.Remove(potentialEncounter);
            }
        }
        else
        {
            player.GetNewPlayerPosition(keyPress);
        }

        return GetUpdatedMapState(levelElements, player);
    }

    private void PrintMap(List<LevelElement> levelElements)
    {
        var x = 0;
        var y = 0;

        foreach (var element in levelElements)
        {
            if (element.Position.Y > y)
            {
                Console.WriteLine();
                y = element.Position.Y;
                x = 0;
            }
            if (x < element.Position.X)
            {
                while (x < element.Position.X)
                {
                    Console.Write(" ");
                    x++;
                }
            }
            element.Draw();
            x++;
        }
    }


    private void ClearMap()
    {
        Console.Clear();
    }
}


