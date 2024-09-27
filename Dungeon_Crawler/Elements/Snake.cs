
class Snake : Enemy
    {
    // 🐍 || s  Will try to make this work >:(

    public Snake(Position position)
        {
        Position = position;
        ColorPicker = ConsoleColor.Green;
        Health = 20;
        MapElement = 's';
        Name = "Snakeboy";
        }

    public override void EnemyMovementPattern()
        {

        }
    }

