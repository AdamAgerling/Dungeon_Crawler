
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