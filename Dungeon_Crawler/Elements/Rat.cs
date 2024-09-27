
class Rat : Enemy
    {
    // 🐀 || r  Will try to make this work >:(

    public Rat(Position position)
        {
        Position = position;
        ColorPicker = ConsoleColor.Red;
        Health = 15;
        MapElement = 'r';
        Name = "Ratman";
        }

    public override void EnemyMovementPattern()
        {

        }
    }
