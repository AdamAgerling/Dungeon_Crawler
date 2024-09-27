
abstract class Enemy : LevelElement
    {
    public string Name { get; set; }
    public int Health { get; set; }
    public Dice Attack { get; set; }
    public Dice Defence { get; set; }

    public abstract void EnemyMovementPattern();

    }

