


//Vidare har LevelData en metod, Load(string filename), som läser in data från filen man anger vid anrop.
//Load läser igenom textfilen tecken för tecken, och för varje tecken den hittar som är någon av #, r, eller s,
//så skapar den en ny instans av den klass som motsvarar tecknet och lägger till en referens till instansen på “elements”-listan.
//Tänk på att när instansen skapas så måste den även få en (X/Y) position; d.v.s Load behöver alltså hålla reda på vilken rad och kolumn i filen som tecknet hittades på.
//Den behöver även spara undan startpositionen för spelaren när den stöter på @.

class LevelData
{
    private List<LevelElement> _elements;
    public readonly List<LevelElement> Elements;
    public LevelData()
    {
        _elements = new List<LevelElement>();
        Elements = _elements;
    }

    public Position Load(string fileName)
    {
        string getCurrentDirectory = Directory.GetCurrentDirectory();
        string level = Path.Combine(getCurrentDirectory, "Levels", fileName);
        Position playerStartPosition = new Position(4, 3);
        if (!File.Exists(level))
        {
            throw new FileNotFoundException($"The file: {fileName} could not be found", fileName);
        }


        string[] mapLevel = File.ReadAllLines(level);

        for (int y = 0; y < mapLevel.Length; y++)
        {
            string row = mapLevel[y];

            for (int x = 0; x < row.Length; x++)
            {
                char charElement = row[x];

                switch (charElement)
                {
                    case '@':
                        playerStartPosition.X = x;
                        playerStartPosition.Y = y;
                        break;
                    case 'r':
                        _elements.Add(new Rat(new Position(x, y)));
                        break;
                    case 's':
                        _elements.Add(new Snake(new Position(x, y)));
                        break;
                    case '#':
                        _elements.Add(new Wall(new Position(x, y)));
                        break;
                }
            }
        }
        return playerStartPosition;
    }
}

